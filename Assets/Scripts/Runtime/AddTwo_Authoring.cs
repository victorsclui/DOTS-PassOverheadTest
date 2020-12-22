using UnityEngine;
using Unity.Entities;

public class AddTwo_Authoring : MonoBehaviour
{
}

public struct AddTwo : IComponentData
{
    public int Value;
}

[ConverterVersion("test", 1)]
[UpdateInGroup(typeof(GameObjectAfterConversionGroup))]
public class AddTwoConversionSystem : GameObjectConversionSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((AddTwo_Authoring comp) =>
        {
            var entity = GetPrimaryEntity(comp);
            DstEntityManager.AddComponent<AddTwo>(entity);

            if (!DstEntityManager.HasComponent<AddSomething>(entity))
            {
                DstEntityManager.AddComponent<AddSomething>(entity);
            }
        });
    }
}