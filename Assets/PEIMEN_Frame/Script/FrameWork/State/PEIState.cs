using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIMEN.Origin
{
    //[AttributeUsage(AttributeTargets.Class)] : Attribute
    public abstract class PEIState
    {
        #region 属性
        private PEIState_Context _context;

        internal void SetStateContext(PEIState_Context context)
        {
            _context = context;
        }

        #endregion


        #region
        /// <summary>
        /// 初始化 -- 只执行一次
        /// </summary>
        public virtual void OnInit()
        {
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        /// <param name="parameters">不确定参数</param>
        public virtual void OnEnter(params object[] parameters)
        {

        }

        /// <summary>
        /// 退出状态
        /// </summary>
        public virtual void OnExit()
        {
        }

        /// <summary>
        /// 渲染帧函数
        /// </summary>
        public virtual void OnUpdate()
        {
        }

        /// <summary>
        /// 固定帧函数
        /// </summary>
        public virtual void OnFixedUpdate()
        {

        }


        #endregion

        #region 内部函数

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <typeparam name="T">游戏状态</typeparam>
        protected void ChangeState<T>(params object[] parameters) where T : PEIState
        {
            if (_context != null)
                _context.ChangeState<T>(parameters);
        }

        #endregion

    }
}