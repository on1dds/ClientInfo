using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.DirectoryServices.ActiveDirectory;
using System.Threading;

namespace ClientInfo
{
    public partial class ClientInfo : Form
    {
        CInfo Client = new CInfo();
        int padding = 14;
    
        public ClientInfo()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Client.ready = false;
            ThreadStart childref = new ThreadStart(getInfo_thread);
            Thread childThread = new Thread(childref);
            childThread.Start();

        }

        private void Icon_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                showWindow();
                getInfo();
                WindowState = FormWindowState.Normal;
                Show();
            } else {
                Hide();
                WindowState = FormWindowState.Minimized;
            }
        }

        private string getDomain()
        {
            try {
                return Domain.GetComputerDomain().ToString() ;
            }
            catch
            {
                return null;
            }
        }

        private bool canConnect(string s)
        {
            try
            {
                return new Ping().Send(s).Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        private void getInfo()
        {
            if (Client.ready == true)
            {
                showWindow();

                // start new update thread
                Client.ready = false;
                ThreadStart childref = new ThreadStart(getInfo_thread);
                Thread childThread = new Thread(childref);
                childThread.Start();

            }
        }
        private void getInfo_thread()
        {
            get_basicinfo();
            get_netinfo();

            Client.ready = true;
        }
        private void get_basicinfo()
        {
            // environment variables
            Client.UserName.Set(Environment.ExpandEnvironmentVariables("%USERNAME%"));
            Client.ComputerName.Set(Environment.ExpandEnvironmentVariables("%COMPUTERNAME%"));
            Client.ClientName.Set(Environment.ExpandEnvironmentVariables("%CLIENTNAME%"));
            Client.Session.Set(Environment.ExpandEnvironmentVariables("%SESSIONNAME%"));
            if (Client.ClientName.text == null)
                Client.ClientName.Set(Client.ComputerName.text);

            // domain controller
            Client.LogonServer.Set(Environment.ExpandEnvironmentVariables("%LOGONSERVER%"));
            while (Client.LogonServer.text.Substring(0, 1) == "\\")
                Client.LogonServer.text = Client.LogonServer.text.Substring(1);

        }
        private void get_netinfo()
        {
            // internet connection
            Client.NetConnect.isValid = canConnect("www.google.be");
            Client.NetConnect.Set(Client.NetConnect.isValid == true ? "ja" : "nee");

            // domain connection
            Client.DomainName.Set(getDomain() == null ? "nee" : getDomain());
            Client.DcConnect.Set(canConnect(Client.LogonServer.text + "." + Client.DomainName.text) == true ? "ja" : "nee");


            Client.Ip.Clear();

            if (Client.NetConnect.isValid == true)
            {
                if (Client.ClientName.text != Client.ComputerName.text)
                {
                    Client.ComputerName.Key = "server";
                    // client is a terminal
                    try
                    {
                        int i = 1;
                        IPAddress[] localIPs = Dns.GetHostAddresses(Client.ClientName.text);
                        foreach (IPAddress ip in localIPs)
                        {
                            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                Dict ipaddress = new Dict("Ip Adres");
                                ipaddress.text = ip.ToString();
                                ipaddress.Key = "Ip #" + i.ToString();
                                i++;
                                // bereken hier interface naam


                                // tot hier
                                Client.Ip.Add(ipaddress);
                            }
                        }
                    }
                    catch (System.Net.Sockets.SocketException) { }
                }

                else
                {
                    // client is a computer
                    try
                    {
                        IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                            if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                        if (!ip.Address.ToString().StartsWith("169.254."))
                                        {
                                            Dict ipaddress = new Dict(("IP " + ni.Name.Split(' ')[0]).Trim());
                                            ipaddress.text = ip.Address.ToString();
                                            Client.Ip.Add(ipaddress);
                                        }
                    }
                    catch (System.Net.Sockets.SocketException) { }
                }
            }
        }

        private void updateInfo_tick(object sender, EventArgs e)
        {
            if(FormWindowState.Normal == WindowState)
                getInfo();
        }

        private void showWindow()
        {
            lstInfo.Items.Clear();
            //lstInfo.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
            lstInfo.Items.Add(Client.UserName.ToString(padding));
            lstInfo.Items.Add(Client.ClientName.ToString(padding));

            if (Client.ComputerName.text != Client.ClientName.text)
            {
                lstInfo.Items.Add(Client.ComputerName.ToString(padding));
                lstInfo.Items.Add(Client.Session.ToString(padding));
            }

            lstInfo.Items.Add(Client.NetConnect.ToString(padding));
            lstInfo.Items.Add(Client.DomainName.ToString(padding));
            lstInfo.Items.Add(Client.LogonServer.ToString(padding));
            lstInfo.Items.Add(Client.DcConnect.ToString(padding));

            if (Client.Ip != null)
                foreach (Dict ip in Client.Ip)
                    lstInfo.Items.Add(ip.ToString(padding));

            // Maak windows klaar
            int len = 0;
            foreach (string li in lstInfo.Items)                                    // bereken window breedte
                if (li.Length > len) len = li.Length;
            lstInfo.Width = len * 8 + 10;
            lstInfo.Height = lstInfo.ItemHeight * (lstInfo.Items.Count + 1);        // bereken window hoogte

            this.Width = lstInfo.Width;
            this.Height = lstInfo.Height;

            int x = Screen.PrimaryScreen.WorkingArea.Width - lstInfo.Width - 5;     // window positie   
            int y = Screen.PrimaryScreen.WorkingArea.Height - lstInfo.Height - 5;

            this.Location = new Point(x, y);
        }


    }
}
