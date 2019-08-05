
/////////////////////////////////////////////////
//
//PEIMEN Frame System || PlayerControl branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for RigidbodyControl
//
//Create On 2017-12-25 17:42:43
//
//Last Update in 2017-12-25 17:42:43
//
/////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTS
{

    [SerializeField]
    public class PEIKnifer_RigidbodyControl : PEIKnifer
    {

        #region Inherent value
        protected Rigidbody myRigidbody;
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
        public Rigidbody PCC_myRigidbody
        {
            get { return myRigidbody; }
            set { myRigidbody = value; }
        }//get CC ins;

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
        protected void Frame_Init(
                                    float speed,
                                    float resistance,
                                    float acceleratedSpeed,
                                    float turnSpeed,
                                    float turnResistance,
                                    float turnAcceleratedSpeed)
        {
            PCC_maxSpeed = speed;
            PCC_resistance = resistance;
            PCC_acceleratedSpeed = acceleratedSpeed;
            PCC_maxTurnSpeed = turnSpeed;
            PCC_turnAesistance = turnResistance;
            PCC_turnAcceleratedSpeed = turnAcceleratedSpeed;
            GetCharacterControllerIns();
        }
        protected void Frame_Init(float speed)
        {
            PCC_maxSpeed = speed;
            PCC_resistance = 0;
            PCC_acceleratedSpeed = 999999;
            PCC_maxTurnSpeed = speed * 0.5f;
            PCC_turnAesistance = 0;
            PCC_turnAcceleratedSpeed = 999999;
            GetCharacterControllerIns();
        }
        protected void Frame_Init(float speed, float turnSpeed)
        {
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
                PCC_myRigidbody = transform.GetComponent<Rigidbody>();
            }
            catch
            {
                Debug.LogError("Error when try to find Rigidbody Controller on " + gameObject.name + "!! maybe there is no Rigidbody ins...");
            }
        }

        protected void Simulatedresistance()
        {
            speed = (speed - resistance * Time.deltaTime) < 0 ? 0 : speed - resistance * Time.deltaTime;
            turnspeed = (turnspeed - turnResistance * Time.deltaTime) < 0 ? 0 : turnspeed - turnResistance * Time.deltaTime;
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
            transform.position += (transform.forward * direction * Time.deltaTime * maxSpeed);
        }
        protected void SimpleMoveOffset(int direction)//use cc system simple move function. (attention in speed * like 10!!! )
        {
            transform.position += (transform.right * direction * Time.deltaTime * maxSpeed);
        }
        protected void MoveRun(int direction)//use rb system move function. (attention in speed * like 1!!! )
        {
            Debug.Log("DDD+" + Vector3.Distance(transform.GetComponent<Rigidbody>().velocity, Vector3.zero) + "mss*0.01=" + maxSpeed * 0.01);
            if (Vector3.Distance(transform.GetComponent<Rigidbody>().velocity, Vector3.zero) < maxSpeed * 0.01)
                myRigidbody.AddForce(transform.forward * direction * Time.deltaTime * maxSpeed);
        }
        protected void MoveOffset(int direction)//use rb system move function. (attention in speed * like 1!!! )
        {
            if (Vector3.Distance(transform.GetComponent<Rigidbody>().velocity, Vector3.zero) < maxSpeed * 0.01)
                myRigidbody.AddForce(transform.right * direction * Time.deltaTime * maxSpeed);
        }


        protected void SimpleTurn(int direction)
        {
            transform.Rotate(0, direction * Time.deltaTime * maxTurnSpeed, 0);
        }

        protected void SimpleControlTest()
        {
            if (Input.GetKey(KeyCode.W))
            {
                MoveRun(1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                MoveRun(-1);
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
