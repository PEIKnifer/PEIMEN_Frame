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
                    _ins = GetIns<PEIMEN_ST_CPU>(_ins);
                    Init();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public float DeltaTime { get; private set; }
        private float tctToolNum;

        private static void Init()
        {
        }
        // Update is called once per frame
        void Update()
        {
            DeltaTime = Time.deltaTime;
        }
        public void SpeedToolDeltaTime(float Max, float Min, float incremental, float resistance,float speed,STCPUDel callBack)
        {
            Loom.RunAsync(() =>
            {
                speed += incremental * DeltaTime - resistance * DeltaTime;
                if (speed > Max)
                    speed = Max;
                else if(speed< Min)
                    speed = Min;
            });
            Loom.QueueOnMainThread(() =>
            {
                callBack(speed);
            });
        }
        public delegate void STCPUDel(float speed);
    }
}