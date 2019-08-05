using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemBtn : MonoBehaviour {
    public GameObject tools;
    public GameObject zuanshi;
    public GameObject tili;
    public GameObject toolsbtn;
    public GameObject zuanshibtn;
    public GameObject tilibtn;
	// Use this for initialization
	void Start () {
        //if (gameObject.name == "zuanshibtn")
        //{
        //    Onclick(zuanshi);
        //}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Onclick(GameObject go)
    {
        tools.SetActive(false);
        zuanshi.SetActive(false);
        tili.SetActive(false);
        toolsbtn.SetActive(false);
        zuanshibtn.SetActive(false);
        tilibtn.SetActive(false);
        go.SetActive(true);
        if (go.name == "tools")
        {
            toolsbtn.SetActive(true);
        }
        if (go.name == "zuanshi")
        {
            zuanshibtn.SetActive(true);
        }
        if (go.name == "tili")
        {
            tilibtn.SetActive(true);
        }
    }
}
