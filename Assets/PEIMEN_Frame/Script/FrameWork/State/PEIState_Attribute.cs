using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class PEIStateAttribute : Attribute
{
    public PEIState_Type StateType
    {
        get;
        private set;
    }

    public PEIStateAttribute()
    {
        StateType = PEIState_Type.Normal;

    }
    public PEIStateAttribute(PEIState_Type value)
    {
        StateType = value;
    }
}

//游戏状态的类型
public enum PEIState_Type
{
    /// <summary>
    /// 正常
    /// </summary>
    Normal = 0,
    /// <summary>
    /// 忽略
    /// </summary>
    Ignore,
    /// <summary>
    /// 开始
    /// </summary>
    Start,
}
