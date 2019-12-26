using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIKTS;
using PEIMEN;
using PEIMEN.Origin;
using System.Threading.Tasks;

namespace PEIMEN
{
    /// <summary>
    /// PEIMEN Frame Simple Tool Entity Class
    /// </summary>
    public class PEIKST_Entity : PEIKnifer
    {
        /// <summary>
        /// PEIKDE Debug Flag
        /// </summary>
        public bool DebugFlag = true;


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
        /// PEIMEN Node Manager Entity
        /// </summary>
        public static PEINode_Manager Node;

        /// <summary>
        /// 当前程序集
        /// </summary>
        public static System.Reflection.Assembly Assembly { get; private set; }


        public async void Awake()
        {
            //默认不销毁
            DontDestroyOnLoad(gameObject);

            Event = PEIMEN_System.GetModule<PEIEvent_Manager>();
            Math = PEIMEN_System.GetModule<PEIMath>();
            Node = PEIMEN_System.GetModule<PEINode_Manager>();

            //new PEITime_STTimeCC(out Time, gameObject);
            //new PEINet_Origin(out Web, gameObject);

            PEIKDE_Manager.DebugFlag = DebugFlag;
            PEIKDE.Log("Entity", "Init Done ");
            #region state
            //开启整个项目的流程
            Assembly = typeof(PEIMEN_Entity).Assembly;
            await Task.Delay(100);
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
            PEIMEN_System.ShutDown();
        }
    }
}