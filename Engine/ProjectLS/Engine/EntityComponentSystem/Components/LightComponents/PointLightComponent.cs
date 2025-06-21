using OpenTK.Mathematics;

namespace ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;

public class PointLightComponent : IComponent
{
    string name = "PointLight";
    Vector3 lightColor;
    Vector3 lightAmbient;
    Vector3 lightSpecular;
    Vector3 lightDiffuse;
    
    float constant;
    float linear;
    float quadratic; 
    
    public PointLightComponent(Vector3 lightColor, Vector3 lightAmbient, Vector3 lightDiffuse, Vector3 lightSpecular,
        float constant, float linear, float quadratic)
    {
        this.lightColor = lightColor;
        this.lightAmbient = lightAmbient;
        this.lightDiffuse = lightDiffuse;
        this.lightSpecular = lightSpecular;
        
        this.constant = constant;
        this.linear = linear;
        this.quadratic = quadratic;
    }
    
    public Vector3 LightColor { get => lightColor; set => lightColor = value; }
    public Vector3 LightAmbient { get => lightAmbient; set => lightAmbient = value; }
    public Vector3 LightDiffuse { get => lightDiffuse; set => lightDiffuse = value; }
    public Vector3 LightSpecular { get => lightSpecular; set => lightSpecular = value; }
    public float Constant {get => constant; set => constant = value; }
    public float Linear {get => linear; set => linear = value; }
    public float Quadratic {get => quadratic; set => quadratic = value; }
    public string Name => name;
}