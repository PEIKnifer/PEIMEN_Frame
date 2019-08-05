/////////////////////////////////////////////////
//
//PEIMEN Frame System || GeneralObjMove branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for SimpleObjMove
//
//Create On 2019 4 4
//
//Last Update in 2019 4 4 16:17:09
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTS
{
    public class AudioPoolManager : PoolManager
    {
        public static AudioPoolManager Ins;
        // Use this for initialization
        void Awake()
        {
            FrameInitAwake();
            Ins = this;
        }
    }
}