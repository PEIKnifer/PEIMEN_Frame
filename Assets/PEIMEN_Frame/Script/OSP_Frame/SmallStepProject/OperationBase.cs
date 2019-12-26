/////////////////////////////////////////////////
//
//PEIMEN Frame System || Frame branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for OperationBase
//
//Create On 2019-3
//
//Last Update in 2019.3.14 15:49:03
//
/////////////////////////////////////////////////
using PEIKDL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKBF_SSP
{
    //PEIKnifer Simple Small Project Step Operation Base Class
    public abstract class OperationBase : PEIKnifer
    {

        #region  Inherent References
        // Step Operation Part Base
        public OperationPartBase PartBase;
        // Step GameObject Mesh
        public MeshRenderer ShowMesh;
        // Step GameObject Alpha Hint Flag
        public bool AlphaFlag;

        // Step GameObject Old Material
        protected Material[] _oldMaterial;
        // Step GameObject On Trigger Enter Material
        protected Material[] _triggerEnterMat;
        // Step GameObject On Trigger Exit Material
        protected Material[] _triggerExitMat;
        // Runing At GamePad In;
        public PEIKnifer_Delegate_Void_Void RayInDel;
        // Runing At GamePad Exit;
        public PEIKnifer_Delegate_Void_Void RayExitDel;
        #endregion

        #region  Inherent Function

        // Running On Trigger Down Function
        public virtual void OnTriggerButtonDown() { }

        // Running On Trigger Trigger Function
        public virtual void OnTriggerButton() { }

        // Running On Trigger Down Function
        public virtual void OnTriggerButtonUp() { }

        // Running On GameObject Get Caught Function
        public abstract bool OnObjGetCaught();

        /// <summary>
        /// Running On Function Begin
        /// </summary>
        public virtual void OnBegin() { }

        /// <summary>
        /// Running On Function Exit
        /// </summary>
        public virtual void OnExit() { }
        /// <summary>
        /// Running On Function Refresh
        /// </summary>
        public virtual void OnRefresh() { }

        // Running On AutoPlay Trigger
        public virtual void AutoPlay() { }

        //running On GamePad Ray Hit In
        public virtual void OnRayIn()
        {
            if (RayInDel != null)
                RayInDel();
        }

        //running On GamePad Ray Exit
        public virtual void OnRayExit()
        {
            if (RayExitDel != null)
                RayExitDel();
        }

        //running On GamePad Ray Exit
        public virtual void OnRayButtonClick(){ }

        #endregion

        protected virtual void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.tag == "GamePad")
            {
                OnRayIn();
            }
        }
        protected virtual void OnTriggerExit(Collider other)
        {

            if (other.gameObject.tag == "GamePad")
            {
                OnRayExit();
            }
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (ShowMesh)
        //    {
        //        if (other.gameObject.tag == "GamePad")
        //        {
        //            //Debug.Log("Mat Tri In");
        //            ShowGreegShader();
        //        }
        //    }
        //}
        //private void OnTriggerExit(Collider other)
        //{
        //    if (ShowMesh)
        //    {
        //        if (other.gameObject.tag == "GamePad")
        //        {
        //            Debug.Log("Mat Tri Exit");
        //            ShowRedShader();
        //        }
        //    }
        //}
    }
}
