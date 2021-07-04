namespace WakSniffer
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.start = new System.Windows.Forms.Button();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AutoScrollClient = new System.Windows.Forms.CheckBox();
            this.AutoScrollServer = new System.Windows.Forms.CheckBox();
            this.DataString = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelLastPacket = new System.Windows.Forms.Label();
            this.ServerList = new WakSniffer.Utils.MyListView();
            this.ClientList = new WakSniffer.Utils.MyListView();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(46, 369);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // dataBox
            // 
            this.dataBox.Location = new System.Drawing.Point(438, 76);
            this.dataBox.Name = "dataBox";
            this.dataBox.Size = new System.Drawing.Size(300, 316);
            this.dataBox.TabIndex = 4;
            this.dataBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Client";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(435, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Data";
            // 
            // AutoScrollClient
            // 
            this.AutoScrollClient.AutoSize = true;
            this.AutoScrollClient.Checked = true;
            this.AutoScrollClient.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoScrollClient.Location = new System.Drawing.Point(157, 408);
            this.AutoScrollClient.Name = "AutoScrollClient";
            this.AutoScrollClient.Size = new System.Drawing.Size(110, 17);
            this.AutoScrollClient.TabIndex = 10;
            this.AutoScrollClient.Text = "Enable auto scroll";
            this.AutoScrollClient.UseVisualStyleBackColor = true;
            // 
            // AutoScrollServer
            // 
            this.AutoScrollServer.AutoSize = true;
            this.AutoScrollServer.Checked = true;
            this.AutoScrollServer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoScrollServer.Location = new System.Drawing.Point(295, 408);
            this.AutoScrollServer.Name = "AutoScrollServer";
            this.AutoScrollServer.Size = new System.Drawing.Size(110, 17);
            this.AutoScrollServer.TabIndex = 11;
            this.AutoScrollServer.Text = "Enable auto scroll";
            this.AutoScrollServer.UseVisualStyleBackColor = true;
            // 
            // DataString
            // 
            this.DataString.Location = new System.Drawing.Point(763, 76);
            this.DataString.Name = "DataString";
            this.DataString.RightMargin = 1000;
            this.DataString.Size = new System.Drawing.Size(499, 316);
            this.DataString.TabIndex = 12;
            this.DataString.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(760, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Strings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(541, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Last packet :";
            // 
            // labelLastPacket
            // 
            this.labelLastPacket.AutoSize = true;
            this.labelLastPacket.Location = new System.Drawing.Point(606, 57);
            this.labelLastPacket.Name = "labelLastPacket";
            this.labelLastPacket.Size = new System.Drawing.Size(0, 13);
            this.labelLastPacket.TabIndex = 16;
            // 
            // ServerList
            // 
            this.ServerList.HideSelection = false;
            this.ServerList.Location = new System.Drawing.Point(295, 73);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(114, 316);
            this.ServerList.TabIndex = 9;
            this.ServerList.UseCompatibleStateImageBehavior = false;
            this.ServerList.View = System.Windows.Forms.View.SmallIcon;
            this.ServerList.Click += new System.EventHandler(this.ServerList_Click);
            // 
            // ClientList
            // 
            this.ClientList.HideSelection = false;
            this.ClientList.Location = new System.Drawing.Point(157, 73);
            this.ClientList.Name = "ClientList";
            this.ClientList.Size = new System.Drawing.Size(114, 316);
            this.ClientList.TabIndex = 8;
            this.ClientList.UseCompatibleStateImageBehavior = false;
            this.ClientList.View = System.Windows.Forms.View.SmallIcon;
            this.ClientList.Click += new System.EventHandler(this.ClientList_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 450);
            this.Controls.Add(this.labelLastPacket);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DataString);
            this.Controls.Add(this.AutoScrollServer);
            this.Controls.Add(this.AutoScrollClient);
            this.Controls.Add(this.ServerList);
            this.Controls.Add(this.ClientList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataBox);
            this.Controls.Add(this.start);
            this.Name = "Form1";
            this.Text = "WakSniffer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.RichTextBox dataBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Utils.MyListView ClientList;
        private Utils.MyListView ServerList;
        private System.Windows.Forms.CheckBox AutoScrollClient;
        private System.Windows.Forms.CheckBox AutoScrollServer;
        private System.Windows.Forms.RichTextBox DataString;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelLastPacket;
    }
}

