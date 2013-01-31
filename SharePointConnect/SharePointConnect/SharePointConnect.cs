using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;



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
