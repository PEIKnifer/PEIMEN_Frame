﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PEIMEN.Test
{
    public class FrameWorkTest : PEIKnifer
    {
        public string SceneName;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene(SceneName);
            }
        }
    }
}
