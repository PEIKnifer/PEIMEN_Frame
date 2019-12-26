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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PEIMEN.Origin
{
    public class PEIJert_Origin : PEIKnifer_Origin
    {
        private bool _flag;
        private PEIMEN_STC_Jert _jert;
        public PEIJert_Origin(PEIMEN_STC_Jert jert)
        {
            _jert = jert;
            _jert.AddJert(this);
        }
        public void SetJert(PEIMEN_STC_Jert jert)
        {
            if (_jert != null)
            {
                if(_jert.HasJert(this))
                _jert.RemoveJert(this);
            }
            _jert = jert;
            _jert.AddJert(this);
        }

        public bool Flag
        {
            get => _flag;
            set
            {
                _flag = value;
                _jert.Check();
            }
        }
        public void Clear()
        {
            _jert.RemoveJert(this);
        }
    }
}
