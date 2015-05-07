namespace SDK.Test.WinForm
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Connect = new System.Windows.Forms.Button();
            this.txb_clientId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bnt_quitConversation = new System.Windows.Forms.Button();
            this.txb_memberId = new System.Windows.Forms.TextBox();
            this.btn_removeMember = new System.Windows.Forms.Button();
            this.btn_addMember = new System.Windows.Forms.Button();
            this.lbx_history = new System.Windows.Forms.ListBox();
            this.lbx_conversations = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_createConversation = new System.Windows.Forms.Button();
            this.txb_friendId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbx_members = new System.Windows.Forms.ListBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.txb_input = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txb_log = new System.Windows.Forms.TextBox();
            this.btn_clearLog = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.brn_queryHistory = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(394, 16);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_Connect.TabIndex = 0;
            this.btn_Connect.Text = "登陆";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // txb_clientId
            // 
            this.txb_clientId.Location = new System.Drawing.Point(64, 16);
            this.txb_clientId.Name = "txb_clientId";
            this.txb_clientId.Size = new System.Drawing.Size(324, 20);
            this.txb_clientId.TabIndex = 1;
            this.txb_clientId.Text = "A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Client Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "  日志：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.brn_queryHistory);
            this.panel1.Controls.Add(this.bnt_quitConversation);
            this.panel1.Controls.Add(this.txb_memberId);
            this.panel1.Controls.Add(this.btn_removeMember);
            this.panel1.Controls.Add(this.btn_addMember);
            this.panel1.Controls.Add(this.lbx_history);
            this.panel1.Controls.Add(this.lbx_conversations);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btn_createConversation);
            this.panel1.Controls.Add(this.txb_friendId);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lbx_members);
            this.panel1.Controls.Add(this.btn_send);
            this.panel1.Controls.Add(this.txb_input);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txb_clientId);
            this.panel1.Controls.Add(this.btn_Connect);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(22, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(685, 553);
            this.panel1.TabIndex = 4;
            // 
            // bnt_quitConversation
            // 
            this.bnt_quitConversation.Location = new System.Drawing.Point(476, 502);
            this.bnt_quitConversation.Name = "bnt_quitConversation";
            this.bnt_quitConversation.Size = new System.Drawing.Size(75, 23);
            this.bnt_quitConversation.TabIndex = 19;
            this.bnt_quitConversation.Text = "退出对话";
            this.bnt_quitConversation.UseVisualStyleBackColor = true;
            this.bnt_quitConversation.Click += new System.EventHandler(this.bnt_quitConversation_Click);
            // 
            // txb_memberId
            // 
            this.txb_memberId.Location = new System.Drawing.Point(561, 118);
            this.txb_memberId.Name = "txb_memberId";
            this.txb_memberId.Size = new System.Drawing.Size(100, 20);
            this.txb_memberId.TabIndex = 18;
            this.txb_memberId.Text = "Tom";
            // 
            // btn_removeMember
            // 
            this.btn_removeMember.Location = new System.Drawing.Point(475, 155);
            this.btn_removeMember.Name = "btn_removeMember";
            this.btn_removeMember.Size = new System.Drawing.Size(80, 23);
            this.btn_removeMember.TabIndex = 17;
            this.btn_removeMember.Text = "剔除成员";
            this.btn_removeMember.UseVisualStyleBackColor = true;
            this.btn_removeMember.Click += new System.EventHandler(this.btn_removeMember_Click);
            // 
            // btn_addMember
            // 
            this.btn_addMember.Location = new System.Drawing.Point(475, 115);
            this.btn_addMember.Name = "btn_addMember";
            this.btn_addMember.Size = new System.Drawing.Size(80, 25);
            this.btn_addMember.TabIndex = 16;
            this.btn_addMember.Text = "添加成员";
            this.btn_addMember.UseVisualStyleBackColor = true;
            this.btn_addMember.Click += new System.EventHandler(this.btn_addMember_Click);
            // 
            // lbx_history
            // 
            this.lbx_history.FormattingEnabled = true;
            this.lbx_history.Location = new System.Drawing.Point(116, 113);
            this.lbx_history.Name = "lbx_history";
            this.lbx_history.Size = new System.Drawing.Size(272, 277);
            this.lbx_history.TabIndex = 15;
            // 
            // lbx_conversations
            // 
            this.lbx_conversations.FormattingEnabled = true;
            this.lbx_conversations.Location = new System.Drawing.Point(16, 115);
            this.lbx_conversations.Name = "lbx_conversations";
            this.lbx_conversations.Size = new System.Drawing.Size(94, 407);
            this.lbx_conversations.TabIndex = 14;
            this.lbx_conversations.SelectedIndexChanged += new System.EventHandler(this.lbx_conversations_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Friend Id";
            // 
            // btn_createConversation
            // 
            this.btn_createConversation.Location = new System.Drawing.Point(394, 52);
            this.btn_createConversation.Name = "btn_createConversation";
            this.btn_createConversation.Size = new System.Drawing.Size(75, 23);
            this.btn_createConversation.TabIndex = 12;
            this.btn_createConversation.Text = "创建对话";
            this.btn_createConversation.UseVisualStyleBackColor = true;
            this.btn_createConversation.Click += new System.EventHandler(this.btn_createConversation_Click);
            // 
            // txb_friendId
            // 
            this.txb_friendId.Location = new System.Drawing.Point(64, 55);
            this.txb_friendId.Name = "txb_friendId";
            this.txb_friendId.Size = new System.Drawing.Size(324, 20);
            this.txb_friendId.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(413, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "成员";
            // 
            // lbx_members
            // 
            this.lbx_members.FormattingEnabled = true;
            this.lbx_members.Location = new System.Drawing.Point(394, 115);
            this.lbx_members.Name = "lbx_members";
            this.lbx_members.Size = new System.Drawing.Size(75, 303);
            this.lbx_members.TabIndex = 9;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(113, 503);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(356, 23);
            this.btn_send.TabIndex = 8;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // txb_input
            // 
            this.txb_input.Location = new System.Drawing.Point(113, 434);
            this.txb_input.Multiline = true;
            this.txb_input.Name = "txb_input";
            this.txb_input.Size = new System.Drawing.Size(356, 63);
            this.txb_input.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(222, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "聊天记录";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "对话";
            // 
            // txb_log
            // 
            this.txb_log.Location = new System.Drawing.Point(69, 21);
            this.txb_log.Multiline = true;
            this.txb_log.Name = "txb_log";
            this.txb_log.Size = new System.Drawing.Size(379, 476);
            this.txb_log.TabIndex = 5;
            // 
            // btn_clearLog
            // 
            this.btn_clearLog.Location = new System.Drawing.Point(69, 503);
            this.btn_clearLog.Name = "btn_clearLog";
            this.btn_clearLog.Size = new System.Drawing.Size(379, 23);
            this.btn_clearLog.TabIndex = 6;
            this.btn_clearLog.Text = "清空日志";
            this.btn_clearLog.UseVisualStyleBackColor = true;
            this.btn_clearLog.Click += new System.EventHandler(this.btn_clearLog_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txb_log);
            this.panel2.Controls.Add(this.btn_clearLog);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(787, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(477, 553);
            this.panel2.TabIndex = 7;
            // 
            // brn_queryHistory
            // 
            this.brn_queryHistory.Location = new System.Drawing.Point(116, 393);
            this.brn_queryHistory.Name = "brn_queryHistory";
            this.brn_queryHistory.Size = new System.Drawing.Size(98, 23);
            this.brn_queryHistory.TabIndex = 20;
            this.brn_queryHistory.Text = "获取聊天记录";
            this.brn_queryHistory.UseVisualStyleBackColor = true;
            this.brn_queryHistory.Click += new System.EventHandler(this.brn_queryHistory_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 635);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TextBox txb_clientId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txb_log;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox txb_input;
        private System.Windows.Forms.Button btn_clearLog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lbx_members;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txb_friendId;
        private System.Windows.Forms.Button btn_createConversation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lbx_conversations;
        private System.Windows.Forms.ListBox lbx_history;
        private System.Windows.Forms.Button btn_addMember;
        private System.Windows.Forms.Button btn_removeMember;
        private System.Windows.Forms.TextBox txb_memberId;
        private System.Windows.Forms.Button bnt_quitConversation;
        private System.Windows.Forms.Button brn_queryHistory;
    }
}

