using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Pastures
{
    public static class PastureReader
    {
        public static spotkania ReadPastures(string pathToXML)
        {
            if (pathToXML is null)
            {
                throw new ArgumentNullException(nameof(pathToXML));
            }

            if (!File.Exists(pathToXML))
            {
                throw new ArgumentException("File does not exist.", nameof(pathToXML));
            }

            XmlReaderSettings settings = InitSettings();

            using (XmlReader reader = XmlReader.Create(pathToXML, settings))
            {
                XmlDocument document = new XmlDocument();
                document.Load(reader);

                document.Validate(ValidationHandler);

                return DeserializePastures(document);
            }
        }

        private static void ValidationHandler(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("Warning: {0}", args.Message);
            else
                Console.WriteLine("Error: {0}", args.Message);
        }


        private static XmlReaderSettings InitSettings()
        {
            //...
            XmlReaderSettings settings = new XmlReaderSettings();
            // Validator settings
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

            // Here we add xsd files to namespaces we want to validate
            // (It's like XML -> Schemas setting in Visual Studio)
            settings.Schemas.Add("http://tempuri.org/Pastures.xsd", "Pastures.xsd");

            // Processing XSI Schema Location attribute
            // (Disabled by default as it is a security risk). 
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;

            // A function delegate that will be called when 
            // validation error or warning occurs
            settings.ValidationEventHandler += ValidationHandler;
            return settings;
        }

        private static spotkania DeserializePastures(XmlDocument document)
        {
            using (var xmlNodeReader = new XmlNodeReader(document))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(spotkania));

                var output = (spotkania)serializer.Deserialize(xmlNodeReader);
                return output;
            }
        }
    }
}
