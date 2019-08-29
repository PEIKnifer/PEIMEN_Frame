using PEIKTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : PEIKnifer_L
{
    public GameObject target;
    PEIMEN_STC_Trans trans;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        trans = new PEIMEN_STC_Trans(transform.gameObject, target,true,true,1,1f,1f,this, CallBack, SimpleTransType.Lerp);
        trans.SetLoopCallBack(4f);
    }
    void Start()
    {
        trans.Flag.Flag = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
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
