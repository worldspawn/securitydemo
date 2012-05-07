using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.XPath;

namespace ApplicationSecurity
{
    public class PermissionConfig<T>
    {
        public PermissionConfig(string path) : this(new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            
        }
        
        public PermissionConfig(Stream configStream)
        {
            var reader = new XPathDocument(configStream);
            var nav = reader.CreateNavigator();
            
            foreach(XPathNavigator p in nav.Select("//permissions/permission"))
            {
                var name = p.GetAttribute("name", "");
                var id = p.GetAttribute("id", "");

                Permissions.Add(id, (T)Enum.Parse(typeof(T), name));
            }

            foreach(XPathNavigator ps in nav.Select("//permissionsets/set"))
            {
                var name = ps.GetAttribute("name", "");
                var permissions = new List<T>();
                Sets.Add(name, permissions);
                foreach(XPathNavigator p in ps.Select("permission"))
                {
                    var permission = p.GetAttribute("ref", "");
                    permissions.Add((T)Enum.Parse(typeof(T), permission));
                }
            }

            foreach (XPathNavigator group in nav.Select("//group"))
            {
                var name = group.GetAttribute("name", "");
                var typeName = group.GetAttribute("type", "");
                var type = Type.GetType(typeName);

                if (type == null)
                    throw new InvalidDataException(string.Format("type unknown {0}", typeName));

                var g = new Group { Name = name };
                Groups.Add(type, g);

                foreach(XPathNavigator set in group.Select("set"))
                {
                    g.Sets.Add(set.GetAttribute("ref", ""));
                }
            }
        }

        private readonly IDictionary<string, T> _permissions = new Dictionary<string, T>();
        private readonly IDictionary<string, List<T>> _sets = new Dictionary<string, List<T>>();
        private readonly IDictionary<Type, Group> _groups = new Dictionary<Type, Group>();

        public IDictionary<string, T> Permissions
        {
            get { return _permissions; }
        }

        public IDictionary<string, List<T>> Sets
        {
            get { return _sets; }
        }

        public IDictionary<Type, Group> Groups
        {
            get { return _groups; }
        }
    }
}