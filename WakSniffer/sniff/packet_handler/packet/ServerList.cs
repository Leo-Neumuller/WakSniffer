using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    class ServerList : Packet
    {
        public string ip = "pandora.platforms.wakfu.com";
        public ServerList(ByteReader rd, PacketsEnum id)
        {
            byte[] new_packet;
            byte[] data = rd.ReadAll();
            BigEndianWriter big = new BigEndianWriter();
            int server_nb;


            rd = new ByteReader(data);
            server_nb = rd.ReadInt();
            big.WriteInt(server_nb);
            for (int i = 0; i < server_nb; i++)
            {
                change_ip(rd, big);
            }
            big.WriteBytes(rd.ReadAll());
            base.id = id;
            rd = new ByteReader(data);
            new_packet = set_header(id, big.Data, false);
            base.data = new_packet;
        }

        private void change_ip(ByteReader rd, BigEndianWriter big)
        {
            int name_length;
            byte[] name;
            int ip_length;
            byte[] ip;
            int port_one;
            int port_two;
            string new_ip = "127.0.0.1";
            int new_ip_length = new_ip.Length;

            big.WriteInt(rd.ReadInt());
            name_length = rd.ReadInt();
            big.WriteInt(name_length);
            name = rd.Read(name_length);
            big.WriteBytes(name);
            big.WriteInt(rd.ReadInt());
            ip_length = rd.ReadInt();
            ip = rd.Read(ip_length);
            big.WriteInt(new_ip_length);
            big.WriteBytes(new_ip.GetBytes());
            big.WriteInt(rd.ReadInt());
            port_one = rd.ReadInt();
            port_two = rd.ReadInt();
            big.WriteInt(port_one);
            big.WriteInt(port_two);
            big.WriteByte(rd.ReadByte());
            Console.WriteLine(System.Text.Encoding.Default.GetString(name) + " " + System.Text.Encoding.Default.GetString(ip) + " " + port_one.ToString() + " " + port_two.ToString());
        }
    }
}
