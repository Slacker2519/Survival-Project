using Elite.GangGang.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectPool
{
    public string PoolName;
    public GameObject Prefab;
    public List<GameObject> PoolList;

    public ObjectPool(string poolName, GameObject prefab)
    {
        PoolName = poolName;
        Prefab = prefab;
        PoolList = new List<GameObject>();
    }
}

public class PoolManager : SingletonMono<PoolManager>
{
    // List containts object after spawned
    private List<ObjectPool> _pools = new List<ObjectPool>();

    private void Awake()
    {
        LoadDataAssets();
    }
    
    //Loop through assets data and get all prefabs to pooled it
    public void LoadDataAssets()
    {
        DataAssets asset = ConfigDataManager.Instance.DataAssets;

        foreach(var item in asset.ConfigCharactersStatList)
        {
            var pool = new ObjectPool(item.Name.ToString(), item.Prefab);
            _pools.Add(pool);
            new GameObject(pool.PoolName).transform.SetParent(transform);
        }

        foreach (var item in asset.ConfigEnemiesStatList) 
        {
            var pool = new ObjectPool(item.Name.ToString(), item.prefab); 
            _pools.Add(pool);
            new GameObject(pool.PoolName).transform.SetParent(transform);
        }
    }

    public void ClearPoolsData()
    {
        _pools.Clear();
    }

    public GameObject SpawnCharacter(CharacterEnum characterName, Transform parent = null)
    {
        return GetObjectFromPool(characterName.ToString(), parent);
    }

    public GameObject SpawnEnemy(EnemyEnum enemyName, Transform parent = null)
    {
        return GetObjectFromPool(enemyName.ToString(), parent);
    }

    private GameObject GetObjectFromPool(string poolName, Transform parent = null)
    {
        var pool = _pools.Find(x => x.PoolName.Equals(poolName));
        GameObject obj = null;
        if (pool == null)
        {
            Debug.LogError("Pool name invalid " + poolName.ToString());
        }
        else
        {
            if (parent == null)
            {
                parent = GameUtilities.GetComponentByName<Transform>(transform.gameObject, poolName.ToString());
            }
            obj = pool.PoolList.Find(x => !x.activeSelf);
            if (obj == null)
            {
                obj = Instantiate(pool.Prefab);
                obj.name = poolName;
                pool.PoolList.Add(obj);
            }
            obj.transform.SetParent(parent);
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(true);
        }
        return obj;
    }

    // Return object to pool after not use it
    public void ReturnObject(GameObject obj, string poolName = "")
    {
        if (string.IsNullOrEmpty(poolName))
        {
            poolName = obj.name;
        }
        obj.SetActive(false);
        Transform parent = GameUtilities.GetComponentByName<Transform>(gameObject, poolName);
        if (parent != null)
        {
            obj.transform.SetParent(parent.transform);
        }
    }

    //Return all objects have spawned to pool
    public void ReturnAllObjects()
    {
        foreach (var pool in _pools)
        {
            foreach (var obj in pool.PoolList)
            {
                ReturnObject(obj, pool.PoolName);
            }
        }
    }
}
