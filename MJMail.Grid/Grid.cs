using MJmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Grid
{
    public class Grid
    {
        static List<PropertyInfo> PropsList = new List<PropertyInfo>();
        public static void Create<T>(List<T> data)
        {
            ReadProps<T>(data);
        }

        #region Property
        static void ReadProps<T>(IEnumerable<T> source)
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
