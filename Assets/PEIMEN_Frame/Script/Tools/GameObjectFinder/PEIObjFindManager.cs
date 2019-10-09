/////////////////////////////////////////////////
//
//PEIMEN Frame System || SimpleTool branch 
//
//creat by PEIKnifer[.CN]
//
//SimpleTool for GameObject Find Manager
//
//Create On 2019-10-9 15:40:42
//
//Last Update in 2019-10-9 15:40:50  
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTS
{
    public class PEIObjFindManager : MonoBehaviour
    {
        /// <summary>
        /// 所有标记了name的物体
        /// </summary>
        public readonly Dictionary<string, PEIObjAssetName> AllAssets;

        public PEIObjFindManager()
        {
            AllAssets = new Dictionary<string, PEIObjAssetName>();
        }

        /// <summary>
        /// 设置所有标记的Name的物体
        /// </summary>
        /// <param name="assets"></param>
        protected virtual void SetAllAssets(PEIObjAssetName[] assets)
        {
            AllAssets.Clear();
            for (int i = 0; i < assets.Length; i++)
            {
                if (AllAssets.ContainsKey(assets[i].OperationName))
                {
                    PEIKDE.LogError("POFM", "The Name [" + assets[i].OperationName + "] Of Obj [" + assets[i].gameObject.name + "] Is Same As [" + AllAssets[assets[i].OperationName].gameObject + "] Would You Want Rewrite Anyway ?");
                }
                if (assets[i].OperationName.Trim() != "")
                {
                    AllAssets[assets[i].OperationName] = assets[i];
                }
                else
                {
                    PEIKDE.LogError("POFM", "Script AssetName On [" + assets[i].gameObject.name + "] Didn`t Have A Name");
                }
            }
        }
        public virtual void RewriteAsset(PEIObjAssetName opAsset)
        {
            if (AllAssets.ContainsKey(opAsset.OperationName))
            {
                PEIKDE.LogError("POFM", "The Name [" + opAsset.OperationName + "] Of Obj [" + opAsset.gameObject.name + "] Is Same As [" + AllAssets[opAsset.OperationName].gameObject + "] Would You Want Rewrite Anyway ?");
            }
            if (opAsset.OperationName.Trim() != "")
            {
                AllAssets[opAsset.OperationName] = opAsset;
            }
            else
            {
                PEIKDE.LogError("POFM", "Script AssetName On [" + opAsset.gameObject.name + "] Didn`t Have A Name");
            }
        }
        public virtual void RemoveAsset(PEIObjAssetName opAsset)
        {
            if (AllAssets.ContainsKey(opAsset.OperationName))
            {
                AllAssets.Remove(opAsset.OperationName);
            }
            else
            {
                PEIKDE.Log("POFM", "The Name [" + opAsset.OperationName + "] You Want Remove Is Null");
            }
        }
        public virtual void GetAllOperationAssetsInScene()
        {
            //AllAssetIds
            //设置所有可操作的物体
            PEIObjAssetName[] allAsset = Resources.FindObjectsOfTypeAll<PEIObjAssetName>();
            List<PEIObjAssetName> allSceneAsset = new List<PEIObjAssetName>();
            for (int i = 0; i < allAsset.Length; i++)
            {
                if (allAsset[i].gameObject.scene == null || string.IsNullOrEmpty(allAsset[i].gameObject.scene.name))
                    continue;
                //添加标记id的物体
                allSceneAsset.Add(allAsset[i]);
            }
            SetAllAssets(allSceneAsset.ToArray());
        }
    }
}