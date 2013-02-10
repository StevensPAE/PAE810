using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nextbus
{
    public class NextBus
    {
        //attributes

        public bool agenciesInit = false;
        public List<Agency> agencyList;


        //methods
        public bool populateAgencyList()
        {
            try
            {

                // Create the web request  
                HttpWebRequest myRequest = WebRequest.Create("http://webservices.nextbus.com/service/publicXMLFeed?command=agencyList") as HttpWebRequest;


                WebResponse myResponse = myRequest.GetResponse(); //responce in a format that needs to be converted

                Stream myStream = myResponse.GetResponseStream();// converts to string

                StreamReader myStreamreader = new StreamReader(myStream);// read all the data

                string myStreamResponse = myStreamreader.ReadToEnd();

                XmlDocument myXMLDoc = new XmlDocument();// the xml document that can be understood

                myXMLDoc.LoadXml(myStreamResponse);// method that populates the xml document with the responce from the stream

                XmlElement myDocElement = myXMLDoc.DocumentElement;//document element is a specific portion of the xml document

                agencyList = new List<Agency>();//resetting the agencylist, we are creating the list, and if it had been created we are resetting it, so as the result it is an 
                //empty list at the moment

                foreach (XmlElement childNode in myDocElement.ChildNodes)// looping through the document and going through the child nodes 
                {
                    Agency myAgency = new Agency(childNode);// the childnodes reoresent information for a particular agency, so need to loop through all the childnodes in this case 
                    //agencies and pull specific information from them... which is done inside of the my agency class constructor

                    agencyList.Add(myAgency);// populated this list with all the agency infomration... in this case it contains 4 columns tag title 

                }

                agenciesInit = true;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
