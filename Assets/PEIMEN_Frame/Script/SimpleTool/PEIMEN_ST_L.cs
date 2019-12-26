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
using PEIMEN.Origin;
using PEIKDL;
using System;
using PEIMEN.Interface;

namespace PEIKTS
{
    public class PEIMEN_ST_LManager : PEIModel_Origin,IUpdate
    {
        public List<PEIKnifer_LOrigin> LList;
        public PEIMEN_ST_LManager()
        {
            LList = new List<PEIKnifer_LOrigin>();
        }

        public PEIKnifer_LOrigin AddL()
        {
            LList.Add(new PEIKnifer_LOrigin());
            return LList[LList.Count - 1];
        }
        public bool HasL(PEIKnifer_LOrigin l)
        {
            for (int i = 0; i < LList.Count; i++)
            {
                if (l.Equals(LList[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public void RemoveL(PEIKnifer_LOrigin l)
        {
            LList.Remove(l);
        }

        public override void OnClose()
        {

        }

        public void OnUpdate()
        {
            for (int i = 0; i < LList.Count; i++)
            {
                try
                {
                    LList[i].Update();
                }
                catch(Exception e)
                {
                    LList.RemoveAt(i);
                    PEIKDE.LogError("LManager","Fix Program Fixed L Class Bug Instance With Error "+ e);
                }
            }
        }
    }

    public class PEIKnifer_LOrigin:PEIKnifer_Origin
    {
        private List<Action> _del;
        public void Update()
        {
            for (int i = 0; i < _del.Count; i++)
            {
                _del[i]();
            }
        }
        public PEIKnifer_LOrigin()
        {
            _del = new List<Action>();
        }
        public void AddElement(Action func)
        {
            _del.Add(func);
        }
        public bool HasElement(Action func)
        {
            for (int i = 0; i < _del.Count; i++)
            {
                if (func.Equals(_del[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public void RemoveElement(Action func)
        {
            _del.Remove(func);
        }
    }
}
