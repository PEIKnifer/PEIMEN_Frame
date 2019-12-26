/////////////////////////////////////////////////
//
//PEIMEN Frame System || PEIMEN branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for PEIMEN_System
//
//Create On 2019-12-5
//
//Last Update in 2019-12-9 18:40:47
//
/////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIKTS;
using PEIMEN;
using PEIMEN.Origin;
using System.Threading.Tasks;
using PEIMEN.Test;

namespace PEIMEN
{
    /// <summary>
    /// PEIMEN Frame Simple Framework Entity Class
    /// </summary>
    public class PEIMEN_Entity : PEIKnifer
    {
        /// <summary>
        /// PEIKDE Debug Flag
        /// </summary>
        private bool debugFlag = true;

        /// <summary>
        /// PEIMEN Entity Instance
        /// </summary>
        public static PEIMEN_Entity Ins;
        
        /// <summary>
        /// PEIEvent Manager Entity
        /// </summary>
        public static PEIEvent_Manager Event;

        /// <summary>
        /// PEIMEN Math Entity
        /// </summary>
        public static PEIMath Math;

        /// <summary>
        /// PEIMEN Net Tool Entity
        /// </summary>
        public static PEINet_Origin Web;

        /// <summary>
        /// PEIMEN Time Center Control Tool Entity
        /// </summary>
        public static PEITime_STTimeCC Time;

        /// <summary>
        /// PEIMEN State Manager Entity
        /// </summary>
        public static PEIState_Manager State;

        /// <summary>
        /// PEIMEN Node Manager Entity
        /// </summary>
        public static PEINode_Manager Node;

        /// <summary>
        /// Need Run In MonoBehaviour Tool Entity
        /// </summary> 
        public static PEIMEN_STMonoBehaviourTool MonoTool;

        /// <summary>
        /// PEIMEN L Class Manager
        /// </summary>
        public static PEIMEN_ST_LManager L;

        /// <summary>
        /// PEIMEN Thread Loom Class Manager
        /// </summary>
        public static PEIMEN_LoomManager Loom;

        /// <summary>
        /// PEIMEN Screen Class Manager
        /// </summary>
        public static PEIMEN_ST_Screen Screen;

        /// <summary>
        /// PEIMEN GameObjectPool Class Manager
        /// </summary>
        public static GameObjectPoolManager GameObjectPool;

        /// <summary>
        /// PEIMEN Jert Trigger Module Manager
        /// </summary>
        public static PEIJert_Manager Jert;


        public static PEIConsole Console;

        /// <summary>
        /// PEIMEN Secret Class Ins
        /// </summary>
        public static PEIMEN_Secret Secret;

        /// <summary>
        /// 当前程序集
        /// </summary>
        public static System.Reflection.Assembly Assembly { get; private set; }
        public bool DebugFlag
        { get => debugFlag; set
            {
                debugFlag = value;
                PEIKDE_Manager.DebugFlag = DebugFlag;
            }
        }

        public async void Awake()
        {
            //if (!Ins)
            Ins = this;
            //else
            //    Destroy(gameObject);
            //默认不销毁
            DontDestroyOnLoad(gameObject);
            
            Event = PEIMEN_System.GetModule<PEIEvent_Manager>();
            Math = PEIMEN_System.GetModule<PEIMath>();
            State = PEIMEN_System.GetModule<PEIState_Manager>();
            Node = PEIMEN_System.GetModule<PEINode_Manager>();
            Time = PEIMEN_System.GetModule<PEITime_STTimeCC>();
            Web = PEIMEN_System.GetModule<PEINet_Origin>();
            L = PEIMEN_System.GetModule<PEIMEN_ST_LManager>();
            MonoTool = PEIMEN_STMonoBehaviourTool.Ins;
            MonoTool.Init();
            Loom = PEIMEN_System.GetModule<PEIMEN_LoomManager>();
            Screen = PEIMEN_System.GetModule<PEIMEN_ST_Screen>();
            GameObjectPool = PEIMEN_System.GetModule<GameObjectPoolManager>();
            Console = PEIMEN_System.GetModule<PEIConsole>();
            Secret = PEIMEN_System.GetModule<PEIMEN_Secret>();
            Jert = PEIMEN_System.GetModule<PEIJert_Manager>();
            //Secret.ChangeKey("66165");
            //PEIKDE.Log("Entity", "Event with "+ Event);

            PEIKDE_Manager.DebugFlag = DebugFlag;

            PEIKDE.Log("Entity", "Init Done ");
            #region state
            //开启整个项目的流程
            Assembly = typeof(PEIMEN_Entity).Assembly;
            State.CreateContext(Assembly);
            await Task.Delay(100);
            State.SetStateStart();
            #endregion
        }
        private void Update()
        {
            PEIMEN_System.Update();
        }

        private void FixedUpdate()
        {
            PEIMEN_System.FixedUpdate();
        }

        private void OnDestroy()
        {
            MonoTool.OnClose();
            PEIMEN_System.ShutDown();
        }
    }
}