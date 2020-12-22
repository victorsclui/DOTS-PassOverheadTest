using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

public class MultiPassSystem : SystemBase
{
    EntityQuery m_AddOneGroup;
    EntityQuery m_AddTwoGroup;
    EntityQuery m_AddThreeGroup;
    EntityQuery m_AddFourGroup;

    [BurstCompile]
    struct AddOneJob : IJobChunk
    {
        public ComponentTypeHandle<AddOne> addOneTypeHandle;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var addOneChunk = chunk.GetNativeArray(addOneTypeHandle);

            for (var i = 0; i < addOneChunk.Length; i++)
            {
                var addOneComp = addOneChunk[i];
                addOneChunk[i] = new AddOne { Value = (int)math.asin(math.exp(math.sin(math.sqrt(addOneComp.Value)) + 1)) };
            }
        }
    }

    [BurstCompile]
    struct AddTwoJob : IJobChunk
    {
        public ComponentTypeHandle<AddTwo> addTwoTypeHandle;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var addTwoChunk = chunk.GetNativeArray(addTwoTypeHandle);

            for (var i = 0; i < addTwoChunk.Length; i++)
            {
                var addTwoComp = addTwoChunk[i];
                addTwoChunk[i] = new AddTwo { Value = (int)math.asin(math.exp(math.sin(math.sqrt(addTwoComp.Value)) + 2)) };
            }
        }
    }

    [BurstCompile]
    struct AddThreeJob : IJobChunk
    {
        public ComponentTypeHandle<AddThree> addThreeTypeHandle;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var addThreeChunk = chunk.GetNativeArray(addThreeTypeHandle);

            for (var i = 0; i < addThreeChunk.Length; i++)
            {
                var addThreeComp = addThreeChunk[i];
                addThreeChunk[i] = new AddThree { Value = (int)math.asin(math.exp(math.sin(math.sqrt(addThreeComp.Value)) + 3)) };
            }
        }
    }

    [BurstCompile]
    struct AddFourJob : IJobChunk
    {
        public ComponentTypeHandle<AddFour> addFourTypeHandle;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var addFourChunk = chunk.GetNativeArray(addFourTypeHandle);

            for (var i = 0; i < addFourChunk.Length; i++)
            {
                var addFourComp = addFourChunk[i];
                addFourChunk[i] = new AddFour { Value = (int)math.asin(math.exp(math.sin(math.sqrt(addFourComp.Value)) + 4)) };
            }
        }
    }

    protected override void OnCreate()
    {
        m_AddOneGroup = GetEntityQuery(ComponentType.ReadWrite<AddOne>());
        m_AddTwoGroup = GetEntityQuery(ComponentType.ReadWrite<AddTwo>());
        m_AddThreeGroup = GetEntityQuery(ComponentType.ReadWrite<AddThree>());
        m_AddFourGroup = GetEntityQuery(ComponentType.ReadWrite<AddFour>());
    }

    protected override void OnUpdate()
    {
        var addOneTypeHandle = GetComponentTypeHandle<AddOne>();
        var addTwoTypeHandle = GetComponentTypeHandle<AddTwo>();
        var addThreeTypeHandle = GetComponentTypeHandle<AddThree>();
        var addFourTypeHandle = GetComponentTypeHandle<AddFour>();

        var dependencies = new NativeArray<JobHandle>(4, Allocator.Temp);

        var addOneJob = new AddOneJob
        {
            addOneTypeHandle = addOneTypeHandle,
        };
        dependencies[0] = addOneJob.ScheduleParallel(m_AddOneGroup, Dependency);

        var addTwoJob = new AddTwoJob
        {
            addTwoTypeHandle = addTwoTypeHandle,
        };
        dependencies[1] = addTwoJob.ScheduleParallel(m_AddTwoGroup, Dependency);

        var addThreeJob = new AddThreeJob
        {
            addThreeTypeHandle = addThreeTypeHandle,
        };
        dependencies[2] = addThreeJob.ScheduleParallel(m_AddThreeGroup, Dependency);

        var addFourJob = new AddFourJob
        {
            addFourTypeHandle = addFourTypeHandle,
        };
        dependencies[3] = addFourJob.ScheduleParallel(m_AddFourGroup, Dependency);

        Dependency = JobHandle.CombineDependencies(dependencies);
    }
}