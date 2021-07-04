using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.sniff.packet_handler.Enums;
using WakSniffer.sniff.packet_handler.globalclasses;
using WakSniffer.sniff.packet_handler.packet;
using WakSniffer.sniff.packet_handler.Utils;

namespace WakSniffer.sniff.packet_handler
{
    public class APlayerInformations
    {

        public ChatacterPositionPart ChatacterPositionPart { get; set; }
        public FightCharacteristiquesPart CharacteristiquesPart { get; set; }
        public CharacterSpellInventoryPart CharacterSpellInventoryPart { get; set; }
        public CustomGuildPart CustomGuildPart { get; set; }

        public void UpdatePosition(MapPosition pos)
        {
            if (ChatacterPositionPart == null)
                ChatacterPositionPart = new ChatacterPositionPart();
            ChatacterPositionPart.CharacterPositon = pos;
        }

        public void UpdatePosition(ChatacterPositionPart chatacterPositionPart)
            => ChatacterPositionPart = chatacterPositionPart;

        public void UpdateCharacteristiques(FightCharacteristiquesPart characteristiquesPart)
            => CharacteristiquesPart = characteristiquesPart;

        public void UpdateSpellInventory(CharacterSpellInventoryPart spellInventoryPart)
            => CharacterSpellInventoryPart = spellInventoryPart;

        public MapPosition GetPosition()
            => ChatacterPositionPart?.CharacterPositon;

        public long GetSpellId(Spell spell)
            => CharacterSpellInventoryPart.Spells.FirstOrDefault(s => s.SpellId == (int)spell).UniqueId;

        public Characteristiques Get(CHARACTERISTIQUES carac)
            => CharacteristiquesPart?.Get(carac);

        public int GetCurrent(CHARACTERISTIQUES carac, int defaultValue = 0)
            => CharacteristiquesPart.GetCurrent(carac, defaultValue);

    }
}
