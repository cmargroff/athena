using System.Collections.Generic;


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
    public static TValue GetValueOrDefault<TKey,TValue>(this Dictionary<TKey,TValue> dic, TKey key)
    {
        if (dic.TryGetValue(key, out TValue value))
        {
            return value;
        }
        else
        {
            return default;
        }

    }
}

