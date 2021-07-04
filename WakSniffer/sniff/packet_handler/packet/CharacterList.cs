using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    public class CharacterList : Packet
    {

        public List<CharacterSelection> Characters = new List<CharacterSelection>();

        public CharacterList(ByteReader rd, PacketsEnum id)
        {
            rd = set_id_and_data(rd, id);
            byte characterCount = rd.ReadByte();
/*            for (int i = 0; i < characterCount; i++)
            {*/
                short serializedSize = rd.ReadShort();
                byte serializerType = rd.ReadByte();
                Characters.Add(new CharacterSelection(rd));
/*            }*/
            data = set_header(id, data, false);
        }
    }

    public class CharacterSelection
    {
        public long Id { get; set; }
        public byte Type;
        public long Owner;
        public string Name;
        public BREED Breed { get; set; }
        public byte ActivteStuffSheet;
        public byte Gender;
        public byte SkinColorIndex;
        public byte HairColorIndex;
        public byte PupilColorIndex;
        public byte SkinColorFactor;
        public byte HairColorFactor;
        public byte PupilColorFactor;
        public byte ClothIndex;
        public byte FaceIndex;
        public byte CurrentTitle;

        public List<StuffApparence> StuffsApparence = new List<StuffApparence>();

        public bool IsNew;
        public bool NeedRecustom;
        public short RecustomValue;
        public byte NeedInitialXp;

        public int Nation;
        public long Xp;
        public long GuildeId;
        public long GuildBlazon;
        public short InstanceId;

        public CharacterSelection(ByteReader rd)
        {

            Id = rd.ReadLong();
            rd.Read(out Type);
            rd.Read(out Owner);
            rd.Read(out Name);
            Breed = (BREED)rd.ReadShort();
/*            rd.Read(out ActivteStuffSheet);
            rd.Read(out Gender);
            rd.Read(out SkinColorIndex);
            rd.Read(out HairColorIndex);
            rd.Read(out PupilColorIndex);
            rd.Read(out SkinColorFactor);
            rd.Read(out HairColorFactor);
            rd.Read(out PupilColorFactor);
            rd.Read(out ClothIndex);
            rd.Read(out FaceIndex);
            rd.Read(out CurrentTitle);
            byte stuffCount = rd.ReadByte();
            for (byte i = 0; i < stuffCount; i++)
            {
                StuffsApparence.Add(new StuffApparence(rd));
            }

            if (rd.ReadByte() == 1)
            {
                rd.Read(out IsNew);
                rd.Read(out NeedRecustom);
                rd.Read(out RecustomValue);
                rd.Read(out NeedInitialXp);
            }
            rd.Read(out Xp);
            rd.Read(out Nation);
            rd.Read(out GuildeId);
            rd.Read(out GuildBlazon);
            rd.Read(out InstanceId);*/
        }

        public string DisplayName()
        {
            return Name;
        }

        public class StuffApparence
        {
            public byte Position;
            public int ReferenceId;
            public int SkinId;

            public StuffApparence(ByteReader rd)
            {
                rd.Read(out Position);
                rd.Read(out ReferenceId);
                rd.Read(out SkinId);
            }
        }
    }
}