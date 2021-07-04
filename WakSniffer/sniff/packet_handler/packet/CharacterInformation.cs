using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.sniff.packet_handler.Utils;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler
{
    class CharacterInformation : Packet
    {
        private List<long> ReservedIds = new List<long>();
        public Character Character;

        public CharacterInformation(ByteReader rd, PacketsEnum id)
        {
            rd = set_id_and_data(rd, id);
            var reservedIds = rd.ReadShort();
            for (var i = 0; i < reservedIds; i++)
                ReservedIds.Add(rd.ReadLong());
            Character = new Character();
            Character.UnserializePlayer(new ByteReader(rd.Read(rd.ReadInt())));
            data = set_header(id, data, false);
        }
    }
}
