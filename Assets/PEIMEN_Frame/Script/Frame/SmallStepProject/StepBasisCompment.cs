/////////////////////////////////////////////////
//
//PEIMEN Frame System || Frame branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for StepManager
//
//Create On 2019-3
//
//Last Update in 2019.3.14 14:45:27
//
/////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKBF_SSP
{
    //PEIKnifer Simple Small Project Step Basis Compment Class
    [Serializable]
    public class StepBasisCompment : PEIKnifer
    {

        #region  Inherent References
        // Hint UI References
        [SerializeField]
        public GameObject UIObj;
        // Hint UI Position
        [SerializeField]
        public GameObject UIPos;
        // Step Operation Base
        [SerializeField]
        public OperationBase OperationBaseIns;
        // Step Operation Part Base
        [SerializeField]
        public OperationPartBase OperationPartBaseIns;
        // Step Need Show GameObject
        [SerializeField]
        public GameObject NeedShowObj;

        #endregion

        #region  Inherent Value
        //Step ID
        [SerializeField]
        public int Id;
        //Hint UI Text
        [SerializeField]
        public string TipText;

        #endregion

    }
}