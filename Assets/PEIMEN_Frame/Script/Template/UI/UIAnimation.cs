using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum AnimationType
{
    Postion,
    Scale,
    Alfa,
    Rotation
}
public delegate void anim();
public class UIAnimation : MonoBehaviour {
    public AnimationType animationtype;
    public anim Anima;
    public Vector3 target;
    public float time;
    public float oStartUp;
    private float Startalpha;
	// Use this for initialization
	void Start () {
        Anima = animAlfa;
        oStartUp = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
        Anima();
        oStartUp = Time.realtimeSinceStartup;
	}
    public void NullFunction()
    {

    }
    public void animpostion()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, time * (Time.realtimeSinceStartup - oStartUp));
        //if(transform.localPosition.x)
    }
    public void animAlfa()
    {
        time = Mathf.Lerp(time, 3, 5);
        GetComponent<Image>().color = new Color(0, 0, 0, Startalpha += time*Time.deltaTime);
        if (GetComponent<Image>().color.a > 0.7f)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, 0.7f);
            Anima = NullFunction;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
