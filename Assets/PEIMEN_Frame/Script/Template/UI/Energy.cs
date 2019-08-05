/////////////////////////////////////////////////
//
//template System || UI branch 
//
//creat by ErHua
//
//Template for EnergyUI
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

public class Energy : MonoBehaviour {
    public int year, mouth, day, hour, min, sec;
    public GameObject timer;//倒计时
    public Text nowtimer;//当前体力值
    private float Timed;
    public giftstage order;
    private PEIKnifer_Timer Et;
    public int AllEnergy;
    public int addEnergytime;
    private int[] localxml;//1体力值，2年，4天，5小时，6分钟，7秒，8恢复一点体力剩余时间
	void Start () {
        AllEnergy = 10;
        addEnergytime = 6;
        Et = new PEIKnifer_Timer();
        order = NullFuntion;
        InitEnergy();
        if (localxml[2] == 0)
        {
            localxml[2] = System.DateTime.Now.Year;
            localxml[4] = System.DateTime.Now.DayOfYear;
            localxml[5] = System.DateTime.Now.Hour;
            localxml[6] = System.DateTime.Now.Minute;
            localxml[7] = System.DateTime.Now.Second;
           // GameInfoXMLCenterControl.SaveFunction();
           // GameInfoXMLCenterControl.LoadFunction();
        }
	}
	
	// Update is called once per frame
	void Update () {
        order();
        nowtimer.text = localxml[1].ToString();
        //if (GameInfoXMLCenterControl.GameInfoItemDB.list[2].intArray[1] > 7000)
        //{
        //    gameObject.SetActive(false);
        //}
	}
    void NullFuntion()
    {

    }
    
    public void InitEnergy()
    {
        if (localxml[1] < AllEnergy)
        {
            if (year > localxml[2] && localxml[2] != 0)
            {
                localxml[1] = AllEnergy;
                localxml[8] = 0;
              //  GameInfoXMLCenterControl.SaveFunction();
              //  GameInfoXMLCenterControl.LoadFunction();
                order = NullFuntion;
                timer.gameObject.SetActive(false);
            }
            else
            {
                int temptime = (int)SplitTime();
                if (temptime > ((AllEnergy - 1) - localxml[1]) * addEnergytime * 60 + localxml[8])
                {
                    localxml[1] = AllEnergy;
                    localxml[8] = 0;
                   // GameInfoXMLCenterControl.SaveFunction();
                   // GameInfoXMLCenterControl.LoadFunction();
                    order = NullFuntion;
                    timer.gameObject.SetActive(false);
                }
                else
                {
                    timer.gameObject.SetActive(true);
                    localxml[1] += temptime / (addEnergytime * 60);
                    localxml[8] -= (temptime % (addEnergytime * 60));
                   // GameInfoXMLCenterControl.SaveFunction();
                   // GameInfoXMLCenterControl.LoadFunction();
                    Et.SetTime(localxml[8]);
                    order = SubTimer;
                }
            }
        }
        else
        {
            timer.gameObject.SetActive(false);

            order = NullFuntion;
        }
        transform.GetChild(0).GetComponent<Text>().text = localxml[1].ToString();
    }
    public void SubTimer()
    {
        if (Et.Timer())
        {
            Et.Clear();
            localxml[8] = 0;
            AddEnergy(1);
        }
        else
        {
            timer.gameObject.SetActive(true);
            if ((int)Et.runTime / 60 > 9)
                timer.transform.GetChild(0).GetComponent<Text>().text = "1";
            else
                timer.transform.GetChild(0).GetComponent<Text>().text = "0";
            if ((int)Et.runTime / 60 > 9)
                timer.transform.GetChild(1).GetComponent<Text>().text = "0";
            else
                timer.transform.GetChild(1).GetComponent<Text>().text = ((int)(Et.runTime / 60)).ToString();
            if ((int)Et.runTime % 60 > 9)
                timer.transform.GetChild(3).GetComponent<Text>().text = ((int)Et.runTime % 60 / 10).ToString();
            else
                timer.transform.GetChild(3).GetComponent<Text>().text = "0";
            timer.transform.GetChild(4).GetComponent<Text>().text = ((int)Et.runTime % 10).ToString();
        }
    }
    float SplitTime()
    {
        //dateTime = dateTime.Replace("-", "|");
        //dateTime = dateTime.Replace(" ", "|");
        //dateTime = dateTime.Replace(":", "|");
        //string[] Times = dateTime.Split('|');
        float lastTime = 0;
        year = System.DateTime.Now.Year;
        day = System.DateTime.Now.DayOfYear;
        hour = System.DateTime.Now.Hour;
        min = System.DateTime.Now.Minute;
        sec = System.DateTime.Now.Second;
        lastTime = 86400 * (day - localxml[4]) + 3600 * (hour - localxml[5]) + 60 * (min - localxml[6]) + (sec - localxml[7]);
        localxml[2] = year;
        localxml[4] = day;
        localxml[5] = hour;
        localxml[6] = min;
        localxml[7] = sec;
       // GameInfoXMLCenterControl.SaveFunction();
       // GameInfoXMLCenterControl.LoadFunction();
        return lastTime;
    }
    public void SumTimed()
    {
        //StartCoroutine
    }
    public void SubEnergy(int _index)
    {
        Et = new PEIKnifer_Timer();
        localxml[1] -= _index;
       // GameInfoXMLCenterControl.SaveFunction();
       // GameInfoXMLCenterControl.LoadFunction();
        if (localxml[1] > (AllEnergy - 1))
        {
            timer.SetActive(false);
            order = NullFuntion;
        }
        else
        {
            if (localxml[8] == 0)
                localxml[8] = addEnergytime * 60;
            // InitEnergy();

           // GameInfoXMLCenterControl.SaveFunction();
           // GameInfoXMLCenterControl.LoadFunction();
            Et.SetTime(localxml[8]);
        }
    }
    public void AddEnergy(int _index)
    {
        localxml[1] += _index;
       // GameInfoXMLCenterControl.SaveFunction();
       // GameInfoXMLCenterControl.LoadFunction();
        if (localxml[1] > (AllEnergy - 1))
        {
            timer.SetActive(false);
            order = NullFuntion;
        }
        else
        {
            if (localxml[8] == 0)
                localxml[8] = addEnergytime * 60;
            // InitEnergy();

           // GameInfoXMLCenterControl.SaveFunction();
           // GameInfoXMLCenterControl.LoadFunction();
            Et.SetTime(localxml[8]);
        }
    }
}
