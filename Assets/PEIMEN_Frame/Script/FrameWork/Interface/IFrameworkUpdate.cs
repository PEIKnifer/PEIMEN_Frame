using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Obsolete("Using PEIE_FrameworkAwake Event Listener To Update Your Update Func", true)]
public interface IFrameworkUpdate : PEIKnifer_Interface
{
    void FrameworkUpdate();
}
