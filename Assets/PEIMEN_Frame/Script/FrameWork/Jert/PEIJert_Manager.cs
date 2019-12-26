/////////////////////////////////////////////////
//
//PEIMEN Frame System || SimpleTool branch 
//
//creat by PEIKnifer[.CN]
//
//SimpleTool for Jert Module
//
//Create On 2019-12-5
//
//Last Update in 2019-12-5 18:07:50 
//
/////////////////////////////////////////////////

using PEIKTS;
using PEIMEN.Origin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PEIMEN.Origin
{
    public class PEIJert_Manager : PEIModel_Origin
    {
        // Start is called before the first frame update
        private Dictionary<string, PEIMEN_STC_Jert> _jerts;
        private PEIMEN_STC_Jert _toolJert;
        public PEIJert_Manager()
        {
            _jerts = new Dictionary<string, PEIMEN_STC_Jert>();
        }

        public PEIMEN_STC_Jert Get(string key)
        {
            _toolJert = null;
            _jerts.TryGetValue(key, out _toolJert);
            return _toolJert;
        }
        public bool Set(string key, Action a)
        {
            if (_jerts.ContainsKey(key))
                return false;
            _jerts.Add(key, new PEIMEN_STC_Jert(a));
            return true;
        }
        public bool Set(string key, Action a, bool flag)
        {
            if (_jerts.ContainsKey(key))
                return false;
            _jerts.Add(key, new PEIMEN_STC_Jert(a, flag));
            return true;
        }

        public override void OnClose()
        {
            _jerts.Clear();
        }
    }
}