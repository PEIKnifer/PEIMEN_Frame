/////////////////////////////////////////////////
//
//template System || UI branch 
//
//creat by ErHua
//
//Template for SigninUI
//
//Create On 2017-12-26 11:32:49
//
//Last Update in 2017-12-26 11:32:49
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SigninP : MonoBehaviour {
    public GameObject[] Signmark;

    public void SigninInit(int _alldaynum,int _nowdays,GameObject _signlist1,GameObject _signlist2) 
    {
        GameObject[] Signitem;
        Signitem = new GameObject[_alldaynum];
            if (_nowdays > _alldaynum)
            {
                _signlist1.gameObject.SetActive(false);
                _signlist2.gameObject.SetActive(true);
                for (int i = 0; i < _alldaynum; i++)
                {
                    Signitem[i] = _signlist2.transform.GetChild(i).gameObject;
                }
            }
            else
            {
                _signlist1.gameObject.SetActive(true);
                _signlist2.gameObject.SetActive(false);
                for (int i = 0; i < _alldaynum; i++)
                {
                    Signitem[i] = _signlist1.transform.GetChild(i).gameObject;
                }
            }
            for (int i = 0; i < _alldaynum; i++)
            {
                if (i == _nowdays % _alldaynum)
                {
                    Signitem[i].GetComponent<Button>().enabled = true;
                }
                else
                {
                    Signitem[i].GetComponent<Button>().enabled = false;
                }
                if (i < _nowdays % _alldaynum)
                {
                    Signmark[i].gameObject.SetActive(true);
                }
                else
                {
                    Signmark[i].gameObject.SetActive(false);
                }
            }
        }
    public void SigninInit(int _alldaynum, int _nowdays, GameObject _signlist1)
    {
        GameObject[] Signitem;
        Signitem = new GameObject[_alldaynum];
            for (int i = 0; i < _alldaynum; i++)
            {
                Signitem[i] = _signlist1.transform.GetChild(i).gameObject;
            }
        for (int i = 0; i < _alldaynum; i++)
        {
            if (i == _nowdays % _alldaynum)
            {
                Signitem[i].GetComponent<Button>().enabled = true;
            }
            else
            {
                Signitem[i].GetComponent<Button>().enabled = false;
            }
            if (i < _nowdays % _alldaynum)
            {
                Signmark[i].gameObject.SetActive(true);
            }
            else
            {
                Signmark[i].gameObject.SetActive(false);
            }
        }
    }
    public void SigninClick(int _alldaynum, int _nowdays, GameObject _signlist1, GameObject _signlist2)
    {
        GameObject[] Signitem;
        Signitem = new GameObject[_alldaynum];
        if (_nowdays > _alldaynum)
        {
            for (int i = 0; i < _alldaynum; i++)
            {
                Signitem[i] = _signlist2.transform.GetChild(i).gameObject;
            }
        }
        else
        {
            for (int i = 0; i < _alldaynum; i++)
            {
                Signitem[i] = _signlist1.transform.GetChild(i).gameObject;
            }
        }
        Signitem[_nowdays % _alldaynum].GetComponent<Button>().enabled = false;
        Signmark[_nowdays % _alldaynum].gameObject.SetActive(true);
    }
    public void SigninClick(int _alldaynum, int _nowdays, GameObject _signlist1)
    {
        GameObject[] Signitem;
        Signitem = new GameObject[_alldaynum];
            for (int i = 0; i < _alldaynum; i++)
            {
                Signitem[i] = _signlist1.transform.GetChild(i).gameObject;
            }
        Signitem[_nowdays % _alldaynum].GetComponent<Button>().enabled = false;
        Signmark[_nowdays % _alldaynum].gameObject.SetActive(true);
    }
}
