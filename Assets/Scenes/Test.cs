using PEIKTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PEIKEV
{
    public class Test : PEIKnifer_L
    {
        public GameObject target, OldPos;
        public PEIKnifer_Timer t;
        PEIMEN_STC_Trans trans;
        public float speed;
        private float _disFlag;
        // Start is called before the first frame update
        public override void Awake()
        {
            t = new PEIKnifer_Timer();
            t.SetTime(1.5f);
            //t.EntrustTimer(2,true,this, LogRNG);
            PEIGameObjectFinder.SetAllAsset();
            base.Awake();
            trans = new PEIMEN_STC_Trans(transform.gameObject, target, true, true, 10, 10f, 1f, this, CallBack, SimpleTransType.MoveTowards);
            //trans.SetLocalFlag(true);
            //trans.SetParTrans(OldPos);
            //trans.SetLoopCallBack(4f);
            // _disFlag = Vector3.Distance(target.transform.position, OldPos.transform.position);
            //PEIKDE.Log("TT", "Finder Test DJJ in [" + PEIGameObjectFinder.GetGameObject("DJJ").name+"] Success");
            //PEIKDE.Log("TT", "Finder Test XDD in [" + PEIGameObjectFinder.GetGameObject("XDD").name + "] Success");

        }
        private void LogRNG()
        {
            PEIKDE.Log(PEIMath.Random.Next(0,100));
            PEIKDE.Log(PEIMath.Random.Next(0, 100));
            PEIKDE.Log(PEIMath.Random.Next(0, 100));
            PEIKDE.Log(PEIMath.Random.Next(0, 100));
            PEIKDE.Log(PEIMath.Random.Next(0, 100));
            PEIKDE.Log(PEIMath.Random.Next(0, 100));
            PEIKDE.Log(PEIMath.Random.Next(0, 100));
            PEIKDE.Log(PEIMath.Random.Next(0, 100));
        }
        void Start()
        {
            //PEIEvent.Ins.AddListener<PEIEvent_Test>(EventCallBack);
            trans.Flag.Flag = true;
            //PEIEvent.Ins.Trigger(this,new PEIEvent_Test() { Sender = "asd" });
            //PEIEvent.Ins.Trigger<PEIEvent_Test>(this);
        }

        private void EventCallBack(object sender, PEIEvent_Origin e)
        {
            PEIKDE.Log("Event Test Trigger On +"+sender);
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
            if (t.Timer())
            {
                LogRNG();
                t.Clear();
            }
            //speed = PEIMEN_ST_CPU.Ins.SpeedCurveTransTool(10,
            //                                      0.5f,
            //                                      2f,
            //                                      trans.GetModel().OldPos,
            //                                      trans.GetModel().Object.transform.position,
            //                                      trans.GetModel().Target.transform.position,
            //                                      trans.GetModel().MoveSpeed, 
            //                                      _disFlag,
            //                                      trans.Flag.Flag);

            //SetTime(speed);

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
                //PEIKDE.Log("Obj Reset Done !!");
            }
        }
    }

    public class PEIEvent_Test : PEIEvent_VitrualArgs<PEIEvent_Test>
    {
        public string Sender;
    }
}