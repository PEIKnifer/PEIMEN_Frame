/////////////////////////////////////////////////
//
//PEIMEN Frame System || Frame branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for StepAudioManager
//
//Create On 2019-3
//
//Last Update in 2019.3.14 11:58:44
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKBF_SSP
{
    //PEIKnifer Simple Small Project Step Audio Manager Class
    public class StepAudioManager : PEIKnifer
    {

        #region  Inherent References
        public static StepAudioManager ins;

        [SerializeField]
        public List<AudioClip> audioClips;

        public AudioSource AudioSource;
        #endregion

        #region  Inherent Function
        // need init in system Awake() function  
        protected void FrameInitAwake()
        {
            ins = this;
        }


        public void ChangeAudio(int ID)
        {
            try
            {
                AudioSource.Stop();
                AudioSource.clip = audioClips[ID];
                AudioSource.Play();
            }
            catch
            {
                PEIKDE.LogError("SAM","Change Audio Error!!!");
            }
        }
        #endregion
    }
}
