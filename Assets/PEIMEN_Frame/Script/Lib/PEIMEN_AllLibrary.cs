/////////////////////////////////////////////////
//
//PEIMEN Frame System || Library AllLibrary 
//
//creat by PEIKnifer[.CN]
//
//AllLibrary for Library
//
//Create On 2019-3
//
//Last Update in 2019.3.14 18:01:34
//
/////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//AllLibrary Class
public class PEIMEN_AllLibrary {

    //PEIKnifer All Library Class Example In There;
     
     //Frame Event Namespace
    #region PEIFET Namespace
    PEIKAL_ET.PEI_BoolEvent PEI_BoolEvent ;
    PEIKAL_ET.PEI_FloatEvent PEI_FloatEvent;
    PEIKAL_ET.PEI_IntEvent PEI_IntEvent;
    PEIKAL_ET.PEI_StringEvent PEI_StringEvent;
    #endregion

    //Frame Small Simple Project Namespace
    #region PEIKBF_SSP Namespace
    PEIKBF_SSP.GamePad GamePad;
    PEIKBF_SSP.OperationBase OperationBase;
    PEIKBF_SSP.OperationPartBase OperationPartBase;
    PEIKBF_SSP.PCDTestControl PCDTestControl;
    PEIKBF_SSP.StepAudioManager StepAudioManager;
    PEIKBF_SSP.StepBasisCompment StepBasisCompment;
    PEIKBF_SSP.StepLoader StepLoader;
    PEIKBF_SSP.StepManager StepManager;
    #endregion

    //Frame Simple Tools Namespace
    #region PEIKTS Namespace
    PEIKTS.PEIKnifer_BTNSimple PEIKnifer_BTNSimple;
    PEIKTS.PEIKnifer_CharacterControl PEIKnifer_CharacterControl;
    PEIKTS.PEIKnifer_GeneralObjFollow PEIKnifer_GeneralObjFollow;
    PEIKTS.PEIKnifer_ObjSimpleMove PEIKnifer_ObjSimpleMove;
    PEIKTS.PEIKnifer_ObjSimpleRotate PEIKnifer_ObjSimpleRotate;
    PEIKTS.PEIKnifer_RigidbodyControl PEIKnifer_RigidbodyControl;
    PEIKTS.PEIKnifer_SimpleCamera PEIKnifer_SimpleCamera;
    PEIKTS.PEIKnifer_XMLCenterControl PEIKnifer_XMLCenterControl;
    #endregion

    //Frame Private Debug System
    #region PEIKDE Namespace
    PEIKDE PEIKDE;
    #endregion

    //Frame Singleton Class
    #region PEIKnifer Singleton Class
    PEIKnifer_Singleton PEIKnifer_Singleton;
    #endregion

    //Frame Custom Class
    #region PEIKnifer Custom Class
    GameObjectPoolInsBase GameObjectPoolInsBase;
    #endregion

}


#region  StateAll
public enum PEIKEM_PartBaseStatus
{
    Done = 0,
    NeedOperation = 1,
}
public enum PEIKEM_ObjSimpleMoveState
{
    simple,
    position
}
public enum GeneralObjFollowObjChange
{
    Drag,
    Script
}
public enum GeneralObjFollowState
{
    immediately,
    delayed,
    monitor
}
public enum ObjSimpleRotateState
{
    simple,
    position
}
#endregion


#region  Extend Unity Event
namespace PEIKAL_ET
{
    [System.Serializable]
    public class PEI_IntEvent : UnityEvent<int>
    {

    }

    [System.Serializable]
    public class PEI_StringEvent : UnityEvent<string>
    {

    }

    [System.Serializable]
    public class PEI_FloatEvent : UnityEvent<float>
    {
    }

    [System.Serializable]
    public class PEI_BoolEvent : UnityEvent<bool>
    {
    }
}
#endregion


#region  PEIKDL Tools Delegate 
namespace PEIKDL
{
    [SerializeField]
    public class PEIKniferDelegateTool : PEIKnifer
    {
        PEIKnifer_Delegate_Void_Void dele;
        public event PEIKnifer_Delegate_Void_Void MyEvent
        {
            add { dele += value; }
            remove { dele -= value; }
        }
    }
    [SerializeField]
    public delegate void PEIKnifer_Delegate_Void_Void();
    [SerializeField]
    public delegate void PEIKnifer_Delegate_Void_Int1(int i);
    [SerializeField]
    public delegate void PEIKnifer_Delegate_Void_Float1(float i);
    [SerializeField]
    public delegate GameObject PEIKnifer_Delegate_GameObject_Void();
    [SerializeField]
    public delegate void PEIKnifer_Delegate_Void_T<T>(T t);
}
#endregion


#region  PEIKnifer singleton
public class PEIKnifer_Singleton : PEIKnifer
{
    protected static T GetInsWithTag<T>(string tag)
    {
        T t = GameObject.FindGameObjectWithTag(tag).GetComponent<T>();
        try { t.GetType(); }catch
        {
            PEIKDE.LogError("SLT", "Singleton Miss With Tag " + tag + " (Check your script Ins)");
        }
        return t;
    }
}
#endregion


#region  Unity RePackaging

[Serializable]
public class PEIKnifer_Flag
{
    [SerializeField]
    public bool Flag
    {
        set
        {
            if (_flag != value)
            {
                _flag = value;
                if (_flag)
                {
                    if (_trueDel != null)
                        _trueDel();
                }
                else
                {
                    if (_falseDel != null)
                        _falseDel();
                }
            }
        }
        get { return _flag; }
    }
    [SerializeField]
    private bool _flag;
    private PEIKDL.PEIKnifer_Delegate_Void_Void _trueDel = null;
    private PEIKDL.PEIKnifer_Delegate_Void_Void _falseDel = null;
    public PEIKnifer_Flag(PEIKDL.PEIKnifer_Delegate_Void_Void trueDel, PEIKDL.PEIKnifer_Delegate_Void_Void falseDel)
    {
        _trueDel = trueDel;
        _falseDel = falseDel;
    }
    public PEIKnifer_Flag(PEIKDL.PEIKnifer_Delegate_Void_Void del, bool TOrF)
    {
        if (TOrF)
        {
            _trueDel = del;
        }
        else
        {

            _falseDel = del;
        }
    }
    public PEIKnifer_Flag()
    {

    }
    public void ClearAllDel()
    {
        _trueDel = null;
        _falseDel = null;
    }
}

#endregion


#region  PEIKnifer Custom Class
[Serializable]
public abstract class GameObjectPoolInsBase : PEIKnifer
{
    public abstract void Refresh();
    public abstract void ReadyInPool();
}

#endregion

#region  PEIKnifer NullFunction Class

public class PEIKNF_NullFunction
{
    public static void NullFunction() { }
    public static void NullFunction(float[] floAry) { }
}

#endregion