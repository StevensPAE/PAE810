using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Nextbus
{
    public class Route
    {
        public string tag, title;
        public string color;
        public List<Path> PathList;
        public double latMin, latMax, lonMin, lonMax;



        public Route(XmlElement xmlRoute)
        {

            if (xmlRoute.Attributes["tag"] != null) tag = xmlRoute.Attributes["tag"].Value;
            if (xmlRoute.Attributes["title"] != null) title = xmlRoute.Attributes["title"].Value;
            if (xmlRoute.Attributes["color"] != null) color = xmlRoute.Attributes["color"].Value;

            if (xmlRoute.Attributes["latMin"] != null) latMin = Convert.ToDouble(xmlRoute.Attributes["latMin"].Value);
            if (xmlRoute.Attributes["latMax"] != null) latMax = Convert.ToDouble(xmlRoute.Attributes["latMax"].Value);
            if (xmlRoute.Attributes["lonMin"] != null) lonMin = Convert.ToDouble(xmlRoute.Attributes["lonMin"].Value);
            if (xmlRoute.Attributes["lonMax"] != null) lonMax = Convert.ToDouble(xmlRoute.Attributes["lonMax"].Value);


            List<XmlElement> xmlStops = new List<XmlElement>();
            List<XmlElement> xmlDirections = new List<XmlElement>();
            List<XmlElement> xmlPaths = new List<XmlElement>();

            foreach (XmlElement aElement in xmlRoute.ChildNodes)
            {
                switch (aElement.Name)
                {
                    case "stop":
                        {
                            xmlStops.Add(aElement);
                            break;
                        }
                    case "direction":
                        {
                            xmlDirections.Add(aElement);
                            break;
                        }
                    case "path":
                        {
                            xmlPaths.Add(aElement);
                            break;
                        }
                    default:
                        break;
                }
            }

        }

        public Route(XElement xRoute)
        {
            // the route contains stops, directions and paths
            IEnumerable<XElement> xStops = xRoute.Elements().Where(x => x.Name == "stop");
            IEnumerable<XElement> xDirections = xRoute.Elements().Where(x => x.Name == "direction");
            IEnumerable<XElement> xPaths = xRoute.Elements().Where(x => x.Name == "path");

            PathList = new List<Path>();
            foreach (XElement aPath in xPaths)
            {
                PathList.Add(new Path(aPath));
            }
        }
    }
}
