using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler
{
    public class Packet
    {
        public PacketsEnum id;
        public byte[] data;
        public PacketsEnum last_id = (PacketsEnum)(-1);
        public byte unknownbyte = 0x08;

        public Packet()
        {
        }

        public Packet(ByteReader rd, PacketsEnum id, bool is_listener)
        {
            data = rd.ReadAll();
            this.id = id;
            data = set_header(id, data, is_listener);
        }

        public ByteReader set_id_and_data(ByteReader rd, PacketsEnum id)
        {
            data = rd.ReadAll();
            rd = new ByteReader(data);
            this.id = id;
            return rd;
        }

        public byte[] set_header(PacketsEnum id, byte[] data, bool is_listener)
        {
            byte[] full_data;
            BigEndianWriter big = new BigEndianWriter();

            if (is_listener)
            {
                big.WriteShort((short)(data.Length + 5));
                big.WriteByte(unknownbyte);
            } else
                big.WriteShort((short)(data.Length + 4));
            big.WriteShort((short)id);
            big.WriteBytes(data);
            full_data = big.Data;
            return full_data;
        }
    }
}
