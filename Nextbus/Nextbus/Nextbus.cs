using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace Nextbus
{
    public class Nextbus
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
                XmlElement myDocElement = webRequest("http://webservices.nextbus.com/service/publicXMLFeed?command=agencyList");

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

        public bool populateRouteList()
        {
            try
            {
                //for every agency:
                foreach (Agency myAgency in agencyList)
                {
                    //send a request with specific agency tag: http://webservices.nextbus.com/service/publicXMLFeed?command=routeList&a=sf-muni

                    //create request to be sent
                    string myCommand = "http://webservices.nextbus.com/service/publicXMLFeed?command=routeList&a=" + myAgency.tag;

                    //create new routelist of that agency and populate it
                    List<Route> myRouteList = new List<Route>();

                    // Create the web request  
                    XmlElement myDocElement = webRequest(myCommand);

                    foreach (XmlElement childNode in myDocElement.ChildNodes)
                    {
                        //Console.WriteLine(childNode.Attributes["title"].Value);
                        Route myRoute = new Route(childNode);
                        myRouteList.Add(myRoute);
                    }
                    myAgency.busRoutes = myRouteList;
                }
                return true;
            }
              
            catch (Exception)
            {

                return false;
            }
        }


        public bool getRouteAttributes(string AgencyTag, string RouteTag)
        {
            try
            {
                ////send a request with specific agency & route tag: http://webservices.nextbus.com/service/publicXMLFeed?command=routeConfig&a=sf-muni&r=N

                //create request to be sent

                //string A = agencyList[AgencyNum].tag;
                //string R = agencyList[AgencyNum].busRoutes[RouteNum].tag;
                string myCommand = "http://webservices.nextbus.com/service/publicXMLFeed?command=routeConfig&a=" + AgencyTag+ "&r=" + RouteTag;
                XmlElement RouteAtts = webRequest(myCommand);

                ////NOT USING RouteAttributes for now
                //agencyList[AgencyNum].busRoutes[RouteNum].RouteAttributes = RouteAtts;


                int RouteInt = 0, agencyInt = 0 , i = 0,j = 0;
                foreach (Agency a in agencyList)
                {
                    if (a.tag == AgencyTag) 
                    {
                        agencyInt = i;
                        //loop through bus routes
                        foreach(Route r in a.busRoutes)
                        {
                            if (r.tag == RouteTag)
                            {
                                RouteInt = j;
                            }
                            j++;
                        }
                    }
                    i++;
                }


                Route myRoute = agencyList[agencyInt].busRoutes[RouteInt];
                //loop through attributes
                foreach (XmlElement XMLNode in RouteAtts.ChildNodes)
                {
                    if (XMLNode.Attributes["color"] != null) myRoute.color = XMLNode.Attributes["color"].Value;
                    if (XMLNode.Attributes["latMin"] != null) myRoute.latMin = Convert.ToDouble(XMLNode.Attributes["latMin"].Value);
                    if (XMLNode.Attributes["latMax"] != null) myRoute.latMax = Convert.ToDouble(XMLNode.Attributes["latMax"].Value);
                    if (XMLNode.Attributes["lonMin"] != null) myRoute.lonMin = Convert.ToDouble(XMLNode.Attributes["lonMin"].Value);
                    if (XMLNode.Attributes["lonMax"] != null) myRoute.lonMax = Convert.ToDouble(XMLNode.Attributes["lonMax"].Value);

                    List<Path> myPathList = new List<Path>();
                    //loop through childnode of childnode to get paths
                    foreach (XmlElement ChildChildNode in XMLNode.ChildNodes)
                    {
                        if (ChildChildNode.Name == "path")
                        {
                            //loop though points of that path and create a path
                            List<Point> myPointList = new List<Point>();
                            foreach (XmlElement PathPt in ChildChildNode.ChildNodes)
                            {
                                Point myPt = new Point();
                                if (PathPt.Attributes["lat"] != null) myPt.lat = Convert.ToDouble(PathPt.Attributes["lat"].Value);
                                if (PathPt.Attributes["lon"] != null) myPt.lon = Convert.ToDouble(PathPt.Attributes["lon"].Value);
                                myPointList.Add(myPt);
                            }

                            Path myPath = new Path();
                            myPath.PointList = myPointList;
                            myPathList.Add(myPath);
                        }
                        myRoute.PathList = myPathList;
                    }
                }




                return true;
            }
            catch (Exception)
            {

                return false;

            }

        }

        public bool getRouteAttributes(int AgencyNum, int RouteNum)
        {
            try
            {
                ////send a request with specific agency & route tag: http://webservices.nextbus.com/service/publicXMLFeed?command=routeConfig&a=sf-muni&r=N

                //create request to be sent

                string A = agencyList[AgencyNum].tag;
                string R = agencyList[AgencyNum].busRoutes[RouteNum].tag;
                string myCommand = "http://webservices.nextbus.com/service/publicXMLFeed?command=routeConfig&a=" + A + "&r=" + R;
                XmlElement RouteAtts = webRequest(myCommand);
                
                ////NOT USING RouteAttributes for now
                //agencyList[AgencyNum].busRoutes[RouteNum].RouteAttributes = RouteAtts;

                Route myRoute = agencyList[AgencyNum].busRoutes[RouteNum];
                //loop through attributes
                foreach (XmlElement XMLNode in RouteAtts.ChildNodes)
                {
                    if (XMLNode.Attributes["color"] != null) myRoute.color = XMLNode.Attributes["color"].Value;
                    if (XMLNode.Attributes["latMin"] != null) myRoute.latMin = Convert.ToDouble(XMLNode.Attributes["latMin"].Value);
                    if (XMLNode.Attributes["latMax"] != null) myRoute.latMax = Convert.ToDouble(XMLNode.Attributes["latMax"].Value);
                    if (XMLNode.Attributes["lonMin"] != null) myRoute.lonMin = Convert.ToDouble(XMLNode.Attributes["lonMin"].Value);
                    if (XMLNode.Attributes["lonMax"] != null) myRoute.lonMax = Convert.ToDouble(XMLNode.Attributes["lonMax"].Value);

                    List<Path> myPathList = new List<Path>();
                    //loop through childnode of childnode to get paths
                    foreach (XmlElement ChildChildNode in XMLNode.ChildNodes)
                    {
                        if (ChildChildNode.Name == "path")
                        {
                            //loop though points of that path and create a path
                            List<Point> myPointList = new List<Point>();
                            foreach (XmlElement PathPt in ChildChildNode.ChildNodes)
                            {
                                Point myPt = new Point();
                                 if (PathPt.Attributes["lat"] != null) myPt.lat = Convert.ToDouble(PathPt.Attributes["lat"].Value);
                                 if (PathPt.Attributes["lon"] != null) myPt.lon = Convert.ToDouble(PathPt.Attributes["lon"].Value);
                                 myPointList.Add(myPt);
                            }

                            Path myPath = new Path();
                            myPath.PointList = myPointList;
                            myPathList.Add(myPath);
                        }
                        myRoute.PathList = myPathList;
                    }
                }
                    



                return true;
            }
            catch (Exception)
            {

                return false;
            
            }

        }

        private XmlElement webRequest (string command)
        {
            // Create the web request  
            HttpWebRequest myRequest = WebRequest.Create(command) as HttpWebRequest;
            WebResponse myResponse = myRequest.GetResponse();
            Stream myStream = myResponse.GetResponseStream();
            StreamReader myStreamreader = new StreamReader(myStream);
            string myStreamResponse = myStreamreader.ReadToEnd();
            XmlDocument myXMLDoc = new XmlDocument();
            myXMLDoc.LoadXml(myStreamResponse);
            return myXMLDoc.DocumentElement;
        }
    }
}