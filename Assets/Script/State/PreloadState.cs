using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIMEN.Interface;
using System.Reflection;
using System;

namespace PEIMEN.Origin
{
    public partial class PEIPreload : PEIState
    {
        public override void OnUpdate()
        {
            base.OnUpdate();
            PEIMEN_Entity.Event.Trigger<PEIE_FrameworkUpdate>(this);
            //PEIKDE.Log("PEI", "State In Update");
        }

        public override void OnEnter(params object[] parameters)
        {
            base.OnEnter(parameters);
            PEIMEN_Entity.Event.Trigger<PEIE_FrameworkAwake>(this);
            PEIMEN_Entity.Event.Trigger<PEIE_FrameworkStart>(this);
            //PEIKDE.Log("PEI", "State In Partial Preload");
            //You Can Change Yourself State Below This Line 
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnInit()
        {
            base.OnInit();
        }
        
    }
}
