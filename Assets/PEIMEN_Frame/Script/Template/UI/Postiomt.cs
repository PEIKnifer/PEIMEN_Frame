using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Postiomt : MonoBehaviour {
    public GameObject nextone;
    public anim Anima;
    public float target;
    public float time;
    public float time1;
    public float time2;
    public float oStartUp;
    private float StartScale;
    private float Startalpha;
    // Use this for initialization
    void Start()
    {
        
        Anima = animPosUp;
        oStartUp = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        animAlfa();
        Anima();
        oStartUp = Time.realtimeSinceStartup;
    }
    public void NullFunction()
    {

    }
    public void animPosUp()
    {
        time = Mathf.Lerp(time, 1200, 5);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x,target,0), time * Time.deltaTime);
        if (transform.localPosition.y == target)
        {
            Anima = animPosDown;
            if (nextone!=null)
                nextone.gameObject.SetActive(true);
        }
    }
    public void animPosDown()
    {
        //time = Mathf.Lerp(time, 8, 5);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, 0, 0), time1 * Time.deltaTime);
        if (transform.localPosition.y == 0)
        {
            Anima = NullFunction;
            // transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void animAlfa()
    {
        if (GetComponent<Image>().color.a >= 1f)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            return;
        }
        time2 = Mathf.Lerp(time2, 5, 5);
        GetComponent<Image>().color = new Color(1, 1, 1, Startalpha += time2 * Time.deltaTime);
    }
}
