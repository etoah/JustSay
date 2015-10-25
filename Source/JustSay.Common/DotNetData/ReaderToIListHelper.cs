using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace JustSay.Common.DotNetData
{
    public class ReaderToIListHelper
    {
        public static DataTable DataTableToIDataReader(IDataReader reader)
        {
            DataTable objDataTable = new DataTable("Table");
            int intFieldCount = reader.FieldCount;
            for (int intCounter = 0; intCounter < intFieldCount; intCounter++)
            {
                objDataTable.Columns.Add(reader.GetName(intCounter).ToUpper(), reader.GetFieldType(intCounter));
            }
            objDataTable.BeginLoadData();
            object[] objValues = new object[intFieldCount];
            while (reader.Read())
            {
                reader.GetValues(objValues);
                objDataTable.LoadDataRow(objValues, true);
            }
            reader.Close();
            objDataTable.EndLoadData();
            return objDataTable;
        }

        public static T ReaderToModel<T>(IDataReader dr)
        {
            T result;
            try
            {
                try
                {
                    if (dr.Read())
                    {
                        List<string> list = new List<string>(dr.FieldCount);
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            list.Add(dr.GetName(i).ToLower());
                        }
                        T model = Activator.CreateInstance<T>();
                        PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
                        for (int j = 0; j < properties.Length; j++)
                        {
                            PropertyInfo pi = properties[j];
                            if (list.Contains(pi.Name.ToLower()))
                            {
                                if (!ReaderToIListHelper.IsNullOrDBNull(dr[pi.Name]))
                                {
                                    pi.SetValue(model, ReaderToIListHelper.HackType(dr[pi.Name], pi.PropertyType), null);
                                }
                            }
                        }
                        result = model;
                        return result;
                    }
                }
                finally
                {
                    if (dr != null)
                    {
                        dr.Dispose();
                    }
                }
                result = default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static List<T> ReaderToList<T>(IDataReader dr)
        {
            List<T> result;
            try
            {
                List<string> field = new List<string>(dr.FieldCount);
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    field.Add(dr.GetName(i).ToLower());
                }
                List<T> list = new List<T>();
                while (dr.Read())
                {
                    T model = Activator.CreateInstance<T>();
                    PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
                    for (int j = 0; j < properties.Length; j++)
                    {
                        PropertyInfo property = properties[j];
                        if (field.Contains(property.Name.ToLower()))
                        {
                            if (!ReaderToIListHelper.IsNullOrDBNull(dr[property.Name]))
                            {
                                property.SetValue(model, ReaderToIListHelper.HackType(dr[property.Name], property.PropertyType), null);
                            }
                        }
                    }
                    list.Add(model);
                }
                result = list;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
            return result;
        }

        private static object HackType(object value, Type conversionType)
        {
            object result;
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    result = null;
                    return result;
                }
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            result = Convert.ChangeType(value, conversionType);
            return result;
        }

        private static bool IsNullOrDBNull(object obj)
        {
            return obj is DBNull || string.IsNullOrEmpty(obj.ToString());
        }
    }
}