using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIMEN.Interface
{
    [Obsolete("Using PEIE_FrameworkAwake Event Listener To Awake Your Init", true)]
    public interface IFrameworkAwake : PEIKnifer_Interface
    {
        void FrameworkAwake();
    }
}