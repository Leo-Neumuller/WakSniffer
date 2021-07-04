using PacketEditor.WakfuBot.Packets;
using WakSniffer.sniff.packet_handler.packet.Character;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    //https://github.com/hussein-aitlahcen/wakfu-src/blob/0b9f7717bd2ead22785b71b6837eb84dac84a0fc/com/ankamagames/wakfu/common/datas/CharacterSerializedAppearance.java
    public class CharactereUNKNOWPart : CharacterSerializedPart
    {
        public override CHARACTER_PART CharacterPart { get; set; } = CHARACTER_PART.CUSTOM_TEMPLATE;


        public override void FromData(ByteReader rd)
        {
        }

        public static CharactereUNKNOWPart Deserialize(ByteReader rd)
        {
            var ret = new CharactereUNKNOWPart();
            ret.FromData(rd);
            return ret;
        }
    }
}
