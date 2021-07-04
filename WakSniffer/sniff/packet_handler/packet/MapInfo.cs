using System.Collections.Generic;
using System.Linq;
using WakSniffer.Utils;
using WakSniffer.sniff.packet_handler.Utils;
using System.Text;
using System;

namespace WakSniffer.sniff.packet_handler.packet
{
    public class MapInfo : Packet
    {
        public static PacketsEnum packetType = PacketsEnum.ACTOR_SPAWN;

        public byte CharacterCount;
        public bool InFightSpawn;

        public byte CharacterType;

        public List<Utils.Character> Units = new List<Utils.Character>();

        public Utils.Character current;

        public MapInfo(ByteReader rd, PacketsEnum id)
        {
            byte[] tmp;
            base.data = rd.ReadAll();
            base.id = id;
            rd = new ByteReader(base.data);
            rd.Read(out InFightSpawn);
            rd.Read(out CharacterCount);
            for (int i = 0; i < CharacterCount; i++)
            {
                CharacterType = rd.ReadByte();
                current = Utils.Character.CreateCharacter(rd, CharacterType == 0, CharacterType);
                Units.Add(current);
            }
            base.data = set_header(id, base.data, false);
        }
    }
}
