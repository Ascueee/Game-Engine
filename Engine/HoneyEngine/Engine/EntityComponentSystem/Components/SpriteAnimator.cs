using OpenTK.Mathematics;

namespace ProjectLS.Engine.EntityComponentSystem.Components;

public class SpriteAnimator : IComponent
{
    string name = "SpriteAnimator";
    private float speed;
    private int spriteSize;
    private int frames;
    private Vector2 textureSize;
    Vector2 offSet;
    public SpriteAnimator(float speed, int spriteSize, Vector2 textureSize, int frames)
    {
        this.speed = speed;
        this.spriteSize = spriteSize;
        this.textureSize = textureSize;
        this.frames = frames;
    }

    public Vector2 UpdateAnimation(float time)
    {
        
        float uStep = spriteSize / textureSize.X;
        
        int currentFrame = (int)(time * speed) % frames;
        
        return offSet = new Vector2(currentFrame * uStep, 0);
        //return  offSet = new Vector2(speed * time, 0);
    }
    public string Name { get => name; set => name = value; }
}