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
            Value = float3.zero
        });

        //Use the statement below when not using URP or HDRP
        
        // entityManager.AddSharedComponentData(myEntity, new RenderMesh
        // {
        //     material = unitMaterial,
        //     mesh = unitMesh
        // });
        
        //Use statement below when using URP or HDRP
        
        RenderMeshUtility.AddComponents(myEntity,entityManager,new RenderMeshDescription(unitMesh,unitMaterial));
        
    }
    
}
