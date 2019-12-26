using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Profiling;

namespace PEIMEN.Origin
{
    [CustomEditor(typeof(PEIMEN_Entity))]
    public class PEIMEN_EntityEditor : Editor
    {
        private PEIMEN_Entity _entity;

        ////Color.cyan;
        private Color _defaultColor;

        //资源加载模块的颜色
        private Color _resourceColor = new Color(0.851f, 0.227f, 0.286f, 1.0f);

        //可操作模块的颜色
        private Color _operationColor = new Color(0.953f, 0.424f, 0.129f, 1.0f);

        //状态模块的颜色
        private Color _stateColor = new Color(0.141f, 0.408f, 0.635f, 1.0f);

        //配置表模块的颜色
        private Color _dataTableColor = new Color(0.989f, 0.686f, 0.090f, 1.0f);

        //数据节点模块的颜色
        private Color _nodeDataColor = new Color(0.435f, 0.376f, 0.667f, 1.0f);

        //步骤模块的颜色
        private Color _stepColor = new Color(0.439f, 0.631f, 0.624f, 1.0f);

        //调试模块的颜色
        private Color _debugColor = new Color(1f, 0.100f, 0.888f, 1.0f);

        //所有的模块
        private List<PEIModule_EditorOrigin> _listModuleEditors;

        private void OnEnable()
        {
            _listModuleEditors = new List<PEIModule_EditorOrigin>();

            _entity = target as PEIMEN_Entity;

            _defaultColor = GUI.color;
            
            _listModuleEditors.Add(new PEIState_ModuleEditor("State Module", _stateColor, _entity));
            _listModuleEditors.Add(new PEIKDE_ModuleEditor("PEIKDE Module", _stateColor, _entity));
        }

        private void OnDisable()
        {
            if (_listModuleEditors == null)
                return;

            for (int i = 0; i < _listModuleEditors.Count; i++)
            {
                _listModuleEditors[i].OnClose();
            }
            _listModuleEditors.Clear();
        }

        public override void OnInspectorGUI()
        {
            if (_entity == null || _listModuleEditors == null)
                return;

            GUILayout.BeginVertical();

            for (int i = 0; i < _listModuleEditors.Count; i++)
            {
                _listModuleEditors[i].OnInspectorGUI();
            }

            GUILayout.EndVertical();

            EditorUtility.SetDirty(_entity);
        }

    }
}
