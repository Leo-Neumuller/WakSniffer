using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    class AskPublicKey : Packet
    {
        public AskPublicKey(ByteReader rd, PacketsEnum id)
        {
            data = Util.StringToByteArray("00050801a9");
            base.id = id;
        }
    }
}
