using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharePointConnect;


namespace UnitTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine(UnitTest1());
            Console.ReadLine();
        }

        private static string UnitTest1()
        {
            string myURL, myUser, myPassword;
            myURL = "https://partners.myskanska.com/usa/teams/sit/hit/_vti_bin/lists.asmx";
            myUser = "j.schumacher";
            //myPassword = "Password1234";

            Console.Write("Enter Password: ");
            myPassword = "";
            ConsoleKeyInfo keystroke=new ConsoleKeyInfo();
            bool keepGoing = true;
            while (keepGoing)
            {
                keystroke = Console.ReadKey(true);
                switch (keystroke.Key)
                {
                    case ConsoleKey.Backspace:
                        {
                            //in leu of taking back from the paswordstring
                            Console.Write("BACKSPACE");
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            keepGoing = false;
                            Console.WriteLine();
                            break;
                        }
                    default:
                        {
                            myPassword += keystroke.KeyChar;
                            Console.Write("*");
                            break;
                        }
                }
            }

            SharePointConnector mytestConnection = new SharePointConnector(myURL,myUser,myPassword);
            if (mytestConnection.connectToSP() == true)
            {
                //Console.WriteLine("Hurray");
                return "UnitTest1 = SUCCESS";
            }
            else
            {
                //Console.WriteLine("Nooooooo");
                return "UnitTest1 = FAILURE";
            }

           
        }
    }
}
