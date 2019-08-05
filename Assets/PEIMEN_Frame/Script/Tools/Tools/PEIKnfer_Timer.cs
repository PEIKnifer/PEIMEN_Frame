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
    #endregion

    #region Inherent Function
    public void SetTime(float value)
    {
        oStarUp = Time.realtimeSinceStartup;
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
            time -= Time.deltaTime;
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
            time -= (Time.realtimeSinceStartup-oStarUp);
        }
        runTime = time;

        oStarUp = Time.realtimeSinceStartup;

        return false;
       

    }

    public void Clear()
    {
        oStarUp = Time.realtimeSinceStartup;
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
