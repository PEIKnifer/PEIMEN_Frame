/////////////////////////////////////////////////
//
//PEIMEN Frame System || PEIMEN_Frame branch 
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

using PEIMEN.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIMEN.Origin
{
    public static class PEIMEN_System
    {
        #region 属性
        //所有的子模块
        private static readonly Dictionary<int, PEIModel_Origin> _allGameModules = new Dictionary<int, PEIModel_Origin>();
        //所有渲染帧函数
        private static List<IUpdate> _allUpdates = new List<IUpdate>();
        //所有的固定帧函数
        private static List<IFixedUpdate> _allFixedUpdates = new List<IFixedUpdate>();
        #endregion

        #region 外部接口
        /// <summary>
        /// 获取模块
        /// </summary>
        /// <typeparam name="T">游戏模块</typeparam>
        /// <returns></returns>
        public static T GetModule<T>() where T : PEIModel_Origin, new()
        {
            return (T)GetModule(typeof(T));
        }

        /// <summary>
        /// 渲染帧
        /// </summary>
        public static void Update()
        {
            for (int i = 0; i < _allUpdates.Count; i++)
                _allUpdates[i].OnUpdate();
        }

        /// <summary>
        /// 固定帧
        /// </summary>
        public static void FixedUpdate()
        {
            foreach (var item in _allFixedUpdates)
                item.OnFixedUpdate();
        }

        /// <summary>
        /// 关闭游戏的所有模块
        /// </summary>
        public static void ShutDown()
        {
            foreach (var item in _allGameModules.Values)
                item.OnClose();

            _allUpdates.Clear();
            _allFixedUpdates.Clear();
            _allGameModules.Clear();
        }

        #endregion


        #region 内部函数

        //获取模块
        private static PEIModel_Origin GetModule(Type type)
        {
            int hashCode = type.GetHashCode();
            PEIModel_Origin module = null;
            if (_allGameModules.TryGetValue(hashCode, out module))
                return module;
            module = CreateModule(type);
            return module;
        }

        //创建模块
        private static PEIModel_Origin CreateModule(Type type)
        {
            int hashCode = type.GetHashCode();
            PEIModel_Origin module = (PEIModel_Origin)Activator.CreateInstance(type);
            _allGameModules[hashCode] = module;
            //整理含IUpdate的模块
            var update = module as IUpdate;
            if (update != null)
                _allUpdates.Add(update);
            //整理含IFixed的模块
            var fixedUpdate = module as IFixedUpdate;
            if (fixedUpdate != null)
                _allFixedUpdates.Add(fixedUpdate);
            return module;
        }

        #endregion

    }
}