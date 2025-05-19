using OpenTK.Mathematics;

namespace ProjectLS.Engine.Math;

public class EngineMath
{
    public float Lerp(float p0, float p1, float t)
    {
        return (1 - t) * p0 + t * p1;
    }
}