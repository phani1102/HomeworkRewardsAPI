﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace WorkRewards.Data.Utility
{
    public static class Extentions
    {
        private static Dictionary<Type, List<PropertyInfo>> typeDictionary = new Dictionary<Type, List<PropertyInfo>>();
        static Dictionary<string, ToListexception> toExceptionColl = null;
        static int rowCount = 0;

        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            List<PropertyInfo> properties = GetPropertiesForType<T>();
            List<T> result = new List<T>();
            toExceptionColl = new Dictionary<string, ToListexception>();
            if (properties.Count > 0)
            {
                rowCount = table.Rows.Count;

                foreach (var row in table.AsEnumerable())
                {
                    try
                    {
                        rowCount--;
                        var itm = CreateItemFromRow<T>((DataRow)row, properties);
                        result.Add(itm);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            if (toExceptionColl.Count > 0)
            {
            }

            return result;
        }
        private static T CreateItemFromRow<T>(DataRow row, List<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            string toDbColumnName = "";

            foreach (var propp in properties)
            {
                toDbColumnName = Regex.Replace(propp.Name, @"(?<!_)([A-Z])", "_$1").TrimStart('_');
                try
                {
                    bool isOfTimeFormat = false;


                    if (row.Table.Columns.Contains(toDbColumnName) && row[toDbColumnName] != DBNull.Value && !isOfTimeFormat)
                    {
                        propp.SetValue(item, ToProxyValue(propp, row[toDbColumnName]));
                    }
                    else if (row.Table.Columns.Contains(toDbColumnName.Replace("_", "")) && row[toDbColumnName.Replace("_", "")] != DBNull.Value && !isOfTimeFormat)
                    {
                        propp.SetValue(item, ToProxyValue(propp, row[toDbColumnName.Replace("_", "")]));
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return item;
        }

        public static object ToProxyValue(PropertyInfo propInfo, object dataValue)
        {
            object dateobject = dataValue;
            string propType = ""; bool ObjIsNull = false;
            if (propInfo.PropertyType.IsGenericType && propInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) { ObjIsNull = true; }
            if (ObjIsNull && String.IsNullOrEmpty(Convert.ToString(dataValue))) { return null; }
            if (dataValue != null)
            {
                try
                {
                    propType = ObjIsNull ? propInfo.PropertyType.GetGenericArguments()[0].Name : propInfo.PropertyType.Name;
                    switch (propType)
                    {
                        case "Int16":
                            dateobject = Convert.ToInt16(dataValue);
                            break;
                        case "Int32":
                            dateobject = Convert.ToInt32(dataValue);
                            break;
                        case "Int64":
                            dateobject = Convert.ToInt64(dataValue);
                            break;
                        case "Single":
                            dateobject = Convert.ToSingle(dataValue);
                            break;
                        case "Double":
                            dateobject = Convert.ToDouble(dataValue);
                            break;
                        case "Boolean":
                            dateobject = Convert.ToBoolean(dataValue);
                            break;
                        case "Char":
                            dateobject = Convert.ToChar(dataValue);
                            break;
                        case "String":
                            dateobject = Convert.ToString(dataValue);
                            break;
                        case "Decimal":
                            dateobject = Convert.ToDecimal(dataValue);
                            break;
                        case "DateTime":
                            dateobject = Convert.ToDateTime(dataValue);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return dateobject;
        }

        private static List<PropertyInfo> GetPropertiesForType<T>()
        {
            typeDictionary = new Dictionary<Type, List<PropertyInfo>>();
            var type = typeof(T);
            try
            {

                if (!typeDictionary.ContainsKey(typeof(T)))
                {
                    typeDictionary.Add(type, type.GetProperties().ToList());
                    return typeDictionary.ContainsKey(type) ? typeDictionary[type] : new List<PropertyInfo>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new List<PropertyInfo>();
        }

    }
}

public class ToListexception
{
    public string ExceptionName { get; set; }
    public string ColumnName { get; set; }
    public string Value { get; set; }
    public string stackTrace { get; set; }
}