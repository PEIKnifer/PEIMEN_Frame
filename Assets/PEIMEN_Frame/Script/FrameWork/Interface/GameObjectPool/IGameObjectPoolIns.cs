using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIMEN.Interface
{
    public interface IGameObjectPoolIns : PEIKnifer_Interface
    {
        void Instance();
        void Destory();
    }
}
