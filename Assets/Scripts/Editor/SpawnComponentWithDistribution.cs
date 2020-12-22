using UnityEditor;
using UnityEngine;

public class SpawnGameObjectWithDistribution
{
    // The uniform distribution
    [MenuItem("SpawnGameObjectWithDistribution/SpwanWithUniformDistribution")]
    static void SpwanWithUniformDistribution()
    {
        for (int i = 0; i < 40000; ++i)
        {
            SpawnObject_UniformDistribution();
        }
    }

    static void SpawnObject_UniformDistribution()
    {
        var newObject = new GameObject();

        bool hasOne, hasTwo, hasThree, hasFour;
        while (true)
        {
            hasOne = Random.Range(0.0f, 1.0f) > 0.5f;
            hasTwo = Random.Range(0.0f, 1.0f) > 0.5f;
            hasThree = Random.Range(0.0f, 1.0f) > 0.5f;
            hasFour = Random.Range(0.0f, 1.0f) > 0.5f;

            if (hasOne | hasThree | hasThree | hasFour)
                break;
        }

        if (hasOne)
        {
            newObject.AddComponent<AddOne_Authoring>();
        }

        if (hasTwo)
        {
            newObject.AddComponent<AddTwo_Authoring>();
        }

        if (hasThree)
        {
            newObject.AddComponent<AddThree_Authoring>();
        }

        if (hasFour)
        {
            newObject.AddComponent<AddFour_Authoring>();
        }

        newObject.AddComponent<Unity.Entities.ConvertToEntity>();
    }

    // The "Pick on Component" distribution
    [MenuItem("SpawnGameObjectWithDistribution/SpawnWithPickOneComponentDistribution")]
    static void SpawnWithPickOneComponentDistribution()
    {
        for (int i = 0; i < 40000; ++i)
        {
            SpawnObject_PickOneComponentDistribution();
        }
    }

    static void SpawnObject_PickOneComponentDistribution()
    {
        var newObject = new GameObject();

        bool hasOne = false, hasTwo = false, hasThree = false, hasFour = false;
        {
            float rdm = Random.Range(0.0f, 4.0f);

            if (rdm < 1.0f)
            {
                hasOne = true;
            }
            else if (rdm < 2.0f)
            {
                hasTwo = true;
            }
            else if (rdm < 3.0f)
            {
                hasThree = true;
            }
            else if (rdm < 4.0f)
            {
                hasFour = true;
            }
        }

        if (hasOne)
        {
            newObject.AddComponent<AddOne_Authoring>();
        }

        if (hasTwo)
        {
            newObject.AddComponent<AddTwo_Authoring>();
        }

        if (hasThree)
        {
            newObject.AddComponent<AddThree_Authoring>();
        }

        if (hasFour)
        {
            newObject.AddComponent<AddFour_Authoring>();
        }

        newObject.AddComponent<Unity.Entities.ConvertToEntity>();
    }

    // The "Pick all Component" distribution
    [MenuItem("SpawnGameObjectWithDistribution/SpawnWithPickAllComponentDistribution")]
    static void SpawnWithPickAllComponentDistribution()
    {
        for (int i = 0; i < 40000; ++i)
        {
            SpawnObject_PickAllComponentDistribution();
        }
    }

    static void SpawnObject_PickAllComponentDistribution()
    {
        var newObject = new GameObject();

        newObject.AddComponent<AddOne_Authoring>();
        newObject.AddComponent<AddTwo_Authoring>();
        newObject.AddComponent<AddThree_Authoring>();
        newObject.AddComponent<AddFour_Authoring>();

        newObject.AddComponent<Unity.Entities.ConvertToEntity>();
    }
}