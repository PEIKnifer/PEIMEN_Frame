/////////////////////////////////////////////////
//
//PEIMEN Frame System || Template AnimeUI branch 
//
//creat by PEIKnifer[.CN]
//
//Template AnimeUI for Anime UI Base
//
//Create On 2019-4
//
//Last Update in 2019 4 25 14:42:46
//
/////////////////////////////////////////////////

using PEIKDL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTL_AU
{
    public abstract class AnimeUIBase : PEIKnifer
    {

        public bool AnimeDoneFlag;
        protected PEIKnifer_Delegate_Void_Void _del;
        public Transform Target;
        protected Vector3 _oldPosition;

        public abstract void Close();
        public abstract void Open();
        protected abstract void Closing();
        protected abstract void Opening();
    }
}
