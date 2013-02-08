using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

//somthing

namespace SharePointConnect
{
    public class SharePointConnector
    {

        ///////FIELDS
        private string myPassWord, myUserName;
        private bool myInitialized;

        ///////PROPERTIES
        public string url { get; set; }
        public PAE810SharePoint.Lists SPConnection { get; set; }


        ////////CONSTRUCTOR
     //   public SharePointConnector();
        public SharePointConnector(string URL, string UserName, string PassWord)
        {
            url = URL;
            myUserName = UserName;
            myPassWord = PassWord;
            myInitialized = true;
        }
        
        ////////METHODS
        public bool connectToSP()
        {
            bool SPconnect = false;
            
            SPConnection = new PAE810SharePoint.Lists();
            SPConnection.Url = url;

            // if you're an outside the network user
            SPConnection.Credentials = new NetworkCredential(myUserName, myPassWord);

            // if you're an inside the network user with integrated security
            // myConnection.Credentials = CredentialCache.DefaultNetworkCredentials;

            //see if we can successfully make web service calls
            try
            {
                XmlNode listCollection = SPConnection.GetListCollection();
                SPconnect = true;
            }
            catch (Exception)
            {
            }
            return SPconnect;
        }


        //create new list in SP(ListName, [List Attributes());
        //ELCIN
       
        //  XElement Class Overview http://msdn.microsoft.com/en-us/library/bb387085.aspx
        //  Programming Sharepoint Lists and Libraries http://msdn.microsoft.com/en-us/library/dd490727(v=office.12).aspx
        //  Add List Method
        //  http://sarangasl.blogspot.com/2009/11/create-sharepoint-list-programmatically.html
        //  http://blog.the-dargans.co.uk/2007/04/programmatically-adding-items-to.html
        
        //public string CreateList()
        public List <string> CreateList(string ListName, string Description) 
        {
            
            string myList;
            string myListDescription;
            int templatetype;
            myList = ListName;
            myListDescription = Description;
            

            // Call AddList Method of the Web service to create new list
            SPConnection.AddList(myList,myListDescription,templatetype);
            
        }
 

        //get List attributes(ListName, Array of AttributeNames);
        //NACIEM

        //get ListNames () 
        //---returns a list of all names
        //MICHAEL

        //get ListItems(ListName)
        //--return List of XElements 
        //SAM

        //get ListTitle(ListName)
        //--return returns list with strings of item titles
        //OLEG

        //addItem(string ListName, XElement item)
        //--adds item to spefici list
        //GIORGOS

        // get list length(ListName)
        //returns integer
        //MICHAEL

        //public List<string> getItemAttributeValues(XElement item, params string[] attributeNames)
        //{
        //ALEX

        //}
    }
}
