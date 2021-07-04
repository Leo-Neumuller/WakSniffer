using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WakSniffer.sniff.auth;

namespace WakSniffer.Utils
{
    class ssl_listener
    {
        public string HostIP;
        public int Port;
        X509Certificate serverCertificate = new X509Certificate2("SERV.pfx", "qwerty");
        ManualResetEvent tcpClientConnected = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(true);
        public StateObject state;
        public event EventHandler ConnectEvent;
        public event EventHandler<PacketReceiveEventArg> packet_receive_event;
        public SslStream sendTo;
        public logbytes log;
        bool missing_zero = false;

        public class StateObject
        {
            public SslStream sslStream = null;
            public const int BufferSize = 400000;
            public byte[] buffer = new byte[BufferSize];
            public StringBuilder sb = new StringBuilder();
        }

        public ssl_listener(string hostip, int port)
        {
            this.HostIP = hostip;
            this.Port = port;
            log = new logbytes(true);
        }

        public void startListener()
        {
            Console.WriteLine("Multi user server. Recive save and send data to clients");
            Console.WriteLine(DateTime.Now + " Waiting for connections....");
            try
            {
                start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void log_bytes(byte[] data)
        {
            StringBuilder hex = new StringBuilder(data.Length * 2);
            foreach (byte b in data)
            {
                hex.AppendFormat("{0:x2}", b);
                hex.Append(" ");
            }
            Console.WriteLine(hex);
        }

        void ProcessIncomingData(object obj)
        {
            SslStream sslStream = (SslStream)obj;

            state = new StateObject();
            state.sslStream = sslStream;
            OnConnection(EventArgs.Empty);
            sslStream.ReadTimeout = 10000;
            sslStream.WriteTimeout = 10000;
            sslStream.BeginRead(state.buffer, 0, StateObject.BufferSize, new AsyncCallback(receiveCallBack), state);
            receiveDone.WaitOne();
        }

        public void send(byte[] data, SslStream sslStream)
        {
            byte[] byteData = data;
            byte[] clientdata = new byte[byteData.Length];
            byteData.CopyTo(clientdata, 0);

            sendDone.WaitOne();
            sendDone.Reset();
            if (clientdata.Length == 0)
            {
                sendDone.Set();
                return;
            }
            sslStream.BeginWrite(clientdata, 0, clientdata.Length,
                    new AsyncCallback(SendCallback), sslStream);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                SslStream client = (SslStream)ar.AsyncState;

                client.EndWrite(ar);
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        byte[] Handle_missing_zero(byte[] data)
        {
            if (missing_zero == true)
            {
                byte[] tmp = { 0x00 };
                tmp = tmp.Concat(data).ToArray();
                data = tmp;
                missing_zero = false;
            }
            return data;
        }

        void receiveCallBack(IAsyncResult ar)
        {
            byte[] buffer;
            SslStream sslStream;
            byte[] data;
            int bytes;

            receiveDone.Set();
            state = (StateObject)ar.AsyncState;
            buffer = state.buffer;
            sslStream = state.sslStream;
            bytes = sslStream.EndRead(ar);
            data = new byte[bytes];
            Array.Copy(buffer, data, bytes);
            data = Handle_missing_zero(data);
            if (bytes != 1)
                OnPacketReceive(data);
            else
                missing_zero = true;
            sslStream.BeginRead(state.buffer, 0, StateObject.BufferSize, new AsyncCallback(receiveCallBack), state);
            receiveDone.WaitOne();
        }

        void ProcessIncomingConnection(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;
            TcpClient client = listener.EndAcceptTcpClient(ar);
            SslStream sslStream = new SslStream(client.GetStream(), false);

            try
            {
                sslStream.AuthenticateAsServer(serverCertificate, false, SslProtocols.Tls, true);
                sslStream.ReadTimeout = 1000;
                sslStream.WriteTimeout = 1000;

                DisplayCertificateInformation(sslStream);
                IPEndPoint newclient = (IPEndPoint)client.Client.RemoteEndPoint;
                Console.WriteLine("Connected with {0} at port {1} and Serialize {2} HASH### {3}", newclient.Address, newclient.Port, newclient.Serialize().ToString(), newclient.GetHashCode());
            }
            catch (Exception ee)
            {
                Console.WriteLine("Client without sslSocket " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
                sslStream.Close();
                client.Client.Close();
            }

            ThreadPool.QueueUserWorkItem(ProcessIncomingData, sslStream);
            tcpClientConnected.Set();
        }
        protected virtual void OnConnection(EventArgs e)
        {
            ConnectEvent?.Invoke(this, e);
        }

        protected virtual void OnPacketReceive(byte[] data)
        {
            var localcopy = packet_receive_event;

            packet_receive_event(this, new PacketReceiveEventArg(data));
        }

        void DisplayCertificateInformation(SslStream stream)
        {
            Console.WriteLine("Certificate revocation list checked: {0}", stream.CheckCertRevocationStatus);

            Console.WriteLine("Cipher: {0} strength {1}", stream.CipherAlgorithm, stream.CipherStrength);
            Console.WriteLine("Hash: {0} strength {1}", stream.HashAlgorithm, stream.HashStrength);
            Console.WriteLine("Key exchange: {0} strength {1}", stream.KeyExchangeAlgorithm, stream.KeyExchangeStrength);
            Console.WriteLine("Protocol: {0}", stream.SslProtocol);

            X509Certificate localCertificate = stream.LocalCertificate;
            if (stream.LocalCertificate != null)
            {
                Console.WriteLine("Local cert was issued to {0} and is valid from {1} until {2}.",
                    localCertificate.Subject,
                    localCertificate.GetEffectiveDateString(),
                    localCertificate.GetExpirationDateString());
            }
            else
            {
                Console.WriteLine("Local certificate is null.");
            }
            X509Certificate remoteCertificate = stream.RemoteCertificate;
            if (stream.RemoteCertificate != null)
            {
                Console.WriteLine("Remote cert was issued to {0} and is valid from {1} until {2}.",
                    remoteCertificate.Subject,
                    remoteCertificate.GetEffectiveDateString(),
                    remoteCertificate.GetExpirationDateString());
            }
            else
            {
                Console.WriteLine("Remote certificate is null.");
            }
        }

        private void start()
        {
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000 * 60);
                }
            });
            t.Start();

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(HostIP), Port);
            TcpListener listener = new TcpListener(endpoint);
            listener.Start();

            tcpClientConnected.Reset();
            listener.BeginAcceptTcpClient(new AsyncCallback(ProcessIncomingConnection), listener);
            tcpClientConnected.WaitOne();
        }

    }

}
