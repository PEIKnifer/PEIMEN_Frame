using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

namespace PEIMEN.Origin
{
    public class PEIState_ModuleEditor : PEIModule_EditorOrigin
    {
        private List<string> _listState;

        public PEIState_ModuleEditor(string name, Color mainColor, PEIMEN_Entity gameMode)
           : base(name, mainColor, gameMode)
        {
            _listState = new List<string>();
            Type[] types = typeof(PEIMEN_Entity).Assembly.GetTypes();
            foreach (var item in types)
            {
                object[] attribute = item.GetCustomAttributes(typeof(PEIStateAttribute), false);
                if (attribute.Length <= 0 || item.IsAbstract)
                    continue;
                PEIStateAttribute stateAttribute = (PEIStateAttribute)attribute[0];
                //if (stateAttribute.StateType == VirtualStateType.Ignore)
                //    continue;
                object obj = Activator.CreateInstance(item);
                PEIState gs = obj as PEIState;
                if (gs != null)
                    _listState.Add("[" + stateAttribute.StateType.ToString() + "]\t" + item.FullName);
                //PEIKDE.Log("UDE", "_listState C " + gs);
            }
        }


        public override void OnDrawGUI()
        {
            GUILayout.BeginVertical("HelpBox");

            foreach (var item in _listState)
            {
                //正在运行
                if (EditorApplication.isPlaying)
                {
                    string runName = "";
                    if (PEIMEN_Entity.State.CurrentState != null)
                        runName = PEIMEN_Entity.State.CurrentState.GetType().Name;
                    if (item.Contains(runName))
                    {
                        GUILayout.BeginHorizontal();
                        GUI.color = Color.green;
                        GUILayout.Label("", GUI.skin.GetStyle("Icon.ExtrapolationContinue"));
                        GUI.color = _defaultColor;
                        GUILayout.Label(item);
                        GUILayout.FlexibleSpace();
                        GUILayout.Label((Profiler.GetMonoUsedSizeLong() / 1000000.0f).ToString("f3"));
                        GUILayout.EndHorizontal();

                        continue;
                    }
                }
                //默认状态
                GUI.enabled = false;
                GUILayout.BeginHorizontal();
                GUILayout.Label("", GUI.skin.GetStyle("Icon.ExtrapolationContinue"));
                GUILayout.Label(item);
                GUILayout.EndHorizontal();
                GUI.enabled = true;
            }

            GUILayout.EndVertical();
        }


        public override void OnClose()
        {
            _listState.Clear();
        }

    }
}