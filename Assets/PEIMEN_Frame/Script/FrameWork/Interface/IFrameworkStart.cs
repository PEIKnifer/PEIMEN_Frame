using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIMEN.Interface
{
    [Obsolete("Using PEIE_FrameworkAwake Event Listener To Start Your Init", true)]
    public interface IFrameworkStart : PEIKnifer_Interface
    {
        void FrameworkStart();
    }
}