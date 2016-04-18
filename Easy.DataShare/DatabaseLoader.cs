using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using Easy.DataShare.DateShare;

namespace Easy.DataShare
{
    public class DatabaseLoader
    {
        public IList<Tuple<string, IList<DefaultDateTimeSplitDatabase>>> Load(XPathNavigator navigator)
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(navigator.NameTable);
            namespaceManager.AddNamespace("abc", "http://www.39541240.com/database");
            namespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            var groupList = new List<Tuple<string, IList<DefaultDateTimeSplitDatabase>>>();
            var groups = navigator.Select("abc:databases/abc:group", namespaceManager);
            foreach (XPathNavigator g in groups)
            {
                string name = g.GetAttribute("name", "");

                var databases = g.Select("abc:timeddatabase", namespaceManager);
                var databaseList = new List<DefaultDateTimeSplitDatabase>();

                foreach (XPathNavigator d in databases)
                {
                    int index = int.Parse(d.GetAttribute("index", ""));
                    DateTime start = DateTime.Parse(d.GetAttribute("start", ""));
                    DateTime end = DateTime.Parse(d.GetAttribute("end", ""));
                    string content = d.Value;
                    databaseList.Add(new DefaultDateTimeSplitDatabase(start, end, index, content));
                }
                groupList.Add(new Tuple<string, IList<DefaultDateTimeSplitDatabase>>(name, databaseList));
            }
            return groupList;
        }
    }
}
