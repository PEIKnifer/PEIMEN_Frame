using PEIMEN;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Jobs;

public class JobTest : JobComponentSystem
{
    [BurstCompile]//使用这个定语来利用Burst编译器，需要在Unity编辑器菜单栏Jobs-》Burst-》Enable Compilition来激活编译器
    struct RotationSpeedJob : IJobForEach<Rotation, RotationSpeed_IJobForEach>
    {
        public float DeltaTime;

        // The [ReadOnly] attribute tells the job scheduler that this job will not write to rotSpeedIJobForEach
        /// <summary>
        /// 该方法会在OnUpdate中每帧执行
        /// </summary>
        /// <param name="rotation">旋转</param>
        /// <param name="rotSpeedIJobForEach">[ReadOnly]定语告诉Jobs任务系统预定器这项任务不需要写入，这样会更快速</param>
        public void Execute(ref Rotation rotation, [ReadOnly] ref RotationSpeed_IJobForEach rotSpeedIJobForEach)
        {
            // Rotate something about its up vector at the speed given by RotationSpeed_IJobForEach.
            //Component的数据在这里使用，使方块旋转起来
            rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(math.up(), rotSpeedIJobForEach.RadiansPerSecond * DeltaTime));
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new RotationSpeedJob
        {
            DeltaTime = Time.deltaTime
        };

        return job.Schedule(this, inputDependencies);
    }
}

[Serializable]
public struct RotationSpeed_IJobForEach : IComponentData
{
    public float RadiansPerSecond;
}

public struct MyJob : IJob
{
    public Vector3 OldRot, TarRot, Rot;
    public float Speed;
    public NativeArray<Vector3> values;
    public void Execute()
    {
        Rot = Vector3.MoveTowards(OldRot, TarRot, Speed);
        values[0] = Rot;
        //PEIKDE.Log("Job", "EXTING!!!");
    }
}

public struct MyParallelJob : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<float> a;
    [ReadOnly]
    public NativeArray<float> b;
    public NativeArray<float> result;

    public void Execute(int i)
    {
        result[i] = a[i] + b[i];
    }
}