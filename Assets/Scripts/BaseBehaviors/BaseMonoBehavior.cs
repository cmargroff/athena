using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BaseMonoBehavior: MonoBehaviour
{
    protected void SafeAssigned<T>(T obj)
    {
        if (obj == null || obj.ToString() == "null")
        {
            throw new System.Exception($"{typeof(T).Name} not assigned in editor");
        }
    }
    protected T SafeFindObjectOfType<T>() where T : UnityEngine.Object
    {
        var obj = FindObjectOfType<T>();
        if (obj == null || obj.ToString() == "null")
        {
            throw new System.Exception($"{typeof(T).Name} not found in world");
        }
        return obj;
    }


}

