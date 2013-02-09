using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace Nextbus1
{
    class Program
    {
        static void Main(string[] args)
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
            List<Agency> myAgencies = new List<Agency>();

            foreach (XmlElement childNode in myDocElement.ChildNodes)
            {
                foreach (XmlAttribute myAtt in childNode.Attributes)
                {
                    Console.WriteLine(myAtt.Name + " my AttName | " + myAtt.Value + " my AttValue");
                   
                }
                Console.WriteLine();
                //Console.WriteLine(childNode.Attributes["title"].Value);
                Agency myAgency = new Agency(childNode);
                myAgencies.Add(myAgency);
            }

            Console.WriteLine(myAgencies.Count + " agencies loaded");
            Console.ReadLine();
        }
    }
}