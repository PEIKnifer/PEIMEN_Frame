using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIKDL;
using System.Threading;


namespace PEIKTS {
    /// <summary>
    /// Simple Tool Class Module For Simple Transform
    /// </summary>
    public class PEIMEN_STC_Trans : PEIKnifer_Origin
    {
        private GameObject _obj,
                           _target;
        public PEIKnifer_Flag Flag;
        private PEIKnifer_Delegate_Void_Void _del,
                                             _callBack;
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
                      _deltaTime;
        private int _toolNum;
        private bool _needRotate,
                     _needScale;
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
        /// <param name="l">base class(need Type of PEIKnifer_L class)</param>
        /// <param name="callBackT">move function callback function</param>
        public PEIMEN_STC_Trans(GameObject obj, GameObject target, bool needRotate, bool needScale, float moveSpeed,float rotSpeed,float scaleSpeed, PEIKnifer_L l,PEIKnifer_Delegate_Void_Void callBackT)
        {
            NormalInit(obj, needRotate, needScale, moveSpeed,rotSpeed, scaleSpeed, l,callBackT);
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
        /// <param name="l">base class(need Type of PEIKnifer_L class)</param>
        /// <param name="callBackT">move function callback function</param>
        public PEIMEN_STC_Trans(GameObject obj, GameObject target, bool needRotate, float moveSpeed, float rotSpeed, PEIKnifer_L l, PEIKnifer_Delegate_Void_Void callBackT)
        {
            NormalInit(obj, needRotate,false, moveSpeed, rotSpeed,100, l, callBackT);
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
        /// <param name="l">base class(need Type of PEIKnifer_L class)</param>
        /// <param name="callBackT">move function callback function</param>
        public PEIMEN_STC_Trans(GameObject obj, bool needRotate, float moveSpeed, float rotSpeed, Vector3 targetPos, Quaternion targetRot, PEIKnifer_L l, PEIKnifer_Delegate_Void_Void callBackT)
        {
            NormalInit(obj, needRotate,false, moveSpeed, rotSpeed,100, l, callBackT);
            _targetPos = targetPos;
            _targetRot = targetRot;
        }

        //this function was prepare for use euler angle rotate
        //public PEIMEN_STC_Trans(GameObject obj, bool needRotate, Vector3 targetPos, Vector3 targetOul)
        //{
        //}

        private void NormalInit(GameObject obj, bool needRotate,bool needScale,float speed,float rotSpeed,float scaleSpeed,PEIKnifer_L l,PEIKnifer_Delegate_Void_Void callBackT)
        {
            _needScale = needScale;
            _scaleSpeed = scaleSpeed;
            _callBack = callBackT;
            _rotSpeed = rotSpeed;
            _del = PEIKNF_NullFunction.NullFunction;
            l.AddElement(Update);
            _needRotate = needRotate;
            _moveSpeed = speed;
            
            _obj = obj;
            Flag = new PEIKnifer_Flag(BeginFunc, BackFunc);
            _oldPos = _obj.transform.position;
            _oldScl = _obj.transform.localScale;
            _oldRot = _obj.transform.rotation;
            PEIKTM.WeekUp();
        }
        /// <summary>
        /// Move To Target Function
        /// </summary>
        public void BeginFunc()
        {
            _toolV3a = _obj.transform.position;
            if (_target)
                _toolV3b = _target.transform.position;
            else
                _toolV3b = _targetPos;
            if (_needRotate)
            {
                _toolQua = _obj.transform.rotation;
                if (_target)
                    _toolQub = _target.transform.rotation;
                else
                    _toolQub = _targetRot;
            }
            if (_needScale)
            {
                _toolS3a = _obj.transform.localScale;
                _toolS3b = _target.transform.localScale;
            }
            _del = MoveStep;
        }

        /// <summary>
        /// Return To Begin State Function
        /// </summary>
        public void BackFunc()
        {
            _toolV3a = _obj.transform.position;
            _toolV3b = _oldPos;
            if (_needRotate)
            {
                _toolQua = _obj.transform.rotation;
                _toolQub = _oldRot;
            }
            if (_needScale)
            {
                _toolS3a = _obj.transform.localScale;
                _toolS3b = _oldScl;
            }
            _del = MoveStep;
        }
        private void MoveStep()
        {
            //PEIKDE.Log("STC", "Move Step!");
            Loom.RunAsync(() =>
            {
                int  toolNum = 0;
                _toolV3a = Vector3.MoveTowards(_toolV3a, _toolV3b, _moveSpeed*PEIKTM.DeltaTime);
                if (_needRotate)
                {
                    _toolQua = Quaternion.RotateTowards(_toolQua, _toolQub, _rotSpeed * PEIKTM.DeltaTime);
                }
                if (_needScale)
                {
                    _toolS3a = Vector3.MoveTowards(_toolS3a, _toolS3b, _scaleSpeed * PEIKTM.DeltaTime);
                }
                //if (Vector3.Distance(_toolV3a, _toolV3b) <= 0 && !_needRotate)
                //    _del = PEIKNF_NullFunction.NullFunction;
                //else if (Vector3.Distance(_toolV3a, _toolV3b) <= 0&& Quaternion.Angle(_toolQua, _toolQub) <= 0)
                //    _del = PEIKNF_NullFunction.NullFunction;
                if (Quaternion.Angle(_toolQua, _toolQub) <= 0 || !_needRotate)
                {
                    toolNum++;
                }
                if (Vector3.Distance(_toolV3a, _toolV3b) <= 0)
                {
                    toolNum++;
                }
                if (Vector3.Distance(_toolS3a, _toolS3b) <= 0||!_needScale)
                {
                    toolNum++;
                }
                if (toolNum >= 3)
                {
                    _del = PEIKNF_NullFunction.NullFunction;
                }
            });
            Loom.QueueOnMainThread(() =>
            {
                _toolNum = 0;
                _obj.transform.position = _toolV3a;
                if (_needRotate)
                    _obj.transform.rotation = _toolQua;
                if (_needScale)
                    _obj.transform.localScale = _toolS3a;
                if (Quaternion.Angle(_toolQua, _toolQub) <= 0 || !_needRotate)
                {
                    _toolNum++;
                }
                if (Vector3.Distance(_toolV3a, _toolV3b) <= 0)
                {
                    _toolNum++;
                }
                if (Vector3.Distance(_toolS3a, _toolS3b) <= 0 || !_needScale)
                {
                    _toolNum++;
                }
                if (_toolNum >= 3)
                {
                    _callBack();
                }
            });
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

        public PEIKnifer_Delegate_Void_Void GetDel()
        {
            return _del;
        }
        // Update is called once per frame
        private void Update()
        {
            //PEIKDE.Log("STC", "Update Running!");
            _del();
            //PEIKDE.Log(_del.Target.ToString());
        }
    }
}