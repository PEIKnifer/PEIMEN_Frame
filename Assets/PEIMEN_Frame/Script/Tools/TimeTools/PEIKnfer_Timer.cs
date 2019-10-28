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
//Last Update in 2019-10-28 18:21:27
//
/////////////////////////////////////////////////


using PEIKTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIMEN;


public class PEIKnifer_Timer  {

    #region Inherent Value
    private float time;
    public float runTime;
    public float oTime;
    public bool timeRunningFlag;
    public float oStarUp;
    private PEIKnifer_L_Del _callBack;
    private PEIKnifer_L _l;
    private bool _done;
    public bool Loop;

    #endregion

    public PEIKnifer_Timer() { }

    /// <summary>
    /// Entrust Timer With A Call Back
    /// </summary>
    /// <param name="value">Lifetime</param>
    /// <param name="isLoop">Is Loop Flag</param>
    /// <param name="l">Func Depend PEIKnifer_L Class Entity</param>
    /// <param name="func">CallBack Func</param>
    public void EntrustTimer(float value,bool isLoop,PEIKnifer_L l,PEIKnifer_L_Del func)
    {
        //PEIKDE.Log("ETimer Init");
        Loop = isLoop;
        Clear();
        SetTime(value);
        _callBack = func;
        l.AddElement(TimerUpdate);
        _l = l;
    }
    private void TimerUpdate()
    {
        PEIMEN_Loom.RunAsync(() =>
        {
            if (Timer())
            {
                _done = true;
                PEIKDE.Log("Timer","Timer Update Done");
            }
            PEIMEN_Loom.QueueOnMainThread(() =>
            {
                if (_done)
                {
                    Clear();
                    PEIKDE.Log("Timer", "Timer CallBack Trigger!!");
                    if (!Loop)
                        _l.RemoveElement(TimerUpdate);
                    _callBack();
                    _done = false;
                }
            });
        });
        
    }
    #region Inherent Function
    /// <summary>
    /// Set And Init Timer
    /// </summary>
    /// <param name="value">Lifetime</param>
    public void SetTime(float value)
    {
        oStarUp = PEIMEN_Entity.Time.RealtimeSinceStartup;
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
            time -= PEIMEN_Entity.Time.DeltaTime;
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
            time -= (PEIMEN_Entity.Time.RealtimeSinceStartup - oStarUp);
        }
        runTime = time;

        oStarUp = PEIMEN_Entity.Time.RealtimeSinceStartup;

        return false;
       

    }

    /// <summary>
    /// Timer Clear Func
    /// </summary>

    public void Clear()
    {
        oStarUp = PEIMEN_Entity.Time.RealtimeSinceStartup;
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
