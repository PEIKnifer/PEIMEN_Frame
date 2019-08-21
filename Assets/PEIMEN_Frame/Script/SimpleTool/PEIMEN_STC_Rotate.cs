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
        private PEIKnifer_Delegate_Void_Void _del;
        private Vector3 _targetPos,
                        _targetOul,
                        _oldPos,
                        _toolV3a,
                        _toolV3b;
        private Quaternion _targetRot,
                           _oldRot,
                           _toolQua,
                           _toolQub;
        private float _moveSpeed, _rotSpeed, deltaTime;
        private bool _needRotate;

        public PEIMEN_STC_Trans(GameObject obj, GameObject target, bool needRotate, float moveSpeed,float rotSpeed, PEIKnifer_L l)
        {
            NormalInit(obj, needRotate, moveSpeed,rotSpeed,l);
            _target = target;
        }
        public PEIMEN_STC_Trans(GameObject obj, bool needRotate, float moveSpeed, float rotSpeed, Vector3 targetPos, Quaternion targetRot, PEIKnifer_L l)
        {
            NormalInit(obj, needRotate, moveSpeed, rotSpeed, l);
            _targetPos = targetPos;
            _targetRot = targetRot;
        }

        //this function was prepare for use euler angle rotate
        //public PEIMEN_STC_Trans(GameObject obj, bool needRotate, Vector3 targetPos, Vector3 targetOul)
        //{
        //}

        private void NormalInit(GameObject obj, bool needRotate,float speed,float rotSpeed,PEIKnifer_L l)
        {
            _rotSpeed = rotSpeed;
            _del = PEIKNF_NullFunction.NullFunction;
            l.AddElement(Update);
            _needRotate = needRotate;
            _moveSpeed = speed;
            _obj = obj;
            Flag = new PEIKnifer_Flag(BeginFunc, BackFunc);
            _oldPos = _obj.transform.position;
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
            _del = MoveStep;

        }
        private void MoveStep()
        {
            //PEIKDE.Log("STC", "Move Step!");
            Loom.RunAsync(() =>
            {
                _toolV3a = Vector3.MoveTowards(_toolV3a, _toolV3b, _moveSpeed*PEIKTM.DeltaTime);
                if (_needRotate)
                {
                    _toolQua = Quaternion.RotateTowards(_toolQua, _toolQub, _rotSpeed * PEIKTM.DeltaTime);
                }
                if (Vector3.Distance(_toolV3a, _toolV3b) <= 0 && !_needRotate)
                    _del = PEIKNF_NullFunction.NullFunction;
                else if (Vector3.Distance(_toolV3a, _toolV3b) <= 0&& Quaternion.Angle(_toolQua, _toolQub) <= 0)
                    _del = PEIKNF_NullFunction.NullFunction;
            });
            Loom.QueueOnMainThread(() =>
            {
                _obj.transform.position = _toolV3a;
                if (_needRotate)
                    _obj.transform.rotation = _toolQua;
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