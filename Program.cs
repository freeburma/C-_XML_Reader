using System;
using System.IO;
using System.Xml;

namespace XMLTest
{
    class Program
    {
        /// <summary>
        /// Tested and Working: 07-July-2019 11:17 am
        /// This is a console which will read the XML string. 
        /// This will extract the attributes with name and inner text. 
        /// When you are reading an XML file, please add "books.xml" file in bin folder or 
        /// give a URL path.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmldocument?redirectedfrom=MSDN&view=netframework-4.8
        /// Ref: https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnode.attributes?view=netframework-4.8
        /// Ref: https://stackoverflow.com/questions/15220115/get-all-valid-attributes-for-a-given-xml-element
        /// </summary>
        /// <param name="args"></param>

        public static XmlNode GetBook (string uniqueAttribute, XmlDocument doc)
        {
            return doc.DocumentElement.GetAttributeNode(uniqueAttribute);
        }

        public static void XMLStringReader ()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<book ISBN='1-861001-57-5' year='07-July-2019'>" +
                            "<title titleAttribute1='tile 1' titleAttribute2='tile 2' titleAttribute3='tile 3'>Pride And Prejudice</title>" +
                            "<price priceAttribute1='price 1' priceAttribute2='price 2'>19.95</price>" +
                            "<hp> HP Computer</hp>" +
                            "<coffee coffee_attr1='cappucino' coffee_attr2='mocca' coffee_attr3='flatwhite'></coffee>" +
                        "</book>");

            /// Get by ISBN
            var getBook = GetBook("ISBN", doc); 
            Console.WriteLine($"ISBN: {getBook.Name} {getBook.Value}");

            getBook = GetBook("year", doc); 
            Console.WriteLine($"year: {getBook.Name} {getBook.Value}");

            

            Console.WriteLine(doc.DocumentElement.OuterXml);

            XmlNode root = doc.FirstChild;
            Console.WriteLine($"Root Last Child: {root.LastChild.OuterXml}");

            /// Creating a new Attribute 
            string ns = root.GetNamespaceOfPrefix("location");
            XmlNode attr = doc.CreateNode(XmlNodeType.Attribute, "location", ns);
            attr.Value = "NZ";

            /// Adding the attribute to doc 
            root.Attributes.SetNamedItem(attr); 

            getBook = GetBook("location", doc); 
            Console.WriteLine($"location: {getBook.Name} {getBook.Value}");

            //Display the contents of the child nodes.
            if (root.HasChildNodes)
            {
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    Console.WriteLine(root.ChildNodes[i].InnerText);
                }
            }


            Console.WriteLine("\n\n");
            /// Looping with Attributes 
            if (root.HasChildNodes)
            {
                foreach (XmlElement element in root.ChildNodes)
                {
                    XmlAttributeCollection xmlAttributeCollection = element.Attributes;

                    foreach (XmlAttribute attribute in xmlAttributeCollection)
                    {
                        Console.WriteLine($"{attribute.Name} {attribute.Value}");
                    }

                    Console.WriteLine($"Inner Text : {element.InnerText}");

                }
            }

            /// Change the price of the last child 
            root.LastChild.InnerText = "1000.99"; 
            Console.WriteLine($"Root Last Child: {root.LastChild.OuterXml}");

        }// end XMLStringReader()

        public static void XML_FileReader(string fileName)
        {
            XmlDocument docFile = new XmlDocument();

            /// Loading XML file
            XmlTextReader xmlTextReader = new XmlTextReader(fileName);

            xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
            xmlTextReader.MoveToContent();
            // xmlTextReader.Read();

            // xmlTextReader.Skip(); /// Skipping the first book


            /// Loading it to doument 
            docFile.Load(xmlTextReader);
            docFile.Save(Console.Out);

            Console.WriteLine($"\n====================================================\n");


            XmlNode root = docFile.FirstChild; 
            if (root.HasChildNodes)
            {
                
                foreach (XmlElement element in root.ChildNodes)
                {
                    XmlAttributeCollection xmlAttributeList = element.Attributes;

                    /// Reading the attributes 
                    foreach (XmlAttribute attribute in xmlAttributeList)
                    {
                        Console.WriteLine($"[{attribute.Name}]: {attribute.Value}");
                    }

                    /// Getting all the inner text
                    foreach (XmlElement eleInner in element.ChildNodes)
                    {
                        Console.WriteLine($"{eleInner.InnerText} ");
                    }

                    Console.WriteLine($"====================================================");
                }// end for 
            }// end if 


        }// end XML_FileReader()

        static void Main (string[] args)
        {
            /// Reading XML String
            // XMLStringReader();

            /// Reading XML file
            XML_FileReader("books.xml"); 
            

            Console.ReadKey(); 

        }// end main ()


    }// end class
}
