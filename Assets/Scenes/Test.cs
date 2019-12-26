using PEIKTS;
using PEIMEN;
using PEIMEN.Origin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PEIMEN.Interface;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PEIKnifer_TestSpace
{
    public class Test : PEIKnifer
    {
        public GameObject target, OldPos,InsR, InsG, InsB,InsTar;
        private List<GameObject> InsListR, InsListG, InsListB;
        public PEIKnifer_Timer t;
        public Image image;
        private Vector2 ScreenScale;
        private PEIMEN_STC_Trans trans, transR, transG, transB;
        private PEIJert _jertR, _jertG, _jertB;
        private PEIMEN_STC_Jert _jertMgr;
        public float speed;
        private float _disFlag;
        private int _insNul = 0;
        // Start is called before the first frame update
        public void Awake()
        {
            if (PEIMEN_Entity.Jert.Set("Test", JertDone))
                _jertMgr = PEIMEN_Entity.Jert.Get("Test");

            _jertR = new PEIJert(_jertMgr);
            _jertG = new PEIJert(_jertMgr);
            _jertB = new PEIJert(_jertMgr);

            ScreenScale.x = image.rectTransform.sizeDelta.x;
            ScreenScale.y = image.rectTransform.sizeDelta.y;
            trans = new PEIMEN_STC_Trans(gameObject,target,true,speed,speed, NullFunction, SimpleTransType.MoveTowards);
            transR = new PEIMEN_STC_Trans(InsR, target, true, speed*0.1F, speed , ()=> { _jertR.Flag = true; }, SimpleTransType.MoveTowards);
            transG = new PEIMEN_STC_Trans(InsG, target, true, speed * 0.1F, speed , () => { _jertG.Flag = true;  }, SimpleTransType.MoveTowards);
            transB = new PEIMEN_STC_Trans(InsB, target, true, speed * 0.1F, speed , () => { _jertB.Flag = true;  }, SimpleTransType.MoveTowards);
            transR.Flag.Flag = true;
            transG.Flag.Flag = true;
            transB.Flag.Flag = true;
            //trans.SetLoom(true);
            //trans.Flag.Flag = true;
            t = new PEIKnifer_Timer();
            t.EntrustTimer(1,true,true, Loop);
            void Loop()
            {
                trans.Flag.Flag = !trans.Flag.Flag;
                PEIKDE.Log("Test", "Timer Done");
            }
            PEIKDE.Log("Test", "Normal Awake");
        }
        void Start()
        {
            InsListR = new List<GameObject>();
            InsListG = new List<GameObject>();
            InsListB = new List<GameObject>();

            PEIMEN_Entity.Event.AddListener<PEIE_FrameworkAwake>(FrameAwake);
            PEIMEN_Entity.Event.AddListener<PEIE_FrameworkStart>(FrameStart);
            //PEIMEN_Entity.Web.Get("http://47.96.167.8:3003/ISOS/webservice/endTime?", WWWCallBack);
            ////PEIEvent.Ins.AddListener<PEIEvent_Test>(EventCallBack);
            //trans.Flag.Flag = true;
            ////PEIEvent.Ins.Trigger(this,new PEIEvent_Test() { Sender = "asd" });
            ////PEIEvent.Ins.Trigger<PEIEvent_Test>(this);
            //PEIMEN_Entity.Event.Trigger<PEIEvent_Test>(this);
            //PEIKDE.Log("Test", "Framework Start");
        }
        private void JertDone()
        {
            PEIKDE.Log("Jert","!!JertDone!!");
        }

        private void FrameStart(object sender, PEIEvent_Origin e)
        {
            //PEIKDE.Log("Test","Framework Start");
        }

        private void FrameAwake(object sender, PEIEvent_Origin e)
        {
            //PEIKDE.Log("Test", "Framework Awake");
        }

        private void OnDestroy()
        {
            trans.Destory();
        }


        private void WWWCallBack(UnityWebRequest obj)
        {
            PEIKDE.Log("Test", obj.downloadHandler.text);
        }

        private void LogRNG()
        {
            //PEIMEN_Entity.WWW.Get("http://47.96.167.8:3003/ISOS/webservice/api?wsdl", WWWCallBack);
            PEIKDE.Log(PEIMEN_Entity.Math.Random.Next(0, 100));
            PEIKDE.Log(PEIMEN_Entity.Math.Random.Next(0, 100));
            PEIKDE.Log(PEIMEN_Entity.Math.Random.Next(0, 100));
            PEIKDE.Log(PEIMEN_Entity.Math.Random.Next(0, 100));
            //PEIKDE.Log(PEIMEN_Entity.Math.Random.Next(0, 100));
            //PEIKDE.Log(PEIMEN_Entity.Math.Random.Next(0, 100));
            //PEIKDE.Log(PEIMEN_Entity.Math.Random.Next(0, 100));
            //PEIKDE.Log(PEIMEN_Entity.Math.Random.Next(0, 100));
        }
        

        private void EventCallBack(object sender, PEIEvent_Origin e)
        {
            PEIKDE.Log("Event Test Trigger On +"+sender);
        }

        // Update is called once per frame
        public void Update()
        {
            //PEIKDE.Log("PEIKDE");
            if (Input.GetKeyDown(KeyCode.Y))
            {
                InsListR.Add(PEIMEN_Entity.GameObjectPool.Instance("R", InsR, InsTar.transform.position+new Vector3(_insNul,0,0), InsTar.transform.rotation));
                _insNul++;
                PEIKDE.Log("asdasdasdasd");
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                InsListG.Add(PEIMEN_Entity.GameObjectPool.Instance("G", InsG, InsTar.transform.position + new Vector3(_insNul, 0, 0), InsTar.transform.rotation));
                _insNul++;
                PEIKDE.Log("qweqweqweqwe");
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                InsListB.Add(PEIMEN_Entity.GameObjectPool.Instance("B", InsB, InsTar.transform.position + new Vector3(_insNul, 0, 0), InsTar.transform.rotation));
                _insNul++;
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (InsListR.Count > 0)
                {
                    PEIMEN_Entity.GameObjectPool.Destory("R", InsListR[0]);
                    InsListR.RemoveAt(0);
                }
                //_insNul++;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (InsListG.Count > 0)
                {
                    PEIMEN_Entity.GameObjectPool.Destory("G", InsListG[0]);
                    InsListG.RemoveAt(0);
                }
                //_insNul++;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (InsListB.Count > 0)
                {
                    PEIMEN_Entity.GameObjectPool.Destory("B", InsListB[0]);
                    InsListB.RemoveAt(0);
                }
                //_insNul++;
            }
            image.rectTransform.sizeDelta =new Vector2(ScreenScale.x * PEIMEN_Entity.Screen.GetScreenWidthScale(), ScreenScale.x * PEIMEN_Entity.Screen.GetScreenHeightScale());
            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            //base.Update();
            //if (t.Timer())
            //{
            //    LogRNG();
            //    t.Clear();
            //}
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

        public void FrameworkAwake()
        {
            //base.Awake();
            //PEIMEN_Entity.Web.Get("http://47.96.167.8:3003/ISOS/webservice/endTime?", WWWCallBack);
            PEIMEN_Entity.Event.AddListener<PEIEvent_Test>(EventCallBack);
            //trans.Flag.Flag = true;
            //PEIEvent.Ins.Trigger(this,new PEIEvent_Test() { Sender = "asd" });
            //PEIEvent.Ins.Trigger<PEIEvent_Test>(this);
            PEIMEN_Entity.Event.Trigger<PEIEvent_Test>(this);

            PEIKDE.Log("Test", "Test Awake!!");
        }

        public void OnUpdate()
        {
        }

        public void FrameworkUpdate()
        {
            //base.Update();
            PEIKDE.Log("Test", "Test Update!!");
            PEIMEN_Entity.Event.Trigger<PEIEvent_Test>(this);
            PEIKDE.Log("Test", "Event Send!!");
        }
    }

    public class PEIEvent_Test : PEIEvent_VitrualArgs<PEIEvent_Test>
    {
        public string Sender;
    }
}