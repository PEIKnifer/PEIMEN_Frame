using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBuybtn : MonoBehaviour {
    public GameObject tips;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnBuyBtnClick(string things)
    {
        //SoundCenterControl.ins.CreatSoundSource(Camera.main.gameObject, SoundCenterControl.ins.buttonClickClip, 0.5f);

        //GetComponent<BuySure>().things = things;
        if (gameObject.tag != "store")
        {

        }
        else
        {

        }
    }
}
