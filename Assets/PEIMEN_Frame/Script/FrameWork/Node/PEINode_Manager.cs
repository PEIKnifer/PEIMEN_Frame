/////////////////////////////////////////////////
//
//PEIMEN Frame System || PEIMEN branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for Node Module
//
//Create On 2019-10-5
//
//Last Update in 2019-10-5 18:07:50 
//
/////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PEIMEN.Origin
{
    public sealed class PEINode_Manager : PEIModel_Origin
    {
        #region 属性
        private readonly Dictionary<int, PEINode_DataOrigin> _allNodeDatas = new Dictionary<int, PEINode_DataOrigin>();
        #endregion

        #region set
        /// <summary>
        /// 设置数据节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set<T>(string key, T value)
        {
            PEINode_DataOrigin nodeDataBase;
            int hashCode = typeof(T).GetHashCode();
            if (!_allNodeDatas.TryGetValue(hashCode, out nodeDataBase))
            {
                nodeDataBase = new PEINode_Data<T>();
                _allNodeDatas[hashCode] = nodeDataBase;
            }
            PEINode_Data<T> nodeData = nodeDataBase as PEINode_Data<T>;
            nodeData.Set(key, value);
        }
        #endregion

        #region get
        /// <summary>
        /// 获取数据节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T Get<T>(string key, T defaultValue = default(T))
        {
            int hashCode = typeof(T).GetHashCode();
            PEINode_DataOrigin nodeDataBase;
            if (_allNodeDatas.TryGetValue(hashCode, out nodeDataBase))
            {
                PEINode_Data<T> nodeData = nodeDataBase as PEINode_Data<T>;
                return nodeData.Get(key, defaultValue);
            }

            return defaultValue;
        }
        #endregion

        #region has
        /// <summary>
        /// 是否包含数据节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Has<T>(string key)
        {
            int hashCode = typeof(T).GetHashCode();
            PEINode_DataOrigin nodeDataBase;
            if (_allNodeDatas.TryGetValue(hashCode, out nodeDataBase))
            {
                PEINode_Data<T> nodeData = nodeDataBase as PEINode_Data<T>;
                return nodeData.Has(key);
            }
            return false;
        }
        #endregion

        #region remove
        /// <summary>
        /// 移除数据节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        public void Remove<T>(string key)
        {
            int hashCode = typeof(T).GetHashCode();
            PEINode_DataOrigin nodeDataBase;
            if (_allNodeDatas.TryGetValue(hashCode, out nodeDataBase))
            {
                PEINode_Data<T> nodeData = nodeDataBase as PEINode_Data<T>;
                nodeData.Remove(key);
            }
        }
        #endregion

        #region clear
        /// <summary>
        /// 清除数据节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Clear<T>()
        {
            int hashCode = typeof(T).GetHashCode();
            PEINode_DataOrigin nodeDataBase;
            if (_allNodeDatas.TryGetValue(hashCode, out nodeDataBase))
                _allNodeDatas.Remove(hashCode);
        }
        #endregion

        #region 重写函数
        public override void OnClose()
        {
            foreach (var item in _allNodeDatas.Values)
            {
                item.Clear();
            }

            _allNodeDatas.Clear();
        }
        #endregion

    }
}
