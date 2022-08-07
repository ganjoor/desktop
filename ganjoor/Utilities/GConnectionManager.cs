using System;
using System.Net;
using ganjoor.Properties;

namespace ganjoor
{
    public class GConnectionManager
    {
        public static bool ConfigureProxy(ref WebRequest req)
        {
            if (!string.IsNullOrEmpty(Settings.Default.HttpProxyServer) && !string.IsNullOrEmpty(Settings.Default.HttpProxyPort))
            {
                var port = Convert.ToInt32(Settings.Default.HttpProxyPort);//try?!
                var proxy = new WebProxy(Settings.Default.HttpProxyServer, port);
                req.Proxy = proxy;
                return true;
            }
            return true;
        }
    }
}
