using OpenTK.Mathematics;

namespace ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;

public class LightingObject : IComponent
{
    string name = "LightingObject";
    private Vector3 lightColor;
    private Vector3 lightAmbient;
    Vector3 lightDiffuse;

    public LightingObject(Vector3 lightColor, Vector3 lightAmbient, Vector3 lightDiffuse)
    {
        this.lightColor = lightColor;
        this.lightAmbient = lightAmbient;
        this.lightDiffuse = lightDiffuse;
    }
    public Vector3 LightColor { get => lightColor; set => lightColor = value; }
    public Vector3 LightAmbient { get => lightAmbient; set => lightAmbient = value; }
    public Vector3 LightDiffuse { get => lightDiffuse; set => lightDiffuse = value; }
    public string Name => name;
}