﻿/////////////////////////////////////////////////
//
//PEIMEN Frame System || SimpleTool branch 
//
//creat by PEIKnifer[.CN]
//
//SimpleTool for Simple Transform Module
//
//Create On 2019-10-9 15:40:42
//
//Last Update in 2019-12-5 18:07:50  
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIKDL;
using System.Threading;
using PEIKTS;
using PEIMEN;


namespace PEIKTS {
    /// <summary>
    /// Simple Tool Class Module For Simple Transform
    /// </summary>
    public class PEIMEN_STC_Trans : PEIKnifer_Origin
    {
        /// <summary>
        /// Trans Class Start Control Flag;
        /// </summary>
        public PEIKnifer_Flag Flag;

        private GameObject _obj,
                           _target,
                           _oldPosObj;

        private PEIKnifer_Timer t;
        private PEIKnifer_Delegate_Void_Void _del,
                                             _callBack;
        private TransModel model;
        private Vector3 _targetPos,
                        _targetOul,
                        _oldPos,
                        _oldScl,
                        _toolV3a,
                        _toolV3b,
                        _toolS3a,
                        _toolS3b;
        private Quaternion _targetRot,
                           _oldRot,
                           _toolQua,
                           _toolQub;
        private float _moveSpeed, 
                      _rotSpeed,
                      _scaleSpeed,
                      _deltaTime,
                      _doneDis,
                      _loopLifeTime;
        private int _toolNum;
        private bool _needRotate,
                     _needScale,
                     _parTrans,
                     _localFlag,
                     _loomFlag;
        private PEIKnifer_LOrigin _l;
        private SimpleTransType _transType;
        /// <summary>
        /// Trans Class Init Function For Mov Rot Scl With Target
        /// </summary>
        /// <param name="obj">need move object</param>
        /// <param name="target">target object</param>
        /// <param name="needRotate">whether need rotate</param>
        /// <param name="needScale">whether need scale</param>
        /// <param name="moveSpeed">object move speed</param>
        /// <param name="rotSpeed">object rotate speed</param>
        /// <param name="scaleSpeed">object scale speed</param>
        /// <param name="callBackT">move function callback function</param>
        /// <param name="type">object move type</param>
        public PEIMEN_STC_Trans(GameObject obj, GameObject target, bool needRotate, bool needScale, float moveSpeed, float rotSpeed, float scaleSpeed, PEIKnifer_Delegate_Void_Void callBackT, SimpleTransType type)
        {
            NormalInit(obj, needRotate, needScale, moveSpeed, rotSpeed, scaleSpeed, callBackT, type);
            _target = target;
        }
        /// <summary>
        /// Trans Class Init Function For Mov Rot Scl With Target
        /// </summary>
        /// <param name="obj">need move object</param>
        /// <param name="target">target object</param>
        /// <param name="needRotate">whether need rotate</param>
        /// <param name="needScale">whether need scale</param>
        /// <param name="moveSpeed">object move speed</param>
        /// <param name="rotSpeed">object rotate speed</param>
        /// <param name="scaleSpeed">object scale speed</param>
        /// <param name="callBackT">move function callback function</param>
        public PEIMEN_STC_Trans(GameObject obj, GameObject target, bool needRotate, bool needScale, float moveSpeed,float rotSpeed,float scaleSpeed,PEIKnifer_Delegate_Void_Void callBackT)
        {
            NormalInit(obj, needRotate, needScale, moveSpeed,rotSpeed, scaleSpeed,callBackT, SimpleTransType.MoveTowards);
            _target = target;
        }
        /// <summary>
        /// Trans Class Init Function For Mov Rot With Target(need use scale , please use another function)
        /// </summary>
        /// <param name="obj">need move object</param>
        /// <param name="target">target object</param>
        /// <param name="needRotate">whether need rotate</param>
        /// <param name="moveSpeed">object move speed</param>
        /// <param name="rotSpeed">object rotate speed</param>
        /// <param name="callBackT">move function callback function</param>
        /// <param name="type">object move type</param>
        public PEIMEN_STC_Trans(GameObject obj, GameObject target, bool needRotate, float moveSpeed, float rotSpeed, PEIKnifer_Delegate_Void_Void callBackT, SimpleTransType type)
        {
            NormalInit(obj, needRotate, false, moveSpeed, rotSpeed, 100, callBackT, type);
            _target = target;
        }
        /// <summary>
        /// Trans Class Init Function For Mov Rot With Target(need use scale , please use another function)
        /// </summary>
        /// <param name="obj">need move object</param>
        /// <param name="target">target object</param>
        /// <param name="needRotate">whether need rotate</param>
        /// <param name="moveSpeed">object move speed</param>
        /// <param name="rotSpeed">object rotate speed</param>
        /// <param name="callBackT">move function callback function</param>
        public PEIMEN_STC_Trans(GameObject obj, GameObject target, bool needRotate, float moveSpeed, float rotSpeed, PEIKnifer_Delegate_Void_Void callBackT)
        {
            NormalInit(obj, needRotate,false, moveSpeed, rotSpeed,100, callBackT, SimpleTransType.MoveTowards);
            _target = target;
        }
        /// <summary>
        /// Trans Class Init Function For Mov Rot With num (need use gameobject target or scale , please use another function)
        /// </summary>
        /// <param name="obj">need move object</param>
        /// <param name="needRotate">whether need rotate</param>
        /// <param name="moveSpeed">object move speed</param>
        /// <param name="rotSpeed">object rotate speed</param>
        /// <param name="targetPos">target position</param>
        /// <param name="targetRot">target rotation</param>
        /// <param name="callBackT">move function callback function</param>
        public PEIMEN_STC_Trans(GameObject obj, bool needRotate, float moveSpeed, float rotSpeed, Vector3 targetPos, Quaternion targetRot, PEIKnifer_Delegate_Void_Void callBackT)
        {
            NormalInit(obj, needRotate,false, moveSpeed, rotSpeed,100, callBackT, SimpleTransType.MoveTowards);
            _targetPos = targetPos;
            _targetRot = targetRot;
        }

