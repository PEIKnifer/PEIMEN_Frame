/////////////////////////////////////////////////
//
//PEIMEN Frame System || Template branch 
//
//creat by PEIKnifer[.CN]
//
//Template for UI Blink Eyes
//
//Create On 2019-4
//
//Last Update in 2019 4 25 14:39:11
//
/////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIKDL;
using UnityEngine.UI;

namespace PEIKTL
{
    public class VREye :  PEIKnifer_Singleton
    {
        public static VREye Ins;
        public Image EyeBlack;
        private PEIKnifer_Delegate_Void_Void EyeBlinkDel;
        private PEIKnifer_Delegate_Void_Void EyeCloseDel;
        public float BlinkSpeed = 0.2f;
        // Use this for initialization
        void Awake()
        {
            Ins = this;
            EyeBlinkDel = EyeOpen;
            EyeCloseDel = Null;
        }

        // Update is called once per frame
        void Update()
        {
            EyeBlinkDel();
        }
        public void BlinkEyes()
        {
            EyeBlinkDel = EyeClose;
        }
        public void BlinkEyes(PEIKnifer_Delegate_Void_Void del)
        {
            EyeCloseDel += del;
            EyeBlinkDel = EyeClose;
        }

        public void EyeCloseDelAddFunction(PEIKnifer_Delegate_Void_Void del)
        {
            EyeCloseDel += del;
        }

        private void EyeClose()
        {
            if (EyeBlack.color.a < 1)
            {
                EyeBlack.color = new Color(EyeBlack.color.r, EyeBlack.color.g, EyeBlack.color.b, EyeBlack.color.a + Time.deltaTime * BlinkSpeed);
            }
            else
            {
                EyeBlinkDel = EyeOpen;
                EyeCloseDel();
                EyeCloseDel = Null;
            }
        }
        private void EyeOpen()
        {
            if (EyeBlack.color.a > 0)
            {
                EyeBlack.color = new Color(EyeBlack.color.r, EyeBlack.color.g, EyeBlack.color.b, EyeBlack.color.a - Time.deltaTime * BlinkSpeed);
            }
            else
            {
                EyeBlinkDel = Null;
            }
        }

        private void Null()
        {

        }
    }
}
