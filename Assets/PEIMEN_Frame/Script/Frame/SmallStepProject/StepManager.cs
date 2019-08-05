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
//Last Update in 2019.3.14 11:58:44
//
/////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKBF_SSP
{
    //PEIKnifer Simple Small Project Step Manager Class 
    public abstract class StepManager : PEIKnifer
    {

        #region  Inherent value
        // All Step Basis Compment Ins
        [SerializeField]
        public List<StepBasisCompment> stepBases = new List<StepBasisCompment>();

        // Hint whether Need Loader
        public bool NeedLoader;

        // Step List Loader
        public StepLoader StepLoader;

        // Pc Test Controller
        public PCDTestControl PCDTestControl;

        // Hint Now Step Num 
        public int NowStep;

        // Hint GameOver Flag
        public bool overFlag;

        // Hint Need Audio Flag
        public bool NeedAudioFlag;
        #endregion

        #region  Inherent Function 
        // On Frame Init Function  need Init In System Start() 
        protected void FrameInit()
        {
            if (StepLoader && NeedLoader)
            {
                stepBases = StepLoader.StepList;
            }
            overFlag = false;
            NowStep = 0;
            try
            { 
            BeginStep();
            }
            catch
            {
                PEIKDE.LogError("STM", "Step Audio Manager Ins Get Error");
            }
            if (NeedAudioFlag && StepAudioManager.ins)
            {
                StepAudioManager.ins.ChangeAudio(0);
                PEIKDE.Log("STM", "Step Audio Flag On");
            }
            else
            {
                if (!StepAudioManager.ins)
                {
                    PEIKDE.Log("STM", "Step Audio Manager Ins Null");
                }
                if (!NeedAudioFlag)
                {
                    PEIKDE.Log("STM", "Step Audio Flag OFF");
                }
            }
        }

        //Use For Auto Play Step
        protected void AutoPlayStep()
        {
            try
            {
                stepBases[NowStep].OperationBaseIns.AutoPlay();
                NextStep();
            }
            catch (Exception e)
            {
                PEIKDE.LogError("STM", "Auto Play Next Step Error!");
                PEIKDE.LogError("STM", e.ToString());
            }
        }

        // On Step Operation Part Status Change Init Function
        protected void OnStatusChange(int status)
        {
            if (status == (int)PEIKEM_PartBaseStatus.Done)
            {
                //Debug.Log("Status Done By ---> " + stepBases[NowStep].OperationBaseIns.gameObject.name);
                NextStep();
            }
        }

        // On Manager Go To Next Step Function
        protected void NextStep()
        {
            StepOver();
            ++NowStep;
            NowStep += NowStep < stepBases.Count ? 0 : -1;
            try
            {
                if (NeedAudioFlag && StepAudioManager.ins)
                    StepAudioManager.ins.ChangeAudio(NowStep);
            }
            catch
            {
                PEIKDE.LogError("STM", "Step Audio Manager Ins Get Error");
            }
            BeginStep();
            if (NowStep == stepBases.Count - 1)
            {
                if (overFlag)
                {
                    if (NeedAudioFlag && StepAudioManager.ins)
                        StepAudioManager.ins.ChangeAudio(NowStep + 1);
                    PEIKDE.Log("STM", "NowStepOverFalse player step --> " + NowStep);
                }
                else
                {
                    if (NeedAudioFlag && StepAudioManager.ins)
                        StepAudioManager.ins.ChangeAudio(NowStep);

                    overFlag = true;
                    PEIKDE.Log("STM", "NowStepOverTrue player step --> " + NowStep);
                }
            }
            else
            {
                PEIKDE.Log("STM","player step --> " + NowStep);
                if (NeedAudioFlag && StepAudioManager.ins)
                    StepAudioManager.ins.ChangeAudio(NowStep);
            }
        }

        // Running On Next Step Begin Step
        protected void BeginStep()
        {
            stepBases[NowStep].UIObj.SetActive(true);
            stepBases[NowStep].NeedShowObj.SetActive(true);
            stepBases[NowStep].UIObj.transform.position = stepBases[NowStep].UIPos.transform.position;
            stepBases[NowStep].UIObj.transform.rotation = stepBases[NowStep].UIPos.transform.rotation;
            stepBases[NowStep].OperationPartBaseIns.gameObject.SetActive(true);
            stepBases[NowStep].OperationPartBaseIns.Refresh();
            stepBases[NowStep].OperationBaseIns.OnBegin();
            stepBases[NowStep].OperationPartBaseIns.OperationStatusChange.AddListener(OnStatusChange);
            //stepBases[NowStep].OperationPartBaseIns.OnStatusChange((int)PartBaseStatus.Done);
            stepBases[NowStep].OperationPartBaseIns.PartBaseAddEvent();
            if (PCDTestControl)
            {
                PCDTestControl.PartBase = stepBases[NowStep].OperationPartBaseIns;
            }
        }

        // Running On Last Step Over 
        protected void StepOver()
        {
            stepBases[NowStep].UIObj.SetActive(false);
            stepBases[NowStep].NeedShowObj.SetActive(false);
            stepBases[NowStep].OperationBaseIns.OnExit();
            stepBases[NowStep].OperationPartBaseIns.OperationStatusChange.RemoveListener(OnStatusChange);
            stepBases[NowStep].OperationPartBaseIns.PartBaseRemoveEvent((int)PEIKEM_PartBaseStatus.Done);
        }
        #endregion
    }
}