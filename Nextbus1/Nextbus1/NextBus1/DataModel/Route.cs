using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Nextbus1.Datamodel
{
    public class Route
    {
        //<route tag="F" title="F-Market & Wharves"/>

        public string tag, title, shortTitle;
        
        //constructor
        public Route (XmlElement XMLNode)
        {
        if (XMLNode.Attributes["tag"] != null) tag = XMLNode.Attributes["tag"].Value;
        if (XMLNode.Attributes["title"] != null) title = XMLNode.Attributes["title"].Value;
        if (XMLNode.Attributes["shortTitle"] != null) shortTitle = XMLNode.Attributes["shortTitle"].Value;
        }


        ////to display Route name
        //public override string ToString()
        //{
        //    return title;
        //}


        //http://webservices.nextbus.com/service/publicXMLFeed?command=routeConfig&a=sf-muni&r= +""XMLNode.Attributes[tag]"
    }
}
