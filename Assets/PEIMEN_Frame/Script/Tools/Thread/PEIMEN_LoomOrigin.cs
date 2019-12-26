/////////////////////////////////////////////////
//
//PEIMEN Frame System || SimpleTool branch 
//
//creat by Loom
//
//SimpleTool Class for Obj Move & Rotate
//
//Create On 2019-8
//
//Last Update in 2019-8-20 17:03:06
//
//PS:this script create by unknown programmer , so I used the name with loom , thanks him
//
/////////////////////////////////////////////////
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PEIMEN.Origin;
using System.Threading;
using System.Linq;

namespace PEIKTS
{
    public class PEIMEN_LoomOrigin : PEIKnifer_Origin
    {
        public int maxThreads = 8;
        static int numThreads;
        private int _count;
        private PEIMEN_LoomManager _parent;
        public PEIMEN_LoomOrigin Current { get; private set; }
        bool initialized;
        private string _token;
        private List<Action> _actions;
        private List<Action> _currentActions;


        public PEIMEN_LoomOrigin(string token, PEIMEN_LoomManager parent)
        {
            _token = token;
            //_current = this;
            //initialized = true;
            _parent = parent;
            initialized = false;
            Initialize();
            //if (!Application.isPlaying)
            //    return;
            //initialized = true;
            //_current = parent.AddComponent<PEIMEN_LoomOrigin>();
        }


        void Initialize()
        {
                if (!Application.isPlaying)
                    return;
                initialized = true;
                //var g = new GameObject("Loom");
                Current = this;
                _actions = new List<Action>();
                _delayed = new List<DelayedQueueItem>();
                _currentDelayed = new List<DelayedQueueItem>();
                _currentActions = new List<Action>();
               // PEIKDE.Log("Loom", "Init Complate");
        }

        public struct DelayedQueueItem
        {
            public float time;
            public Action action;
        }
        private List<DelayedQueueItem> _delayed;

        private List<DelayedQueueItem> _currentDelayed;

        public void QueueOnMainThread(Action action)
        {
            QueueOnMainThread(action, 0f);
        }
        public void QueueOnMainThread(Action action, float time)
        {
            if (time != 0)
            {
                lock (Current._delayed)
                {
                    Current._delayed.Add(new DelayedQueueItem { time = Time.time + time, action = action });
                }
            }
            else
            {
                lock (Current._actions)
                {
                    Current._actions.Add(action);
                }
            }
        }

        public Thread RunAsync(Action a)
        {
            //Initialize();
            while (numThreads >= maxThreads)
            {
                Thread.Sleep(1);
            }
            Interlocked.Increment(ref numThreads);
            //a();
            //PEIKDE.Log("Loom", "Action With " + a.Method.Name);
            ThreadPool.QueueUserWorkItem(RunAction, a);
            return null;
        }

        private void RunAction(object action)
        {
            try
            {
                ((Action)action)();
                //PEIKDE.Log("Loom", "Action With " + ((Action)action).Method.Name);
            }
            catch(Exception e)
            {
                //Interlocked.
                PEIKDE.Log("LoomOrigin", "Action Running Non-fatal Error With E --> " + e);
                //_parent.RemoveLoom(_token);
                PEIKDE.Log("LoomOrigin", "Fix Program Fixed Bug");
            }
            finally
            {
                Interlocked.Decrement(ref numThreads);
            }

        }

        void OnDisable()
        {
            if (Current == this)
            {

                Current = null;
            }
        }


        // Update is called once per frame
        public void Update()
        {
            //if(_actions!=null)
            lock (_actions)
            {
                _currentActions.Clear();
                _currentActions.AddRange(_actions);
                _actions.Clear();
            }
            foreach (var a in _currentActions)
            {
                a();
            }
            lock (_delayed)
            {
                _currentDelayed.Clear();
                _currentDelayed.AddRange(_delayed.Where(d => d.time <= Time.time));
                foreach (var item in _currentDelayed)
                    _delayed.Remove(item);
            }
            foreach (var delayed in _currentDelayed)
            {
                delayed.action();
            }
        }
    }
}