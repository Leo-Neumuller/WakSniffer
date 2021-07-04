using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    class PublicKey : Packet
    {
        public byte[] key;
        public long salt;
        public PublicKey(ByteReader rd, PacketsEnum id)
        {
            base.id = id;
            data = rd.ReadAll();
            rd = new ByteReader(data);
            salt = rd.ReadLong();
            key = rd.ReadAll();
            data = set_header(id, data, false);
        }
    }
}
