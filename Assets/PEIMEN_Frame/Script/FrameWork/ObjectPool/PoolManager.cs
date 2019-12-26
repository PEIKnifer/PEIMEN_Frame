using PEIMEN.Origin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : PEIModel_Origin
{
    private readonly Dictionary<int, ObjectData_Origin> _allObjectPool;

    public PoolManager()
    {
        _allObjectPool = new Dictionary<int, ObjectData_Origin>();
    }

    public T Spawn<T>() where T : class, new()
    {
        ObjectData_Origin objectData;
        int hashCode = typeof(T).GetHashCode();
        if (!_allObjectPool.TryGetValue(hashCode, out objectData))
        {
            objectData = new ObjectData<T>();
            _allObjectPool[hashCode] = objectData;
        }
        ObjectData<T> data = objectData as ObjectData<T>;
        return data.Spawn();
    }

    public void DeSpawn<T>(T obj) where T : class, new()
    {
        ObjectData_Origin objectData;
        int hashCode = typeof(T).GetHashCode();
        if (_allObjectPool.TryGetValue(hashCode, out objectData))
        {
            ObjectData<T> data = objectData as ObjectData<T>;
            data.DeSpawn(obj);
        }
    }

    public void Clear<T>() where T : class, new()
    {
        ObjectData_Origin objectData;
        int hashCode = typeof(T).GetHashCode();
        if (_allObjectPool.TryGetValue(hashCode, out objectData))
        {
            objectData.Clear();
            _allObjectPool.Remove(hashCode);
        }
    }


    public override void OnClose()
    {
        foreach (var item in _allObjectPool.Values)
        {
            item.Clear();
        }
        _allObjectPool.Clear();
    }

}