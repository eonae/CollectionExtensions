using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Eonae.CollectionExtensions
{
    /// Направления для улучшения:
    /// 1. Разобраться, что будет если применить методы, например к IEnumerable<int> или подобному
    /// 2. Добавить возможность выбора столбцов и настроек для варианта IEnumerable
    
    public static class TableToString
    {
        private const char simpleDelimeterChar = '-';
        private const char strongDelimeterChar = '=';
        private const int leftMargin = 1;

        public static string CreateStringTable<SomeClass>(this IEnumerable<SomeClass> list, string name)
        {
            var table = list.ToDataTable(name);
            return table.CreateStringTable();
        }

        public static string CreateStringTable(this DataTable table)
        {

            var tableInfo = GetTableInfo(table);

            var columnsInfo = tableInfo.ColumnsInfo;
            var tableWidth = tableInfo.TableWidth;
            var sb = new StringBuilder();
            if (table.TableName != "") sb.AppendLine($"Title: {table.TableName} ");
            sb.Append(AddTitles(columnsInfo, tableWidth));
            if (table.Rows.Count == 0) return sb.ToString();

            string simpleDelimeter = new string(simpleDelimeterChar, tableWidth);
            foreach (DataRow row in table.AsEnumerable())
            {
                if (IsEmpty(row.ItemArray))
                    continue;

                sb.Append(new string(' ', leftMargin) + "| ");
                foreach (var key in columnsInfo.Keys)
                {
                    object value = row[key];
                    string toPrint;

                    if (value != null) toPrint = value.ToString();
                    else toPrint = "null";
                    sb.Append(toPrint);
                    int spacesToInsert = columnsInfo[key] - toPrint.Length;
                    sb.Append(new string(' ', spacesToInsert) + " | ");
                }
                sb.AppendLine();
                sb.AppendLine(new string(' ', leftMargin) + simpleDelimeter);
            }
            return sb.ToString();
        }

        private static string AddTitles(Dictionary<string, int> columnsInfo, int tableWidth)
        {
            var sb = new StringBuilder();
            string strongDelimeter = new string(' ', leftMargin) + new string(strongDelimeterChar, tableWidth);
            sb.AppendLine(strongDelimeter);
            sb.Append(new string(' ', leftMargin) + "| ");
            foreach (var column in columnsInfo)
            {
                sb.Append(column.Key);
                int spacesToInsert = column.Value - column.Key.Length;
                sb.Append(new string(' ', spacesToInsert) + " | ");
            }
            sb.AppendLine();
            sb.AppendLine(strongDelimeter);
            return sb.ToString();
        }
        private static (Dictionary<string, int> ColumnsInfo, int TableWidth) GetTableInfo(DataTable table)
        {
            var result = new Dictionary<string, int>();
            var tableWidth = 1;

            foreach (DataColumn column in table.Columns)
            {
                int maxLength = GetMaxValueLength(column);
                string name = column.ColumnName;
                if (name.Length > maxLength) maxLength = name.Length;
                result.Add(name, maxLength);
                tableWidth += (maxLength + 3); // " | "
            }
            return (result, tableWidth);
        }
        private static int GetMaxValueLength(DataColumn column)
        {
            int max_length = 0;
            foreach (DataRow row in column.Table.Rows)
            {
                var length = 0;
                var value = row[column];
                if (value == null) length = 4;
                else length = value.ToString().Length;
                if (length > max_length)
                    max_length = length;
            }
            return max_length;
        }
        private static bool IsEmpty(object[] array)
        {
            foreach (var obj in array)
                if (obj != null)
                    if (obj.ToString() != string.Empty)
                        return false;
            return true;
        }

    }
}
