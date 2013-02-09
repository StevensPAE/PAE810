using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Nextbus1
{
    public class Agency
    {
        //<agency tag="charles-river" title="Charles River TMA - EZRide" shortTitle="Charles River EZRide" regionTitle="Massachusetts"/>

        public string tag, title, shortTitle, regionTitle;
        public List<Route> busRoutes;
        
        //constructor
        public Agency(XmlElement XMLNode)
        {
            if (XMLNode.Attributes["tag"] != null) tag = XMLNode.Attributes["tag"].Value;
            if (XMLNode.Attributes["title"] != null) title = XMLNode.Attributes["title"].Value;
            if (XMLNode.Attributes["shortTitle"] != null) shortTitle = XMLNode.Attributes["shortTitle"].Value;
            if (XMLNode.Attributes["regionTitle"] != null) regionTitle = XMLNode.Attributes["regionTitle"].Value;
            //hello change test
        }
    }


}
