/////////////////////////////////////////////////
//
//PEIMEN Frame System || SimpleTool branch 
//
//creat by PEIKnifer[.CN]
//
//SimpleTool for GameObject Find
//
//Create On 2019-10-9 15:40:42
//
//Last Update in 2019-10-9 15:40:50  
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIKTS;

public class PEIGameObjectFinder : PEIKnifer
{
    private static PEIObjFindManager _manager;
    private static bool _flag;
    private static PEIObjAssetName _toolAss;

    // Update is called once per frame
    public static void SetAllAsset()
    {
        CheckManagerState();
        _manager.GetAllOperationAssetsInScene();
    }
    public static GameObject GetGameObject(string name)
    {
        _toolAss = null;
        if (!_manager.AllAssets.TryGetValue(name, out _toolAss))
            PEIKDE.LogError("PGOF", "Finder Didn`t Have GameObject Of Type [" + name + "]");
        if (_toolAss)
        {
            return _toolAss.gameObject;
        }
        return null;
    }
    private static void CheckManagerState()
    {
        if (!_manager)
            _manager = new PEIObjFindManager();
    }
}