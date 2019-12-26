/////////////////////////////////////////////////
//
//PEIMEN Frame System || PlayerControl branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for RigidbodyControl
//
//Create On 2017-12-25 16:29:54
//
//Last Update in 2017-12-25 16:29:54
//
/////////////////////////////////////////////////

using PEIKDL;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTS
{
    [Serializable]
    public class PEIKnifer_SimpleCamera : PEIKnifer
    {

        #region Inherent value
        public GameObject tGB;
        public GameObject boss;
        public GameObject Player;
        public static GameObject oTGB;
        public float Yf;
        public float Zf;
        Transform target;
        Vector3 offsetPos; //new Vector3(5, 2, -2);
        Vector3 lOffsetPos; //new Vector3(4, 3.25f, 4);
        private float hHeigth;
        private float lHeight;
        private bool hChangeFlag;
        public static int sceness;
        public float followSpeed = 0.23f;
        int spNum = 40;
        /// <summary>
        /// //////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        private float shakeTime = 0.0f;
        private float fps = 20.0f;
        private float frameTime = 0.0f;
        private float shakeDelta = 0.05f;
        public Camera cam;
        public static bool isshakeCamera = false;
        public PEIKnifer_Delegate_Void_Void bossCamOrder;
        public bool bossShow;
        /// <summary>
        /// /////////////////////////////////////////////////
        /// </summary>
        /// 
        #endregion

        #region Inherent Frame Function

        public void CameraShakeInit()
        {
            shakeTime = 2.0f;
            fps = 20.0f;
            frameTime = 0.03f;
            shakeDelta = 0.01f;
            isshakeCamera = false;
        }

        public IEnumerator CameraSimpleFollowUpdate()
        {
            if (target)
            {
                UpdatePosition();
            }
            yield return 0;
        }
        public IEnumerator CameraShakeUpdate()
        {
            CameraShakeUpdateFunction();
            yield return 0;
        }

        public void CameraShakeUpdateFunction()
        {
            if (isshakeCamera)
            {
                if (shakeTime > 0)
                {
                    shakeTime -= Time.deltaTime;
                    if (shakeTime <= 0)
                    {
                        cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                        isshakeCamera = false;
                        shakeTime = 1.0f;
                        fps = 20.0f;
                        frameTime = 0.03f;
                        shakeDelta = 0.01f;
                    }
                    else
                    {
                        frameTime += Time.deltaTime;
                        if (frameTime > 1.0 / fps)
                        {
                            frameTime = 0;
                            cam.rect = new Rect(shakeDelta * (-1.0f + 2.0f * UnityEngine.Random.value), shakeDelta * (-1.0f + 2.0f * UnityEngine.Random.value), 1.0f, 1.0f);
                        }
                    }
                }
            }
        }

        public void SimpleCameraFollowInit(Vector3 op, Vector3 lop)
        {
            offsetPos = op;
            lOffsetPos = lop;
            sceness = 0;
            target = tGB.transform;
            SetupCamera();
            oTGB = tGB;
            hHeigth = 0;
            lHeight = 0f;
        }
        #endregion

        #region Inherent Function
        void SetupCamera()
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-offsetPos), Time.deltaTime * followSpeed * 500);//smooth rotate
        }
        //update camera position
        private void UpdatePosition()
        {
            if (!tGB.Equals(oTGB))
            {
                tGB = oTGB;
                try
                {
                    target = tGB.transform;
                }
                catch
                {
                }
                offsetPos = lOffsetPos;
                hChangeFlag = true;
            }
            if (boss && Vector3.Distance(boss.transform.position, transform.position) < 15 && !bossShow)
            {
                bossShow = true;
                bossCamOrder = Order1;

            }
            if (hChangeFlag)
            {
                HeightChange();
            }
            else
            {
                SetupCamera();
                Vector3 targetPos = target.transform.position + offsetPos;
                targetPos.Set(targetPos.x, target.transform.position.y + 8, targetPos.z + Zf);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime * 50);//smooth move


            }
            try
            {
                bossCamOrder();
            }
            catch { }
        }
        public void Order1()
        {
            hChangeFlag = true;
            if (Time.timeScale > 0.1)
            {
                Time.timeScale -= Time.unscaledDeltaTime;
            }
            else
            {
                bossCamOrder = Order2;
            }
        }

        public void Order2()
        {
            hChangeFlag = false;
            if (Time.timeScale < 0.95f)
            {
                Time.timeScale += Time.unscaledDeltaTime;
            }
            else
            {
                Time.timeScale = 1;
                bossCamOrder = null;
            }
        }
        private void HeightChange()
        {



            SetupCamera();
            Vector3 targetPos = boss.transform.position + offsetPos * 0.75f;
            targetPos.Set(targetPos.x, boss.transform.position.y + 6, targetPos.z + Zf);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime * 50 * 2);//smooth move


        }
        #endregion

    }
}
