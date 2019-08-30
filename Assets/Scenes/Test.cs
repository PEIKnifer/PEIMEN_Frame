using PEIKTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : PEIKnifer_L
{
    public GameObject target,OldPos;
    PEIMEN_STC_Trans trans;
    public float speed;
    private float _disFlag;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        trans = new PEIMEN_STC_Trans(transform.gameObject, target,true,true,10,10f,1f,this, CallBack, SimpleTransType.MoveTowards);
        trans.SetParTrans(OldPos);
        trans.SetLoopCallBack(4f);
        _disFlag = Vector3.Distance(target.transform.position, OldPos.transform.position);
    }
    void Start()
    {
        trans.Flag.Flag = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
            speed = PEIMEN_ST_CPU.Ins.SpeedCurveTransTool(10,
                                                  0.5f,
                                                  2f,
                                                  trans.GetModel().OldPos,
                                                  trans.GetModel().Object.transform.position,
                                                  trans.GetModel().Target.transform.position,
                                                  trans.GetModel().MoveSpeed, 
                                                  _disFlag,
                                                  trans.Flag.Flag);
            
                SetTime(speed);
            
    }
    private void SetTime(float speed)
    {
        trans.SetMoveSpeed(speed);
    }
    public void CallBack()
    {
        if (trans.Flag.Flag)
        {
            trans.Flag.Flag = false;
            PEIKDE.Log("Obj Reset Done !!");
        }
    }
}
