using System.Xml;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// XML implementation of a scene IO handler, capable of saving and
    /// loading a scene from disk in an XML format.
    /// </summary>
    public class XmlSceneIOHandler : ISceneIOHandler
    {
        readonly ISceneFactory sceneFactory;

        public XmlSceneIOHandler(
            ISceneFactory sceneFactory)
        {
            this.sceneFactory = sceneFactory;
        }

        public IScene LoadSceneFromFile(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText(path));

            if (doc.DocumentElement == null ||
                doc.DocumentElement.Name != "Scene")
            {
                throw new InvalidOperationException("Xml lacks Scene root node");
            }

            return SceneFromNode(doc.DocumentElement);
        }

        public void SaveSceneToFile(IScene scene, string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement? root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement sceneRoot =
                CreateElementWithAttributes(
                    doc,
                    "Scene",
                    ("Name", scene.Name)
                );

            sceneRoot.AppendChild(
                ToElement(
                    doc, 
                    scene.Root
                )
            );

            doc.AppendChild(sceneRoot);
            doc.Save(path);
        }

        private XmlElement CreateElement(
            XmlDocument doc, 
            string name)
        {
            return doc.CreateElement(name);
        }

        private XmlAttribute CreateAttribute(
            XmlDocument doc, 
            string name, 
            string value)
        {
            XmlAttribute attribute = doc.CreateAttribute(name);
            attribute.Value = value;

            return attribute;
        }

        private XmlElement CreateElementWithAttributes(
            XmlDocument doc, 
            string name,
            params (string, string)[] attributes)
        {
            List<XmlAttribute> attributeList =
                attributes
                .Select(a => CreateAttribute(doc, a.Item1, a.Item2))
                .ToList();

            var element = doc.CreateElement(name);

            foreach (var a in attributeList)
            {
                element.Attributes.Append(a);
            }

            return element;
        }

        private XmlElement ToElement(
            XmlDocument doc,
            ISceneObject sceneObject)
        {
            if (sceneObject is ISceneAssembly assembly)
            {
                return ToElement(doc, assembly);
            }
            else if (sceneObject is ISceneEntity entity)
            {
                return ToElement(doc, entity);
            }
            else
            {
                throw new ArgumentException($"ISceneObject type {sceneObject.GetType()} not supported");
            }
        }

        private XmlElement ToElement(
            XmlDocument doc,
            ISceneAssembly assembly)
        {
            XmlElement assemblyElement =
                CreateElementWithAttributes(
                    doc,
                    "Assembly",
                    ("Name", assembly.Name),
                    ("ID", assembly.ID.ToString())
                );

            XmlElement assemblyChildren = CreateElement(doc, "Children");
            assemblyElement.AppendChild(assemblyChildren);

            foreach (var sceneObject in assembly.Children)
            {
                assemblyChildren.AppendChild(ToElement(doc, sceneObject));
            }

            return assemblyElement;
        }

        private XmlElement ToElement(
            XmlDocument doc,
            ISceneEntity entity)
        {
            XmlElement entityElement =
                CreateElementWithAttributes(
                    doc,
                    "Entity",
                    ("Name", entity.Name),
                    ("ID", entity.ID.ToString())
                );

            return entityElement;
        }

        private IScene SceneFromNode(XmlNode node)
        {
            IScene scene = sceneFactory.GetScene();

            if (node.Attributes != null)
            {
                var sceneNameAttrib = node.Attributes["Name"];
                if (sceneNameAttrib != null)
                {
                    scene.Name = sceneNameAttrib.Value;
                }
            }

            var rootAssemblyNode = node.SelectSingleNode("Assembly");

            if (rootAssemblyNode == null)
            {
                throw new InvalidOperationException("Xml lacks root assembly node");
            }

            if (rootAssemblyNode.ChildNodes.Count != 1)
            {
                throw new InvalidOperationException("More than 1 root assembly node definied");
            }

            var rootAssemblyChildren = rootAssemblyNode.SelectSingleNode("Children");
            
            if (rootAssemblyChildren == null)
            {
                throw new InvalidOperationException("Root assembly node has no Children node");
            }

            foreach (XmlNode childNode in rootAssemblyChildren.ChildNodes)
            {
                ObjectsFromNode(childNode, scene.Root);
            }

            return scene;
        }

        private void ObjectsFromNode(
            XmlNode node,
            ISceneAssembly parentAssembly)
        {
            switch (node.Name)
            {
                case "Assembly":
                    AssemblyFromNode(node, parentAssembly);
                    break;

                case "Entity":
                    EntityFromNode(node, parentAssembly);
                    break;

                default:
                    throw new ArgumentException($"Scene object type {node.Name} not supported");
            }
        }

        private void AssemblyFromNode(
            XmlNode node,
            ISceneAssembly parentAssembly)
        {
            if (node.Attributes == null)
            {
                throw new ArgumentException("Assembly node missing attributes");
            }

            var assemblyNameAttrib = node.Attributes["Name"];
            if (assemblyNameAttrib == null)
            {
                throw new ArgumentException("Assembly node missing Name attribute");
            }

            var assembly = parentAssembly.CreateNewAssembly(assemblyNameAttrib.Value);

            var assemblyChildren = node.SelectSingleNode("Children");
            if (assemblyChildren == null)
            {
                throw new InvalidOperationException("Assembly node has no Children node");
            }

            foreach (XmlNode childNode in assemblyChildren.ChildNodes)
            {
                ObjectsFromNode(childNode, assembly);
            }
        }

        private void EntityFromNode(
            XmlNode node,
            ISceneAssembly parentAssembly)
        {
            if (node.Attributes == null)
            {
                throw new ArgumentException("Entity node missing attributes");
            }

            var entityNameAttrib = node.Attributes["Name"];
            if (entityNameAttrib == null)
            {
                throw new ArgumentException("Entity node missing Name attribute");
            }

            var entity = parentAssembly.CreateNewEntity(entityNameAttrib.Value);
        }
    }
}
