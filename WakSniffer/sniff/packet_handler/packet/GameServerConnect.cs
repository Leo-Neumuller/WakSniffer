using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    class GameServerConnect : Packet
    {
        public GameServerConnect(ByteReader rd, PacketsEnum id)
        {
            int id_server = 9;
            long unknown = 0;
            data = id_server.GetBytes().Concat(unknown.GetBytes()).ToArray();
            data = set_header(id, data, true);
            base.id = id;
        }
    }
}
