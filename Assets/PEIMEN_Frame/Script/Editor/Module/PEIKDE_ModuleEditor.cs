using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

namespace PEIMEN.Origin
{
    public class PEIKDE_ModuleEditor : PEIModule_EditorOrigin
    {

        public PEIKDE_ModuleEditor(string name, Color mainColor, PEIMEN_Entity gameMode)
           : base(name, mainColor, gameMode)
        { }

        public override void OnDrawGUI()
        {
            GUILayout.BeginVertical("HelpBox");

            GUI.color = _entity.DebugFlag ? Color.white : Color.gray;
            _entity.DebugFlag = GUILayout.Toggle(_entity.DebugFlag, "PEIKDE Enable");
            GUI.color = Color.white;

            GUILayout.EndVertical();
        }

        public override void OnClose()
        {
        }

    }
}
