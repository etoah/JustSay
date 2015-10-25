using JustSay.Common.DotNetData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace JustSay.Common.DotNetJson
{
    public class JsonHelper
    {
        public static string IListToJson<T>(IList<T> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (T t in list)
            {
                sb.Append(JsonHelper.ObjectToJson<T>(t) + ",");
            }
            string _temp = sb.ToString().TrimEnd(new char[]
			{
				','
			});
            return _temp + "]";
        }

        public static string IListToJson<T>(IList<T> list, string ClassName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"" + ClassName + "\":[");
            foreach (T t in list)
            {
                sb.Append(JsonHelper.ObjectToJson<T>(t) + ",");
            }
            string _temp = sb.ToString().TrimEnd(new char[]
			{
				','
			});
            return _temp + "]}";
        }

        public static string ObjectToJson<T>(T t)
        {
            StringBuilder sb = new StringBuilder();
            string json = "";
            if (t != null)
            {
                sb.Append("{");
                PropertyInfo[] properties = t.GetType().GetProperties();
                PropertyInfo[] array = properties;
                for (int i = 0; i < array.Length; i++)
                {
                    PropertyInfo pi = array[i];
                    sb.Append("\"" + pi.Name.ToString() + "\"");
                    sb.Append(":");
                    sb.Append("\"" + pi.GetValue(t, null) + "\"");
                    sb.Append(",");
                }
                json = sb.ToString().TrimEnd(new char[]
				{
					','
				});
                json += "}";
            }
            return json;
        }

        public static string ObjectToJson<T>(T t, string ClassName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"" + ClassName + "\":[");
            string json = "";
            if (t != null)
            {
                sb.Append("{");
                PropertyInfo[] properties = t.GetType().GetProperties();
                PropertyInfo[] array = properties;
                for (int i = 0; i < array.Length; i++)
                {
                    PropertyInfo pi = array[i];
                    sb.Append("\"" + pi.Name.ToString() + "\"");
                    sb.Append(":");
                    sb.Append("\"" + pi.GetValue(t, null) + "\"");
                    sb.Append(",");
                }
                json = sb.ToString().TrimEnd(new char[]
				{
					','
				});
                json += "}]}";
            }
            return json;
        }

        public static string ObjectToJson<T>(IList<T> IL, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"" + jsonName + "\":[");
            if (IL.Count > 0)
            {
                for (int i = 0; i < IL.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    Type type = obj.GetType();
                    PropertyInfo[] pis = type.GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        Json.Append(string.Concat(new object[]
						{
							"\"",
							pis[j].Name.ToString(),
							"\":\"",
							pis[j].GetValue(IL[i], null),
							"\""
						}));
                        if (j < pis.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < IL.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        public static string ArrayToJson<T>(List<T> list, string propertyname)
        {
            StringBuilder sb = new StringBuilder();
            string result;
            if (list.Count > 0)
            {
                sb.Append("[{\"");
                sb.Append(propertyname);
                sb.Append("\":[");
                foreach (T t in list)
                {
                    sb.Append("\"");
                    sb.Append(t.ToString());
                    sb.Append("\",");
                }
                string _temp = sb.ToString();
                _temp = _temp.TrimEnd(new char[]
				{
					','
				});
                _temp += "]}]";
                result = _temp;
            }
            else
            {
                result = "";
            }
            return result;
        }

        public static string DataTableToJson(DataTable dt, string dtName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"");
            sb.Append(dtName);
            sb.Append("\":[");
            if (DataTableHelper.IsExistRows(dt))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{");
                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        sb.Append("\"");
                        sb.Append(dc.ColumnName);
                        sb.Append("\":\"");
                        if (dr[dc] != null && dr[dc] != DBNull.Value && dr[dc].ToString() != "")
                        {
                            sb.Append(dr[dc]).Replace("\\", "/");
                        }
                        else
                        {
                            sb.Append("&nbsp;");
                        }
                        sb.Append("\",");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]}");
            return JsonHelper.JsonCharFilter(sb.ToString());
        }

        public static string DataRowToJson(DataRow dr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (DataColumn dc in dr.Table.Columns)
            {
                sb.Append("\"");
                sb.Append(dc.ColumnName);
                sb.Append("\":\"");
                if (dr[dc] != null && dr[dc] != DBNull.Value && dr[dc].ToString() != "")
                {
                    sb.Append(dr[dc]);
                }
                else
                {
                    sb.Append("&nbsp;");
                }
                sb.Append("\",");
            }
            sb = sb.Remove(0, sb.Length - 1);
            sb.Append("},");
            return sb.ToString();
        }

        public static string ArrayToJson(string[] strs)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strs.Length; i++)
            {
                sb.AppendFormat("'{0}':'{1}',", i + 1, strs[i]);
            }
            string result;
            if (sb.Length > 0)
            {
                result = "{" + sb.ToString().TrimEnd(new char[]
				{
					','
				}) + "}";
            }
            else
            {
                result = "";
            }
            return result;
        }

        public static string ListToJson<T>(List<T> objlist, string jsonName)
        {
            string result = "{";
            if (jsonName.Equals(string.Empty))
            {
                object o = objlist[0];
                jsonName = o.GetType().ToString();
            }
            result = result + "\"" + jsonName + "\":[";
            bool firstline = true;
            foreach (object oo in objlist)
            {
                if (!firstline)
                {
                    result = result + "," + JsonHelper.ObjectToJson(oo);
                }
                else
                {
                    result += JsonHelper.ObjectToJson(oo);
                    firstline = false;
                }
            }
            return result + "]}";
        }

        private static string ObjectToJson(object o)
        {
            string result = "{";
            List<string> ls_propertys = new List<string>();
            ls_propertys = JsonHelper.GetObjectProperty(o);
            foreach (string str_property in ls_propertys)
            {
                if (result.Equals("{"))
                {
                    result += str_property;
                }
                else
                {
                    result = result + "," + str_property;
                }
            }
            return result + "}";
        }

        private static List<string> GetObjectProperty(object o)
        {
            List<string> propertyslist = new List<string>();
            PropertyInfo[] propertys = o.GetType().GetProperties();
            PropertyInfo[] array = propertys;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo p = array[i];
                propertyslist.Add(string.Concat(new object[]
				{
					"\"",
					p.Name.ToString(),
					"\":\"",
					p.GetValue(o, null),
					"\""
				}));
            }
            return propertyslist;
        }

        public static string HashtableToJson(Hashtable data, string dtName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"");
            sb.Append(dtName);
            sb.Append("\":[{");
            foreach (object key in data.Keys)
            {
                object value = data[key];
                sb.Append("\"");
                sb.Append(key);
                sb.Append("\":\"");
                if (!string.IsNullOrEmpty(value.ToString()) && value != DBNull.Value)
                {
                    sb.Append(value).Replace("\\", "/");
                }
                else
                {
                    sb.Append(" ");
                }
                sb.Append("\",");
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append("}]}");
            return JsonHelper.JsonCharFilter(sb.ToString());
        }

        private static string JsonCharFilter(string sourceStr)
        {
            return sourceStr;
        }
    }
}