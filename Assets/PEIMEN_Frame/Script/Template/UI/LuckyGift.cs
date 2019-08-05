/////////////////////////////////////////////////
//
//template System || UI branch 
//
//creat by ErHua
//
//Template for GiftUI
//
//Create On 2017-12-26 11:32:49
//
//Last Update in 2017-12-26 11:32:49
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void giftstage();
public class LuckyGift : MonoBehaviour {
    public GameObject mark;
    public GameObject gifts;
    public GameObject reward;
    giftstage runstage;
    public float t1;
    public float t2;
    public float t3;
    public PEIKnifer_Timer timer1;
    public PEIKnifer_Timer timer2;
    public PEIKnifer_Timer timer3;
    public int[] allindex;
    public int[] probability;//各项奖品的中奖几率
    private int giftindex;//mark锁定的奖品序号
    private int roundnum;//开始抽奖后加速实时圈数
    private int StartIndex;//开始抽奖时的序号
    public int allround;//开始抽奖后加速总圈数
    public int slownum;//减速数量
    private bool isStart;//是否开始抽奖
    private bool isSlowdown;//是否开始减速
    void Start()
    {
        timer1 = new PEIKnifer_Timer();
        timer2 = new PEIKnifer_Timer();
        timer3 = new PEIKnifer_Timer();
        InitGifts();
        allindex = new int[100];
        int tempadd = 0;
        for (int i = 0; i < probability.Length; i++)
        {
            for (int a = tempadd; a < tempadd+probability[i]; a++)
            {
                allindex[a] = i;
            }
            tempadd += probability[i];
        }
        runstage = normalstage;
    }
    void Update()
    {
        runstage();
        GiftStageControl();
    }
    public void InitGifts()
    {
        timer1.SetTime(t1 / gifts.transform.childCount);
        timer2.SetTime(t2 / gifts.transform.childCount);
        timer3.SetTime(t3 / gifts.transform.childCount);
    }
    public void GiftStageControl()
    {
        if (isStart)
        {
            StartIndex = giftindex;
            runstage = startstage;
            isStart = false;
        }
        if (isSlowdown)
        {
            runstage = slowdownstage;
            isSlowdown = false;
        }
    }
    public void normalstage()
    {
        if (timer1.Timer())
        {
            timer1.Clear();
            giftindex++;
            if (giftindex >= gifts.transform.childCount)
            {
                giftindex = 0;
            }
            mark.transform.localPosition = gifts.transform.GetChild(giftindex).transform.localPosition;
            timer1.SetTime(t1 / gifts.transform.childCount);
        }
    }
    public void startstage()
    {
        if (timer2.Timer())
        {
            timer2.Clear();
            giftindex++;
            if (giftindex >= gifts.transform.childCount)
            {
                giftindex = 0;
            }
            if (giftindex == StartIndex)
            {
                roundnum++;
            }
            mark.transform.localPosition = gifts.transform.GetChild(giftindex).transform.localPosition;
            timer2.SetTime(t2 / gifts.transform.childCount);
            if (roundnum > allround)
            {
                runstage = bufferstage;
            }
        }
    }
    public void bufferstage()
    {
        if (timer2.Timer())
        {
            timer2.Clear();
            giftindex++;
            //StartIndex++;
            if (giftindex >= gifts.transform.childCount)
            {
                giftindex = 0;
            }
            //if (StartIndex >= gifts.transform.childCount)
            //{
            //    StartIndex = 0;
            //}
            if (giftindex == getbuffernum())
            {
                isSlowdown = true;
            }
            mark.transform.localPosition = gifts.transform.GetChild(giftindex).transform.localPosition;
            timer2.SetTime(t2 / gifts.transform.childCount);
        }
    }
    public void slowdownstage()
    {
        if (timer3.Timer())
        {
            timer3.Clear();
            giftindex++;
            if (giftindex >= gifts.transform.childCount)
            {
                giftindex = 0;
            }
            mark.transform.localPosition = gifts.transform.GetChild(giftindex).transform.localPosition;
            timer3.SetTime(t3 / gifts.transform.childCount);
            if (giftindex == randemgift())
            {
                reward.GetComponent<ShowReward>().Refresh();
                reward.SetActive(true);
                runstage = NullFunction;
            }
        }
    }
    public int randemgift()
    {
        return allindex[Random.Range(0, 100)];
    }
    public int getbuffernum()
    {
        if (randemgift() - slownum < 0)
        {
            return ((randemgift() + gifts.transform.childCount) - slownum);
        }
        else
        {
            return (randemgift() - slownum);
        }
    }
    public void Startgift()
    {
        isStart = true;
        randemgift();
    }
    public void NullFunction() {
        if (!reward.activeSelf)
        {
            runstage = normalstage;
        }
    }
}
