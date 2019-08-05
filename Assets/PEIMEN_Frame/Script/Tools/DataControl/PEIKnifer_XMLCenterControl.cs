
/////////////////////////////////////////////////
//
//PEIMEN Frame System || DataControl branch 
//
//creat by PEIKnifer[.CN]
//
//Frame for XMLCenterControl
//
//Create On 2017-12-26 15:40:45
//
//Last Update 2018-1-10 17:25:50
//
/////////////////////////////////////////////////

using PEIKDL;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace PEIKTS
{
    public class PEIKnifer_XMLCenterControl : MonoBehaviour
    {

        #region Inherent Value
        public static PEIKnifer_XMLCenterControl ins;
        public string xmlPath;
        public int infoInsNum = 0;
        #endregion

        #region Inherent Frame Function
        protected void XMLInitAwake(string xmlPathStr, PEIKnifer_Delegate_Void_Void loadFunction)
        {
            ins = this;
            xmlPath = Application.persistentDataPath + "/" + xmlPathStr + ".xml";
            FileInfo t = new FileInfo(xmlPath);
            if (!t.Exists)
            {
                FirstLoadXml();
            }
            loadFunction();
        }
        #endregion

        #region Inherent Function
        public virtual void FirstLoadXml()
        {

        }
        public static void SaveFunction()
        {
        }
        public static void LoadFunction()
        {
            Debug.Log("old running");
        }
        public static void SaveFunction<T>(string xmlPath, ref T saveInfo)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream stream = new FileStream(xmlPath, FileMode.Create);
            serializer.Serialize(stream, saveInfo);
            stream.Close();
        }
        public static void LoadFunction<T>(string xmlPath, ref T saveInfo)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream stream = new FileStream(xmlPath, FileMode.Open);
            saveInfo = (T)serializer.Deserialize(stream);
            stream.Close();
            //System.
        }
        public int GetInfoInsNum()
        {
            return infoInsNum++;
        }
        #endregion

    }
}
#region Demo
//public class Test : PEIKnifer_XMLCenterControl
//{
//    public static MyDatabase MyDB;
//    public GameObject obj;
//    public override void FirstLoadXml()
//    {
//        MyDB = new MyDatabase();
//        Unit1();
//        SaveFunction();
//        LoadFunction();

//        base.FirstLoadXml();
//    }
//    public void Unit1()
//    {
//        MyDB.list.Add(new MyEntry());
//        MyDB.list[GetInfoInsNum()].info = "PPshigedashabi";
//    }
//    public static new void SaveFunction()
//    {
//        SaveFunction<MyDatabase>(ref MyDB);
//    }
//    public static new void LoadFunction()
//    {
//        Debug.Log("new");
//        LoadFunction<MyDatabase>(ref MyDB);
//        Debug.Log("over");
//    }
//    // Use this for initialization
//    void Start()
//    {
//        XMLInitAwake("PPShiSB", LoadFunction);
//    }
//    // Update is called once per frame
//    void LateUpdate()
//    {
//    }
//    public void OnClick()
//    {

//        Debug.Log(MyDB.list[0].info);

//    }
//}
//[System.Serializable]
//public class MyEntry
//{
//    public string info;
//}
//[System.Serializable]
//public class MyDatabase
//{
//    public List<MyEntry> list = new List<MyEntry>();
//}
#endregion