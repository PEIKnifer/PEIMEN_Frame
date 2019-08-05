
/////////////////////////////////////////////////
//
//PEIMEN Frame System || UICenterControl branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for BTNSimple
//
//Create On 2017-12-29 11:46:38
//
//Last Update in 2017-12-29 11:46:41
//
/////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTS
{

    public class PEIKnifer_BTNSimple : MonoBehaviour
    {

        #region Inherent Value;
        public GameObject obj;
        public bool switchOF;
        #endregion

        #region Inherent Function
        public void OnClick()
        {
            obj.SetActive(switchOF);
        }
        #endregion

    }
}