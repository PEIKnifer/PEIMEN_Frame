using PEIMEN;
using PEIMEN.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : PEIKnifer_Origin
{
    private List<GameObjectPoolItem> _objList;
    private GameObject _toolobj, _logicProcessor;
    private int _poolNum;
    private string _token;

    public GameObjectPool(string token,int poolNum)
    {
        _poolNum = poolNum;
        _objList = new List<GameObjectPoolItem>();
        _logicProcessor = Object.Instantiate(new GameObject("Logic Processor"));
    }
    public GameObject GetObj(GameObject obj)
    {
        if (_objList.Count > 0)
        {
            _toolobj = _objList[0].Obj;
            if(_objList[0].Ins!=null)
            _objList[0].Ins.Instance();
            _objList.RemoveAt(0);
        }
        else
        {
            _toolobj = GameObject.Instantiate(obj);
        }
        _toolobj.transform.SetParent(null);
        _toolobj.SetActive(true);
        return _toolobj;
    }
    public void DestoryObj(GameObject obj)
    {
        _objList.Add(new GameObjectPoolItem()
        {
            Obj = obj,
            Ins = obj.GetComponent<IGameObjectPoolIns>()
        });
        if(_objList[_objList.Count - 1].Ins!=null)
        _objList[_objList.Count - 1].Ins.Destory();
        obj.transform.SetParent(_logicProcessor.transform);
        obj.SetActive(false);
    }
    private void CheckPoolNum()
    {
        while (_objList.Count > _poolNum)
        {
            _toolobj = _objList[0].Obj;
            Object.Destroy(_toolobj);
            _objList.RemoveAt(0);
        }
    }
    public void SetPoolNum(int poolNum)
    {
        _poolNum = poolNum;
        CheckPoolNum();
    }
    public void Destory()
    {
        while (_objList.Count > 0)
        {
            _toolobj = _objList[0].Obj;
            Object.Destroy(_toolobj);
            _objList.RemoveAt(0);
        }
        Object.Destroy(_logicProcessor);
    }
}
public struct GameObjectPoolItem
{
    public GameObject Obj;
    public IGameObjectPoolIns Ins;
}

