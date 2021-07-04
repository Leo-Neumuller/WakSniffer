using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WakSniffer.sniff.auth;
using WakSniffer.sniff.packet_handler;
using WakSniffer.Utils;

namespace WakSniffer
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        public static MyListView ClientView;
        public static MyListView ServerView;
        public static EventHandler ButtonMoveEvent;
        public Form1()
        {
            Instance = this;
            InitializeComponent();
            ClientView = this.ClientList;
            ServerView = this.ServerList;
            ClientView.AddEvent += AddValueEventClient;
            ServerView.AddEvent += AddValueEventServer;
        }

        public static void Invoke(Action action)
    => Instance.BeginInvoke(action);

        private auth auth;
        Thread t;
        private void start_Click(object sender, EventArgs e)
        {
            start.Enabled = false;
            t = new Thread(() =>
            {
                try
                {
                    auth = new auth(Properties.Settings.Default.auth_ip, Properties.Settings.Default.auth_port);
                } catch(Exception a)
                {
                    Console.WriteLine(a);
                }
            });
            t.IsBackground = true;
            t.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
            Environment.Exit(0);
        }

        private void ClientList_Click(object sender, EventArgs e)
        {
            if (ClientList.SelectedItems != null)
            {
                Packet packet = (Packet)ClientList.SelectedItems[0].Tag;
                StringBuilder hex = new StringBuilder(packet.data.Length * 2);
                foreach (byte b in packet.data)
                {
                    hex.AppendFormat("{0:x2}", b);
                    hex.Append(" ");
                }
                this.dataBox.Text = hex.ToString();
                this.DataString.Text = Hex.HexDump(packet.data);
                this.labelLastPacket.Text = packet.last_id.ToString();
            }
            else
            {
                this.dataBox.Text = "";
            }
        }

        private void ServerList_Click(object sender, EventArgs e)
        {
            if (ServerList.SelectedItems != null)
            {
                Packet packet = (Packet)ServerList.SelectedItems[0].Tag;
                StringBuilder hex = new StringBuilder(packet.data.Length * 2);
                foreach (byte b in packet.data)
                {
                    hex.AppendFormat("{0:x2}", b);
                    hex.Append(" ");
                }
                this.dataBox.Text = hex.ToString();
                this.DataString.Text = Hex.HexDump(packet.data);
                this.labelLastPacket.Text = packet.last_id.ToString();
            }
            else
            {
                this.dataBox.Text = "";
            }
        }

        public void AddValueEventClient(object sender, EventArgs e)
        {
            if (this.AutoScrollClient.Checked == true)
                ClientView.Items[ClientView.Items.Count - 1].EnsureVisible();
        }

        public void AddValueEventServer(object sender, EventArgs e)
        {
            if (this.AutoScrollServer.Checked == true)
                ServerView.Items[ServerView.Items.Count - 1].EnsureVisible();
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            ButtonMoveEvent?.Invoke(this, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            start_Click(this, e);
        }
    }
}
