
/////////////////////////////////////////////////
//
//PEIMEN Frame System || GeneralObjMove branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for SimpleObjMove
//
//Create On 2017-12-26 11:40:08
//
//Last Update in 2017-12-26 11:40:08
//
/////////////////////////////////////////////////

using PEIKDL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTS
{

    public class PEIKnifer_ObjSimpleMove : PEIKnifer
    {

        #region Inherent Value
        public PEIKEM_ObjSimpleMoveState objSimpleMoveState;
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
            switch (objSimpleMoveState)
            {
                case PEIKEM_ObjSimpleMoveState.simple:
                    SimpleInit();
                    break;
                case PEIKEM_ObjSimpleMoveState.position:
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
            transform.position += transform.right * Time.deltaTime * xSpeed;
        }
        public void SimpleMoveY()
        {
            transform.position += transform.up * Time.deltaTime * ySpeed;
        }
        public void SimpleMoveZ()
        {
            transform.position += transform.forward * Time.deltaTime * zSpeed;
        }

        public void PositionOrder1()
        {
            transform.position = Vector3.MoveTowards(transform.position, target, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target - transform.position), Time.deltaTime * turnSpeed);//平滑旋转

            if (Vector3.Distance(transform.position, target) < 0.1f)
                frameOrder = Null;
        }
        public void Null()
        {

        }
        #endregion

    }
}
