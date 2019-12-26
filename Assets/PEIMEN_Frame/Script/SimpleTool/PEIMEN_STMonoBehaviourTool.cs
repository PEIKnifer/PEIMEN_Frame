/////////////////////////////////////////////////
//
//PEIMEN Frame System || GeneralObjMove branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for GeneralObjFollow
//
//Create On 2019-12-5
//
//Last Update in 2019-12-5 18:07:03
//
/////////////////////////////////////////////////


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIMEN.Interface;
using System;

namespace PEIKTS
{
    public class PEIMEN_STMonoBehaviourTool : PEIKnifer_Singleton, IModuleClose
    {
        private static PEIMEN_STMonoBehaviourTool _ins;
        private List<Action> _onGui;
        public static PEIMEN_STMonoBehaviourTool Ins
        {
            get
            {
                if (!_ins)
                    _ins = GetIns<PEIMEN_STMonoBehaviourTool>(true);
                return _ins;
            }
        }
        public void Init()
        {
            _onGui = new List<Action>();
        }
        public void OnClose()
        {
            Destroy(_ins);
        }

        public void OnGUI()
        {
            for (int i = 0; i < _onGui.Count; i++)
            {
                _onGui[i]();
            }
        }
        
        public void AddOnGUIAction(Action a)
        {
            _onGui.Add(a);
        }
        public void RemoveOnGUIAction(Action a)
        {
            if(_onGui.Contains(a))
            _onGui.Remove(a);
        }
        public void StartCoroutineFunc(string name)
        {
            StartCoroutine(name);
        }
        public void StartCoroutineFunc(IEnumerator func)
        {
            StartCoroutine(func);
        }
        public void DestroyObj(UnityEngine.Object obj)
        {
            Destroy(obj);
        }
        public void InstanceObj(UnityEngine.Object obj)
        {
            Instantiate(obj);
        }
    }
}
