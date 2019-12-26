/////////////////////////////////////////////////
//
//PEIMEN Frame System || SimpleTool branch 
//
//creat by PEIKnifer[.CN]
//
//SimpleTool for Ran
//
//Create On 2019-10-9 17:51:55
//
//Last Update in 2019-12-5 18:08:11  
//
/////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PEIKTS
{
    /// <summary>
    /// PEIknifer Random Class
    /// </summary>
    public class PEIRNG : PEIMathOrigin
    {
        private int iSeed = 10;
        private System.Random _seed;

        public PEIRNG()
        {
            SeedIns();
        }

        /// <summary>
        /// Return Int Random Num Between Min & Max;
        /// </summary>
        /// <param name="min">Min Num</param>
        /// <param name="max">Max Num</param>
        /// <returns></returns>
        public virtual int Next(int min , int max)
        {
            //PEIKnifer_SingletonTool.CheckIns_Normal(_seed, SeedIns);
            return _seed.Next(min, max);
        }
        /// <summary>
        /// Return Int Random Less Than Max;
        /// </summary>
        /// <param name="max">Max Num</param>
        /// <returns></returns>
        public virtual int Next(int max)
        {
            //PEIKnifer_SingletonTool.CheckIns_Normal(_seed, SeedIns);
            return _seed.Next(max);
        }
        /// <summary>
        /// Return Double Random Num Between 0 - 1;
        /// </summary>
        /// <returns></returns>
        public virtual double NextDouble()
        {
            //PEIKnifer_SingletonTool.CheckIns_Normal(_seed, SeedIns);
            return _seed.NextDouble();
        }
        /// <summary>
        /// Test Random Byte Func
        /// </summary>
        /// <param name="buffer">Test Byte Array</param>
        /// <returns></returns>
        public virtual byte[] NextDouble(byte[] buffer)
        {
            //PEIKnifer_SingletonTool.CheckIns_Normal(_seed, SeedIns);
            _seed.NextBytes(buffer);
            PEIKDE.Log("RNG", "You Are Trying To Get Random Byte Array , This Function Is Not Safe , Please Affirm");
            return buffer;
        }
        protected virtual void SeedIns()
        {
            //Random ro = new Random(535);
            long tick = DateTime.Now.Ticks;
            _seed = new System.Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
        }
    }
    
}
