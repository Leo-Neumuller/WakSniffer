using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    class GameServerConnectToken : Packet
    {
        public byte[] token;
        public GameServerConnectToken(ByteReader rd, PacketsEnum id)
        {
            base.id = id;
            base.data = rd.ReadAll();
            rd = new ByteReader(data);
            rd.Read(4);
            token = rd.ReadAll();
            data = set_header(id, data, false);
        }
    }
}
