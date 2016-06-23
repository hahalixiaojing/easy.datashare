using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using Easy.DataShare.DateShare;

namespace Easy.DataShare
{
    public class DatabaseLoader
    {
        public IList<DefaultDateTimeSplitDatabase> Load(XPathNavigator navigator)
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(navigator.NameTable);
            namespaceManager.AddNamespace("abc", "http://www.39541240.com/database");
            namespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            var sourceList = new List<DefaultDateTimeSplitDatabase> ();

            var xmlSource = navigator.Select("abc:databases/abc:source", namespaceManager);
            foreach (XPathNavigator s in xmlSource)
            {
                int index = int.Parse(s.GetAttribute("index", ""));
                DateTime start = DateTime.Parse(s.GetAttribute("start", ""));
                DateTime end = DateTime.Parse(s.GetAttribute("end", ""));
                string content = s.Value;
                sourceList.Add(new DefaultDateTimeSplitDatabase(start, end, index, content));
            }
            return sourceList;
        }
    }
}
