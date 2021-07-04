using PacketEditor.WakfuBot.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.sniff.packet_handler.packet.Character;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    public abstract class CharacterSerializedPart
    {
        public abstract CHARACTER_PART CharacterPart { get; set; }
        public abstract void FromData(ByteReader rd);
    }
}
