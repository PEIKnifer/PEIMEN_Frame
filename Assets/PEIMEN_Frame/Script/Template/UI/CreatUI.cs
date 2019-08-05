using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatUI : MonoBehaviour {
    public GameObject targrtUI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClick()
    {
        targrtUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
