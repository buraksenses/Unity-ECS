using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
public class Spawner : MonoBehaviour
{
    [SerializeField] private Material unitMaterial;
    [SerializeField] private Mesh unitMesh;
    
    void Start()
    {
        MakeEntitiy();
    }

    private void MakeEntitiy()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype
        (
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld)
        );

        Entity myEntity = entityManager.CreateEntity(entityArchetype);

        entityManager.AddComponentData(myEntity, new Translation
        {
            Value = new float3(1,2,3)
        });

        entityManager.AddSharedComponentData(myEntity, new RenderMesh
        {
            material = unitMaterial,
            mesh = unitMesh
        });
    }
    
}