        //this function was prepare for use euler angle rotate
        //public PEIMEN_STC_Trans(GameObject obj, bool needRotate, Vector3 targetPos, Vector3 targetOul)
        //{
        //}

        private void NormalInit(GameObject obj, bool needRotate,bool needScale,float speed,float rotSpeed,float scaleSpeed,PEIKnifer_Delegate_Void_Void callBackT, SimpleTransType type)
        {
            t = new PEIKnifer_Timer();
            _loomFlag = false;
            _loopLifeTime = 0;
            _doneDis = 0.01f;
            _transType = type;
            _needScale = needScale;
            _scaleSpeed = scaleSpeed;
            _callBack = callBackT;
            _rotSpeed = rotSpeed;
            _del = PEIKNF_NullFunction.NullFunction;
            _l = PEIMEN_Entity.L.AddL();
            _l.AddElement(TransUpdate);
            //l.AddElement(Update);
            //Awake();
            _needRotate = needRotate;
            _moveSpeed = speed;
            
            _obj = obj;
            Flag = new PEIKnifer_Flag(BeginFunc, BackFunc);
            _oldPos = _obj.transform.position;
            _oldScl = _obj.transform.localScale;
            _oldRot = _obj.transform.rotation;
            model = new TransModel();
            model.Target = _target;
            model.OldPos = _oldPos;
            model.Object = _obj;
            //PEITime.WeekUp();
        }
        /// <summary>
        /// Move To Target Function
        /// </summary>
        public void BeginFunc()
        {
            if (!_localFlag)
                _toolV3a = _obj.transform.position;
            else
                _toolV3a = _obj.transform.localPosition;
            if (_needRotate)
            {
                _toolQua = _obj.transform.rotation;
            }
            if (_needScale)
            {
                _toolS3a = _obj.transform.localScale;
            }
                _del = BeginStep;
        }

