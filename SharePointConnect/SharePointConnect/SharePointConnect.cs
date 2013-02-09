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
        public List<string> ListNames;

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
            myInitialized = connectToSP();
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

        //get ListNames () 
        public bool getListNames()
        {
            bool NamesRetrieved = false;

            //check if connection is established
            try
            {
                XmlNode listCollection = SPConnection.GetListCollection();

                ListNames = new List<string>();
                //loop through listCollection
                foreach(XmlNode myElement in listCollection.ChildNodes)
                {
                    //  for each node, get the title
                    ListNames.Add(myElement.Attributes["Title"].Value);
                }
                NamesRetrieved = true;
            }

            catch (Exception)
            {
                
            }
            return NamesRetrieved;  
        }
        //MICHAEL

        //create new list in SP(ListName, [List Attributes());
        //ELCIN
        // testing github

        //get List attributes(ListName, Array of AttributeNames);
        //NACIEM

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
