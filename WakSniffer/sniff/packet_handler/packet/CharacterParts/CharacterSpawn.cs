﻿using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WakSniffer.sniff.packet_handler.packet;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.Utils
{
    public class Character
    {
        public SERIALIZATION_TYPE SerializationType;
        public CharacterIDPart CharacterIDPart;
        public CharactereIdentityPart CharactereIdentityPart;
        public CharactereNamePart CharactereNamePart;
        public CharactereBreedPart CharactereBreedPart;
        public ChatacterPositionPart ChatacterPositionPart;
        public CompanionControllerIdPart CompanionControllerIdPart;
        public ControlledByIAPart ControlledByIAPart;
        public FightPropsPart FightPropsPart;
        public CharacterSpellInventoryPart CharacterSpellInventoryPart;
        public FightCharacteristiquesPart CharacteristiquesPart;
        public CharacterFightPart FightInfo;
        public CustomLevelPart CustomLevelPart;
        public GroupPart GroupPart;
        public TemplatePart TemplatePart;
        public WorldPropsPart WorldPropsPart;
        public CollectPart CollectPart;
        public CustomGuildPart CustomGuildPart;
        public byte CharacterType;



        public static Character CreateCharacter(ByteReader rd, bool isPlayer, byte CharacterType)
        {
            long characterId = rd.ReadLong();
            short serializedSize = rd.ReadShort();
            ByteReader SerializedCharacter = new ByteReader(rd.Read(serializedSize));
            var character = new Character();
            character.CharacterType = CharacterType;
            if (isPlayer)
                character.UnserializePlayer(SerializedCharacter);
            else
                character.UnserializeNPC(SerializedCharacter);
            return character;
        }

        public int GetGroupLevel() => GroupPart.Group.Sum(i => i.level);

        public void UnserializePlayer(ByteReader rd)
        {
            SerializationType = (SERIALIZATION_TYPE)rd.ReadByte();
            FieldInfo[] properties = typeof(Character).GetFields();
            Serializationsinstructions.TryGetValue(SerializationType, out Type[] seserializers);
            seserializers?.Select(type =>
            {
                return (CharacterSerializedPart)type.GetMethod("Deserialize").Invoke(null, new object[] { rd });
            }).ForEach(infos =>
            {
                properties.FirstOrDefault(p => p.FieldType == infos.GetType())?.SetValue(this, infos);
            });
        }



        public void UnserializeNPC(ByteReader rd)
        {
            if (rd.Remaining() == 0)
                return;
            //Console.WriteLine("test");
            SerializationType = (SERIALIZATION_TYPE)rd.ReadByte();
            //Console.WriteLine("test1");
            CharacterIDPart = CharacterIDPart.Deserialize(rd); //long
            //Console.WriteLine("test2");
            CharactereIdentityPart = CharactereIdentityPart.Deserialize(rd); // byte long
            //Console.WriteLine("test3");
            CharactereNamePart = CharactereNamePart.Deserialize(rd); // short
            //Console.WriteLine("test4");
            CharactereBreedPart = CharactereBreedPart.Deserialize(rd); //short
            //Console.WriteLine("test5");
            rd.Index -= 4;
            ChatacterPositionPart = ChatacterPositionPart.Deserialize(rd); //int int short short byte byte
            //Console.WriteLine("test6");
            rd.Read(6);
            //Console.WriteLine("test7");
            CustomLevelPart = CustomLevelPart.Deserialize(rd);
            //Console.WriteLine("test8");
            PublicCaractsPart PublicCaractsPart = PublicCaractsPart.Deserialize(rd);
            //Console.WriteLine("test9");
            FightPropsPart = FightPropsPart.Deserialize(rd);
            //Console.WriteLine("test10");
            FightInfo = CharacterFightPart.Deserialize(rd);
            //Console.WriteLine("test11");
            CurrentMovementPathPart CurrentMovementPathPart = CurrentMovementPathPart.Deserialize(rd);
            //Console.WriteLine("test12");
            WorldPropsPart = WorldPropsPart.Deserialize(rd);
            //Console.WriteLine("test13");
            GroupPart = GroupPart.Deserialize(rd);
            //Console.WriteLine("test14");
            TemplatePart = TemplatePart.Deserialize(rd);
            //Console.WriteLine("test15");
            CollectPart = CollectPart.Deserialize(rd);
            //Console.WriteLine("test16");
            CompanionControllerIdPart = CompanionControllerIdPart.Deserialize(rd);
            //Console.WriteLine("test17");
        }


        /*public StartFight FightPacket() => StartFight.GetPacket(this);*/


        private static Dictionary<SERIALIZATION_TYPE, Type[]> Serializationsinstructions = new Dictionary<SERIALIZATION_TYPE, Type[]>
        {
            {
                SERIALIZATION_TYPE.TOTAL,
                new [] { typeof(CharacterIDPart), typeof(CharactereIdentityPart), typeof(CharactereNamePart) }
            },
            {
                SERIALIZATION_TYPE.FOR_LOCAL_CHARACTER_INFORMATION,
                new [] {
                    typeof(CharacterIDPart),
                    typeof(CharactereIdentityPart),
                    typeof(CharactereNamePart),
                    typeof(CharactereBreedPart),
                    typeof(CustomGuildPart),
                    typeof(CharactereHPPart),
                    typeof(ChatacterPositionPart),
                    typeof(CharacterAppearancePart),
                    typeof(CharacterShortcutInventory),
                    typeof(CharactereEmoteInventoryPart),
                    typeof(CharactereLandmarkInventoryPart),
                    typeof(CharactereDiscoveredItemsPart),
                    typeof(CharacterSpellInventoryPart)
                }
            },
            {
                SERIALIZATION_TYPE.FOR_REMOTE_CHARACTER_INFORMATION,
                new [] {
                    typeof(CharacterIDPart),
                    typeof(CharactereIdentityPart),
                    typeof(CharactereNamePart),
                    typeof(CharactereBreedPart),
                    typeof(CustomGuildPart),
                    typeof(ChatacterPositionPart),
/*                    typeof(CharacterAppearancePart),*/
                }
            },
            {
                SERIALIZATION_TYPE.FOR_HERO_CHARACTER_LOAD,
                new [] {
                    typeof(CharacterIDPart),
                    typeof(CharactereIdentityPart),
                    typeof(CharactereNamePart),
                    typeof(CharactereBreedPart),
                    typeof(CharactereHPPart),
                    typeof(CharacterAppearancePart),
                    typeof(CharacterShortcutInventory),
                    typeof(CharactereEmoteInventoryPart),
                    typeof(CharactereLandmarkInventoryPart),
                    typeof(CharactereDiscoveredItemsPart),
                    typeof(CharacterSpellInventoryPart)
                }
            },
             {
                SERIALIZATION_TYPE.FIGHTER_DATA_FOR_RECONNECTION,
                new [] {
                    typeof(ChatacterPositionPart),
                    typeof(CharacterFightPart),
                    typeof(FightCharacteristiquesPart),
                    typeof(FightPropsPart),
                    typeof(WorldPropsPart),
                    typeof(CharactereBreedPart),
                    typeof(ControlledByIAPart),
                    typeof(CharacterIDPart),
                    typeof(CharactereIdentityPart),
                }
            }
        };
    }


    public enum SERIALIZATION_TYPE : byte
    {
        UNKNOW,
        TOTAL,
        FOR_CHARACTER_CREATION_INFORMATION,
        FIGHTER_DATAS,
        INVENTORIES,
        FOR_CHARACTER_LIST,
        FOR_GAME_TO_GAME_SERVER_EXCHANGE,
        FOR_GAME_TO_GAME_SERVER_HERO_EXCHANGE,
        FOR_LOCAL_CHARACTER_INFORMATION = 8,
        FOR_REMOTE_CHARACTER_INFORMATION = 9,
        FOR_REMOTE_UPDATE_CHARACTER_INFORMATION,
        FOR_HERO_CHARACTER_LOAD = 11,
        FOR_PASSEPORT_INFO_UPDATE,
        FOR_APTITUDE_INVENTORY_UPDATE,
        FOR_CITIZEN_SCORE_UPDATE,
        FOR_NATION_PVP_UPDATE,
        FOR_WORLD_TO_GAME_SERVER_EXCHANGE,
        FOR_WORLD_TO_GAME_SERVER_HERO,
        FOR_WORLD_TO_GLOBAL_EXCHANGE,
        FOR_GLOBAL_TO_GAME_SERVER_EXCHANGE,
        FOR_GLOBAL_TO_LOCAL_CHARACTER,
        FOR_GLOBAL_TO_WORLD_EXCHANGE,
        FOR_AI_SERVER_INFORMATION,
        FOR_ACCOUNT_INFORMATION_UPDATE,
        FOR_REMOTE_ACCOUNT_INFORMATION_UPDATE,
        FOR_PROPERTIES_UPDATE_WITH_FIGHT,
        FOR_PROPERTIES_UPDATE_WITHOUT_FIGHT,
        FOR_CHARACTER_POSITION_INFORMATION,
        FOR_REMOTE_PET_UPDATE,
        FOR_DISCOVERED_ITEMS_UPDATE,
        BREED_SPECIFIC,
        FOR_PERSONAL_EFFECTS,
        FOR_ANTI_ADDICTION,
        SEX,
        BAGS,
        VISIBILITY,
        APTITUDE_BONUS_INVENTORY,
        NATION_PVP_MONEY,
        FIGHTER_DATA_FOR_RECONNECTION = 37,
        PUBLIC_CHARACTERISTICS,
        ALL_CHARACTERISTICS
    }
}
