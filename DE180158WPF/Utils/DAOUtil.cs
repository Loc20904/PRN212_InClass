using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace DE180158WPF.Utils
{
    delegate bool checkExist<T>(T item);
    public static class DAOUtil
    {
        public static bool CheckExist<T>(this List<T> list, Func<T, bool> check)
        {
            if (list == null || list.Count == 0)
                return false;

            foreach (var item in list)
            {
                if (check(item))
                    return true;
            }

            return false;
        }
    }
}
