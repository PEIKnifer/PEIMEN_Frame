using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleT : MonoBehaviour {

    public anim Anima;
    public float target;
    public float time;
    public float time1;
    public float oStartUp;
    private float StartScale;
    // Use this for initialization
    void Start()
    {
        Anima = animScaleUp;
        oStartUp = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        Anima();
        oStartUp = Time.realtimeSinceStartup;
    }
    public void NullFunction()
    {

    }
    public void animScaleUp()
    {
        time = Mathf.Lerp(time, 30, 5);
        transform.localScale = Vector3.MoveTowards(transform.localScale,Vector3.one*target,time*Time.deltaTime);
        if (transform.localScale.x == target)
        {
            Anima = animScaleDown;
            if (transform.childCount!=0)
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void animScaleDown()
    {
        //time = Mathf.Lerp(time, 8, 5);
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, time1 * Time.deltaTime);
        if (transform.localScale.x == 1f)
        {
            Anima = NullFunction;
            // transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
