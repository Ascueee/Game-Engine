using OpenTK.Mathematics;

namespace ProjectLS.Engine.Math;
public class PerlinNoise
{ 
    private readonly int[] permutation;
    EngineMath math = new EngineMath();

    public PerlinNoise(int seed)
    {
        var random = new Random(seed);
        permutation = new int[512];
        int[] p = new int[256];

        for (int i = 0; i < 256; i++)
            p[i] = i;

        for (int i = 255; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (p[i], p[j]) = (p[j], p[i]);
        }
        
        for (int i = 0; i < 512; i++)
            permutation[i] = p[i % 256];
    }

    public float Noise(float x, float y)
    {
        int xi = (int)MathF.Floor(x) & 255;
        int yi = (int)MathF.Floor(y) & 255;

        float xf = x - MathF.Floor(x);
        float yf = y - MathF.Floor(y);

        float u = Fade(xf);
        float v = Fade(yf);

        int aa = permutation[permutation[xi] + yi];
        int ab = permutation[permutation[xi] + yi + 1];
        int ba = permutation[permutation[xi + 1] + yi];
        int bb = permutation[permutation[xi + 1] + yi + 1];

        float x1, x2;
        x1 = math.Lerp(Grad(aa, xf, yf), Grad(ba, xf - 1, yf), u);
        x2 = math.Lerp(Grad(ab, xf, yf - 1), Grad(bb, xf - 1, yf - 1), u);

        return (math.Lerp(x1, x2, v) + 1) / 2; 
    }
    
    public float OctaveNoise(float x, float y, int octaves, float persistence = 0.4f, float lacunarity = 2f)
    {
        float total = 0f;
        float frequency = 0.2f;
        float amplitude = 1f;
        float maxValue = 0f; 

        for (int i = 0; i < octaves; i++)
        {
            total += Noise(x * frequency, y * frequency) * amplitude;

            maxValue += amplitude;

            amplitude *= persistence;
            frequency *= lacunarity;
        }

        return total / maxValue;
    }
    
    private float Fade(float t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }
    
    private float Grad(int hash, float x, float y)
    {
        int h = hash & 7;
        float u = h < 4 ? x : y;
        float v = h < 4 ? y : x;

        return ((h & 1) == 0 ? u : -u) +
               ((h & 2) == 0 ? v : -v);
    }
}