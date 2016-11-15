using MJMail.Grid.Cells;
using MJMail.Grid.GridRows;
using MJMail.Grid.Paging;
using MJMail.Methods.Messages;
using System;
using System.Collections.Generic;
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
            pager = Pager(paging);
            return grid + pager;
        }
        public static string Create(IEnumerable<T> data, PagingInfo paging)
        {
            GridCleaner();
            ReadProps(data);
            PrepareData(data, PropsList, null);
            grid = Generate(rows.GetRows());
            pager = Pager(paging);
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
            if (columns != null)
            {
                foreach(var item in data)
                {
                    Row row = new Row("<tr>","</tr>");
                    foreach(var column in columns)
                    {
                        foreach(var prop in props)
                        {
                            if(prop.Name == "id" || prop.Name == "ID" || prop.Name == "Id") row._prefix = "<tr id='" + MessageControl.Encode(prop.GetValue(item,null).ToString()) + "'>";
                            if(prop.Name == column) row.AddCell(new Cell("<td>","</td>", prop.GetValue(item,null).ToString()));
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
                outer += row._prefix + inner + row._postfix;
            }

            return "<div class='scrollbar-outer'><table class='table table-striped'>" + outer + "</table></div>";
        }
        static string Pager(PagingInfo paging)
        {
            string pager = "";
            for(int i = 1; i <= paging.pageTotal; i++)
            {
                if (i == paging.pageNumber) pager += "<li class='active'><a></a></li>";
                else pager += "<li><a href='/"+paging.controller+"/"+paging.action+"?page="+i+"'></a></li>";
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
#endregion
    }
}
