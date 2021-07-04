using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.sniff.packet_handler.globalclasses;
using WakSniffer.sniff.packet_handler.packet.Character;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    //https://github.com/hussein-aitlahcen/wakfu-src/blob/0b9f7717bd2ead22785b71b6837eb84dac84a0fc/com/ankamagames/wakfu/common/datas/CharacterSerializedPosition.java
    public class ChatacterPositionPart : CharacterSerializedPart
    {
        public override CHARACTER_PART CharacterPart { get; set; } = CHARACTER_PART.POSITION;
        public MapPosition CharacterPositon;
        public short InstanceId;
        public byte Direction;
        public MapPosition DimBagPosition;
        public short DimBagInstance;

        public override void FromData(ByteReader rd)
        {
            rd.ReadInt();
            CharacterPositon = new MapPosition(rd);
            rd.Read(out InstanceId);
            rd.Read(out Direction);
            if (rd.ReadBool())
            {
                DimBagPosition = new MapPosition(rd);
                rd.Read(out DimBagInstance);
            }
        }

        public static ChatacterPositionPart Deserialize(ByteReader rd)
        {
            var ret = new ChatacterPositionPart();
            ret.FromData(rd);
            return ret;
        }
    }
}
