using System;
using System.Text;
using System.Windows.Forms;
using WakSniffer.sniff.packet_handler;

namespace WakSniffer.Utils
{
    public class logbytes
    {
        private bool is_client;
        public bool is_hashed_logs;
        public logbytes(bool is_client)
        {
            this.is_client = is_client;
        }

        public void log(Packet packet)
        {
            if (ignore_packets(packet) == 0)
                add_to_list_view(packet);
        }

        public static void log_bytes(byte[] data)
        {
            StringBuilder hex = new StringBuilder(data.Length * 2);
            foreach (byte b in data)
            {
                hex.AppendFormat("{0:x2}", b);
                hex.Append(" ");
            }
            Console.WriteLine(hex);
        }

        private int ignore_packets(Packet packet)
        {
            return 0;
        }

        private void add_to_list_view(Packet packet)
        {
            ListViewItem item = new ListViewItem();

            if (Enum.IsDefined(typeof(PacketsEnum), packet.id))
                item.Text = packet.id.ToString() + " " + ((int)packet.id).ToString();
            else
                item.Text = packet.id.ToString();
            item.Tag = packet;
            if (is_client)
            {
                Form1.Invoke(() => Form1.ClientView.Items.Add(item));
            } else
            {
                Form1.Invoke(() => Form1.ServerView.Items.Add(item));
            }
        }

        public void print_byte(byte[] data)
        {
            StringBuilder hex = new StringBuilder(data.Length * 2);
            foreach (byte b in data)
            {
                hex.AppendFormat("{0:x2}", b);
                hex.Append(" ");
            }
            Console.WriteLine(hex);
        }
    }
}
