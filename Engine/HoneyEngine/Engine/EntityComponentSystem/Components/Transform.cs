using OpenTK.Mathematics;

namespace ProjectLS.Engine.EntityComponentSystem.Components;

public class Transform : IComponent
{
    string name = "Transform";
    Vector3 position;
    Vector3 rotation;
    Vector3 scale;
    private Matrix4 model;

    public Transform(Vector3 position, Vector3 rotation, Vector3 scale)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        
        Matrix4 rotationMatrix = Matrix4.CreateRotationX(rotation.X) * Matrix4.CreateRotationY(rotation.Y) 
                                                                     * Matrix4.CreateRotationZ(rotation.Z);
        model = Matrix4.CreateTranslation(this.position) * rotationMatrix * Matrix4.CreateScale(this.scale);
    }
    
    public Transform()
    {
        this.position = Vector3.Zero;
        this.rotation = Vector3.Zero;
        this.scale = Vector3.One;
    }
    
    public Matrix4 UpdateModel()
    {
        Matrix4 rotationMatrix = Matrix4.CreateRotationX(rotation.X) * Matrix4.CreateRotationY(rotation.Y) 
                                                                     * Matrix4.CreateRotationZ(rotation.Z);
        return model = Matrix4.CreateTranslation(position) * rotationMatrix * Matrix4.CreateScale(scale);
    }
    



    public Matrix4 GetModelMatrix()
    {
        return Matrix4.Identity;
    }
    
    public Vector3 Position { get => position; set => position = value; }
    public Vector3 Rotation { get => rotation; set => rotation = value; }
    public Vector3 Scale { get => scale; set => scale = value; }
    public Matrix4 Model { get => model; set => model = value; }
    public string Name { get => name; set => name = value; }
}