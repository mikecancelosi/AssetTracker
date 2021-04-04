using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AssetTracker.Services
{
    public static class SQLHelper
    {

        public static string GetTableAttribute(Type t)
        {
            TableAttribute attribute = Attribute.GetCustomAttribute(t, typeof(TableAttribute)) as TableAttribute;
            return attribute?.Name;
        }

        public static string GetColumnAttribute(Type t, string fieldName)
        {
            ColumnAttribute attribute = Attribute.GetCustomAttribute(t.GetField(fieldName), typeof(ColumnAttribute)) as ColumnAttribute;
            return attribute?.Name;
        }

        public static List<FieldInfo> GetColumnFields(Type t)
        {
            return t.GetFields().Where(x => x.GetCustomAttribute<ColumnAttribute>() != null).ToList();
        }

        public static FieldInfo GetIDField(Type t)
        {
            return t.GetFields().FirstOrDefault(x=>x.GetCustomAttribute<DisplayAttribute>().Name == "ID");
        }

        public static string GenerateInClause(List<int> ids)
        {
            string result = "(";
            for (int i = 0; i < ids.Count; i++)
            {
                result += i;
                if (i != ids.Count - 1)
                {
                    result += ",";
                }
            }

            result += ")";
            return result;
        }
    }
}
