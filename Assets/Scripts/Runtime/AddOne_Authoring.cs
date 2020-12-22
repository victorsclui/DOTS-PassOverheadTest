using UnityEngine;
using Unity.Entities;

public class AddOne_Authoring : MonoBehaviour
{
}

public struct AddOne : IComponentData
{
    public int Value;
}

[ConverterVersion("test", 1)]
[UpdateInGroup(typeof(GameObjectAfterConversionGroup))]
public class AddOneConversionSystem : GameObjectConversionSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((AddOne_Authoring comp) =>
        {
            var entity = GetPrimaryEntity(comp);
            DstEntityManager.AddComponent<AddOne>(entity);

            if (!DstEntityManager.HasComponent<AddSomething>(entity))
            {
                DstEntityManager.AddComponent<AddSomething>(entity);
            }
        });
    }
}