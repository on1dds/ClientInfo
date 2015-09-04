using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientInfo
{
    public class Dict : object
    {
        public string Key;
        public bool isValid;
        public string text;
     
        public void Set(string s)
        {
            if (!(s.StartsWith("%") && s.EndsWith("%")))
                text = s;
            else
                text = null;
        }

        public Dict(string k)
        {
            Key = k;
        }

        public string ToString(int pad)
        {
            return Key.PadRight(pad) + text;
        }

    }
    public class CInfo
    {
        public bool ready = false;
        public Dict UserName= new Dict("gebruiker");
        public Dict ComputerName = new Dict("host");
        public Dict ClientName = new Dict("client");
        public Dict Session = new Dict("session");
        public Dict LogonServer = new Dict("dc");
        public Dict NetConnect = new Dict("internet");
        public Dict DomainName = new Dict("domeinnaam");
        public Dict DcConnect = new Dict("bereikbaar");
        public List<Dict> Dns = new List<Dict>();
        public List<Dict> Ip = new List<Dict>();
    }
}
