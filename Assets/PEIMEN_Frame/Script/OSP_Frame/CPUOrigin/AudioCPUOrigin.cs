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
using PEIKTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PEIKBF_CPU
{
    [RequireComponent(typeof(AudioPoolManager))]
    public abstract class AudioCPUOrigin : PEIKnifer
    {
        protected GameObject InstanceAudio(GameObject ins,string type,Vector3 position,Quaternion rotation)
        {
            try
            {
                return AudioPoolManager.Ins.Instance(ins, type, position, rotation);
            }
            catch
            {
                PEIKDE.LogError("ACO", "Audio Instance Error With Type --> "+type);
                return null;
            }
        }
    }
}
