
/////////////////////////////////////////////////
//
//PEIMEN Template System || FlyText branch 
//
//creat by PEIKnifer[.CN]
//
//Template for FlyText
//
//Create On 2017-11-20 11:37:59
//
//Last Update in 2017-12-25 17:47:09
//
/////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PEIKnifer_FlyText : MonoBehaviour {
    #region Inherent Value
    float distance;
    public Vector3 head;
    public Transform target;
    public float flyNum;
    public GameObject[] text;
    public GameObject[] NeedState;
    public PEIKnifer_Timer t;
    #endregion
    // Use this for initialization
    #region Inherent Function
    void Start()
    {
        t = new PEIKnifer_Timer();
        t.SetTime(1f);
        flyNum = 0;

    }
    public void FirstSet(Vector3 head, int num, FightTextState state)
    {
        this.head = head + Vector3.up * 0.8f;
        //text.GetComponent<Text>().text = num.ToString();
        for (int i = 0; i < NeedState.Length; i++)
        {
            text[i].SetActive(false);
            NeedState[i].SetActive(false);
        }
        switch (state)
        {
            case FightTextState.normal:
                text[0].SetActive(true);
                NeedState[0].SetActive(true);
                text[0].GetComponent<Text>().text = num.ToString();
                break;
            case FightTextState.skill:
                text[1].SetActive(true);
                NeedState[1].SetActive(true);
                text[1].GetComponent<Text>().text = num.ToString();
                break;
        }
        distance = Vector3.Distance(head, Camera.main.transform.position);

        float newDistance = distance / Vector3.Distance(head, Camera.main.transform.position);
        Vector3 position = Camera.main.WorldToScreenPoint(head + Vector3.up * flyNum * Time.deltaTime);
        transform.position = position;//位置
        transform.localScale = Vector3.one * newDistance;
        transform.localScale = new Vector3((Vector3.one * newDistance).x * 0.5f, (Vector3.one * newDistance).y, (Vector3.one * newDistance).z);

    }


    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            if (t.Timer())
            {
                Destroy(gameObject);
            }

            flyNum += Time.deltaTime * 3f * t.runTime;
            float newDistance = distance / Vector3.Distance(head, Camera.main.transform.position);
            Vector3 position = Camera.main.WorldToScreenPoint(head + Vector3.up * flyNum);
            transform.position = position;//位置
            transform.localScale = Vector3.one * newDistance;
            transform.localScale = new Vector3((Vector3.one * newDistance).x, (Vector3.one * newDistance).y, (Vector3.one * newDistance).z);
        }
    }
#endregion
}

    #region Enum Tool
public enum FightTextState
{
    normal,
    highATK,
    skill
}
#endregion