using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEIKEV;
using PEIKTS;
using PEIMEN;

/// <summary>
/// PEIMEN Frame Simple Tool Entity Class
/// </summary>
public class PEIMEN_Entity : PEIKnifer_Singleton
{
    private static PEIMEN_Entity _ins;

    /// <summary>
    /// Class Instance
    /// </summary>
    public static PEIMEN_Entity I
    {
        get
        {
            if (!_ins)
            {
                _ins = GetIns<PEIMEN_Entity>();
                PEIMEN_Entity.Init();
            }
            return _ins;
        }
    }

    /// <summary>
    /// PEIEvent Manager Entity
    /// </summary>
    public static PEIEvent_Manager Event { get { Init(); return _event; } }
    
    /// <summary>
    /// PEIMEN Math Entity
    /// </summary>
    public static PEIMath Math { get { Init();return _math; } }
    /// <summary>
    /// PEIMEN Net Tool Entity
    /// </summary>
    public static PEINet_Origin WWW { get { Init();return _net; } }
    /// <summary>
    /// PEIMEN Time Center Control Tool Entity
    /// </summary>
    public static PEIMEN_STTimeCC Time { get { Init(); return _time; } }

    private static PEIEvent_Manager _event;
    private static PEIMath _math;
    private static PEINet_Origin _net;
    private static PEIMEN_STTimeCC _time;


    private static void Init()
    {
        if (_ins)
            return;
        _ins = GetIns<PEIMEN_Entity>();


        _event = new PEIEvent_Manager();
        _math = new PEIMath();
        new PEIMEN_STTimeCC(out _time, I.gameObject);
        new PEINet_Origin(out _net,I.gameObject);
        PEIKDE.Log("Entity", "PEIMEN_Entity Init Complete");
    }
}
