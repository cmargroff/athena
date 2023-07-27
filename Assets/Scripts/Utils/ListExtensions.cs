using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static  class ListExtensions
{
    public static T GetValueOrDefault<T>(this List<T> list, int pos)
    {
        if (pos < list.Count)
        {
            return list[pos];
        }
        else
        {
            return default;
        }

    }

}

