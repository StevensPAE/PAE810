using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Nextbus;
using System.Xml.Linq;

namespace NextbusTester
{
    class Program
    {
        static void Main(string[] args)
        {

            Nextbus.Nextbus myNextbusInstance = new Nextbus.Nextbus();

            UnitTest1(myNextbusInstance);

            Console.WriteLine();
            Console.WriteLine();

            UnitTest2(myNextbusInstance);

            int intAgency, intRoute;

            intAgency = 0;
            intRoute = 0;

            //unit test 3:
            UnitTest3(myNextbusInstance, intAgency,intRoute);

            Console.ReadLine();

        }

        private static void UnitTest2(Nextbus.Nextbus myNextbusInstance)
        {
            Console.WriteLine("GETTING ROUTES");
            if (myNextbusInstance.populateRouteList() == true)
            {
                foreach (Agency aObject in myNextbusInstance.agencyList)
                {
                    Console.WriteLine(aObject.busRoutes[0].tag.ToString());
                }
            }
            else Console.WriteLine("Could not connect. Check your Internet connection");
        }

        private static void UnitTest1(Nextbus.Nextbus myNextbusInstance)
        {
            
            Console.WriteLine("GETTING AGENCIES");
            if (myNextbusInstance.populateAgencyList() == true)
            {
                foreach (Agency aObject in myNextbusInstance.agencyList)
                {
                    Console.WriteLine(aObject.tag);
                }
            }
            else Console.WriteLine("Could not connect. Check your Internet connection");
        }

        private static void UnitTest3(Nextbus.Nextbus BusInstance, int ANum, int RNum)
        {
            Console.WriteLine();
            Console.WriteLine("PRINTING COORDINATES OF FIRST PATH OF TEST ROUTE");
            if (BusInstance.getRouteAttributes(ANum, RNum) == true)
            {
                //to see if this worked, extract first path and write line all of it's points
                Console.WriteLine();
                Route myRoute =  BusInstance.agencyList[ANum].busRoutes[RNum];

                foreach (Point pt in myRoute.PathList[0].PointList)
                {
                    Console.WriteLine("lat: " + pt.lat + "  |  lon: " + pt.lon); 
                }
            }
            else Console.WriteLine("/nCould not get bus attributes");
        }
    }
}
