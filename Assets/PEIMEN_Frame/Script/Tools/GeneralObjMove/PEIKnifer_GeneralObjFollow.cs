/////////////////////////////////////////////////
//
//PEIMEN Frame System || GeneralObjMove branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for GeneralObjFollow
//
//Create On 2017-12-26 10:36:12
//
//Last Update in 2017-12-26 10:36:19
//
/////////////////////////////////////////////////
using PEIKDL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTS {

[SerializeField]
public class PEIKnifer_GeneralObjFollow : PEIKnifer {

    #region Inherent value
    public GeneralObjFollowObjChange generalObjFollowObjChange;
    public GeneralObjFollowState generalObjFollowState;
    public GameObject tar;
    public float followSpeed;
    public float turnSpeed;
    public float distance;
    public float delayedTime;
    public float monitorDistance;
    private PEIKnifer_Timer timer; 
    private GameObject tarSafe;
    protected PEIKnifer_Delegate_GameObject_Void getTarDelegate;
    protected PEIKnifer_Delegate_Void_Void frameOrder;
    #endregion

    #region SetValue
    public GameObject TarSafe
    {
        set { tarSafe = value; }
        get { if (!tarSafe) { Debug.Log("The Tar Obj that you what follow is error");return transform.gameObject; }return tarSafe; }
    }
    #endregion

    #region Inherent Frame Function

    public void FrameInit()
    {
        switch (generalObjFollowObjChange)
        {
            case GeneralObjFollowObjChange.Drag:
                getTarDelegate = GetTarOrder;
                break;
            case GeneralObjFollowObjChange.Script:
                getTarDelegate = GetTarSafeOrder;
                break;
        }
        switch (generalObjFollowState)
        {
            case GeneralObjFollowState.immediately:
                frameOrder = ImmediatelyOrder1;
                break;
            case GeneralObjFollowState.delayed:
                frameOrder = DelayedOrder1;
                break;
            case GeneralObjFollowState.monitor:
                frameOrder = MonitorOrder1;
                break;
        }
        timer = new PEIKnifer_Timer();
        timer.SetTime(delayedTime);
    }
    public IEnumerator FrameLateIE()
    {
        FrameLateUpdate();
        yield return 0;
    }
    public void FrameLateUpdate()
    {
        frameOrder();
    }
    #endregion

    #region Inherent Funcion
    public GameObject GetTarOrder()
    {
        return tar;
    }
    public GameObject GetTarSafeOrder()
    {
        if (!tarSafe)
        {
            Debug.Log("The Tar Obj that you what follow is error");
            return transform.gameObject;
        }
        return tar;
    }
    public void ImmediatelyOrder1()
    {
        if (Vector3.Distance(transform.position, getTarDelegate().transform.position) > distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, getTarDelegate().transform.position, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(getTarDelegate().transform.position - transform.position), Time.deltaTime * turnSpeed);//平滑旋转
        }
    }
    public void DelayedOrder1()
    {
        transform.position = Vector3.MoveTowards(transform.position, getTarDelegate().transform.position, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(getTarDelegate().transform.position - transform.position), Time.deltaTime * turnSpeed);//平滑旋转

        if (Vector3.Distance(transform.position, getTarDelegate().transform.position) < distance)
        {
            timer.Clear();
            frameOrder = DelayedOrder2;
        }
    }
    public void DelayedOrder2()
    {
        if (Vector3.Distance(transform.position, getTarDelegate().transform.position) > distance && timer.Timer())
        {
            frameOrder = DelayedOrder1;
        }
        else
        if (Vector3.Distance(transform.position, getTarDelegate().transform.position) < distance)
        {
            timer.Clear();
        }
    }
    public void MonitorOrder1()
    {
        transform.position = Vector3.MoveTowards(transform.position, getTarDelegate().transform.position, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(getTarDelegate().transform.position - transform.position), Time.deltaTime * turnSpeed);//平滑旋转

        if (Vector3.Distance(transform.position, getTarDelegate().transform.position) < distance)
        {
            frameOrder = MonitorOrder2;
        }
    }
    public void MonitorOrder2()
    {
        if (Vector3.Distance(transform.position, getTarDelegate().transform.position) > monitorDistance)
        {
            frameOrder = MonitorOrder1;
        }
    }
}
#endregion
    
}