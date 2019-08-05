/////////////////////////////////////////////////
//
//PEIMEN Frame System || SimpleTool branch 
//
//creat by PEIKnifer[.CN]
//
//SimpleTool for Rotate
//
//Create On 2019-3
//
//Last Update in 2019.3.14 15:49:03
//
/////////////////////////////////////////////////
using PEIKTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEIMEN_ST_Rotate : PEIKnifer_ObjSimpleRotate {

    private PSTRotateState _pSTRotateState;

	// Use this for initialization
	void Start () {
        FrameInit();
	}
	
	// Update is called once per frame
	void Update () {
        if (_pSTRotateState == PSTRotateState.Update)
        {
            FrameUpdate();
        }
        else
        {
            StartCoroutine(FrameUpdateIE());
        }
	}
}
public enum PSTRotateState
{
    Update,
    IE
}
