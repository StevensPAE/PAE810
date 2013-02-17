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
    public class Agency
    {
        //<agency tag="charles-river" title="Charles River TMA - EZRide" shortTitle="Charles River EZRide" regionTitle="Massachusetts"/>

        public string tag, title, shortTitle, regionTitle;
        public List<Route> busRoutes = new List<Route>(); // the list of bus Routes to be return end of this class. 
        
        // properties
        public List<string> busRouteTitles { get; set; }

        //constructor
        public Agency(XmlElement XMLNode)
        {
        if (XMLNode.Attributes["tag"] != null) tag = XMLNode.Attributes["tag"].Value;
        if (XMLNode.Attributes["title"] != null) title = XMLNode.Attributes["title"].Value;
        if (XMLNode.Attributes["shortTitle"] != null) shortTitle = XMLNode.Attributes["shortTitle"].Value;
        if (XMLNode.Attributes["regionTitle"] != null) regionTitle = XMLNode.Attributes["regionTitle"].Value;
        }

        public Agency(string Title)
        {
            title = Title;
            getBusRoutes(); //this method will return list<Route> for an agency 
        }


        //to display Agency name
        public override string ToString()
        {
            return title;
        }


        //method get BusRoutes
        public void getBusRoutes()
        {
            ////http://webservices.nextbus.com/service/publicXMLFeed?command=routeList&a= +"tag" ADD AGENCY TAG HERE IMPORTANT

            List<Route> busRoutes = new List<Route>();
            busRouteTitles = new List<string>();

            XmlElement XmlAgencies = new NBHelper().NextBusWebRequest("http://webservices.nextbus.com/service/publicXMLFeed?command=routeList&a=" + tag);

            foreach (XmlElement childNode in XmlAgencies.ChildNodes)
            {
                Route myRoute = new Route(childNode);
                busRoutes.Add(myRoute);
                //Console.WriteLine(childNode.Attributes["title"].Value);
                busRouteTitles.Add(myRoute.title);
            }
        }



        
    }


}
