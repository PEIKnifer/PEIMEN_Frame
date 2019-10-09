/////////////////////////////////////////////////
//
//PEIMEN Frame System || SimpleTool branch 
//
//creat by PEIKnifer[.CN]
//
//SimpleTool for ToolCPU
//
//Create On 2019-10-9 15:40:42
//
//Last Update in 2019-10-9 15:40:50  
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEIWWW : PEINet_Origin
{
    static PEIWWW instance;
    public static PEIWWW Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GetIns<PEIWWW>();
            }
            return instance;
        }
    }
}
