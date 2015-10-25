using System.Collections;
using System.Data;
using System.IO;

namespace JustSay.Common.DotNetData
{
    public class XMLHelper
    {
        public static Hashtable XMLToHashtable(string xmlData)
        {
            DataTable dt = XMLHelper.XMLToDataTable(xmlData);
            return DataTableHelper.DataTableToHashtable(dt);
        }

        public static DataTable XMLToDataTable(string xmlData)
        {
            DataTable result;
            if (!string.IsNullOrEmpty(xmlData))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xmlData));
                if (ds.Tables.Count > 0)
                {
                    result = ds.Tables[0];
                    return result;
                }
            }
            result = null;
            return result;
        }

        public static DataSet XMLToDataSet(string xmlData)
        {
            DataSet result;
            if (!string.IsNullOrEmpty(xmlData))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xmlData));
                result = ds;
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}