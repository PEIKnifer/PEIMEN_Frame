
/////////////////////////////////////////////////
//
//PEIMEN Frame System || GeneralObjMove branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for SimpleObjRotate
//
//Create On 2017-12-26 11:40:08
//
//Last Update in 2017-12-26 11:40:08
//
/////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIKDL;

namespace PEIKTS
{

    public class PEIKnifer_ObjSimpleRotate : PEIKnifer
    {

        #region Inherent Value
        public ObjSimpleRotateState objSimpleRotateState;
        public bool x,
                    y,
                    z;
        public float xSpeed,
                     ySpeed,
                     zSpeed,
                     followSpeed,
                     turnSpeed;
        public Vector3 target;
        private PEIKnifer_Delegate_Void_Void frameOrder, MoveOrder;
        #endregion

        #region Inherent Frame Function
        public void FrameInit()
        {
            frameOrder = Null;
            switch (objSimpleRotateState)
            {
                case ObjSimpleRotateState.simple:
                    SimpleInit();
                    break;
                case ObjSimpleRotateState.position:
                    frameOrder = PositionOrder1;
                    break;
            }
        }
        public void FrameUpdate()
        {
            frameOrder();
        }
        public IEnumerator FrameUpdateIE()
        {
            FrameUpdate();
            yield return 0;
        }
        #endregion

        #region Inherent Function

        public void SimpleInit()
        {
            if (x)
                frameOrder += SimpleMoveX;
            if (y)
                frameOrder += SimpleMoveY;
            if (z)
                frameOrder += SimpleMoveZ;
        }
        public void SimpleMoveX()
        {
            transform.Rotate(Time.deltaTime * xSpeed, 0, 0);
        }
        public void SimpleMoveY()
        {
            transform.Rotate(0, Time.deltaTime * ySpeed, 0);
        }
        public void SimpleMoveZ()
        {
            transform.Rotate(0, 0, Time.deltaTime * zSpeed);
        }

        public void PositionOrder1()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target - transform.position), Time.deltaTime * turnSpeed);//平滑旋转

            if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(target - transform.position)) < 1f)
                frameOrder = Null;
        }
        public void Null()
        {

        }
        #endregion
    }
}