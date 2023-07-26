using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolBehavior : AthenaMonoBehavior
{
    protected override  void Start()
    {
        base.Start();
        _gameManager.AddTimedEvent(5, new Action(()=>
        {
            StartCoroutine(CleanPools());
        }), gameObject);
    }

    private IEnumerator CleanPools()
    {
        foreach (var poolSection in _pool.Values)
        {
            if (poolSection.GameObjects.Count(x => x.activeInHierarchy == false) > poolSection.GameObjects.Count * 3f / 4f)
            {
                int numEnabledItemsToRemove = (int)Math.Floor(poolSection.GameObjects.Count / 2f);
                if (numEnabledItemsToRemove > 0)
                {

                    poolSection.GameObjects.RemoveAll(item =>
                        item.activeInHierarchy == false && numEnabledItemsToRemove-- > 0 && (DestroyBool(item)
                        ));
                }
            }

            yield return null;
        }
    }

    private bool DestroyBool(GameObject go)
    {
        Destroy(go);
        return true;
    }


    [SerializeField]
    private readonly Dictionary<string, PoolContainer> _pool= new ();
    public GameObject GetPooledObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        var poolName = prefab.name;

        if (!_pool.TryGetValue(poolName, out var pool))
        { 
            pool= new PoolContainer
            {
                Container = new GameObject(poolName)
                {
                    transform =
                    {
                        parent = transform
                    }
                }
            };
            _pool.Add(poolName, pool);
        }
        
        var item = pool.GameObjects.FirstOrDefault(x => x.activeInHierarchy==false);

        if (item == null)
        {
            var obj = Instantiate(prefab, position, rotation);
            obj.transform.parent = pool.Container.transform;
            pool.GameObjects.Add(obj);
            return obj;
        }
        else
        {
            item.transform.SetPositionAndRotation(position, rotation);
            item.SetActive(true);
            return item;
        }
    }

    private class PoolContainer
    {
        public GameObject Container;
        public readonly List<GameObject> GameObjects= new ();

    }


}
