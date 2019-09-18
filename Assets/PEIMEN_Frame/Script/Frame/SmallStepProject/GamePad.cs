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
//Last Update in 2019 9 18 16:48:08
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using PEIKTS;

namespace PEIKBF_SSP
{
    [Serializable]
    public abstract class GamePad : PEIKnifer_L
    {
        #region  Inherent Member
        public static GamePad Ins;
        protected GameObject _operationBaseObj;

        protected OperationBase _operationBase;

        //protected GameObject _handTar;

        protected bool _hasObjFlag;

        private Ray _ray;

        public LineRenderer Line;

        public Transform GamePadObj;

        public GameObject playerObj;

        public string OperationTag;

        RaycastHit _hitInfo;

        protected bool _isinOperationFlag;
        protected Vector2 oldPadPos;

        [SerializeField]
        protected Transform _player;
        protected GameObject _lastButton;
        protected GameObject _lastOperation;
        #endregion

        #region Inherent Function
        // Use this for initialization

        public void FrameInitStart()
        {
            _isinOperationFlag = false;
            Ins = this;
            //Line = new LineRenderer();
            //_handTar = transform.Find("HandTar").gameObject;
            AddElement(FrameUpdate);
        }
        public void FrameUpdate()
        {
            //PEIKDE.Log("GPD","GamePad Update Runing");
            Line.gameObject.SetActive(!_hasObjFlag);
            DrawRay();
        }
        public RaycastHit CustomDrawRay()
        {
            Ray ray = new Ray(GamePadObj.position, GamePadObj.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(_ray, out hitInfo))
            {
                return hitInfo;
            }
            return hitInfo;
        }
        public void DrawRay()
        {
            _ray = new Ray(GamePadObj.position, GamePadObj.forward);
            //Debug.DrawRay(GamePadObj.position, GamePadObj.forward);
            Line.SetPosition(0, transform.position);

            if (Physics.Raycast(_ray, out _hitInfo))
            {
                Line.SetPosition(1, transform.position + transform.forward * (Vector3.Distance(transform.position, _hitInfo.point)));
                if (_hitInfo.collider != null)
                {
                    if (_hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Road"))
                    {
                        //Debug.Log("asdasdasdasd");
                        Line.SetPosition(1, _hitInfo.point);
                        RayButtonPush(0,_hitInfo.collider.gameObject);
                        //上一个按钮的事件 exit
                        if (_lastButton != null)
                        {
                            ExecuteEvents.Execute<IPointerExitHandler>(_lastButton, new PointerEventData(null), ExecuteEvents.pointerExitHandler);
                            _lastButton = null;
                        }
                        if (_lastOperation != null)
                        {
                            _lastOperation.GetComponent<OperationBase>().OnRayExit();
                            _lastOperation = null;
                        }
                    }
                    else if (_hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("UI"))
                    {
                        //exit
                        if (_lastButton != null && _lastButton != _hitInfo.collider.gameObject)
                        {
                            ExecuteEvents.Execute<IPointerExitHandler>(_lastButton, new PointerEventData(null), ExecuteEvents.pointerExitHandler);
                            _lastButton = null;
                        }
                        if (_lastOperation != null&&_lastOperation!= _hitInfo.collider.gameObject)
                        {
                            _lastOperation.GetComponent<OperationBase>().OnRayExit();
                            _lastOperation = null;
                        }
                        //enter
                        ExecuteEvents.Execute<IPointerEnterHandler>(_hitInfo.collider.gameObject, new PointerEventData(null), ExecuteEvents.pointerEnterHandler);
                        //click
                        RayButtonPush(1, _hitInfo.collider.gameObject);
                        //保存
                        _lastButton = _hitInfo.collider.gameObject;
                    }
                    else if (_hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("OperationBase"))
                    {
                        if (_lastButton != null && _lastButton != _hitInfo.collider.gameObject)
                        {
                            ExecuteEvents.Execute<IPointerExitHandler>(_lastButton, new PointerEventData(null), ExecuteEvents.pointerExitHandler);
                            _lastButton = null;
                        }
                        if (_lastOperation != null && _lastOperation != _hitInfo.collider.gameObject)
                        {
                            _lastOperation.GetComponent<OperationBase>().OnRayExit();
                            _lastOperation = null;
                        }

                        _lastOperation = _hitInfo.collider.gameObject;
                        RayButtonPush(2, _hitInfo.collider.gameObject);
                        _lastOperation.GetComponent<OperationBase>().OnRayIn();
                    }
                    else
                    {
                        //Debug.Log("Rat=y Not Hit");
                        //上一个按钮的事件 exit
                        //Debug.Log("[PEIKCH]  Hit in " + _hitInfo.collider.gameObject.name);
                        if (_lastButton != null)
                        {
                            ExecuteEvents.Execute<IPointerExitHandler>(_lastButton, new PointerEventData(null), ExecuteEvents.pointerExitHandler);
                            _lastButton = null;
                        }
                        if (_lastOperation != null)
                        {
                            _lastOperation.GetComponent<OperationBase>().OnRayExit();
                            _lastOperation = null;
                        }
                    }

                }
                else
                {
                    //上一个按钮的事件 exit
                    if (_lastButton != null)
                    {
                        ExecuteEvents.Execute<IPointerExitHandler>(_lastButton, new PointerEventData(null), ExecuteEvents.pointerExitHandler);
                        _lastButton = null;
                    }
                    if (_lastOperation != null )
                    {
                        _lastOperation.GetComponent<OperationBase>().OnRayExit();
                        _lastOperation = null;
                    }
                }
            }
            else
            {
                Line.SetPosition(1, transform.position + transform.forward * 100);

                //上一个按钮的事件 exit
                if (_lastButton != null)
                {
                    ExecuteEvents.Execute<IPointerExitHandler>(_lastButton, new PointerEventData(null), ExecuteEvents.pointerExitHandler);
                    _lastButton = null;
                }
                if (_lastOperation != null )
                {
                    _lastOperation.GetComponent<OperationBase>().OnRayExit();
                    _lastOperation = null;
                }
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == OperationTag && !_hasObjFlag)
            {
                _operationBaseObj = other.gameObject;
                _operationBase = _operationBaseObj.GetComponent<OperationBase>();
                _isinOperationFlag = true;
                _hasObjFlag = true;
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == OperationTag && _hasObjFlag&& _operationBaseObj == other.gameObject)
            {
                _operationBaseObj = null;
                _operationBase = null;
                _isinOperationFlag = false;
                _hasObjFlag = false;
            }
        }
        public void OperationExit()
        {
            if (_operationBase)
            {
                _operationBase = null;
            }
            if (_operationBaseObj)
            {
                _operationBaseObj.transform.SetParent(null);
                _operationBaseObj = null;
            }
            _hasObjFlag = false;
            _isinOperationFlag = false;
        }
        public bool OnTouchAndButtonDown()
        {
            if (_operationBase )
            {
                _operationBase.PartBase.Status = (int)PEIKEM_PartBaseStatus.Done;
                return _operationBase.OnObjGetCaught();
            }
            PEIKDE.LogError("SSPGQ", "GamePad Can`t Get A OperationBase Ins ");
            return false;
        }
        //state 1 = move 2 =clickUI;
        public abstract void RayButtonPush(int state, GameObject obj);
        #endregion
    }
}
