/////////////////////////////////////////////////
//
//PEIMEN Frame System || Template AnimeUI branch 
//
//creat by PEIKnifer[.CN]
//
//Template AnimeUI for Anime UI Anime UI Page
//
//Create On 2019-4
//
//Last Update in 2019 4 25 14:45:12
//
/////////////////////////////////////////////////

using PEIKDL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTL_AU
{
    public class AnimeUIPage : PEIKnifer
    {

        public List<AnimeUIBase> UIList;
        public bool Flag;
        private PEIKnifer_Delegate_Void_Void _del;
        private PEIKnifer_Delegate_Void_Void _doneDel;
        private List<Collider> _colliderList;
        private Collider _toolCollider;

        public void Open()
        {
            Flag = true;
            _doneDel = NullFunction;
            for (int i = 0; i < UIList.Count; i++)
            {
                UIList[i].Open();
                UIList[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < _colliderList.Count; i++)
            {
                _colliderList[i].enabled = true;
            }
            _del = DelCheck;
            PEIKDE.Log("PG", "OBJ " + gameObject.name + " Anime UI Open");
        }
        public void Open(PEIKnifer_Delegate_Void_Void del)
        {
            Flag = true;
            _doneDel = del;
            for (int i = 0; i < UIList.Count; i++)
            {
                UIList[i].Open();
                UIList[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < _colliderList.Count; i++)
            {
                _colliderList[i].enabled = true;
            }
            _del = DelCheck;
            PEIKDE.Log("PG", "OBJ " + gameObject.name + " Anime UI Open");
        }
        public void Close(PEIKnifer_Delegate_Void_Void del)
        {
            Flag = false;
            _doneDel = del;
            for (int i = 0; i < UIList.Count; i++)
            {
                UIList[i].Close();
            }
            for (int i = 0; i < _colliderList.Count; i++)
            {
                _colliderList[i].enabled = false;
            }
            _del = DelCheck;
        }

        // Use this for initialization
        void Start()
        {
            _del = NullFunction;
            _colliderList = new List<Collider>();
            for (int i = 0; i < UIList.Count; i++)
            {
                _toolCollider = UIList[i].GetComponent<Collider>();
                if (_toolCollider && _toolCollider.enabled)
                {
                    _colliderList.Add(_toolCollider);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            _del();

        }
        private void DelCheck()
        {
            for (int i = 0; i < UIList.Count; i++)
            {
                if (UIList[i].AnimeDoneFlag != Flag)
                {
                    return;
                }
            }
            _doneDel();
            for (int i = 0; i < UIList.Count; i++)
            {
                UIList[i].gameObject.SetActive(Flag);
            }
            _doneDel = NullFunction;
        }
    }
}
