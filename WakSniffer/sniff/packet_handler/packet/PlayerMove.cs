using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.sniff.packet_handler.globalclasses;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler.packet
{
    class PlayerMove : Packet
    {
        public MapPosition pos;
        public PlayerMove(PathMove pathMove, PacketsEnum id, DeplacementType deplacementType = DeplacementType.RUN)
        {
            base.id = id;
            pos = pathMove.StartPos;
            data = ((int)deplacementType).GetBytes().Concat(pathMove.StartPos.GetBytes()).Concat(pathMove.EncodedPath).ToArray();
            unknownbyte = 3;
            data = set_header(id, data, true);
        }

        public PlayerMove(ByteReader rd, PacketsEnum id)
        {
            base.id = id;
            rd.ReadInt();
            pos = new MapPosition(rd);
            unknownbyte = 3;
            data = set_header(id, rd.ReadAll(), true);
        }
    }

    public enum DeplacementType : int
    {
        WALK = 0,
        RUN = 1,
        SLIDE = 2,
        SWIM = 3,
        WALK_CARRY = 4,
        THROW = 5,
        CUSTOM_WALK = 6,
        TRAJECTORY = 7,
        NONE = 8,
        WALK_SPELL = 9,
        RUN_IN_FIGHT = 10,
        MOUNT = 11,
        MOUNT_WALK = 12,
        WALK_WEAPON = 13,
        UNKNOWN = -1
    }
}
