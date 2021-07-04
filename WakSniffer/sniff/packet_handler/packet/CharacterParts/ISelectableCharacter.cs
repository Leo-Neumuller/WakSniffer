using WakSniffer.sniff.packet_handler.packet.Character;
using WakSniffer.Utils;

namespace PacketEditor.WakfuBot.Packets.ToReceiv
{
    public interface ISelectableCharacter
    {
        long Id { get; set; }
        string DisplayName();
    }
}