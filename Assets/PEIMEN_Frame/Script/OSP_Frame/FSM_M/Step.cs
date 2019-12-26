/////////////////////////////////////////////////
//
//PEIMEN Frame System || FSM_M branch 
//
//creat by wanderer
//
//Frame for Simple FSM Frame StepOrigin
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
    public abstract class Step : PEIKnifer
    {

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName;

        /// <summary>
        /// 步骤管理器
        /// </summary>
        public StepContext StepContext { get; set; }
        public int NextAudioID;


        public virtual void OnEnter()
        { }

        public virtual void OnExit()
        { }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnStop()
        {

        }

        protected void ChangeStep(string name)
        {
            StepContext?.ChangeStep(name);
        }
        protected void PlayNextAudio()
        {
        }

    }
}
