/////////////////////////////////////////////////
//
//PEIMEN Frame System || GeneralObjMove branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for GeneralObjFollow
//
//Create On 2016-5
//
//Last Update in 2019年12月5日18:07:14
//
/////////////////////////////////////////////////


using PEIKTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIMEN;
using System;

public class PEIKnifer_Timer:PEIKnifer_Origin  {

    #region Inherent Value
    private float time;
    public float runTime;
    public float oTime;
    public bool timeRunningFlag;
    public float oStarUp;
    private Action _callBack;
    private PEIKnifer_LOrigin _l;
    private bool _done;
    public bool Loop;
    private bool _loomFlag,_entityFlag;

    #endregion

    public PEIKnifer_Timer()
    {
        _entityFlag = false;
        _loomFlag = false;
        if(PEIMEN_Entity.Ins)
            _entityFlag = true;
    }
    ~PEIKnifer_Timer()
    {
        if (_l != null && PEIMEN_Entity.L.HasL(_l))
            PEIMEN_Entity.L.RemoveL(_l);
    }
    /// <summary>
    /// Entrust Timer With A Call Back
    /// </summary>
    /// <param name="value">Lifetime</param>
    /// <param name="isLoop">Is Loop Flag</param>
    /// <param name="l">Func Depend PEIKnifer_L Class Entity</param>
    /// <param name="func">CallBack Func</param>
    public void EntrustTimer(float value,bool isLoop, Action func)
    {
        if (!_entityFlag)
        {
            PEIKDE.LogError("Timer", "PEIMEN_Entity Not Init But You Try To Use It !");
            return;
        }
        //PEIKDE.Log("ETimer Init");
        Loop = isLoop;
        Clear();
        SetTime(value);
        _callBack = func;
        _l = PEIMEN_Entity.L.AddL();
        _l.AddElement(TimerUpdate);
    }
    public void EntrustTimer(float value, bool isLoop,bool loomFlag, Action func)
    {
        if (!_entityFlag)
        {
            PEIKDE.LogError("Timer", "PEIMEN_Entity Not Init But You Try To Use It !");
            return;
        }
        //PEIKDE.Log("ETimer Init");
        Loop = isLoop;
        Clear();
        SetTime(value);
        _callBack = func;
        _loomFlag = loomFlag;
        _l = PEIMEN_Entity.L.AddL();
        if (_loomFlag)
            _l.AddElement(TimerUpdateLoom);
        else
            _l.AddElement(TimerUpdate);
    }

    public void EntrustPause()
    {
        if (!_entityFlag)
        {
            PEIKDE.LogError("Timer", "PEIMEN_Entity Not Init But You Try To Use It !");
            return;
        }
        if (_l != null)
        {
            if (_loomFlag)
                if (_l.HasElement(TimerUpdateLoom))
                    _l.RemoveElement(TimerUpdateLoom);
                else
                if (_l.HasElement(TimerUpdate))
                    _l.RemoveElement(TimerUpdate);
        }
        else
        {
            PEIKDE.LogError("Timer", "Timer Not Set L Init But You Try To Pause It !");
        }
    }

    public void EntrustPlay()
    {
        if (!_entityFlag)
        {
            PEIKDE.LogError("Timer", "PEIMEN_Entity Not Init But You Try To Use It !");
            return;
        }
        if (_l != null)
        {
            if (_loomFlag)
                if (_l.HasElement(TimerUpdateLoom))
                    _l.RemoveElement(TimerUpdateLoom);
                else
                if (_l.HasElement(TimerUpdate))
                    _l.RemoveElement(TimerUpdate);
        }
        else
        {
            //PEIKDE.LogError("Timer", "Timer Not Set L Init But You Try To Play It !");
        }
    }

    private void TimerUpdate()
    {
        if (Timer())
        {
            Clear();
            //PEIKDE.Log("Timer", "Timer CallBack Trigger!!");
            if (!Loop)
                _l.RemoveElement(TimerUpdate);
            _callBack();
            _done = false;
        }
    }
    
    private void TimerUpdateLoom()
    {
        void Run()
        {
            if (Timer())
            {
                _done = true;
                //PEIKDE.Log("Timer", "Timer Update Done");
            }
        }
            void MainTH()
        {
            if (_done)
            {
                Clear();
                //PEIKDE.Log("Timer", "Timer CallBack Trigger!!");
                if (!Loop)
                    _l.RemoveElement(TimerUpdate);
                _callBack();
                _done = false;
            }
        }
        PEIMEN_Entity.Loom.RunAsync("Timer", Run, MainTH);

    }
    #region Inherent Function
    /// <summary>
    /// Set And Init Timer
    /// </summary>
    /// <param name="value">Lifetime</param>
    public void SetTime(float value)
    {
        oStarUp = _entityFlag ? PEIMEN_Entity.Time.RealtimeSinceStartup:Time.realtimeSinceStartup;
        runTime = value;
        time = value;
        oTime = time;
        timeRunningFlag = false;
    }

    /// <summary>
    /// Timer Main Func With DeltaTime
    /// </summary>
    /// <returns></returns>
    public bool Timer()
    {

        //if (Time.timeScale == 0)
        //{
        //    time = time - Time.fixedDeltaTime;
        //}

        if (time < 0)
        {
            timeRunningFlag = false;
            //Debug.Log(this.ToString()+"---PEIKnfer Timer Complete" );
            return true;
        }
        else
        {
            timeRunningFlag = true;
            time -= _entityFlag? PEIMEN_Entity.Time.DeltaTime:Time.deltaTime;
        }
        runTime = time;
        return false;
    }

    /// <summary>
    /// Timer Main Func With RealTime
    /// </summary>
    /// <returns></returns>

    public bool RealTimer()
    {

        //if (Time.timeScale == 0)
        //{
        //    time = time - Time.fixedDeltaTime;
        //}

        if (time < 0)
        {
            timeRunningFlag = false;
            //Debug.Log(this.ToString()+"---PEIKnfer Timer Complete" );
            return true;
        }
        else
        {
            timeRunningFlag = true;
            time -= (_entityFlag?PEIMEN_Entity.Time.RealtimeSinceStartup:Time.realtimeSinceStartup - oStarUp);
        }
        runTime = time;

        oStarUp = _entityFlag? PEIMEN_Entity.Time.RealtimeSinceStartup:Time.realtimeSinceStartup;

        return false;
       

    }

    /// <summary>
    /// Timer Clear Func
    /// </summary>

    public void Clear()
    {
        oStarUp = _entityFlag? PEIMEN_Entity.Time.RealtimeSinceStartup:Time.realtimeSinceStartup;
        //Debug.Log("OTime==" + oTime);
        time = oTime;
        timeRunningFlag = false;
    }
   
    public float GetRunTime()
    {
        return time;
    }
#endregion

}
