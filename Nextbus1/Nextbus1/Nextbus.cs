using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace Nextbus
{
    class Nextbus
    {
        //attributes:
        public List<Agency> agencyList;



        //methods:
        public bool populateAgencyList()
        {
            agencyList = new List<Agency>();

            try
            {
                // Create the web request  
                HttpWebRequest myRequest = WebRequest.Create("http://webservices.nextbus.com/service/publicXMLFeed?command=agencyList") as HttpWebRequest;
                WebResponse myResponse = myRequest.GetResponse();
                Stream myStream = myResponse.GetResponseStream();
                StreamReader myStreamreader = new StreamReader(myStream);
                string myStreamResponse = myStreamreader.ReadToEnd();
                XmlDocument myXMLDoc = new XmlDocument();
                myXMLDoc.LoadXml(myStreamResponse);
                XmlElement myDocElement = myXMLDoc.DocumentElement;

                foreach (XmlElement childNode in myDocElement.ChildNodes)
                {
                    //Console.WriteLine(childNode.Attributes["title"].Value);
                    Agency myAgency = new Agency(childNode);
                    agencyList.Add(myAgency);
                }
                return true;
            }

            catch (Exception)
            {

                return false;
            }
        }
    }
}
