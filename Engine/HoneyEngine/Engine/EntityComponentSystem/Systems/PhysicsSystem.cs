using System;
using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Components;
using ProjectLS.Engine.EntityComponentSystem.Components.PhysicsComponents;

namespace ProjectLS.Engine.EntityComponentSystem.Systems;

public class PhysicsSystem
{
    List<Entity> physicsEntities = new List<Entity>();
    static float gravity = -9f;
    Vector3 gravityVector = new Vector3(0.0f, gravity, 0.0f);

    Vector3 testVelocity = Vector3.Zero;

    // Fixed timestep parameters
    private const float fixedDelta = 1.0f / 30.0f; // 60 FPS physics step
    private float accumulator = 0f;

    // This method should be called every frame with the variable deltaTime
    public void Update(float deltaTime)
    {
        accumulator += deltaTime;

        while (accumulator >= fixedDelta)
        {
            PhysicsLoop(fixedDelta);
            accumulator -= fixedDelta;
        }
    }

    private void PhysicsLoop(float deltaTime)
    {
        foreach (var entity in physicsEntities)
        {
            BoxCollider boxCollider = (BoxCollider)entity.GetComponent("BoxCollider");
            Transform transform = (Transform)entity.GetComponent("Transform");

            if (boxCollider.IsStatic)
                continue;

            // Apply gravity and predict movement
            testVelocity += gravityVector * deltaTime;
            Vector3 proposedMove = testVelocity * deltaTime;
            transform.Position += proposedMove;

            boxCollider.UpdateColliders(transform.Position);
            entity.EntityInstance.InstancePositions[0] = transform.Position;

            foreach (Entity other in physicsEntities)
            {
                if (other == entity) continue;

                BoxCollider otherCollider = (BoxCollider)other.GetComponent("BoxCollider");
                if (!otherCollider.IsStatic) continue;

                if (Collides(boxCollider, otherCollider))
                {
                    Vector3 penetration = GetPenetrationDepth(boxCollider, otherCollider);
                    Vector3 correction = MinimalTranslationVector(penetration);
                    
                    if (MathF.Abs(correction.X) < 0.001f) correction.X = 0;
                    if (MathF.Abs(correction.Y) < 0.001f) correction.Y = 0;
                    if (MathF.Abs(correction.Z) < 0.001f) correction.Z = 0;


                    Vector3 maxCorrection = new Vector3(0.5f);
                    correction = Vector3.Clamp(correction, -maxCorrection, maxCorrection);


                    transform.Position += correction;

                    if (correction.X != 0) testVelocity.X = 0;
                    if (correction.Y != 0) testVelocity.Y = 0;
                    if (correction.Z != 0) testVelocity.Z = 0;

                    boxCollider.UpdateColliders(transform.Position);
                    entity.EntityInstance.InstancePositions[0] = transform.Position;
                }
            }
        }
    }

    public bool Collides(BoxCollider a, BoxCollider b)
    {
        return !(a.Max.X < b.Min.X || a.Min.X > b.Max.X ||
                 a.Max.Y < b.Min.Y || a.Min.Y > b.Max.Y ||
                 a.Max.Z < b.Min.Z || a.Min.Z > b.Max.Z);
    }

    private Vector3 GetPenetrationDepth(BoxCollider a, BoxCollider b)
    {
        float xOverlap = MathF.Min(a.Max.X, b.Max.X) - MathF.Max(a.Min.X, b.Min.X);
        float yOverlap = MathF.Min(a.Max.Y, b.Max.Y) - MathF.Max(a.Min.Y, b.Min.Y);
        float zOverlap = MathF.Min(a.Max.Z, b.Max.Z) - MathF.Max(a.Min.Z, b.Min.Z);

        Vector3 aCenter = (a.Min + a.Max) * 0.5f;
        Vector3 bCenter = (b.Min + b.Max) * 0.5f;

        float xSign = (aCenter.X < bCenter.X) ? -1 : 1;
        float ySign = (aCenter.Y < bCenter.Y) ? -1 : 1;
        float zSign = (aCenter.Z < bCenter.Z) ? -1 : 1;

        return new Vector3(xOverlap * xSign, yOverlap * ySign, zOverlap * zSign);
    }

    private Vector3 MinimalTranslationVector(Vector3 penetration)
    {
        float absX = MathF.Abs(penetration.X);
        float absY = MathF.Abs(penetration.Y);
        float absZ = MathF.Abs(penetration.Z);

        if (absX <= absY && absX <= absZ)
            return new Vector3(penetration.X, 0, 0);
        else if (absY <= absZ)
            return new Vector3(0, penetration.Y, 0);
        else
            return new Vector3(0, 0, penetration.Z);
    }

    public List<Entity> PhysicsEntities { get => physicsEntities; set => physicsEntities = value; }
}
