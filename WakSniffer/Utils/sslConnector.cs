using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WakSniffer.sniff.auth;

namespace WakSniffer.Utils
{
    class ssl_client
    {
        public string hostname = "www.domain.com";
        public string host;// = "52.16.189.225";
        public int port;// = 5558;
        private ManualResetEvent receiveDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(true);
        public event EventHandler ConnectEvent;
        public event EventHandler<PacketReceiveEventArg> packet_receive_event;
        public StateObject state;
        public SslStream sendTo;
        public logbytes log;
        int count_to_receive;

        public class StateObject
        {
            public SslStream sslStream = null;
            public const int BufferSize = 400000;
            public byte[] buffer = new byte[BufferSize];
            public StringBuilder sb = new StringBuilder();
        }

        public ssl_client(string host, int port)
        {
            this.host = host;
            this.port = port;
            log = new logbytes(false);
            count_to_receive = 0;
        }

        public void startClient()
        {
            try
            {
                ConnectSSL();
            }
            catch (Exception eee)
            {
                Console.WriteLine(eee.ToString());
            }

        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
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

        protected virtual void OnConnection(EventArgs e)
        {
            ConnectEvent?.Invoke(this, e);
        }

        protected virtual void OnPacketReceive(byte[] data)
        {
            var localcopy = packet_receive_event;

            packet_receive_event(this, new PacketReceiveEventArg(data));
        }

        void ReadMessage(SslStream sslStream)
        {
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
        int check_if_not_full_packet(byte[] data, int bytes)
        {
            ByteReader rd = new ByteReader(data);
            int size = rd.ReadShort();

            if (size > bytes)
                return size - bytes;
            return 0;
        }

        void receiveCallBack(IAsyncResult ar)
        {
            byte[] buffer;
            SslStream sslStream;
            byte[] data;
            int bytes = -1;

            receiveDone.Set();
            state = (StateObject)ar.AsyncState;
            buffer = state.buffer;
            sslStream = state.sslStream;
            bytes = sslStream.EndRead(ar);
            data = new byte[bytes];
            Array.Copy(buffer, data, bytes);
            if (count_to_receive != 0)
                count_to_receive -= bytes;
            else
                count_to_receive = check_if_not_full_packet(data, bytes);
            if (count_to_receive == 0)
                OnPacketReceive(data);
            if (count_to_receive < 0)
                count_to_receive = 0;
            sslStream.BeginRead(state.buffer, count_to_receive, StateObject.BufferSize - count_to_receive, new AsyncCallback(receiveCallBack), state);
            receiveDone.WaitOne();
        }

        public void ConnectSSL()
        {
            try
            {
                TcpClient client = new TcpClient(host, port);

                SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                try
                {
                    sslStream.AuthenticateAsClient(hostname);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    client.Close();
                    return;
                }
                state = new StateObject();
                state.sslStream = sslStream;
                OnConnection(EventArgs.Empty);
                ReadMessage(sslStream);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

        }
    }

}
