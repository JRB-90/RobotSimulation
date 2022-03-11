using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JSim.Core;
using System.Reflection;
using System.Xml;

namespace JSim.Logging
{
    public class Log4NetInstaller : IWindsorInstaller
    {
        readonly XmlDocument xmlDocument;

        /// <summary>
        /// Creates a new log4net logger from a given file.
        /// </summary>
        /// <param name="loggingConfigFilePath">Path to the logging config xml file.</param>
        public static Log4NetInstaller FromPath(string loggingConfigFilePath)
        {
            var xmlString = File.ReadAllText(loggingConfigFilePath);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);

            if (xmlDocument == null)
            {
                throw new ArgumentException("Config file not valid XML");
            }

            return new Log4NetInstaller(xmlDocument);
        }

        /// <summary>
        /// Creates a new log4net logger from an embedded file.
        /// </summary>
        /// <param name="loggingConfigFilePath">Name of the embedded logging config xml file.</param>
        public static Log4NetInstaller FromEmbedded(string loggingConfigFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "JSim.Logging." + loggingConfigFileName;
            var xmlString = "";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    xmlString = reader.ReadToEnd();
                }
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);

            if (xmlDocument == null)
            {
                throw new ArgumentException("Config file not valid XML");
            }

            return new Log4NetInstaller(xmlDocument);
        }

        /// <summary>
        /// Installs a Log4Net logger to a windsor container.
        /// </summary>
        /// <param name="container">Container to install to.</param>
        /// <param name="store">Configuration setting storage object.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ILogger>()
                .ImplementedBy<Log4NetLogger>()
                .DependsOn(Dependency.OnValue("xmlDocument", xmlDocument))
            );
        }

        private Log4NetInstaller(XmlDocument xmlDocument)
        {
            this.xmlDocument = xmlDocument;
        }
    }
}
