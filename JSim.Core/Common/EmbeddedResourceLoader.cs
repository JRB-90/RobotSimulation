using System.Reflection;

namespace JSim.Core.Common
{
    public static class EmbeddedResourceLoader
    {
        public static string LoadEmbeddedFile(
            string rootFolder,
            string fileName,
            Assembly assembly)
        {
            var resources = assembly.GetManifestResourceNames();
            var resourceName = rootFolder + fileName;

            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new ArgumentException("Cannot find embedded resource");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();

                    return result;
                }
            }
        }
    }
}
