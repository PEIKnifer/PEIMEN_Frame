/////////////////////////////////////////////////
//
//PEIMEN Frame System || SimpleTool branch 
//
//creat by PEIKnifer[.CN]
//
//SimpleTool for ToolCPU
//
//Create On 2019-10-9 15:40:42
//
//Last Update in 2019-10-9 15:40:50  
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTS { 
public class PEIMEN_ST_CPU : PEIKnifer_Singleton
    {
        private static PEIMEN_ST_CPU _ins;

        public static PEIMEN_ST_CPU Ins
        {
            get
            {
                if (!_ins)
                {
                    _ins = GetIns<PEIMEN_ST_CPU>();
                    //Init();
                }
                return _ins;
            }
            private set { _ins = value; }
        }

        public float DeltaTime { get; private set; }
        public float RealtimeSinceStartup { get; private set; }
        private float _tctToolNum,
                      _tctToolNumB,
                      _tctToolNumS;

        private static void Init()
        {
        }
        // Update is called once per frame
        void Update()
        {
            DeltaTime = Time.deltaTime;
            RealtimeSinceStartup = Time.realtimeSinceStartup;
        }
        public float SpeedToolDeltaTime(float Max, float Min, float acc, Vector3 ZPos, Vector3 pos, Vector3 tar, float speed, float scale)
        {
            _tctToolNum = Vector3.Distance(pos, tar);
            _tctToolNumB = Vector3.Distance(ZPos, tar);
            _tctToolNumS = (_tctToolNum / scale)>1? _tctToolNum / scale:1;

           speed = speed + ((_tctToolNum > (_tctToolNumB * 0.5f) ? acc * DeltaTime : -acc * DeltaTime));
            if (speed > Max)
                speed = Max;
            else if (speed < Min)
                speed = Min;
            //PEIKDE.Log("PSC","Speed Tool Done With "+speed);
            return speed* _tctToolNumS;

        }
        public float SpeedCurveTransTool(float Max, float Min, float acc, Vector3 ZPos, Vector3 pos, Vector3 tar, float speed,float scale,bool flag)
        {
           // PEIKDE.Log("PSC", "Curve Tool Running");
            if (flag)
            {
                return SpeedToolDeltaTime(Max, Min, acc, ZPos, pos, tar, speed, scale);
            }
            else
            {
                return SpeedToolDeltaTime(Max, Min, acc, tar, pos, ZPos, speed, scale);
            }
        }

        public delegate void STCPUDel(float speed);
    }
}