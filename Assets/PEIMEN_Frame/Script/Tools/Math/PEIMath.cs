/////////////////////////////////////////////////
//
//PEIMEN Frame System || SimpleTool branch 
//
//creat by PEIKnifer[.CN]
//
//SimpleTool for PEIKnifer Math Class
//
//Create On 2019-10-9 17:51:55
//
//Last Update in 2019-10-9 17:51:55  
//
/////////////////////////////////////////////////
using PEIKTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PEIKnifer Math Class
/// </summary>
public class PEIMath : PEIKnifer_Singleton
{
    /// <summary>
    /// PEIMath Class Random Object
    /// </summary>
    private static PEIRNG _random;

    public static PEIRNG Random
    {
        get
        {
            PEIKnifer_SingletonTool.CheckIns_Normal(_random, () => { _random = new PEIRNG(); });
            return _random;
        }
    }
}
