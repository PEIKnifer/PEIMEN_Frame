using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKEV
{
    public class PEIEvent_Manager : PEIKnifer_Origin
    {
        #region 属性
        //监听函数
        private static readonly Dictionary<int, EventHandler<PEIEvent_Origin>> _allActions = new Dictionary<int, EventHandler<PEIEvent_Origin>>();
        #endregion

        #region 外部接口
        /// <summary>
        /// 增加监听函数
        /// </summary>
        /// <typeparam name="T">BaseEventArgs</typeparam>
        /// <param name="handler"></param>
        public void AddListener<T>(EventHandler<PEIEvent_Origin> handler) where T : PEIEvent_Origin
        {
            int eventId = typeof(T).GetHashCode();
            EventHandler<PEIEvent_Origin> eventHandler = null;
            if (!_allActions.TryGetValue(eventId, out eventHandler))
                _allActions[eventId] = handler;
            else
            {
                eventHandler += handler;
                _allActions[eventId] = eventHandler;
            }
        }

        /// <summary>
        /// 移除监听函数
        /// </summary>
        /// <typeparam name="T">BaseEventArgs</typeparam>
        /// <param name="handler"></param>
        public void RemoveListener<T>(EventHandler<PEIEvent_Origin> handler) where T : PEIEvent_Origin
        {
            int eventId = typeof(T).GetHashCode();
            EventHandler<PEIEvent_Origin> eventHandler = null;
            if (!_allActions.TryGetValue(eventId, out eventHandler))
                return;
            else
            {
                eventHandler -= handler;
                if (eventHandler == null)
                    _allActions.Remove(eventId);
                else
                    _allActions[eventId] = eventHandler;
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="T">事件类</typeparam>
        /// <param name="sender">触发事件的对象</param>
        public void Trigger<T>(object sender) where T : PEIEvent_Origin
        {
            int eventId = typeof(T).GetHashCode();
            HanleEvent(sender, eventId);
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="value">事件参数</param>
        public void Trigger(object sender, PEIEvent_Origin value)
        {
            HanleEvent(sender, value);
        }

        #endregion

        //处理事件
        private void HanleEvent(object sender, PEIEvent_Origin baseEventArgs)
        {
            if (baseEventArgs == null)
                return;
            EventHandler<PEIEvent_Origin> eventHandler = null;
            int Id = baseEventArgs.Id;
            if (_allActions.TryGetValue(Id, out eventHandler))
            {
                if (eventHandler != null)
                    eventHandler(sender, baseEventArgs);
            }
        }

        //处理事件  不带参数
        private void HanleEvent(object sender, int eventId)
        {
            EventHandler<PEIEvent_Origin> eventHandler = null;
            if (_allActions.TryGetValue(eventId, out eventHandler))
            {
                if (eventHandler != null)
                    eventHandler(sender, null);
            }
        }

    }
}