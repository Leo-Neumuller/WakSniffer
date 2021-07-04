using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    class ClientVersion : Packet
    {
        public ClientVersion(ByteReader rd, PacketsEnum id)
        {
            data = Util.StringToByteArray("000c00001801004702022d31");
            base.id = id;
        }
    }
}
