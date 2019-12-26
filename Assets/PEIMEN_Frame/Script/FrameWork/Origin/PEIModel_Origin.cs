using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIMEN.Origin
{
    public abstract class PEIModel_Origin : PEIKnifer_Origin
    {
        /// <summary>
        /// 关闭当前模块
        /// </summary>
        public abstract void OnClose();
    }
}