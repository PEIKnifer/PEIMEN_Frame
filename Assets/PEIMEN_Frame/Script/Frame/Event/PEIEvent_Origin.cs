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