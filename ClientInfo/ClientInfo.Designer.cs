namespace ClientInfo
{
    partial class ClientInfo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientInfo));
            this.nfy_clientinfo = new System.Windows.Forms.NotifyIcon(this.components);
            this.lstInfo = new System.Windows.Forms.ListBox();
            this.updateInfo = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // nfy_clientinfo
            // 
            resources.ApplyResources(this.nfy_clientinfo, "nfy_clientinfo");
            this.nfy_clientinfo.Click += new System.EventHandler(this.Icon_Click);
            // 
            // lstInfo
            // 
            resources.ApplyResources(this.lstInfo, "lstInfo");
            this.lstInfo.FormattingEnabled = true;
            this.lstInfo.Name = "lstInfo";
            this.lstInfo.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstInfo.Click += new System.EventHandler(this.Icon_Click);
            // 
            // updateInfo
            // 
            this.updateInfo.Enabled = true;
            this.updateInfo.Interval = 1000;
            this.updateInfo.Tick += new System.EventHandler(this.updateInfo_tick);
            // 
            // ClientInfo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientInfo";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon nfy_clientinfo;
        private System.Windows.Forms.ListBox lstInfo;
        private System.Windows.Forms.Timer updateInfo;
    }
}

