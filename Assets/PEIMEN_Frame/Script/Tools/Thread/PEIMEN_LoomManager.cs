/////////////////////////////////////////////////
//
//PEIMEN Frame System || PEI Loom branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for PEIMEN
//
//Create On 2019-12-5
//
//Last Update in 2019-12-9 18:40:47
//
/////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIMEN.Origin;
using PEIKTS;
using PEIMEN.Interface;
using System;
using System.Linq;

namespace PEIMEN.Origin
{
    public class PEIMEN_LoomManager : PEIModel_Origin,IUpdate
    {
        private Dictionary<string, PEIMEN_LoomOrigin> _loomDic;


        public PEIMEN_LoomManager()
        {
            _loomDic = new Dictionary<string, PEIMEN_LoomOrigin>();
        }
        public void RunAsync(string token, Action action, Action qMAction)
        {
            if (!_loomDic.ContainsKey(token))
            {
                _loomDic.Add(token, new PEIMEN_LoomOrigin(token,this));
            }
            _loomDic[token].RunAsync(() =>
            {
                action();
                _loomDic[token].QueueOnMainThread(qMAction);
            });
        }
        public override void OnClose()
        {
            _loomDic.Clear();
        }
        public bool RemoveLoom(string token)
        {
            if (_loomDic.ContainsKey(token))
            {
                var loom = _loomDic[token];
                _loomDic.Remove(token);
                return true;
            }
            return false;
        }
        public void OnUpdate()
        {
            if (_loomDic.Count > 0)
            {
                for (int index = 0; index < _loomDic.Count; index++)
                {
                    var item = _loomDic.ElementAt(index);
                    try
                    {
                        item.Value.Update();
                    }
                    catch
                    {
                        _loomDic.Remove(item.Key);
                    }
                }
            }
        }
    }
}
