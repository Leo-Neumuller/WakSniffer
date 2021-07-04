using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    class SendToken : Packet
    {
        public SendToken(byte[] token, PacketsEnum id)
        {
            unknownbyte = 0x01;
            token = Util.StringToByteArray("000000").Concat(token).ToArray();
            data = set_header(id, token, true);
            this.id = id;
        }
    }
}
