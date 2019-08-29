using PEIKTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PEIKniferDebug : PEIKnifer {
    
    [SerializeField]
    private List<Text> MsgTL;
    //float t;

    private void Awake()
    {
        //PEIKDE.Log("ASDASDASD");
        PEIKDE.AddErrorDel(AddMessage);
        PEIKDE.AddLogDel(AddMessage);
        PEIKDE.AddWarningDel(AddMessage);
    }


    // Update is called once per frame
    //void Update () {
    //       //t += Time.deltaTime;
    //       //if (t > 1)
    //       //{
    //       //    t = 0;
    //       //    Log("[PEIKTT]");
    //       //    Debug.Log("[PEIKTT] as");
    //       //}
    //   }
    private void AddMessage(object message)
    {
        try
        {
            Loom.RunAsync(() =>
            {
                for (int i = MsgTL.Count - 1; i >= 0; i--)
                {
                    if (i - 1 > 0)
                        MsgTL[i].text = MsgTL[i - 1].text;
                    else
                    {
                        MsgTL[i].text = message.ToString();
                    }
                }
            });
        }
        catch
        {
            PEIKDE.RemoveErrorDel(AddMessage);
            PEIKDE.RemoveLogDel(AddMessage);
            PEIKDE.RemoveWarningDel(AddMessage);
        }
    }
}