        /// <summary>
        /// Return To Begin State Function
        /// </summary>
        public void BackFunc()
        {
            if (!_localFlag)
                _toolV3a = _obj.transform.position;
            else
                _toolV3a = _obj.transform.localPosition;
            _del = BackStep;
            if (_needRotate)
            {
                _toolQua = _obj.transform.rotation;
            }
            if (_needScale)
            {
                _toolS3a = _obj.transform.localScale;
            }
        }
        private void MoveStepLoom()
        {
            //PEIKDE.Log("STC", "Move Step!");
            void Asyn()
            {
                //PEIKDE.Log("PST", "Asyn!");
                int toolNum = 0;
                if (_transType == SimpleTransType.MoveTowards)
                    _toolV3a = Vector3.MoveTowards(_toolV3a, _toolV3b, _moveSpeed * PEIMEN_Entity.Time.DeltaTime);
                else
                    _toolV3a = Vector3.Lerp(_toolV3a, _toolV3b, _moveSpeed * PEIMEN_Entity.Time.DeltaTime);
                if (_needRotate)
                {
                    if (_transType == SimpleTransType.MoveTowards)
                        _toolQua = Quaternion.RotateTowards(_toolQua, _toolQub, _rotSpeed * PEIMEN_Entity.Time.DeltaTime);
                    else
                        _toolQua = Quaternion.Lerp(_toolQua, _toolQub, _rotSpeed * PEIMEN_Entity.Time.DeltaTime);
                }
                if (_needScale)
                {
                    if (_transType == SimpleTransType.MoveTowards)
                        _toolS3a = Vector3.MoveTowards(_toolS3a, _toolS3b, _scaleSpeed * PEIMEN_Entity.Time.DeltaTime);
                    else
                        _toolS3a = Vector3.Lerp(_toolS3a, _toolS3b, _scaleSpeed * PEIMEN_Entity.Time.DeltaTime);
                }
                //if (Vector3.Distance(_toolV3a, _toolV3b) <= 0 && !_needRotate)
                //    _del = PEIKNF_NullFunction.NullFunction;
                //else if (Vector3.Distance(_toolV3a, _toolV3b) <= 0&& Quaternion.Angle(_toolQua, _toolQub) <= 0)
                //    _del = PEIKNF_NullFunction.NullFunction;
                if (Quaternion.Angle(_toolQua, _toolQub) <= _doneDis || !_needRotate)
                {
                    _toolQua = _toolQub;
                    toolNum++;
                }
                if (Vector3.Distance(_toolV3a, _toolV3b) <= _doneDis)
                {
                    _toolV3a = _toolV3b;
                    toolNum++;
                }
                if (Vector3.Distance(_toolS3a, _toolS3b) <= _doneDis || !_needScale)
                {
                    _toolS3a = _toolS3b;
                    toolNum++;
                }
                if (toolNum >= 3)
                {
                    //PEIKDE.Log("PST","Trans Done!");
                    _del = PEIKNF_NullFunction.NullFunction;
                    _toolNum = 3;
                }
                else
                {
                    _toolNum = 0;
                }
                //PEIKDE.Log("PST", "Asyn Done!");
            }
            void MainTH()
            {
                //PEIKDE.Log("PST", "MainTH!");
                if (!_localFlag)
                    _obj.transform.position = _toolV3a;
                else
                    _obj.transform.localPosition = _toolV3a;
                if (_needRotate)
                    _obj.transform.rotation = _toolQua;
                if (_needScale)
                    _obj.transform.localScale = _toolS3a;

                if (_toolNum >= 3)
                {
                    _callBack();
                }
                //PEIKDE.Log("PST", "MainTH! End");
            }
            PEIMEN_Entity.Loom.RunAsync("66165", Asyn, MainTH);
        }

