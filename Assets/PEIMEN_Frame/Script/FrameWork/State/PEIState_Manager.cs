/////////////////////////////////////////////////
//
//PEIMEN Frame System || PEI Event branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for PEIMEN
//
//Create On 2019-12-5
//
//Last Update in 2019-12-9 18:40:47
//
/////////////////////////////////////////////////
using PEIMEN.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace PEIMEN.Origin
{
    public sealed class PEIState_Manager : PEIModel_Origin, IUpdate, IFixedUpdate
    {
        #region 属性
        private PEIState_Context _stateContext;
        private PEIState _startState;
        /// <summary>
        /// 当前的游戏状态
        /// </summary>
        public PEIState CurrentState
        {
            get
            {
                if (_stateContext == null)
                    return null;
                return _stateContext.CurrentState;
            }
        }
        #endregion

        #region 外部接口
        /// <summary>
        /// 创建游戏状态的环境
        /// </summary>
        /// <param name="assembly">重写游戏状态所在的程序集</param>
        public void CreateContext(Assembly assembly)
        {
            if (_stateContext != null)
                return;

            PEIState_Context stateContext = new PEIState_Context();
            List<PEIState> listState = new List<PEIState>();

            Type[] types = assembly.GetTypes();
            foreach (var item in types)
            {
                object[] attribute = item.GetCustomAttributes(typeof(PEIStateAttribute), false);
                if (attribute.Length <= 0 || item.IsAbstract)
                    continue;
                PEIStateAttribute stateAttribute = (PEIStateAttribute)attribute[0];
                if (stateAttribute.StateType == PEIState_Type.Ignore)
                    continue;
                object obj = Activator.CreateInstance(item);
                PEIState gs = obj as PEIState;
                if (gs != null)
                {
                    listState.Add(gs);
                    if (stateAttribute.StateType == PEIState_Type.Start)
                        _startState = gs;
                }
            }
            stateContext.SetAllState(listState.ToArray());
            _stateContext = stateContext;
        }
        /// <summary>
        /// 设置状态开始
        /// </summary>
        public void SetStateStart()
        {
            if (_stateContext != null && _startState != null)
                _stateContext.SetStartState(_startState);
        }
        #endregion

        #region 重写函数
        /// <summary>
        /// 渲染帧函数
        /// </summary>
        public void OnUpdate()
        {
            if (_stateContext != null)
                _stateContext.Update();
        }
        /// <summary>
        /// 固定帧函数
        /// </summary>
        public void OnFixedUpdate()
        {
            if (_stateContext != null)
                _stateContext.FixedUpdate();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public override void OnClose()
        {
            _stateContext.Close();
            _stateContext = null;
        }
        #endregion
    }
}