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
//Last Update in 2019-12-5 18:08:04  
//
/////////////////////////////////////////////////

using PEIKTS;
using PEIMEN;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// AllLibrary Class
/// </summary>
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
/// <summary>
/// PEIKnifer singleton Origin
/// </summary>
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
    protected static T GetIns<T>() where T : PEIKnifer
    {
        //if (!obj)
        //{
        T obj;
        var g = new GameObject("PEIMEN_Singleton");
        obj = (T)g.AddComponent<T>();
        Type t = typeof(T);
        PEIKDE.Log("Singleton", "Get Ins Init With " + t);
        // }
        return obj;
    }
    protected static T GetIns<T>(bool entityFlag) where T : PEIKnifer
    {
        T obj;
        if (entityFlag)
        {
            obj = (T)PEIMEN_Entity.Ins.gameObject.AddComponent<T>();
            Type t = typeof(T);
            PEIKDE.Log("Singleton", "Get Entity Ins Init With " + t);
        }
        else
        {
            var g = new GameObject("PEIMEN_Singleton");
            obj = (T)g.AddComponent<T>();
            Type t = typeof(T);
            PEIKDE.Log("Singleton", "Get Ins Init With " + t);
        }
        return obj;
    }
}
/// <summary>
/// PEIKnifer singleton Tool
/// </summary>
public class PEIKnifer_SingletonTool
{
    public static T GetInsWithTag<T>(string tag)
    {
        T t = GameObject.FindGameObjectWithTag(tag).GetComponent<T>();
        try { t.GetType(); }
        catch
        {
            PEIKDE.LogError("SLT", "Singleton Miss With Tag " + tag + " (Check your script Ins)");
        }
        return t;
    }
    public static T GetIns<T>() where T : PEIKnifer
    {
        //if (!obj)
        //{
        T obj;
        var g = new GameObject("PEIMEN_Singleton");
        obj = (T)g.AddComponent<T>();
        Type t = typeof(T);
        PEIKDE.Log("Singleton", "Get Ins Init With " + t);
        // }
        return obj;
    }
    public static bool CheckIns_Normal(object obj,Action InsFunc)
    {
        if (obj!=null)
        {
            return true;
        }
        try
        {
            InsFunc();
            return true;
        }
        catch
        {
            PEIKDE.LogError("PSL","Check Ins Func Error");
            return false;
        }
    }
}
#endregion


#region  Unity RePackaging


/// <summary>
/// PEIKnifer Bool Flag Origin 
/// </summary>
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
/// <summary>
/// PEIKnifer GameObject Pool Ins Origin 
/// </summary>
[Serializable]
public abstract class GameObjectPoolInsBase : PEIKnifer
{
    public abstract void Refresh();
    public abstract void ReadyInPool();
}

#endregion

#region  PEIKnifer NullFunction Class
/// <summary>
/// PEIKnifer Null Function Tool Class Origin 
/// </summary>
public class PEIKNF_NullFunction
{
    public static void NullFunction() { }
    public static void NullFunction(float[] floAry) { }
}

#endregion
#region  PEIKnifer NullFunction Class
/// <summary>
/// PEIKnifer Time Class Origin 
/// </summary>
public class PEITime : PEIKnifer
{
    private static PEITime _ins;

    public static float DeltaTime { get; private set; }

    public static bool WeekUp()
    {
        if (_ins)
            return true;

        try
        {
            _ins = Instantiate(new GameObject()).AddComponent<PEITime>();
            DeltaTime = Time.deltaTime;
            //PEIKDE.Log("PTM", "PEIKTM Time Class Week Up");
            return true;
        }
        catch {
            return false;
        }
    }
    private void Update()
    {
        DeltaTime = Time.deltaTime;
    }
}

#endregion