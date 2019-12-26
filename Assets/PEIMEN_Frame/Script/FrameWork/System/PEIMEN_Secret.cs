/////////////////////////////////////////////////
//
//PEIMEN Frame System || PEI Test branch 
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
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIKDL;
using PEIMEN.Origin;
using PEIKTS;
using System;

namespace PEIMEN.Test
{
    public class PEIMEN_Secret : PEIModel_Origin
    {
        private PEIKnifer_LOrigin _l;
        private PEIKnifer_Delegate_Void_Void _del;
        private string _key = "PEIMEN";
        private List<PEIKey_Origin> _keyL;
        private int _toolNum;
        private bool flag;
        private PEIKnifer_Timer _t;
        public PEIMEN_Secret()
        {
            Init();
        }

        public void ChangeKey(string key)
        {
            _key = key;
            if (_keyL != null)
            {
                _keyL.Clear();
                PEIMEN_Entity.L.RemoveL(_l);
                _l.RemoveElement(Update);
                Init();
            }
            else
                PEIKDE.LogError("Secret","Change Key Error");
        }

        public void Init()
        {
            _toolNum = 0;
            _keyL = new List<PEIKey_Origin>();
            _l = PEIMEN_Entity.L.AddL();
            _l.AddElement(Update);
            _t = new PEIKnifer_Timer();
            _t.SetTime(1);
            flag = false;
            //_del = Step1;
            for (int i = 0; i < _key.Length; i++)
            {
                _keyL.Add(new PEIKey_Origin(_key[i].ToString(), Check));
            }
            _keyL[_key.Length - 1].A = End;
            _del = _keyL[0].Check;
            //PEIKDE.Log("Secret", "Init Done");
        }

        public void Update()
        {
            _del();
            if (_t.Timer())
            {
                ChangeZero();
            }
        }
        public void ChangeZero()
        {
            _t.Clear();
            _toolNum = 0;
            _del = _keyL[_toolNum].Check;
        }
        public void Check()
        {
            _toolNum++;
            _del = _keyL[_toolNum].Check;
            _t.Clear();
        }
        public void End()
        {
            flag = !flag;
            PEIMEN_Entity.Event.Trigger(this,new PEIE_Secret() { flag = flag });
            _del = _keyL[0].Check;
            _t.Clear();
            _toolNum = 0;
            //PEIKDE.Log("Secret", flag);
        }

        public override void OnClose()
        {
            PEIMEN_Entity.L.RemoveL(_l);
            _l.RemoveElement(Update);
        }
    }
    public class PEIKey_Origin
    {
        public string _keyCode;
        public Action A;
        public PEIKey_Origin(string key,Action a)
        {
            _keyCode = key;
            A = a;
        }
        public void Check()
        {
            //PEIKDE.Log("Secret",_keyCode);
            if (Input.GetKey(StringConvertToEnum(_keyCode)))
            {
                A();
            }
        }
        private KeyCode StringConvertToEnum(string str)
        {
            KeyCode color = KeyCode.A;
            try
            {
                color = (KeyCode)Enum.Parse(typeof(KeyCode), str);
            }
            catch (Exception ex)
            {
                PEIKDE.LogError(ex.Message);
                return color;
            }

            return color;
        }
    }
}
