using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Nextbus
{
    public class Path
    {
        public List<Point> PointList;
        public Path() { }

        public Path(XElement xPath)
        {
            PointList = new List<Point>();
            foreach (XElement aPoint in xPath.Elements())
            {
                PointList.Add(new Point(aPoint));
            }
        }
    }

    public class Point
    {
        public double lat;
        public double lon;

        public Point() { }

        public Point(XElement xPoint)
        {
            lat = Convert.ToDouble(xPoint.Attribute("lat").Value);
            lon = Convert.ToDouble(xPoint.Attribute("lon").Value);
        }
    }
}
