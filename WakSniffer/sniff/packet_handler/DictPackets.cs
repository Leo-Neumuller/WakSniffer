using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakSniffer.sniff.packet_handler.packet;

namespace WakSniffer.sniff.packet_handler
{
    class DictPackets
    {
        public Dictionary<PacketsEnum, Type> Constructors = new Dictionary<PacketsEnum, Type>
        {
            { PacketsEnum.DEFAULT_RESULT_MESSAGE, typeof(DefaultResultMessage) },
            { PacketsEnum.PUBLIC_KEY, typeof(PublicKey) },
            { PacketsEnum.LOGIN_SUCCESS, typeof(LoginSuccess) },
            { PacketsEnum.SERVER_LIST, typeof(ServerList) },
            { PacketsEnum.GAMESERVER_CONNECT_TOKEN, typeof(GameServerConnectToken) },
            { PacketsEnum.ACTOR_SPAWN, typeof(MapInfo) },
            { PacketsEnum.PLAYER_MOVE, typeof(PlayerMove) },
            { PacketsEnum.CHARACTER_LIST, typeof(CharacterList) },
            { PacketsEnum.CHARACTER_INFORMATION, typeof(CharacterInformation) },
        };
    }
}
