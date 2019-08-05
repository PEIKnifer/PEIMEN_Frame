/////////////////////////////////////////////////
//
//PEIMEN Frame System || Template AnimeUI branch 
//
//creat by PEIKnifer[.CN]
//
//Template AnimeUI for Anime UI CenterController
//
//Create On 2019-4
//
//Last Update in 2019 4 25 14:43:52
//
/////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKTL_AU
{
    public abstract class AnimeUIController : PEIKnifer
    {

        public List<AnimeUIPage> PageList;
        protected int nowStateNum;
        protected PEIKnifer_Flag pageIsOpen;

        protected void TemplateInit()
        {
            pageIsOpen = new PEIKnifer_Flag();
            pageIsOpen.Flag = false;
            ChangeNowState(0);
        }
        protected virtual void ChangeNowState(int state)
        {
            nowStateNum = state;
            if (PageList.Count > 0)
            {
                ChangeToPage();
            }
        }

        protected virtual void ChangeToPage()
        {
            for (int i = 0; i < PageList.Count; i++)
            {
                if (PageList[i].Flag)
                {
                    PageList[i].Close(OpenTargetPage);
                    pageIsOpen.Flag = true;
                }
            }
            if (!pageIsOpen.Flag)
            {
                OpenTargetPage();
            }
        }
        protected virtual void OpenTargetPage()
        {
            PageList[nowStateNum].Open();
        }

        private void Null()
        {

        }
    }
}
