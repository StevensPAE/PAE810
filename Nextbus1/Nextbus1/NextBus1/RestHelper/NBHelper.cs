using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;

namespace Nextbus1.RestHelper
{
    public class NBHelper
    {
        public XmlElement NextBusWebRequest(string command)
        {
            HttpWebRequest myRequest = WebRequest.Create(command) as HttpWebRequest;
            WebResponse myResponse = myRequest.GetResponse();
            Stream myStream = myResponse.GetResponseStream();
            StreamReader myStreamreader = new StreamReader(myStream);
            string myStreamResponse = myStreamreader.ReadToEnd();
            XmlDocument myXMLDoc = new XmlDocument();
            myXMLDoc.LoadXml(myStreamResponse);
            XmlElement myDocElement3 = myXMLDoc.DocumentElement;

            return myXMLDoc.DocumentElement;
        }
    }
}
