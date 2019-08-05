/////////////////////////////////////////////////
//
//PEIMEN Frame System || PlayerControl branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for CharacterController
//
//Create On 2017-12-25 14:26
//
//Last Update in 2017-12-25 14:26
//
/////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTS
{
    public class PEIKnifer_CharacterControl : PEIKnifer
    {

        #region Inherent Value
        protected CharacterController myCharacterController;
        protected float gravity;
        protected Vector3 moveDirection;
        protected float maxSpeed;
        protected float resistance;
        protected float acceleratedSpeed;
        protected float maxTurnSpeed;
        protected float turnResistance;
        protected float turnAcceleratedSpeed;
        private float speed;
        private float turnspeed;
        #endregion

        #region SetValue
        public CharacterController PCC_myCharacterController
        {
            get { return myCharacterController; }
            set { myCharacterController = value; }
        }//get CC ins;

        public float PCC_gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }//set gravity;
        public Vector3 PCC_moveDirection
        {
            get { return moveDirection; }
            set { moveDirection = value; }
        }//set moveDirection;
        public float PCC_maxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }//set velocity speed;
        public float PCC_resistance
        {
            get { return resistance; }
            set { resistance = value; }
        }//set velocity resistance;
        public float PCC_acceleratedSpeed
        {
            get { return acceleratedSpeed; }
            set { acceleratedSpeed = value; }
        }//set velocity accelerated Speed;
        public float PCC_maxTurnSpeed
        {
            get { return maxTurnSpeed; }
            set { maxTurnSpeed = value; }
        }//set velocity max Turn Speed;
        public float PCC_turnAesistance
        {
            get { return turnResistance; }
            set { turnResistance = value; }
        }//set velocity turn resistance;
        public float PCC_turnAcceleratedSpeed
        {
            get { return turnAcceleratedSpeed; }
            set { turnAcceleratedSpeed = value; }
        }//set velocity turn Accelerated Speed;
        #endregion

        #region Inherent Frame Function
        protected void Frame_Init(float gravity,
                                    float speed,
                                    float resistance,
                                    float acceleratedSpeed,
                                    float turnSpeed,
                                    float turnResistance,
                                    float turnAcceleratedSpeed)
        {
            PCC_gravity = gravity;
            PCC_moveDirection = Vector3.zero;
            PCC_maxSpeed = speed;
            PCC_resistance = resistance;
            PCC_acceleratedSpeed = acceleratedSpeed;
            PCC_maxTurnSpeed = turnSpeed * 5;
            PCC_turnAesistance = turnResistance;
            PCC_turnAcceleratedSpeed = turnAcceleratedSpeed;
            GetCharacterControllerIns();
        }
        protected void Frame_Init(float gravity,
                                    float speed,
                                    float turnSpeed)
        {
            PCC_gravity = gravity;
            PCC_moveDirection = Vector3.zero;
            PCC_maxSpeed = speed;
            PCC_resistance = 0;
            PCC_acceleratedSpeed = 999999;
            PCC_maxTurnSpeed = turnSpeed;
            PCC_turnAesistance = 0;
            PCC_turnAcceleratedSpeed = 999999;
            GetCharacterControllerIns();
        }
        #endregion

        #region Inherent Function

        protected void GetCharacterControllerIns()//get cc from transform.(cc must on transform!)
        {
            try
            {
                PCC_myCharacterController = transform.GetComponent<CharacterController>();
            }
            catch
            {
                Debug.LogError("Error when try to find Character Controller on " + gameObject.name + "!! maybe there is on Character Controller ins...");
            }
        }
        protected void SimulatedGravity()//Simulated gravity function.
        {
            //moveDirection.y -= gravity * Time.deltaTime;
            if (!PCC_myCharacterController.isGrounded)
                PCC_myCharacterController.Move(Vector3.down * gravity * Time.deltaTime);
        }
        protected void Simulatedresistance()
        {
            speed = (speed - resistance * Time.deltaTime) < 0 ? 0 : speed - resistance * Time.deltaTime;
            turnspeed = (turnspeed - turnResistance * Time.deltaTime) < 0 ? 0 : turnspeed - turnResistance * Time.deltaTime;
        }
        protected void FrameUpdate()
        {
            Simulatedresistance();
            SimulatedGravity();
        }
        protected bool SmoothMove(Vector3 target, Vector3 finalLookTarget, float moveStandard)//move to tar .(there is a final tar in the end.)
        {
            target = new Vector3(target.x, target.y, target.z);
            if (Vector3.Distance(transform.position, target) > moveStandard)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, maxSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target - transform.position), Time.deltaTime * 10);//平滑旋转
                return false;
            }
            if (Vector3.Distance((finalLookTarget - transform.position), Vector3.zero) != 0 && Quaternion.Angle(transform.rotation, Quaternion.LookRotation(finalLookTarget - transform.position)) > 0.1)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(finalLookTarget - transform.position), Time.deltaTime * 10);
                return false;
            }
            return true;
        }

        protected void SimpleMoveRun(int direction)//use cc system simple move function. (attention in speed * like 10!!! )
        {
            myCharacterController.SimpleMove(transform.forward * direction * Time.deltaTime * maxSpeed);
        }
        protected void SimpleMoveOffset(int direction)//use cc system simple move function. (attention in speed * like 10!!! )
        {
            myCharacterController.SimpleMove(transform.right * direction * Time.deltaTime * maxSpeed);
        }
        protected void MoveRun(int direction)//use cc system move function. (attention in speed * like 1!!! )
        {
            myCharacterController.Move(transform.forward * direction * Time.deltaTime * maxSpeed);
        }
        protected void MoveOffset(int direction)//use cc system move function. (attention in speed * like 1!!! )
        {
            myCharacterController.Move(transform.right * direction * Time.deltaTime * maxSpeed);
        }

        #region ACFunction 

        protected void SimpleACMoveRun(int direction)//AC!! use cc system simple move function. (attention in speed * like 10!!! )
        {
            speed = (speed + acceleratedSpeed * Time.deltaTime) > maxSpeed ? maxSpeed : speed + acceleratedSpeed * Time.deltaTime;
            myCharacterController.SimpleMove(transform.forward * direction * Time.deltaTime * speed);
        }
        protected void SimpleACMoveOffset(int direction)//AC!! use cc system simple move function. (attention in speed * like 10!!! )
        {
            speed = (speed + acceleratedSpeed * Time.deltaTime) > maxSpeed ? maxSpeed : speed + acceleratedSpeed * Time.deltaTime;
            myCharacterController.SimpleMove(transform.right * direction * Time.deltaTime * speed);
        }
        protected void ACMoveRun(int direction)//AC!! use cc system move function. (attention in speed * like 1!!! )
        {
            speed = (speed + acceleratedSpeed * Time.deltaTime) > maxSpeed ? maxSpeed : speed + acceleratedSpeed * Time.deltaTime;
            myCharacterController.Move(transform.forward * direction * Time.deltaTime * speed);
        }
        protected void ACMoveOffset(int direction)//AC!! use cc system move function. (attention in speed * like 1!!! )
        {
            speed = (speed + acceleratedSpeed * Time.deltaTime) > maxSpeed ? maxSpeed : speed + acceleratedSpeed * Time.deltaTime;
            myCharacterController.Move(transform.right * direction * Time.deltaTime * speed);
        }
        #endregion
        protected void SimpleTurn(int direction)
        {
            transform.Rotate(0, direction * Time.deltaTime * maxTurnSpeed, 0);
        }

        protected void SimpleControlTest()
        {
            if (Input.GetKey(KeyCode.W))
            {
                ACMoveRun(1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                ACMoveRun(-1);
            }
            if (Input.GetKey(KeyCode.A))
            {
                SimpleTurn(-1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                SimpleTurn(1);
            }

        }
        #endregion

    }
}
