using OpenTK.Mathematics;

namespace ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;

public class DirectionalLightComponent : IComponent
{
    string name = "DirectionalLight";
    Vector3 lightColor;
    Vector3 lightAmbient;
    Vector3 lightSpecular;
    Vector3 lightDiffuse;

    public DirectionalLightComponent(Vector3 lightColor, Vector3 lightAmbient, Vector3 lightDiffuse, Vector3 lightSpecular)
    {
        this.lightColor = lightColor;
        this.lightAmbient = lightAmbient;
        this.lightDiffuse = lightDiffuse;
        this.lightSpecular = lightSpecular;
    }
    public Vector3 LightColor { get => lightColor; set => lightColor = value; }
    public Vector3 LightAmbient { get => lightAmbient; set => lightAmbient = value; }
    public Vector3 LightDiffuse { get => lightDiffuse; set => lightDiffuse = value; }
    public Vector3 LightSpecular { get => lightSpecular; set => lightSpecular = value; }
    public string Name => name;
}