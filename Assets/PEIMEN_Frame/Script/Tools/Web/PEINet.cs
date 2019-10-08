using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEINet : PEINet_Origin
{
    static PEINet instance;
    public static PEINet Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GetIns<PEINet>();
            }
            return instance;
        }
    }
}
