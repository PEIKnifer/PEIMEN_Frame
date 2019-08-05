
/////////////////////////////////////////////////
//
//PEIMEN Frame System || GeneralObjMove branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for SimpleObjMove
//
//Create On 2019 3 25
//
//Last Update in 2019 3 25 16:35:23
//
/////////////////////////////////////////////////


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTS
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PoolManager : PEIKnifer_Singleton
    {

        //public static PoolManager Ins;
        [SerializeField]
        public List<string> KindList;
        [SerializeField]
        public List<List<GameObjectPoolInsBase>> InsList;
        private GameObject ToolObj;

        //Need Init In System Awake Function
        protected void FrameInitAwake()
        {
            //Ins = this;
            KindList = new List<string>();
            InsList = new List<List<GameObjectPoolInsBase>>();
        }

        public GameObject Instance(GameObject obj, string kind)
        {
            for (int i = 0; i < KindList.Count; i++)
            {
                if (KindList[i] == kind)
                {
                    ToolObj = GetObj(i, obj);
                    return ToolObj;
                }
            }
            KindList.Add(kind);
            ToolObj = GetObj(KindList.Count - 1, obj);

            return ToolObj;
        }

        public GameObject Instance(GameObject obj, string kind, Vector3 position, Quaternion rotation)
        {
            ToolObj = Instance(obj, kind);
            ToolObj.transform.position = position;
            ToolObj.transform.rotation = rotation;
            return ToolObj;
        }
        public GameObject Instance(GameObject obj, string kind, Vector3 position, Quaternion rotation, Transform trans)
        {
            ToolObj = Instance(obj, kind);
            ToolObj.transform.position = position;
            ToolObj.transform.rotation = rotation;
            ToolObj.transform.SetParent(trans);
            return ToolObj;
        }
        public GameObject Instance(GameObject obj, string kind, Transform trans)
        {
            ToolObj = Instance(obj, kind);
            ToolObj.transform.position = trans.position;
            ToolObj.transform.rotation = trans.rotation;
            ToolObj.transform.SetParent(trans);
            return ToolObj;
        }

        private void AddNewList(string kind)
        {

        }

        private GameObject GetObj(int i, GameObject obj)
        {
            try
            {
                if (InsList.Count < i + 1)
                {
                    InsList.Add(new List<GameObjectPoolInsBase>());
                }

                if (InsList[i].Count > 0)
                {
                    InsList[i][0].gameObject.SetActive(true);
                    InsList[i][0].transform.SetParent(null);
                    InsList[i][0].Refresh();
                    ToolObj = InsList[i][0].gameObject;
                    InsList[i].RemoveAt(0);
                    return ToolObj;
                }
                else
                {
                    return Instantiate(obj);
                }
            }
            catch (Exception e)
            {
                PEIKDE.LogError("PMR", "Pool Manager GetObj Error In Index --> "+i+" System Exception -->" + e);
                return null;
            }
        }

        public void DestroyObj(GameObject obj, string kind)
        {
            for (int i = 0; i < KindList.Count; i++)
            {
                if (KindList[i] == kind)
                {
                    DesObj(i, kind, obj);
                    return;
                }
            }
            InsList.Add(new List<GameObjectPoolInsBase>());
            KindList.Add(kind);
            DesObj(KindList.Count - 1, kind, obj);
        }

        private void DesObj(int i, string kind, GameObject obj)
        {
            try
            {
                if (InsList.Count < i + 1)
                {
                    InsList.Add(new List<GameObjectPoolInsBase>());
                    KindList.Add(kind);
                }
                PEIKDE.Log("PMR", "i = " + i);
                PEIKDE.Log("PMR", "Obj" + obj.name);
                PEIKDE.Log("PMR", obj.GetComponent<GameObjectPoolInsBase>().ToString());
                InsList[i].Add(obj.GetComponent<GameObjectPoolInsBase>());
                InsList[i][InsList[i].Count - 1].ReadyInPool();
                InsList[i][InsList[i].Count - 1].gameObject.SetActive(false);
                InsList[i][InsList[i].Count - 1].gameObject.transform.position = Vector3.zero;
                InsList[i][InsList[i].Count - 1].gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                InsList[i][InsList[i].Count - 1].transform.SetParent(transform);
                return;
            }
            catch(Exception e)
            {
                PEIKDE.LogError("PMR", "Pool Manager DesObj Error --> " + e);
            }
        }

    }
    public enum PoolManagerStateBase:int
    {
        StateBase=0
    }

}