        private void MoveStep()
        {
            //PEIKDE.Log("STC", "Move Step!");
            int toolNum = 0;
            if (_transType == SimpleTransType.MoveTowards)
                _toolV3a = Vector3.MoveTowards(_toolV3a, _toolV3b, _moveSpeed * PEIMEN_Entity.Time.DeltaTime);
            else
                _toolV3a = Vector3.Lerp(_toolV3a, _toolV3b, _moveSpeed * PEIMEN_Entity.Time.DeltaTime);
            if (_needRotate)
            {
                if (_transType == SimpleTransType.MoveTowards)
                    _toolQua = Quaternion.RotateTowards(_toolQua, _toolQub, _rotSpeed * PEIMEN_Entity.Time.DeltaTime);
                else
                    _toolQua = Quaternion.Lerp(_toolQua, _toolQub, _rotSpeed * PEIMEN_Entity.Time.DeltaTime);
            }
            if (_needScale)
            {
                if (_transType == SimpleTransType.MoveTowards)
                    _toolS3a = Vector3.MoveTowards(_toolS3a, _toolS3b, _scaleSpeed * PEIMEN_Entity.Time.DeltaTime);
                else
                    _toolS3a = Vector3.Lerp(_toolS3a, _toolS3b, _scaleSpeed * PEIMEN_Entity.Time.DeltaTime);
            }
            //if (Vector3.Distance(_toolV3a, _toolV3b) <= 0 && !_needRotate)
            //    _del = PEIKNF_NullFunction.NullFunction;
            //else if (Vector3.Distance(_toolV3a, _toolV3b) <= 0&& Quaternion.Angle(_toolQua, _toolQub) <= 0)
            //    _del = PEIKNF_NullFunction.NullFunction;
            if (Quaternion.Angle(_toolQua, _toolQub) <= _doneDis || !_needRotate)
            {
                _toolQua = _toolQub;
                toolNum++;
            }
            if (Vector3.Distance(_toolV3a, _toolV3b) <= _doneDis)
            {
                _toolV3a = _toolV3b;
                toolNum++;
            }
            if (Vector3.Distance(_toolS3a, _toolS3b) <= _doneDis || !_needScale)
            {
                _toolS3a = _toolS3b;
                toolNum++;
            }
            if (toolNum >= 3)
            {
                //PEIKDE.Log("PST","Trans Done!");
                _del = PEIKNF_NullFunction.NullFunction;
                _toolNum = 3;
            }
            else
            {
                _toolNum = 0;
            }

            if (!_localFlag)
                _obj.transform.position = _toolV3a;
            else
                _obj.transform.localPosition = _toolV3a;
            if (_needRotate)
                _obj.transform.rotation = _toolQua;
            if (_needScale)
                _obj.transform.localScale = _toolS3a;

            if (_toolNum >= 3)
            {
                _callBack();
            }
        }

        private void BeginStep()
        {
            //PEIKDE.Log("BeginFunc Running!");
            if (_target)
                if(!_localFlag)
                _toolV3b = _target.transform.position;
                else
                    _toolV3b = _target.transform.localPosition;
            else
                _toolV3b = _targetPos;
            if (_needRotate)
            {
                //_toolQua = _obj.transform.rotation;
                if (_target)
                    _toolQub = _target.transform.rotation;
                else
                    _toolQub = _targetRot;
            }
            if (_needScale)
            {
                //_toolS3a = _obj.transform.localScale;
                _toolS3b = _target.transform.localScale;
            }
            //PEIKDE.Log("Moving!");
            if (!_loomFlag)
                MoveStep();
            else
                MoveStepLoom();
        }
        private void BackStep()
        {
            if (!_parTrans)
                _toolV3b = _oldPos;
            else
                if (!_localFlag)
                _toolV3b = _oldPosObj.transform.position;
            else
                _toolV3b = _oldPosObj.transform.localPosition;
            if (_needRotate)
            {
                //_toolQua = _obj.transform.rotation;
                if (!_parTrans)
                    _toolQub = _oldRot;
                else
                    _toolQub = _oldPosObj.transform.rotation;
            }
            if (_needScale)
            {
               // _toolS3a = _obj.transform.localScale;
                if (!_parTrans)
                    _toolS3b = _oldScl;
                else
                    _toolS3b = _oldPosObj.transform.localScale;
            }
            if (!_loomFlag)
                MoveStep();
            else
                MoveStepLoom();
        }

