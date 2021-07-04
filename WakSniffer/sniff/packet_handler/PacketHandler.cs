using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WakSniffer.sniff.auth;
using WakSniffer.sniff.packet_handler.packet;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler
{
    class PacketHandler
    {
        public ssl_listener listener;
        public ssl_client connector;
        public logbytes log_listener;
        public logbytes log_connector;
        private readonly Dictionary<PacketsEnum, List<Action<object>>> OneExecutionActionStack = InitActionStack();
        private string serverconnect_ip;
        public PacketsEnum last_id;
        public byte[] token;


        public PacketHandler()
        {
        }

        public PacketHandler(ssl_listener listener, ssl_client connector)
        {
            this.listener = listener;
            this.connector = connector;
            log_listener = new logbytes(true);
            log_connector = new logbytes(false);
            last_id = (PacketsEnum)(-1);
            SubscribeActions();
        }

        private bool read_data(byte[] data, logbytes log, bool is_listener)
        {
            ByteReader rd = new ByteReader(data);
            Packet packet = new Packet();
            DictPackets dictpacket = new DictPackets();
            int size;

            while (rd.Remaining() > 1)
            {
                size = rd.ReadShort();
                if (is_listener)
                    rd.ReadByte();
                packet.id = (PacketsEnum)rd.ReadShort();
                packet.last_id = last_id;
                if (dictpacket.Constructors.TryGetValue(packet.id, out Type type))
                {
                    var inst = type.GetConstructor(new[] { typeof(ByteReader), typeof(PacketsEnum) }).Invoke(new object[] { rd, packet.id });
                    var exec = OneExecutionActionStack[packet.id].ToArray();
                    foreach (var i in exec)
                        i.Invoke(inst);
                    if (ignore_current_packet(packet.id))
                        return true;
                }
                else
                {
                    if (ignore_current_packet(packet.id))
                        return true;
                    rd = new ByteReader(data);
                    packet.data = rd.ReadAll();
                    log.log(packet);
                }
                last_id = packet.id;
            }
            return false;
        }

        private bool ignore_current_packet(PacketsEnum id)
        {
            if (ignore_packet.Contains(id))
                return true;
            return false;
        }

        private static Dictionary<PacketsEnum, List<Action<object>>> InitActionStack()
        {
            var actionStack = new Dictionary<PacketsEnum, List<Action<object>>>();
            foreach (PacketsEnum p in Enum.GetValues(typeof(PacketsEnum)))
                actionStack[p] = new List<Action<object>>();
            return actionStack;
        }

        public void AddOneExecutionAction<T>(PacketsEnum messageType, Action<T> call)
        {
            OneExecutionActionStack[messageType].Add((obj) => call((T)obj));
        }

        public void listener_packet_receive(object sender, PacketReceiveEventArg e)
        {
            try
            {
                if (read_data(e.Data, log_listener, true) == false)
                    listener.send(e.Data, listener.sendTo);
            } catch (Exception a)
            {
                Console.WriteLine(a);
                listener.send(e.Data, listener.sendTo);
            }
        }

        public void connector_packet_receive(object sender, PacketReceiveEventArg e)
        {
            try
            {
                if (read_data(e.Data, log_connector, false) == false)
                    connector.send(e.Data, connector.sendTo);
            }
            catch (Exception a)
            {
                Console.WriteLine(a);
                connector.send(e.Data, connector.sendTo);
            }
        }

        public void log_packet_and_set_lastid(Packet packet, logbytes log)
        {
            packet.last_id = last_id;
            log.log(packet);
            last_id = packet.id;
        }

        public void cancel_packet_and_send_remade(Packet o, Packet packet)
        {
            log_packet_and_set_lastid(o, log_connector);
            log_packet_and_set_lastid(packet, log_listener);
            listener.send(packet.data, listener.sendTo);
        }

        private void SubscribeActions()
        {
            AddOneExecutionAction(PacketsEnum.SERVER_LIST, (ServerList o) =>
            {
                GameServerConnect packet = new GameServerConnect(null, PacketsEnum.GAMESERVER_CONNECT);

                log_packet_and_set_lastid(o, log_connector);
                serverconnect_ip = o.ip;
                connector.send(o.data, connector.sendTo);
            });
            AddOneExecutionAction(PacketsEnum.GAMESERVER_CONNECT_TOKEN, (GameServerConnectToken o) =>
            {
                auth.auth auth;

                log_packet_and_set_lastid(o, log_connector);
                connector.send(o.data, connector.sendTo);
                Thread t = new Thread(() =>
                {
                    auth = new auth.auth(serverconnect_ip, 5556, token);
                });
                t.Start();
            });
        }
        public List<PacketsEnum> ignore_packet = new List<PacketsEnum>
        {
            PacketsEnum.SERVER_LIST,
        };
    }
}
