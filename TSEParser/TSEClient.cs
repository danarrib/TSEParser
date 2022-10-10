using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TSEParser
{
    public class TSEWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            int itimeout = Convert.ToInt32(TimeSpan.FromSeconds(60).TotalMilliseconds);
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = itimeout;
            ((HttpWebRequest)w).ReadWriteTimeout = itimeout;
            return w;
        }
    }
}
