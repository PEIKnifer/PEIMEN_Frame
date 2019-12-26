using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIMEN.Origin
{
    internal sealed class PEIState_Context:PEIKnifer_Origin
    {
        #region 属性
        //所有状态
        private readonly Dictionary<int, PEIState> _allStates = new Dictionary<int, PEIState>();
        //当前状态
        private PEIState _curState;

        /// <summary>
        /// 当前状态
        /// </summary>
        public PEIState CurrentState
        {
            get { return _curState; }
        }
        #endregion


        #region 外部接口

        /// <summary>
        /// 设置所有状态
        /// </summary>
        /// <param name="states"></param>
        public void SetAllState(PEIState[] states)
        {
            foreach (var item in states)
            {
                int hashCode = item.GetType().GetHashCode();
                if (!_allStates.ContainsKey(hashCode))
                {
                    item.SetStateContext(this);
                    item.OnInit();
                    _allStates[hashCode] = item;
                }
            }
        }

        /// <summary>
        /// 设置开始状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetStartState<T>(params object[] parameters) where T : PEIState
        {
            if (_curState == null)
            {
                int hashCode = typeof(T).GetHashCode();
                if (_allStates.ContainsKey(hashCode))
                {
                    _curState = _allStates[hashCode];
                    _curState.OnEnter(parameters);
                }
            }
        }
        /// <summary>
        /// 设置开始状态
        /// </summary>
        /// <param name="state"></param>
        public void SetStartState(PEIState state, params object[] parameters)
        {
            if (_curState == null)
            {
                if (_allStates.ContainsValue(state))
                {
                    _curState = state;
                    _curState.OnEnter(parameters);

                }
            }
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ChangeState<T>(params object[] parameters) where T : PEIState
        {
            int hashCode = typeof(T).GetHashCode();
            if (_allStates.ContainsKey(hashCode))
            {
                if (_curState != null)
                    _curState.OnExit();
                _curState = _allStates[hashCode];
                _curState.OnEnter(parameters);
            }
        }

        /// <summary>
        /// 渲染帧函数
        /// </summary>
        public void Update()
        {
            if (_curState != null)
                _curState.OnUpdate();
        }
        /// <summary>
        /// 固定帧函数
        /// </summary>
        public void FixedUpdate()
        {
            if (_curState != null)
                _curState.OnFixedUpdate();
        }
        /// <summary>
        /// 关闭函数
        /// </summary>
        public void Close()
        {
            foreach (var item in _allStates.Values)
                item.OnExit();
            _allStates.Clear();
        }

        #endregion

    }
}