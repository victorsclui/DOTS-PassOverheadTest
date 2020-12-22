using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

public class SinglePassSystem : SystemBase
{
    EntityQuery m_AddSomethingGroup;

    [BurstCompile]
    struct AddAllJob : IJobChunk
    {
        public ComponentTypeHandle<AddOne> addOneTypeHandle;
        public ComponentTypeHandle<AddTwo> addTwoTypeHandle;
        public ComponentTypeHandle<AddThree> addThreeTypeHandle;
        public ComponentTypeHandle<AddFour> addFourTypeHandle;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            if (chunk.Has(addOneTypeHandle))
            {
                var addOneChunk = chunk.GetNativeArray(addOneTypeHandle);

                for (var i = 0; i < addOneChunk.Length; i++)
                {
                    var addOneComp = addOneChunk[i];
                    addOneChunk[i] = new AddOne { Value = (int)math.asin(math.exp(math.sin(math.sqrt(addOneComp.Value)) + 1)) };
                }
            }

            if (chunk.Has(addTwoTypeHandle))
            {
                var addTwoChunk = chunk.GetNativeArray(addTwoTypeHandle);

                for (var i = 0; i < addTwoChunk.Length; i++)
                {
                    var addTwoComp = addTwoChunk[i];
                    addTwoChunk[i] = new AddTwo { Value = (int)math.asin(math.exp(math.sin(math.sqrt(addTwoComp.Value)) + 2)) };
                }
            }

            if (chunk.Has(addThreeTypeHandle))
            {
                var addThreeChunk = chunk.GetNativeArray(addThreeTypeHandle);

                for (var i = 0; i < addThreeChunk.Length; i++)
                {
                    var addThreeComp = addThreeChunk[i];
                    addThreeChunk[i] = new AddThree { Value = (int)math.asin(math.exp(math.sin(math.sqrt(addThreeComp.Value)) + 3)) };
                }
            }

            if (chunk.Has(addFourTypeHandle))
            {
                var addFourChunk = chunk.GetNativeArray(addFourTypeHandle);

                for (var i = 0; i < addFourChunk.Length; i++)
                {
                    var addFourComp = addFourChunk[i];
                    addFourChunk[i] = new AddFour { Value = (int)math.asin(math.exp(math.sin(math.sqrt(addFourComp.Value)) + 4)) };
                }
            }
        }
    }

    protected override void OnCreate()
    {
        m_AddSomethingGroup = GetEntityQuery(ComponentType.ReadWrite<AddSomething>());
    }

    protected override void OnUpdate()
    {
        var addOneTypeHandle = GetComponentTypeHandle<AddOne>();
        var addTwoTypeHandle = GetComponentTypeHandle<AddTwo>();
        var addThreeTypeHandle = GetComponentTypeHandle<AddThree>();
        var addFourTypeHandle = GetComponentTypeHandle<AddFour>();

        var addAllJob = new AddAllJob
        {
            addOneTypeHandle = addOneTypeHandle,
            addTwoTypeHandle = addTwoTypeHandle,
            addThreeTypeHandle = addThreeTypeHandle,
            addFourTypeHandle = addFourTypeHandle
        };

        Dependency = addAllJob.ScheduleParallel(m_AddSomethingGroup, Dependency);
    }
}