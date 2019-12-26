/////////////////////////////////////////////////
//
//PEIMEN Frame System || SimpleTool branch 
//
//creat by PEIKnifer[.CN]
//
//SimpleTool for PEIKnifer Math Class
//
//Create On 2019-10-9 17:51:55
//
//Last Update in 2019-10-9 17:51:55  
//
/////////////////////////////////////////////////
using PEIKTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIMEN.Origin
{
    /// <summary>
    /// PEIKnifer Math Class
    /// </summary>
    public class PEIMath : PEIModel_Origin
    {
        /// <summary>
        /// PEIMath Class Random Object
        /// </summary>
        public PEIRNG Random;
        public PEIMath()
        {
            Random = new PEIRNG();
        }

        public override void OnClose()
        {
            Random = null;
        }
    }
}