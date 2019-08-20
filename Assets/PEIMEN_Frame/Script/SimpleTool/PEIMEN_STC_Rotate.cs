using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIKDL;
using System.Threading;


namespace PEIKTS {
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
                           _toolQua;
        private float _speed;

        public PEIMEN_STC_Trans(GameObject obj, GameObject target, bool needRotate,float speed)
        {

        }
        public PEIMEN_STC_Trans(GameObject obj, bool needRotate, Vector3 targetPos, Quaternion targetRot)
        {

        }
        public PEIMEN_STC_Trans(GameObject obj, bool needRotate, Vector3 targetPos, Vector3 targetOul)
        {

        }

        private void NormalInit(GameObject obj, bool needRotate,float speed)
        {
            _speed = speed;
            _obj = obj;
            Flag = new PEIKnifer_Flag(MoveStep, PosBackStep);
            _oldPos = _obj.transform.position;
            _oldRot = _obj.transform.rotation;
        }

        private void MoveStep()
        {
            _toolV3a = _obj.transform.position;
            _toolV3b = _target.transform.position;
            Loom.RunAsync(() =>
            {
                _toolV3a = Vector3.MoveTowards(_toolV3a, _toolV3b, Time.deltaTime * _speed);
            });
            Loom.QueueOnMainThread(() =>
            {
                _obj.transform.position = _toolV3a;
            });
        }
        private void PosBackStep()
        {
            _toolV3a = _obj.transform.position;
            _toolV3b = _oldPos;
            Loom.RunAsync(() =>
            {
                _toolV3a = Vector3.MoveTowards(_toolV3a, _toolV3b, Time.deltaTime * _speed);
            });
            Loom.QueueOnMainThread(() =>
            {
                _obj.transform.position = _toolV3a;
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}