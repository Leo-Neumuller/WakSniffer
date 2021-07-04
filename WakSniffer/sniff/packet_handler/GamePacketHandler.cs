using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WakSniffer.sniff.packet_handler.globalclasses;
using WakSniffer.sniff.packet_handler.packet;
using WakSniffer.Utils;

namespace WakSniffer.sniff.packet_handler
{
    class GamePacketHandler : PacketHandler
    {
        public List<CharacterSelection> Characters;
        public APlayerInformations CharacterData;
        int back_and_forth;


        public GamePacketHandler(ssl_listener listener, ssl_client connector, byte[] token)
        {
            this.token = token;
            this.listener = listener;
            this.connector = connector;
            log_listener = new logbytes(true);
            log_connector = new logbytes(false);
            CharacterData = new APlayerInformations();
            ignore_packet = ignore_gamepacket;
            back_and_forth = 1;
            this.SubscribeActions();
        }

        private void SubscribeActions()
        {
        }

        public List<PacketsEnum> ignore_gamepacket = new List<PacketsEnum>
        {
        };
    }
}
