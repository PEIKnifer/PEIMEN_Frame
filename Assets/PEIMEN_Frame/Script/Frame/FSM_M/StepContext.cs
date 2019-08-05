/////////////////////////////////////////////////
//
//PEIMEN Frame System || FSM_M branch 
//
//creat by wanderer
//
//Frame for Simple FSM Frame Context
//
//Create On 2019 8 5
//
//Last Update in 2019 8 5 15:48:08
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tiny.Core
{
    public sealed class StepContext : MonoBehaviour
    {
        [SerializeField]
        private List<Step> _steps;

        public Step CureentStep { get; set; }

        //在start 函数中是否运行
        [SerializeField]
        private bool _runOnStart = true;

        // Use this for initialization
        void Start()
        {
            if (_steps != null)
            {
                foreach (var item in _steps)
                {
                    item.StepContext = this;
                }

                //在start函数中运行
                if (_runOnStart)
                    StepStart();
            }
        }

        // Update is called once per frame
        void Update()
        {
            CureentStep?.OnUpdate();
            //PEIKDE.Log("SC", "Con Update = " + CureentStep.gameObject.name);
        }

        /// <summary>
        /// 步骤开始 步骤为null时，默认调用列表的第一个
        /// </summary>
        /// <param name="name"></param>
        public void StepStart(string name = null)
        {
            if (_steps == null || _steps.Count == 0)
                return;

            Step step = string.IsNullOrEmpty(name) ? _steps[0] : _steps.Find(x => x.name.Equals(name));
            if (step != null)
            {
                step.OnEnter();
                CureentStep = step;
            }
        }

        /// <summary>
        /// 切换步骤
        /// </summary>
        /// <param name="name"></param>
        public void ChangeStep(string name)
        {
            //PEIKDE.Log("SCT", "Change Name = "+name);
            if (_steps == null)
                return;

            Step step = _steps.Find(x => x.StepName.Equals(name));

            if (step != null)
            {
                //调用上一个步骤的退出函数
                if (CureentStep != null)
                {
                    CureentStep.OnExit();
                }
                //切换最新的步骤函数
                step.OnEnter();
                CureentStep = step;
            }
            else
            {
                PEIKDE.LogError("SCT", "[Step Error] No current step with " + name);
            }

        }

        /// <summary>
        /// 步骤停止  可以重置数据
        /// </summary>
        public void Stop()
        {
            if (CureentStep != null)
                CureentStep.OnExit();
            CureentStep = null;
            if (_steps != null)
            {
                foreach (var item in _steps)
                {
                    item.OnStop();
                }
            }
        }

    }
}