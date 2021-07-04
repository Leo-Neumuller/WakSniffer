﻿using PacketEditor.WakfuBot.Packets;
using WakSniffer.sniff.packet_handler.packet.Character;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    //https://github.com/hussein-aitlahcen/wakfu-src/blob/0b9f7717bd2ead22785b71b6837eb84dac84a0fc/com/ankamagames/wakfu/common/datas/CharacterSerializedAppearance.java
    public class CompanionControllerIdPart : CharacterSerializedPart
    {
        public override CHARACTER_PART CharacterPart { get; set; } = CHARACTER_PART.COMPANION_CONTROLLER_ID;
        public long ControllerId;
        public long CompanionId;

        public override void FromData(ByteReader rd)
        {
            rd.Read(out ControllerId);
            rd.Read(8);
        }

        public static CompanionControllerIdPart Deserialize(ByteReader rd)
        {
            var ret = new CompanionControllerIdPart();
            ret.FromData(rd);
            return ret;
        }
    }
}
