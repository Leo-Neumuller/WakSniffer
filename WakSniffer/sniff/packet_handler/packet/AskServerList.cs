using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    class AskServerList : Packet
    {
        public AskServerList(ByteReader rd, PacketsEnum id)
        {
            data = Util.StringToByteArray("00050801ba");
            base.id = id;
        }
    }
}
