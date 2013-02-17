using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using Nextbus1.RestHelper;

namespace Nextbus1.Datamodel
{
    public class NextBus
    {
        // attribute
        public List<Agency> AgencyList;
        //private bool agenciesLoaded = false;


        //constructors
        public NextBus() // should be same name of the class
        {
            AgencyList = new List<Agency>();
            // XmlElement XmlAgencies = NextBusWebRequest("http://webservices.nextbus.com/service/publicXMLFeed?command=agencyList");
            XmlElement XmlAgencies = new NBHelper().NextBusWebRequest("http://webservices.nextbus.com/service/publicXMLFeed?command=agencyList");
            foreach (XmlElement a in XmlAgencies.ChildNodes)
            {
                //Create new Agency
                AgencyList.Add(new Agency(a));
            }
        }

        //constructor
        public NextBus(bool loadAgencies)
        {
            if (loadAgencies) LoadAgencies();
        }


        //methods
        public void LoadAgencies()
        {
            AgencyList = new List<Agency>();
            XmlElement XmlAgencies = new NBHelper().NextBusWebRequest("http://webservices.nextbus.com/service/publicXMLFeed?command=agencyList");
            foreach (XmlElement a in XmlAgencies.ChildNodes)
            {
                //Create new Agency
                AgencyList.Add(new Agency(a));
            }
        }
    }
}
