using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JustSay.Common.DotNetData
{
    public class HashtableHelper
    {
        public static string HashtableToXml(Hashtable ht)
        {
            StringBuilder xml = new StringBuilder("<root>");
            xml.Append(HashtableHelper.HashtableToNode(ht));
            xml.Append("</root>");
            return xml.ToString();
        }

        private static string HashtableToNode(Hashtable ht)
        {
            StringBuilder xml = new StringBuilder("");
            foreach (string key in ht.Keys)
            {
                object value = ht[key];
                xml.Append("<").Append(key).Append(">").Append(value).Append("</").Append(key).Append(">");
            }
            xml.Append("");
            return xml.ToString();
        }

        public static string IListToXML(IList<Hashtable> datas)
        {
            StringBuilder xml = new StringBuilder("<root>");
            foreach (Hashtable ht in datas)
            {
                xml.Append(HashtableHelper.HashtableToNode(ht));
            }
            xml.Append("</root>");
            return xml.ToString();
        }

        public static Hashtable GetModelToHashtable<T>(T model)
        {
            Hashtable ht = new Hashtable();
            PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo item = array[i];
                string key = item.Name;
                ht[key] = item.GetValue(model, null);
            }
            return ht;
        }
    }
}