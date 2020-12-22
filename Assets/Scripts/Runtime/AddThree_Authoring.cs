using UnityEngine;
using Unity.Entities;

public class AddThree_Authoring : MonoBehaviour
{
}

public struct AddThree : IComponentData
{
    public int Value;
}

[ConverterVersion("test", 1)]
[UpdateInGroup(typeof(GameObjectAfterConversionGroup))]
public class AddThreeConversionSystem : GameObjectConversionSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((AddThree_Authoring comp) =>
        {
            var entity = GetPrimaryEntity(comp);
            DstEntityManager.AddComponent<AddThree>(entity);

            if (!DstEntityManager.HasComponent<AddSomething>(entity))
            {
                DstEntityManager.AddComponent<AddSomething>(entity);
            }
        });
    }
}