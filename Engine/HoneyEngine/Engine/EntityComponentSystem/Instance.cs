using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Components;


namespace ProjectLS.Engine.EntityComponentSystem;
/// <summary>
/// Holds data related to a entities instance used for determining an instances position for rendering and physics
/// </summary>
public class Instance
{
    List<Vector3> instancePositions = new List<Vector3>();
    
    public List<Vector3> InstancePositions { get => instancePositions; set => instancePositions = value; }
}