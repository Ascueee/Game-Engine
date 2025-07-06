using OpenTK.Mathematics;

namespace ProjectLS.Engine.EntityComponentSystem.Components.PhysicsComponents;

public class BoxCollider : IComponent
{
    string name = "BoxCollider";
    Vector3 max;
    Vector3 min;
    Vector3 colliderSizeMax;
    Vector3 colliderSizeMin;
    Vector3 localOffset;
    bool isStatic;
    
    public BoxCollider(Vector3 center,  bool isStatic)
    {
        colliderSizeMax = new Vector3(0.5f);
        colliderSizeMin = new Vector3(0.5f);
        max = center + colliderSizeMax;
        min = center - colliderSizeMin;
        
        this.isStatic = isStatic;
    }
    
    public BoxCollider(Vector3 center,Vector3 colliderSizeMax, Vector3 colliderSizeMin, Vector3 localOffset ,bool isStatic)
    {
        
        this.colliderSizeMax = colliderSizeMax;
        this.colliderSizeMin = colliderSizeMin;
        this.localOffset = localOffset;
        center = center + localOffset;
        this.max = center + colliderSizeMax;
        this.min = center - colliderSizeMin;
        
        this.isStatic = isStatic;
    }
    
    public void UpdateColliders(Vector3 newCenter)
    {
        newCenter = newCenter + localOffset; 
        max = newCenter + colliderSizeMax;
        min = newCenter - colliderSizeMin;
    }
    
    public string Name { get => name; set => name = value; }
    public Vector3 Max { get => max; set => max = value; }
    public Vector3 Min { get => min; set => min = value; }
    public bool IsStatic { get => isStatic; set => isStatic = value; }
}