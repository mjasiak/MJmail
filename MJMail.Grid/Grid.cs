using MJMail.Grid.Cells;
using MJMail.Grid.GridRows;
using MJMail.Grid.Paging;
using MJMail.Methods.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace MJMail.Grid
{
    public class Grid<T>
    {
        static List<PropertyInfo> PropsList = new List<PropertyInfo>();
        static Rows rows = new Rows();

#region GridCreate
        static string grid, pager = "";
        public static string Create(IEnumerable<T> data, List<string> columns, PagingInfo paging)
        {
            GridCleaner();
            ReadProps(data);
            PrepareData(data, PropsList, columns);
            grid = Generate(rows.GetRows());
            if (paging != null) pager = Pager(paging);
            return grid + pager;
        }
        public static string Create(IEnumerable<T> data, PagingInfo paging)
        {
            GridCleaner();
            ReadProps(data);
            PrepareData(data, PropsList, null);
            grid = Generate(rows.GetRows());
            if (paging != null)  pager = Pager(paging);
            return grid + pager;
        }
        public static string Create(IEnumerable<T> data)
        {
            GridCleaner();
            ReadProps(data);
            PrepareData(data, PropsList, null);
            grid = Generate(rows.GetRows());
            return grid;
        }
#endregion

        static void PrepareData(IEnumerable<T> data, List<PropertyInfo> props, List<string> columns)
        {
            if (props.Count == 0) return;
            if (columns != null)
            {
                foreach(var item in data)
                {
                    Row row = new Row("<tr>","</tr>");
                    foreach(var column in columns)
                    {
                        foreach(var prop in props)
                        {
                            if (prop.Name == "id" || prop.Name == "ID" || prop.Name == "Id")
                            {
                                row._prefix = "<tr id='" + MessageControl.Encode(prop.GetValue(item, null).ToString()) + "'>";
                                row.ID = MessageControl.Encode(prop.GetValue(item, null).ToString());
                            }
                            if(prop.Name == column) row.AddCell(new Cell("<td>","</td>", prop.GetValue(item,null).ToString(), prop.PropertyType.Name));
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
                            if (prop.Name == "id" || prop.Name == "ID" || prop.Name == "Id")
                            {
                                row._prefix = "<tr id='" + MessageControl.Encode(prop.GetValue(item, null).ToString()) + "'>";
                                row.ID = MessageControl.Encode(prop.GetValue(item, null).ToString());
                            }
                            row.AddCell(new Cell("<td>", "</td>", prop.GetValue(item, null).ToString(), prop.PropertyType.Name));
                        }
                    rows.AddRow(row);
                }
            }
        }
        static string Generate(List<Row> rows)
        {
            if (rows.Count == 0)
            {
                return "<div class='scrollbar-outer'><table class='table table-striped'><tbody><tr><td>Nie znaleziono podanej frazy</td></tr></tbody></table></div>";
            }
            string outer = "";
            foreach(var row in rows)
            {
                string inner = "";
                foreach (var cell in row.GetCells())
                {
                    if (cell._type == "DateTime")
                    {
                        cell.Content = SetDateTime(cell.Content);
                        cell.Prefix = "<td class='right-align'>";
                    }
                    inner += cell.Build();
                }
                outer += row._prefix + CheckboxCreator(row) + inner + row._postfix;
            }

            return "<div class='scrollbar-outer'><table class='table table-striped'>" + outer + "</table></div>";
        }
        static string Pager(PagingInfo paging)
        {
            string pager = "";
            for(int i = 1; i <= paging.pageTotal; i++)
            {
                if (i == paging.pageNumber) pager += "<li class='active'><a></a></li>";
                else
                {
                    if (paging.searchString == null) pager += "<li><a href='/" + paging.controller + "/" + paging.action + "?page=" + i + "'></a></li>";
                    else pager += "<li><a href='/" + paging.controller + "/" + paging.action + "?page=" + i + "&searchString="+paging.searchString+"'></a></li>";
                }
            }
            return "<div class='dotstyle'><ul>"+pager+"</ul></div>";
        }

#region Helpers
        static void ReadProps(IEnumerable<T> source)
        {
            Type Type = null;
            foreach (var item in source)
            {
                Type = item.GetType();
                break;
            }

            if (Type == null) return;

            var props = Type.GetProperties();
            foreach (var prop in props)
            {
                PropsList.Add(prop);
            }
        }
        static void GridCleaner()
        {
            grid = "";
            pager = "";
            PropsList = new List<PropertyInfo>();
            rows = new Rows();
        }
        static string CheckboxCreator(Row row)
        {
            return "<td><div class='checkbox'><input type='checkbox' value='"+row.ID+"'><label></label></div></td>";
        }
        static string SetDateTime(string cellContent)
        {
                DateTime dataCzas = DateTime.Parse(cellContent);
                if (DateTime.Now.Date == dataCzas.Date)
                {
                    return "Today,  " + dataCzas.ToShortTimeString();
                }
                else if (DateTime.Now.Date.AddDays(-1) == dataCzas.Date)
                {
                    return "Yesterday,  " + dataCzas.ToShortTimeString();
                }
                else
                {
                    return dataCzas.ToString("dd MMM", new CultureInfo("en-US"));
                }            
        }
#endregion
    }
}
