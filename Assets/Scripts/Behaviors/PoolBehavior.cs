using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolBehavior : AthenaMonoBehavior
{
    private readonly Dictionary<string, PoolContainer> _pool = new();
    private bool DestroyBool(GameObject go)
    {
        Destroy(go);
        return true;
    }
    public GameObject GetPooledObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        var poolName = prefab.name;

        if (!_pool.TryGetValue(poolName, out var pool))
        {
            pool = new PoolContainer
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

        var item = pool.GameObjects.FirstOrDefault(x => x.activeInHierarchy == false);

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

            var stateMachine = item.GetComponent<BaseStateMachineBehavior>();
            stateMachine?.ResetInitialState();

            return item;
        }
    }
    private class PoolContainer
    {
        public GameObject Container;
        public readonly List<GameObject> GameObjects = new();
    }
}