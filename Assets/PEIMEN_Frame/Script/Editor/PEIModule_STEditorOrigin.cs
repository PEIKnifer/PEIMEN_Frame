using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PEIMEN.Origin
{
    public abstract class PEIModule_STEditorOrigin
    {
        #region property
        //名称
        protected string _name;
        //主颜色
        protected Color _mainColor;
        //默认颜色
        protected Color _defaultColor;
        //主类
        protected PEIKST_Entity _entity;
        //展开
        protected bool _isExpand;
        #endregion

        public PEIModule_STEditorOrigin(string name, Color mainColor, PEIKST_Entity entity)
        {
            _name = name;
            _mainColor = mainColor;
            _defaultColor = GUI.color;
            _entity = entity;
            _isExpand = true;
        }


        //默认绘制界面
        public virtual void OnInspectorGUI()
        {
            GUI.color = _mainColor;
            GUILayout.BeginVertical("Box");
            GUI.color = _defaultColor;
            GUILayout.BeginHorizontal();
            GUILayout.Space(12);
            _isExpand = EditorGUILayout.Foldout(_isExpand, _name, true);
            GUILayout.EndHorizontal();
            if (_isExpand)
                OnDrawGUI();
            GUILayout.EndVertical();
        }

        //绘制界面
        public abstract void OnDrawGUI();

        //关闭界面
        public abstract void OnClose();

    }

}