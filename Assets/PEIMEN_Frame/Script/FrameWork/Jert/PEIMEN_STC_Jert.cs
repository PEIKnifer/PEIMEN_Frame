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

using PEIKDL;
using PEIMEN.Origin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTS
{
    public class PEIMEN_STC_Jert : PEIKnifer_Origin
    {
        private PEIKnifer_Flag _flag;
        private bool _tfFlag;
        private PEIKnifer_Delegate_Void_Void _del;
        private Action _actions;
        private List<PEIJert_Origin> _jerts;
        public bool TFFlag
        {
            get { return _tfFlag; }
        }

        public bool Flag
        {
            get => _flag.Flag;
        }

        public PEIMEN_STC_Jert(Action a)
        {
            _tfFlag = true;
            Init(a);
        }
        /// <summary>
        /// 初始化指示变量型Jert
        /// </summary>
        /// <param name="_tfFlag"></param>
        public PEIMEN_STC_Jert(Action a, bool tfFlag)
        {
            _tfFlag = tfFlag;
            Init(a);
        }
        private void Init(Action a)
        {
            _actions = a;
            _del = Done;
            _flag = new PEIKnifer_Flag(_del, _tfFlag);
            _jerts = new List<PEIJert_Origin>();
        }

        public void Check()
        {
            for (int i = 0; i < _jerts.Count; i++)
            {
                if (_jerts[i].Flag != _tfFlag)
                {
                    _flag.Flag = !_tfFlag;
                    return;
                }
            }
            _flag.Flag = _tfFlag;
        }

        public void AddJert(PEIJert_Origin mem)
        {
            _jerts.Add(mem);
        }
        public bool RemoveJert(PEIJert_Origin mem)
        {
            return _jerts.Remove(mem);
        }
        public bool HasJert(PEIJert_Origin mem)
        {
            return _jerts.Contains(mem);
        }
        private void Done()
        {
            _actions();
        }
    }
}