using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData<T> : ObjectData_Origin where T : class, new()
{
    private Stack<T> _objects = new Stack<T>();

    public T Spawn()
    {
        if (_objects.Count > 0)
            return _objects.Pop();
        else
            return new T();
    }

    public void DeSpawn(T obj)
    {
        _objects.Push(obj);
    }

    public override void Clear()
    {
        _objects.Clear();
    }

}
