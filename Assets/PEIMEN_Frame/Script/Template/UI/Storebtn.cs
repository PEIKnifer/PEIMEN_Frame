using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storebtn : MonoBehaviour {
    public GameObject store;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnstoreClick()
    {
       // SoundCenterControl.ins.CreatSoundSource(Camera.main.gameObject, SoundCenterControl.ins.buttonClickClip, 0.5f);
        if (transform.parent.gameObject.name == "Tips")
        {
            store.transform.GetChild(11).GetComponent<StoreItemBtn>().Onclick(store.transform.GetChild(2).gameObject);
        }
        store.transform.localScale = Vector3.zero;
       // store.GetComponent<Windows>().Refresh();
        store.SetActive(true);
        
    }
}
