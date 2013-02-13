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
        public double latMin, latMax, lonMin, lonMax;
        public List<Path> PathList;

        public Route(XmlElement xmlRoute)
        {
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
