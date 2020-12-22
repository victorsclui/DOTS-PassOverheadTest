using UnityEngine;
using Unity.Entities;

public class AddFour_Authoring : MonoBehaviour
{
}

public struct AddFour : IComponentData
{
    public int Value;
}

[ConverterVersion("test", 1)]
[UpdateInGroup(typeof(GameObjectAfterConversionGroup))]
public class AddFourConversionSystem : GameObjectConversionSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((AddFour_Authoring comp) =>
        {
            var entity = GetPrimaryEntity(comp);
            DstEntityManager.AddComponent<AddFour>(entity);

            if (!DstEntityManager.HasComponent<AddSomething>(entity))
            {
                DstEntityManager.AddComponent<AddSomething>(entity);
            }
        });
    }
}