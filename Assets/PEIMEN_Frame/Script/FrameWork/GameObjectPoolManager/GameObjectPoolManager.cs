using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIMEN.Origin;
using System;
using System.Linq;

public class GameObjectPoolManager : PEIModel_Origin
{
    private Dictionary<string, GameObjectPool> _pool;
    private GameObjectPool _toolPool;
    private GameObject _toolObj;
    private int _poolNum =99999;

    public GameObjectPoolManager()
    {
        _pool = new Dictionary<string, GameObjectPool>();
    }
    public GameObject Instance(string token, GameObject obj)
    {
        Find(token);
        return _toolPool.GetObj(obj);
    }
    public void SetPoolNum(int poolNum)
    {
        _poolNum = poolNum;
        if (_pool.Count > 0)
            foreach (var item in _pool)
            {
                item.Value.SetPoolNum(_poolNum);
            }
    }
    public void SetPoolNum(string token, int poolNum)
    {
        _poolNum = poolNum;
        Find(token).SetPoolNum(poolNum);
    }
    public GameObject Instance(string token, GameObject obj,Transform parent)
    {
        Find(token);
        _toolObj = _toolPool.GetObj(obj);
        _toolObj.transform.SetParent(parent);
        return _toolObj;
    }
    public GameObject Instance(string token, GameObject obj, Transform parent,Vector3 position,Quaternion rotation)
    {
        Find(token);
        _toolObj = _toolPool.GetObj(obj);
        _toolObj.transform.position = position;
        _toolObj.transform.rotation = rotation;
        _toolObj.transform.SetParent(parent);
        return _toolObj;
    }
    public GameObject Instance(string token, GameObject obj, Vector3 position, Quaternion rotation)
    {
        Find(token);
        _toolObj = _toolPool.GetObj(obj);
        _toolObj.transform.position = position;
        _toolObj.transform.rotation = rotation;
        return _toolObj;
    }
    public GameObject Instance(string token, GameObject obj, Vector3 position)
    {
        Find(token);
        _toolObj = _toolPool.GetObj(obj);
        _toolObj.transform.position = position;
        return _toolObj;
    }

    public void Destory(string token, GameObject obj)
    {
        Find(token);
        _toolPool.DestoryObj(obj);
    }

    public void Clear()
    {
        for (int index = 0; index < _pool.Count; index++)
        {
            var item = _pool.ElementAt(index);
            try
            {
                item.Value.Destory();
            }
            catch
            {
                _pool.Remove(item.Key);
            }
        }
    }

    private GameObjectPool Find(string key)
    {
        if (_pool.ContainsKey(key))
        {
            _toolPool = _pool[key];
        }
        else
        {
            _toolPool = new GameObjectPool(key, _poolNum);
            _pool.Add(key, _toolPool);
        }
        return _toolPool;
        //return false;
    }

    public override void OnClose()
    {
        Clear();
    }
}
