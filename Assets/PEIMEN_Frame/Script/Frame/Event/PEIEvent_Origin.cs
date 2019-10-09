/////////////////////////////////////////////////
//
//PEIMEN Frame System || Event branch 
//
//creat by PEIKnifer[.CN]
//
//Event for Simple Event System
//
//Create On 2019-10-9 15:40:42
//
//Last Update in 2019-10-9 15:40:50  
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKEV
{
    public abstract class PEIEvent_Origin : PEIKnifer_Origin
    {
        public abstract int Id { get; }
    }

    public abstract class PEIEvent_VitrualArgs<T> : PEIEvent_Origin where T : PEIEvent_Origin
    {
        public override int Id
        {
            get { return typeof(T).GetHashCode(); }
        }
    }
}