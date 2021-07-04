using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakSniffer.sniff.packet_handler
{
    public enum PacketsEnum : short
    {
        DEFAULT_RESULT_MESSAGE = 392,
        SERVER_VERSION = 1,
        PUBLIC_KEY = 503,
        ACCCOUNT_INFO = 568,
        LOGIN_SUCCESS = 421,
        SERVER_LIST = 424,
        GAMESERVER_CONNECT_TOKEN = 562,
        RECEIVE_PLAYER_MOVE = 13245,
        ACTOR_SPAWN = 12837,
        CHARACTER_LIST = 23135,
        CHARACTER_INFORMATION = 13815,

        CLIENT_VERSION = 20,
        ASK_PUBLIC_KEY = 408,
        LOGIN = 506,
        ASK_SERVER_LIST = 442,
        GAMESERVER_CONNECT = 553,
        SEND_TOKEN = 434,
        PLAYER_MOVE = 12714,
        SEND_MSG = 625,
        SELECT_CHARACTER = 22199,
    }
}
