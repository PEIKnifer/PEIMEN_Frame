using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowReward : PEIKnifer {
    public new giftstage animation;
    public float oStartUp;
    void Start()
    {
        oStartUp = Time.realtimeSinceStartup;
        animation = animationopen;
    }
    void Update()
    {
        animation();
    }
    public void nullfunction() { }
    public void animationopen()
    {
        if (transform.localScale.x < 0.99f)
        {
            transform.localScale += Vector3.one * (Time.realtimeSinceStartup - oStartUp) * 6f;
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 0);
            animation = nullfunction;
        }
        oStartUp = Time.realtimeSinceStartup;
    }
    public void Refresh()
    {
        transform.localScale = Vector3.zero;
        animation = animationopen;
        oStartUp = Time.realtimeSinceStartup;
    }
    public void InsReward(string name)
    {
        Debug.Log(name);
        Instantiate(Resources.Load<GameObject>(name), transform, false);
    }
    public void CloseReward()
    {
        gameObject.SetActive(false);
        Destroy(transform.GetChild(1).gameObject);
    }
}
