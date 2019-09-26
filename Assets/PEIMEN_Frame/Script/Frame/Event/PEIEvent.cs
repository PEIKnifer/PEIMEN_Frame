﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PEIKEV
{
    public class PEIEvent : PEIEvent_Manager
    {
        private static PEIEvent ins;

        public static PEIEvent Ins
        {
            get
            {
                if (ins==null)
                    ins = new PEIEvent();
                return ins;
            }
        }
    }
}