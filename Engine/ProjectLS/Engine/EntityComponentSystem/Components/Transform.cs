using OpenTK.Mathematics;

namespace ProjectLS.Engine.EntityComponentSystem.Components;

public class Transform : IComponent
{
    string name = "Transform";
    Vector3 position;
    Vector3 rotation;
    Vector3 scale;

    public Transform(Vector3 position, Vector3 rotation, Vector3 scale)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }
    
    public Transform(Vector3 position)
    {
        this.position = position;
    }
    

    public Transform()
    {
        this.position = Vector3.Zero;
        this.rotation = Vector3.Zero;
        this.scale = Vector3.One;
    }

    public Matrix4 GetModelMatrix()
    {
        Matrix4 rotationMatrix = Matrix4.CreateRotationX(rotation.X) * Matrix4.CreateRotationY(rotation.Y) 
                                                                     * Matrix4.CreateRotationZ(rotation.Z);
        
        return Matrix4.CreateTranslation(position) *  rotationMatrix *Matrix4.CreateScale(scale);
    }
    
    public Vector3 Position { get => position; set => position = value; }
    public Vector3 Rotation { get => rotation; set => rotation = value; }
    public Vector3 Scale { get => scale; set => scale = value; }
    public string Name { get => name; set => name = value; }
}