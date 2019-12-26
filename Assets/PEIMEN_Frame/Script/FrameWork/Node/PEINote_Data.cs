using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIMEN.Origin
{
    public class PEINode_Data<T> : PEINode_DataOrigin
    {
        private readonly Dictionary<string, T> _nodeDatas = new Dictionary<string, T>();

        public T Get(string key, T defaultValue = default(T))
        {
            T t;
            if (!_nodeDatas.TryGetValue(key, out t))
                t = defaultValue;
            return t;
        }

        public void Set(string key, T value)
        {
            _nodeDatas[key] = value;
        }

        public bool Has(string key)
        {
            return _nodeDatas.ContainsKey(key);
        }

        public void Remove(string key)
        {
            if (_nodeDatas.ContainsKey(key))
                _nodeDatas.Remove(key);
        }

        public override void Clear()
        {
            _nodeDatas.Clear();
        }
    }
}