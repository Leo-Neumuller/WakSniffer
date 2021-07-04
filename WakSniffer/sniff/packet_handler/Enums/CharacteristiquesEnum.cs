﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakSniffer.sniff.packet_handler.Enums
{
    //https://github.com/hussein-aitlahcen/wakfu-src/blob/0b9f7717bd2ead22785b71b6837eb84dac84a0fc/com/ankamagames/wakfu/common/game/fighter/FighterCharacteristicType.java
    public enum CHARACTERISTIQUES : byte
    {
        HP = 1,
        AP = 2,
        MP = 3,
        WP = 4,
        TACKLE = 7,
        DODGE = 8,
        FEROCITY = 9,
        FUMBLE_RATE = 1,
        RANGE = 11,
        LEADERSHIP = 12,
        MECHANICS = 13,
        KO_TIME_BEFORE_DEATH = 14,
        BACKSTAB_BONUS = 16,
        RES_BACKSTAB = 17,
        HEAL_IN_PERCENT = 18,
        MECHANISM_MASTERY = 19,
        DMG_IN_PERCENT = 2,
        DMG_FIRE_PERCENT = 21,
        DMG_WATER_PERCENT = 22,
        DMG_EARTH_PERCENT = 23,
        DMG_AIR_PERCENT = 24,
        DMG_REBOUND = 25,
        DMG_ABSORB = 26,
        RES_IN_PERCENT = 27,
        RES_FIRE_PERCENT = 28,
        RES_WATER_PERCENT = 29,
        RES_EARTH_PERCENT = 3,
        RES_AIR_PERCENT = 31,
        RES_HEAL_PERCENT = 32,
        PROSPECTION = 33,
        INIT = 35,
        WISDOM = 36,
        WATER_MASTERY = 37,
        AIR_MASTERY = 38,
        EARTH_MASTERY = 39,
        FIRE_MASTERY = 4,
        VITALITY = 41,
        MP_DEBUFF_RES = 42,
        AP_DEBUFF_RES = 43,
        EQUIPMENT_KNOWLEDGE = 45,
        AP_DEBUFF_POWER = 46,
        MP_DEBUFF_POWER = 47,
        FINAL_DMG_IN_PERCENT = 48,
        FINAL_RES_IN_PERCENT = 49,
        CHRAGE = 5,
        CRITICAL_BONUS = 52,
        LIFE_STOLEN_BONUS = 53,
        VOODOOL_HP_LOSS_BONUS = 54,
        SUMMONING_MASTERY = 55,
        DMG_VS_SUMMMONS = 56,
        OCCUPATION_RESOURCEFULNESS = 65,
        OCCUPATION_HARVEST_QUICKNESS = 66,
        OCCUPATION_CRAFT_QUICKNESS = 67,
        OCCUPATION_QUICK_LEARNER_HARVEST = 7,
        OCCUPATION_QUICK_LEARNER_CRAFT = 71,
        OCCUPATION_GREEN_THUMBS = 72,
        ECAFLIP_DOUBLE_OR_QUITS_BUFF = 73,
        BOMB_COOLDOWN = 74,
        ARMOR_PLATE = 75,
        ARMOR_PLATE_BONUS = 76,
        STATE_APPLICATION_BONUS = 77,
        STATE_RESISTANCE_BONUS = 78,
        FECA_GLYPH_CHARGE_BONUS = 79,
        XELORS_DIAL_CHARGE = 8,
        PERCEPTION = 81,
        STRENGTH = 82,
        INTELLIGENCE = 83,
        AGILITY = 84,
        LUCK = 85,
        WILLPOWER = 86,
        BLOCK = 87,
        VIRTUAL_ARMOR_BONUS = 88,
        AREA_HP = 89,
        DMG_STASIS_PERCENT = 9,
        RES_STASIS_PERCENT = 91,
        VIRTUAL_HP = 92,
        STEAMER_MICROBOT_MAX_DISTANCE = 93,
        TOTAL_HP = 94,
        CRITICAL_RES = 95,
        OSA_INVOCATION_KNOWLEDGE = 96,
        ARMOR = 97,
        STASIS_MASTERY = 98,
        GREED = 99,
        BARRIER = 1,
        NON_CRIT_DMG = 1,
        MELEE_DMG = 1,
        RANGED_DMG = 1,
        SINGLE_TARGET_DMG = 1,
        AOE_DMG = 1,
        BERSERK_DMG = 1
    }
}