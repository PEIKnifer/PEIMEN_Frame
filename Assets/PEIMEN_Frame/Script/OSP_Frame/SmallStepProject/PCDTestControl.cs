/////////////////////////////////////////////////
//
//PEIMEN Frame System || Frame branch 
//
//creat by PEIKnifer[.CN]
//
//Tools for PCDTestControl
//
//Create On 2019-3
//
//Last Update in 2019.3.18 17:17:13
//
/////////////////////////////////////////////////
using PEIKBF_SSP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKBF_SSP
{
    // PEIKnifer Simple Small Project PC Du TestControl Class
    public class PCDTestControl : PEIKnifer
    {
        public StepManager Manager;
        public OperationPartBase PartBase;
        private bool _switch; 

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.E))
            {
                Manager.RefreshStep();
            }
            if (Input.GetKeyUp(KeyCode.B) || Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                _switch = false;
            }
            if (_switch)
              return; 
            // Key B can auto play step
            if (Input.GetKey(KeyCode.B)|| Input.GetKey(KeyCode.KeypadEnter))
            {
                PartBase.Status = (int)PEIKEM_PartBaseStatus.Done;
                _switch = true;
            }
        }
    }
}
