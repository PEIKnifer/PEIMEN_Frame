/////////////////////////////////////////////////
//
//PEIMEN Frame System || GeneralObjMove branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for GeneralObjFollow
//
//Create On 2016-5
//
//Last Update in 2019-12-5 18:07:03
//
/////////////////////////////////////////////////
using PEIMEN;
using PEIMEN.Origin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTS
{
    public class PEIMEN_ST_Screen : PEIModel_Origin
    {
        private Vector2 _resolution,_tool;
        //private PEIKnifer_LOrigin _l;
        public PEIMEN_ST_Screen()
        {
            _resolution.x = 1280;
            _resolution.y = 720;
            // _l = PEIMEN_Entity.L.AddL();
        }
        public Vector2 GetScreenScale()
        {
            _tool.x = Screen.width / _resolution.x;
            _tool.y = Screen.height / _resolution.y;
            return _tool;
        }
        public float GetScreenWidthScale()
        {
            _tool.y = Screen.height / _resolution.y;
            return _tool.y;
        }
        public float GetScreenHeightScale()
        {
            _tool.x = Screen.width / _resolution.x;
            return _tool.x;
        }

        public void SetResolution(int width,int height)
        {
            _resolution.x = width;
            _resolution.y = height;
        }
        public void SetResolution(Vector2 resolution)
        {
            _resolution = resolution;
        }
        public override void OnClose()
        {
            //if (PEIMEN_Entity.L.HasL(_l))
            //    PEIMEN_Entity.L.RemoveL(_l);
        }
    }
}