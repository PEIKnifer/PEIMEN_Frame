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
//Last Update in 2017-12-26 10:36:19
//
/////////////////////////////////////////////////


using PEIKTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    /// <param name="value"></param>
    /// <param name="isLoop"></param>
    /// <param name="l"></param>
    /// <param name="func"></param>
    public void EntrustTimer(float value,bool isLoop,PEIKnifer_L l,PEIKnifer_L_Del func)
    {
        //PEIKDE.Log("ETimer Init");
        Loop = isLoop;
        SetTime(value);
        Clear();
        _callBack = func;
        l.AddElement(TimerUpdate);
        _l = l;
    }
    private void TimerUpdate()
    {
        Loom.RunAsync(() =>
        {
            if (Timer())
            {
                _done = true;
                PEIKDE.Log("Timer","Timer Update Done");
            }
            Loom.QueueOnMainThread(() =>
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
    public void SetTime(float value)
    {
        oStarUp = PEIMEN_ST_CPU.Ins.RealtimeSinceStartup;
        runTime = value;
        time = value;
        oTime = time;
        timeRunningFlag = false;
    }


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
            time -= PEIMEN_ST_CPU.Ins.DeltaTime;
        }
        runTime = time;
        return false;
    }
    
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
            time -= (PEIMEN_ST_CPU.Ins.RealtimeSinceStartup - oStarUp);
        }
        runTime = time;

        oStarUp = PEIMEN_ST_CPU.Ins.RealtimeSinceStartup;

        return false;
       

    }

    public void Clear()
    {
        oStarUp = PEIMEN_ST_CPU.Ins.RealtimeSinceStartup;
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
