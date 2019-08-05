///////////////////////////////////////////////////
////
////PEIMEN Frame System || Frame branch 
////
////creat by PEIKnifer[.CN]
////
////Frame for GamePad
////
////Create On 2019-3
////
////Last Update in 2019.3.14 15:49:03
////
///////////////////////////////////////////////////

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Valve.VR;

//namespace PEIKBF_SSP
//{
//    public abstract class HTCGamePadBase : GamePad
//    {
//        public SteamVR_Input_Sources handType;

//        //public SteamVR_Behaviour_Pose trackedObject;

//        public SteamVR_Action_Boolean grabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

//        public SteamVR_Action_Boolean grabGripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");

//        public SteamVR_Action_Vibration hapticAction = SteamVR_Input.GetAction<SteamVR_Action_Vibration>("Haptic");

//        public SteamVR_Action_Boolean uiInteractAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

//        public SteamVR_Action_Boolean TeleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");

//        public override abstract void RayButtonPush(int state, GameObject obj);

        
//        protected virtual bool OnShootTriggerDown() { return grabPinchAction.GetStateDown(handType); }
//        protected virtual bool OnShootTriggerUp() { return grabPinchAction.GetStateUp(handType); }
//        protected bool OnPadDown() { return TeleportAction.GetStateDown(handType); }
//        protected virtual bool OnSideButtonDown() { return grabGripAction.GetStateDown(handType); }

//    }
//}