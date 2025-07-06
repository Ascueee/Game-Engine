using HoneyEngine.Engine;
using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Textures;
using ProjectLS.Engine.Shaders;

namespace ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;

public class Material : IComponent
{
    string name = "Material";
    private Shader shader;
    private Shader shaderTwo;
    private TextureAtlas diffuseDiffuseTexture;
    private TextureAtlas normalMapTexture;
    private TextureAtlas displacementMapTexture;
    private Texture baseTexture;
    private SkyboxTexture skyboxTexure;
    private Vector3 shapeColor;

    public Material(Shader shader, Vector3 shapeColor)
    {
        this.shader = shader;
        this.shapeColor = shapeColor;
        
    }
    public Material(Shader shader, TextureAtlas diffuseDiffuseTexture)
    {
        this.shader = shader;
        this.diffuseDiffuseTexture = diffuseDiffuseTexture;
    }
    
    public Material(Shader shader, Texture texture, Vector3 shapeColor)
    {
        this.shader = shader;
        this.baseTexture = texture;
    }
    
    public Material(Shader shader, TextureAtlas diffuseDiffuseTexture
        ,TextureAtlas normalMapTexture)
    {
        this.shader = shader;
        this.diffuseDiffuseTexture = diffuseDiffuseTexture;
        this.normalMapTexture = normalMapTexture;
    }
    
    public Material(Shader shader, TextureAtlas diffuseDiffuseTexture,
        Vector3 shapeColor)
    {
        this.shader = shader;
        this.diffuseDiffuseTexture = diffuseDiffuseTexture;
        this.ShapeColor = shapeColor;
    }
    
    public Material(Shader shader, TextureAtlas diffuseDiffuseTexture,
        TextureAtlas normalMapTexture, Vector3 shapeColor)
    {
        this.shader = shader;
        this.diffuseDiffuseTexture = diffuseDiffuseTexture;
        this.normalMapTexture = normalMapTexture;
        this.ShapeColor = shapeColor;
    }
    
    public Material(Shader shader, Shader shaderTwo, TextureAtlas diffuseDiffuseTexture,
        TextureAtlas normalMapTexture, Vector3 shapeColor)
    {
        this.shader = shader;
        this.shaderTwo = shaderTwo;
        this.diffuseDiffuseTexture = diffuseDiffuseTexture;
        this.normalMapTexture = normalMapTexture;
        this.ShapeColor = shapeColor;
    }
    

    public Material(string vertexShaderPath, string fragmentShaderPath, SkyboxTexture skyboxTexture)
    {
        this.shader = new Shader(vertexShaderPath, fragmentShaderPath);
        this.skyboxTexure = skyboxTexture;
    }
    
    public Shader Shader { get => shader; set => shader = value; }
    public Shader ShaderTwo { get => shaderTwo; set => shaderTwo = value; }
    public TextureAtlas DiffuseTexture { get => diffuseDiffuseTexture; set => diffuseDiffuseTexture = value; }
    public TextureAtlas NormalMapTexture { get => normalMapTexture; set => normalMapTexture = value; }
    public TextureAtlas DisplacementMapTexture { get => displacementMapTexture; set => displacementMapTexture = value; }
    public Texture BaseTexture { get => baseTexture; set => baseTexture = value; }
    public SkyboxTexture SkyBoxTexture { get => skyboxTexure; set => skyboxTexure = value; }
    public Vector3 ShapeColor { get => shapeColor; set => shapeColor = value; }
    public string Name { get => name; }

}