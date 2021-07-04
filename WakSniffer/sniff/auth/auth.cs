using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;
using System.Configuration;
using System.Windows.Forms;
using WakSniffer.sniff.packet_handler;

namespace WakSniffer.sniff.auth
{
    public class PacketReceiveEventArg : EventArgs
    {
        public byte[] Data { get; set; }
        public PacketReceiveEventArg(byte[] data)
        {
            Data = data;
        }
    }
    class auth
    {
        public ssl_listener listener;
        public ssl_client connector;
        string ip;
        int port;
        bool is_game;
        public byte[] token;

        public auth(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            this.is_game = false;
            listener = new ssl_listener("127.0.0.1", port);
            listener.ConnectEvent += ConnectEvent;
            listener.startListener();
        }

        public auth(string ip, int port, byte[] token)
        {
            this.token = token;
            this.ip = ip;
            this.port = port;
            this.is_game = true;
            listener = new ssl_listener("127.0.0.1", port);
            listener.ConnectEvent += ConnectEvent;
            listener.startListener();
        }

        public void ConnectEvent(object sender, EventArgs e)
        {
            connector = new ssl_client(ip, port);
            connector.sendTo = listener.state.sslStream;
            connector.ConnectEvent += ServerConnectEvent;
            connector.startClient();
        }

        public void ServerConnectEvent(object sender, EventArgs e)
        {
            listener.sendTo = connector.state.sslStream;
            if (!is_game)
            {
                PacketHandler packet_handler = new PacketHandler(listener, connector);
                listener.packet_receive_event += new EventHandler<PacketReceiveEventArg>(packet_handler.listener_packet_receive);
                connector.packet_receive_event += new EventHandler<PacketReceiveEventArg>(packet_handler.connector_packet_receive);
            } else
            {
                GamePacketHandler packet_handler = new GamePacketHandler(listener, connector, token);
                listener.packet_receive_event += new EventHandler<PacketReceiveEventArg>(packet_handler.listener_packet_receive);
                connector.packet_receive_event += new EventHandler<PacketReceiveEventArg>(packet_handler.connector_packet_receive);
            }
        }
    }
}
