using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIMEN.Origin {

    [PEIState(PEIState_Type.Start)]
    public class PEILaunghState : PEIState
    {
        #region 重写函数
        public override void OnEnter(params object[] parameters)
        {
            base.OnEnter(parameters);

            //加载更新或加载界面
            //...

           // PEIKDE.Log("PEIMEN","PEIMEN Frame Init Done");
            ChangeState<PEIPreload>();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        #endregion

    }
}