        //private void RotateStep()
        //{
        //    //PEIKDE.Log("STC", "Move Step!");
        //    Loom.RunAsync(() =>
        //    {
        //        _toolQua = Quaternion.RotateTowards(_toolQua, _toolQub, _moveSpeed * PEIKTM.DeltaTime);
        //        if (Quaternion.Angle(_toolQua, _toolQub) <= 0)
        //        {
        //            _del = PEIKNF_NullFunction.NullFunction;
        //        }
        //    });
        //    Loom.QueueOnMainThread(() =>
        //    {
        //        _obj.transform.position = _toolV3a;
        //    });
        //}
        //private void PosBackStep()
        //{
        //    //PEIKDE.Log("STC", "Move Step!");
        //    Loom.RunAsync(() =>
        //    {
        //        _toolV3a = Vector3.MoveTowards(_toolV3a, _toolV3b, _moveSpeed * PEIKTM.DeltaTime);
        //        if (_needRotate)
        //        {
        //            _toolQua = Quaternion.RotateTowards(_toolQua, _toolQub, _moveSpeed * PEIKTM.DeltaTime);
        //        }
        //        if (Vector3.Distance(_toolV3a, _toolV3b) <= 0 && !_needRotate)
        //            _del = PEIKNF_NullFunction.NullFunction;
        //        else if (Vector3.Distance(_toolV3a, _toolV3b) <= 0 && Quaternion.Angle(_toolQua, _toolQub) <= 0)
        //            _del = PEIKNF_NullFunction.NullFunction;
        //    });
        //    Loom.QueueOnMainThread(() =>
        //    {
        //        _obj.transform.position = _toolV3a;
        //        if (_needRotate)
        //            _obj.transform.rotation = _toolQua;
        //    });
        //}

        public void SetLoom(bool flag)
        {
            _loomFlag = flag;
        }

        public PEIKnifer_Delegate_Void_Void GetDel()
        {
            return _del;
        }
        public void SetTransType(SimpleTransType type)
        {
            _transType = type;
        }
        public void SetTriggerDis(float dis)
        {
            _doneDis = dis;
        }
        public void SetRotateSpeed(float speed)
        {
            _rotSpeed = speed;
        }
        public void SetMoveSpeed(float speed)
        {
            _moveSpeed = speed;
        }
        public void SetScaleSpeed(float speed)
        {
            _scaleSpeed = speed;
        }
        public void SetSpeed(float moveSpeed, float rotSpeed, float sclSpeed)
        {
            _rotSpeed = rotSpeed;
            _moveSpeed = moveSpeed;
            _scaleSpeed = sclSpeed;
        }
        public void SetLoopCallBack(float loopTimeLife)
        {
            //PEIKDE.Log("SCT", "Set Loop Call Back!");
            _loopLifeTime = loopTimeLife;
            _callBack = LoopCallBack;
        }
        public void SetTransValue(TransModel model)
        {
            _target = model.Target;
            _obj = model.Object;
            _oldPos = _obj.transform.position;
            _oldScl = _obj.transform.localScale;
            _oldRot = _obj.transform.rotation;
            _moveSpeed = model.MoveSpeed ;
            _rotSpeed = model.RotSpeed ;
            _scaleSpeed = model.SclSpeed ;
        }
        public void SetParTrans(GameObject obj)
        {
            _oldPosObj = obj;
            _parTrans = true;
        }
        private void LoopCallBack()
        {
            //PEIKDE.Log("SCT", "Loop Call Back Trigger!");
            
            t.EntrustTimer(_loopLifeTime, false, ()=> { Flag.Flag = !Flag.Flag; });
        }
        public void GetLoopCallBack()
        {
            Flag.Flag = !Flag.Flag;
        }
        public TransModel GetModel()
        {
            model.Target = _target;
            if (!_parTrans)
                model.OldPos = _oldPos;
            else
                model.OldPos = _oldPosObj.transform.position;
            model.Object = _obj;
            model.MoveSpeed = _moveSpeed;
            model.RotSpeed = _rotSpeed;
            model.SclSpeed = _scaleSpeed;
            return model;
        }
        
        // Update is called once per frame
        public void TransUpdate()
        {
            //Update();
            //PEIKDE.Log("STC", "Update Running!");
            _del();
            //PEIKDE.Log(_del.Target.ToString());
        }
        public void SetLocalFlag(bool flag)
        {
            _localFlag = flag;
        }

        ~PEIMEN_STC_Trans()
        {
            if(PEIMEN_Entity.L.HasL(_l))
            PEIMEN_Entity.L.RemoveL(_l);
            PEIKDE.Log("STC", "Trans Destory");
        }
        public void Destory()
        {
            if (PEIMEN_Entity.L.HasL(_l))
                PEIMEN_Entity.L.RemoveL(_l);
            //PEIKDE.Log("STC", "Trans Destory");
        }
    }
    public enum SimpleTransType
    {
        MoveTowards,
        Lerp
    }
    public class TransModel
    {
        public GameObject Object, Target;
        public Vector3 OldPos;
        public float MoveSpeed,RotSpeed,SclSpeed;
    }
}