/////////////////////////////////////////////////
//
//PEIMEN Frame System || Frame branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for OperationPartBase
//
//Create On 2019-3
//
//Last Update in 2019.3.14 18:00:30
//
/////////////////////////////////////////////////
using PEIKAL_ET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKBF_SSP
{
    // PEIKnifer Simple Small Project Operation Base Class
    public abstract class OperationPartBase : PEIKnifer
    {

        #region  Inherent value;
        public int Id;
        private int _status;
        public int Status
        {
            set
            {
                _status = value;
                OperationStatusChange.Invoke(_status);
            }
            get { return _status; }
        }
        #endregion

        #region  Inherent Function;
        public PEIKEM_PartBaseStatus PartStatus= PEIKEM_PartBaseStatus.NeedOperation;
        public PEI_IntEvent OperationStatusChange = new PEI_IntEvent();

        public OperationPartBase()
        {

        }
        public void PartBaseAddEvent()
        {
        }

        public void PartBaseRemoveEvent(int status)
        {
            OnStatusChange(status);
            OperationStatusChange.RemoveListener(OnStatusChange);
        }

        public abstract void OnStatusChange(int status);

        public virtual void Refresh()
        {
            Status = (int)PartStatus;
        }
        #endregion

    }
}