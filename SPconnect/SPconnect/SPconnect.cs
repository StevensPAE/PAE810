/*
 * CLASS NAME: SPConnect 
 * AUTHOR: Jonatan Schumacher
 * VERSION: v 0.1
 * DESCRIPTION:
 * This class contains methods to connect to the Skanska SIT/HIT SharePoint site.
 * 
 * ! ! ! ! ! ! ! ! I N S T R U C T I O N ! ! ! ! ! ! ! ! 
 * In order to use it (for NavisWorks, Revit, Grasshopper etc), one must always create a Service Reference to the Skanska Site. 
 * THE SERVICE REFERENCE MUST BE NAMED: SRef1
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Net;
using System.Xml;
using System.Windows.Forms;

namespace SPconnect
{
    public class SPConnect
    {
        ////PROPERTIES
        public string URL { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<XElement> SPLists{get; set;}
        public List<string> ListNames{get; set;}
        private SRef1.ListsSoapClient myClient;
        private bool init = false;
   

        ////CONSTRUCTOR
        public SPConnect(string url, string username, string password)        
        {
            //establish connection to SP site

            if (!init)
            {
                URL = url;
                Username = username;
                Password = password;

                if (!connectToSP(URL, Username, Password))
                {
                    Console.WriteLine("Could not connect to specified SharePoint Site.\nTERMINATING!\n");
                    return;
                }
                else
                {
                    Console.WriteLine("YES!\nSuccessfully connected to SP Site.\n");
                    init = true;
                }
            }
        }


        ////METHODS
        public bool connectToSP(string siteURL, string user, string pass)
        {
            try
            {
                myClient = new SRef1.ListsSoapClient();
                myClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(siteURL);
                NetworkCredential NCred = new NetworkCredential();
                NCred.UserName = user;
                NCred.Password = pass;
                myClient.ClientCredentials.Windows.ClientCredential = NCred;
            }

            catch 
            {
                //if it doesnt work...
                return false;
            }
            return true;
        }

        //gets all list names in SharePoint site
        public List<string> getListNames()
        {
            XElement myListCollection = myClient.GetListCollection();
            SPLists = new List<XElement>();
            ListNames = new List<string>();
            ListNames.Clear();

            foreach (XElement e in myListCollection.Nodes())
            {
                SPLists.Add(e);
                ListNames.Add(e.Attribute("Title").Value);
            }
            return ListNames;
        }

        /// <summary>
        /// gets all items from a list: WORK IN PROGRESS. Needs Defence
        /// </summary>
        public List<XElement> getListItems(string listName)
        {
            //check to make sure that list name exists
            //
            //

            List<XElement> myItems = new List<XElement>();
            try
            {
                //get items from a list
                // thanks to http://ojasmaru.blogspot.com/2012/08/sharepoint-2010-get-list-items-using.html
                var query = new XElement("Query", "");
                var viewFields = new XElement("ViewFields", "");
                var queryOptions = new XElement("QueryOptions", "");
                XElement myListItemCollection = myClient.GetListItems(listName, null, query, viewFields, null, queryOptions, null);
                //Console.WriteLine(aListItemCollection);
                foreach (XElement myItem in myListItemCollection.Elements().Elements())
                {
                    myItems.Add(myItem);
                }
            }
            catch {Console.WriteLine("\nTrying to get List Items. Are you sure that a list with name {0} exist?\n",listName);}
            return myItems;
        }

        /// <summary>
        /// get specific attributes with values from an item based on one or more attributes
        /// </summary>
        public List<XAttribute> getItemAttributes(XElement item, params string[] attributeNames)
        {
            List<XAttribute> AttributeList = new List<XAttribute>();
            foreach (string searchString in attributeNames)
            {
                IEnumerable<XAttribute> AttList = item.Attributes(searchString);
                foreach (XAttribute att in AttList)
                {
                    AttributeList.Add(att);
                }
            }
            return AttributeList;
        }

        /// <summary>
        /// get the attribute values of a specific item and return a list (of string)
        /// </summary>       
        public List<string> getItemAttributeValues(XElement item, params string[] attributeNames)
        {
            List<string> AttributeList = new List<string>();
            foreach(string attName in attributeNames)
            {
                AttributeList.Add(item.Attribute(attName).Value);
            }
            return AttributeList;
        }

        public List<string> getItemAttributeValues(XElement item, List<string> attributeNames)
        {
            List<string> AttributeList = new List<string>();
            foreach (string attName in attributeNames)
            {
                AttributeList.Add(item.Attribute(attName).Value);
            }
            return AttributeList;
        }

        /// <summary>
        /// add item to list
        /// thanks to http://www.drowningintechnicaldebt.com/ShawnWeisfeld/archive/2009/02/24/sharepoint-webservice-are-your-friend.aspx
        /// thanks to http://go4answers.webhost4life.com/Example/insert-data-sharepoint-list-silverlight-22367.aspx
        /// also http://msdn.microsoft.com/en-us/library/lists.lists.updatelistitems(v=office.12).aspx
        /// </summary>
        public bool addListItem(string listName, XElement listItem)
        {
            myClient.UpdateListItems(listName, listItem);
            return true;
        }


        /// <summary>
        /// Add LIST ITEM TO list (feed attr names and properties
        /// add new XAttribute("ID", "1")
        /// add new XAttribute("Cmd", "New")
        /// add new XElement("Field", new XAttribute("Name", "ID"), "New")
        /// add new XElements from input lists
        /// create new XElement("Method") and add list of elements
        /// add MEthod XElement to XElement "Batch"
        /// </summary>
        public bool addListItem(string listName, string title, List<string> attributeNames, List<object> attributeValues)
        {
            if (attributeNames.Count() != attributeValues.Count())
            {
                Console.WriteLine("\n\nThere was a problem with addListItem.");
                Console.WriteLine("You inserted {0} AttributeNames and {1} AttributeValues.",attributeNames.Count(),attributeValues.Count());
                Console.WriteLine("Lists have to be of equal length\n\n");
                return false;
            }

            List<XElement> newElements = new List<XElement>();
            for(int i = 0; i<attributeNames.Count();i++)
            {
                newElements.Add(new XElement("Field", new XAttribute("Name", attributeNames[i]), attributeValues[i]));
            }

            XElement newElement = new XElement("Batch", new XElement("Method", new XAttribute("ID", "1"), new XAttribute("Cmd", "New"), new XElement("Field", new XAttribute("Name", "ID"), "New"),
                new XElement("Field", new XAttribute("Name", "Title"), title), newElements));

            //XElement newElement = new XElement("Batch",new XElement("Method",new XAttribute("ID", "1"),new XAttribute("Cmd", "New"),new XElement("Field", new XAttribute("Name", "ID"), "New"), newElements));

            myClient.UpdateListItems(listName, newElement);
            return true;
        }


        /// ////////////////////////////////ADDITIONAL LISTS TO BE CREATED//////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// 1. create a new list
        public bool createList(string ListName, string ListType)
        {
            //add new list
            return true;
        }

        //2. get list length

        //3. update existing item in list

    }
}
