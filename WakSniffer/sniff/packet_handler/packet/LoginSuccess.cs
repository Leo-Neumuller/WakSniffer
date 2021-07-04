using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    class LoginSuccess : Packet
    {
        public LoginSuccess(ByteReader rd, PacketsEnum id) : base(rd, id, false)
        {
        }
    }
}
