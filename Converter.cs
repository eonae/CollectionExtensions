using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Eonae.CollectionExtensions
{
    /// 1. Может быть попробовать сделать обратную операцию?? С генерацией нового класса.
    /// 2. В конце концов сгенерировать новый файл с расширение cs проблем никаких нет...

    public static class TableConverter
    {
        public static DataTable ToDataTable<SomeClass>(this IEnumerable<SomeClass> list, string name)
        {

            DataTable table = new DataTable() { TableName = name };
            Type type = typeof(SomeClass);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var memberTypes = new Dictionary<string, MemberTypes>();
            foreach (var field in fields)
            {
                memberTypes.Add(field.Name, MemberTypes.Field);
                table.Columns.Add(field.Name);
            }
            foreach (var property in properties)
            {
                memberTypes.Add(property.Name, MemberTypes.Property);
                table.Columns.Add(property.Name);
            }
            foreach (SomeClass item in list)
            {
                var values = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    string memberName = table.Columns[i].ColumnName;
                    switch (memberTypes[memberName])
                    {
                        case MemberTypes.Field:
                            values[i] = type.GetField(memberName).GetValue(item); break;
                        case MemberTypes.Property:
                            values[i] = type.GetProperty(memberName).GetValue(item); break;
                    }
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
