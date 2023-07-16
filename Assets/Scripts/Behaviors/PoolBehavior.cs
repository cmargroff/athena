using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolBehavior : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, List<GameObject>> _pool= new Dictionary<string, List<GameObject>>();
    public GameObject GetPooledObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        var name = prefab.name;

        if (!_pool.TryGetValue(name, out var list))
        { 
            list= new List<GameObject>();
            _pool.Add(name, list);
        }
        
        var item = list.FirstOrDefault(x => x.activeInHierarchy==false);

        if (item == null)
        {
            var obj = Instantiate(prefab, position, rotation);
            obj.transform.parent = transform;
            list.Add(obj);
            return obj;
        }
        else
        {
            item.transform.position = position;
            item.transform.rotation = rotation;
            item.SetActive(true);
            return item;
        }

        
    }
}
