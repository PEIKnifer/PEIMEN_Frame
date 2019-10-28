///////////////////////////////////////////////////
////
////PEIMEN Frame System || SimpleTool branch 
////
////creat by Loom
////
////SimpleTool Class for Obj Move & Rotate
////
////Create On 2019-8
////
////Last Update in 2019-8-20 17:03:06
////
////PS:this script create by unknown programmer , so I used the name with loom , thanks him
////
///////////////////////////////////////////////////
//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System;
//using System.Threading;
//using System.Linq;

//namespace PEIKTS
//{
//    public class PEIMEN_LoomOrigin : PEIKnifer
//    {
//        public int maxThreads = 8;
//        static int numThreads;

//        private PEIMEN_LoomOrigin _current;
//        private int _count;
//        private GameObject _parent;
//        public PEIMEN_LoomOrigin Current
//        {
//            get
//            {
//                Initialize();
//                return _current;
//            }
//        }


//        public PEIMEN_LoomOrigin(GameObject parent)
//        {
//            //_current = this;
//            //initialized = true;
//            _parent = parent;
//            initialized = false;
//            //if (!Application.isPlaying)
//            //    return;
//            //initialized = true;
//            //_current = parent.AddComponent<PEIMEN_LoomOrigin>();
//        }

//        bool initialized;

//        void Initialize()
//        {
//            if (!initialized)
//            { 
//                if (!Application.isPlaying)
//                    return;
//                initialized = true;
//                //var g = new GameObject("Loom");
//                _current = _parent.AddComponent<PEIMEN_LoomOrigin>();
//                _actions = new List<Action>();
//                _delayed = new List<DelayedQueueItem>();
//                _currentDelayed = new List<DelayedQueueItem>();
//                _currentActions = new List<Action>();
//                PEIKDE.Log("Loom", "Init Complate");
//            }

//        }

//        private List<Action> _actions;
//        public struct DelayedQueueItem
//        {
//            public float time;
//            public Action action;
//        }
//        private List<DelayedQueueItem> _delayed;

//        private List<DelayedQueueItem> _currentDelayed;

//        public void QueueOnMainThread(Action action)
//        {
//            QueueOnMainThread(action, 0f);
//        }
//        public void QueueOnMainThread(Action action, float time)
//        {
//            if (time != 0)
//            {
//                lock (Current._delayed)
//                {
//                    Current._delayed.Add(new DelayedQueueItem { time = Time.time + time, action = action });
//                }
//            }
//            else
//            {
//                lock (Current._actions)
//                {
//                    Current._actions.Add(action);
//                }
//            }
//        }

//        public Thread RunAsync(Action a)
//        {
//            Initialize();
//            while (numThreads >= maxThreads)
//            {
//                Thread.Sleep(1);
//            }
//            Interlocked.Increment(ref numThreads);
//            //a();
//            //PEIKDE.Log("PLO","Action With "+a.GetType());
//            ThreadPool.QueueUserWorkItem(RunAction, a);
//            return null;
//        }

//        private void RunAction(object action)
//        {
//            try
//            {
//                ((Action)action)();
//            }
//            catch (Exception e)
//            {
//                PEIKDE.Log("Loom", "Action Running Non-fatal Error With E --> " + e);
//            }
//            finally
//            {
//                Interlocked.Decrement(ref numThreads);
//            }

//        }

//        void OnDisable()
//        {
//            if (_current == this)
//            {

//                _current = null;
//            }
//        }

//        private List<Action> _currentActions;

//        // Update is called once per frame
//        void Update()
//        {
//            //if(_actions!=null)
//            lock (_actions)
//            {
//                _currentActions.Clear();
//                _currentActions.AddRange(_actions);
//                _actions.Clear();
//            }
//            foreach (var a in _currentActions)
//            {
//                a();
//            }
//            lock (_delayed)
//            {
//                _currentDelayed.Clear();
//                _currentDelayed.AddRange(_delayed.Where(d => d.time <= Time.time));
//                foreach (var item in _currentDelayed)
//                    _delayed.Remove(item);
//            }
//            foreach (var delayed in _currentDelayed)
//            {
//                delayed.action();
//            }
//        }
//    }
//}