using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientInfo
{
    public partial class ClientInfo : Form
    {
        public ClientInfo()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            CInfo Client = new CInfo();
            Client.clientname = Environment.ExpandEnvironmentVariables("%COMPUTERNAME%");
            Client.logonserver = Environment.ExpandEnvironmentVariables("%LOGONSERVER%");
            Client.username = Environment.ExpandEnvironmentVariables("%USERNAME%");

            if (Client.clientname.Length > 10 && Client.clientname.Substring(0,10).ToLower() == "srv-citrix")
            {
                Client.tsserver = Client.clientname;
                Client.clientname = Environment.ExpandEnvironmentVariables("%CLIENTNAME%");
                Client.session = Environment.ExpandEnvironmentVariables("%SESSIONNAME%");
            }

            Client.clientip = IPToString(System.Net.Dns.Resolve(Client.clientname).AddressList[0].Address);

            lstInfo.Items.Add("client   " + Client.clientname);
            lstInfo.Items.Add("ip       " + Client.clientip);
            if (Client.tsserver != null)
            {
                lstInfo.Items.Add("server   " + Client.tsserver);
                lstInfo.Items.Add("session  " + Client.session);
            }
            lstInfo.Items.Add("logon    " + Client.logonserver);
            lstInfo.Items.Add("user     " + Client.username);
            

            int len = 0;
            foreach (string li in lstInfo.Items)
                if (li.Length > len) len = li.Length;
            lstInfo.Width = len * 8+10;
            lstInfo.Height = lstInfo.ItemHeight * (lstInfo.Items.Count+1);
            this.Width = lstInfo.Width;
            this.Height = lstInfo.Height;

            lstInfo.Top = 0;
            lstInfo.Left = 0;

            //this.Height = lstInfo.Height;

            // Maak windows klaar
            int x = Screen.PrimaryScreen.WorkingArea.Right - (this.Width + 5);
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - (this.Height + 5);
            this.Location = new Point(x, y);
        }

        private string IPToString(long ip)
        {
            string s="";
            while(ip > 0)
            {
                s =  s + "." + (ip % 256).ToString() ;
                ip = Math.Abs(ip / 256);
            }
            return s.Substring(1);
        }

        private void Icon_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            } else {
                Hide();
                WindowState = FormWindowState.Minimized;
            }
        }
    }
}
