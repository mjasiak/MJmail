using MJMail.Grid.Cells;
using MJMail.Grid.GridRows;
using MJMail.Grid.Paging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MJMail.Grid
{
    public class Grid<T>
    {
        static List<PropertyInfo> PropsList = new List<PropertyInfo>();
        static Rows rows = new Rows();

        public static string Create(IEnumerable<T> data, List<string> columns, PagingInfo paging)
        {
            ReadProps(data);
            PrepareData(data, PropsList, columns);
            return Generate(rows.GetRows());
        }
        public static string Create(IEnumerable<T> data, PagingInfo paging)
        {
            ReadProps(data);
            PrepareData(data, PropsList, null);
            return Generate(rows.GetRows());
        }

        static void PrepareData(IEnumerable<T> data, List<PropertyInfo> props, List<string> columns)
        {
            if (columns != null)
            {
                foreach(var item in data)
                {
                    Row row = new Row("<tr>","</tr>");
                    foreach(var column in columns)
                    {
                        foreach(var prop in props)
                        {
                            if(prop.Name == column)
                            {
                                row.AddCell(new Cell("<td>","</td>", prop.GetValue(item,null).ToString()));
                            }
                        }
                    }
                    rows.AddRow(row);
                }
            }
            else
            {
                foreach (var item in data)
                {
                    Row row = new Row("<tr>", "</tr>");                    
                        foreach (var prop in props)
                        {                           
                                row.AddCell(new Cell("<td>", "</td>", prop.GetValue(item, null).ToString()));
                        }
                    rows.AddRow(row);
                }
            }
        }

        static string Generate(List<Row> rows)
        {
            string outer = "";
            foreach(var row in rows)
            {
                string inner = "";
                foreach (var cell in row.GetCells())
                {
                    inner += cell.Build();
                }
                outer += "<tr>" + inner + "</tr>";
            }

            return "<div class='scrollbar-outer'><table class='table table-striped'>" + outer + "</table></div>";
        }

        #region Property
        static void ReadProps(IEnumerable<T> source)
        {
            Type Type = null;
            foreach (var item in source)
            {
                Type = item.GetType();
                break;
            }

            var props = Type.GetProperties();
            foreach (var prop in props)
            {
                PropsList.Add(prop);
            }
        }
        #endregion
    }
}
