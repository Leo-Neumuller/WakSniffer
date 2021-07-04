using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    class SelectCharacter : Packet
    {
        public SelectCharacter(long playerid, string name, PacketsEnum id)
        {
            data = playerid.GetBytes().Concat(name.Length.GetBytes()).Concat(name.GetBytes()).ToArray();
            this.id = id;
            unknownbyte = 0x02;
            data = set_header(id, data, true);
        }
    }
}
