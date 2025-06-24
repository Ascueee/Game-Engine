using HoneyEngine.Engine;
using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Textures;
using ProjectLS.Engine.Shaders;

namespace ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;

public class Material : IComponent
{
    string name = "Material";
    private Shader shader;
    private TextureAtlas diffuseDiffuseTexture;
    private TextureAtlas normalMapTexture;
    private TextureAtlas displacementMapTexture;
    private Texture baseTexture;
    private SkyboxTexture skyboxTexure;
    private Vector3 shapeColor;

    public Material(string vertexShaderPath, string fragmentShaderPath, Vector3 shapeColor)
    {
        shader = new Shader(vertexShaderPath, fragmentShaderPath);
        this.shapeColor = shapeColor;
        
    }
    public Material(string vertexShaderPath, string fragmentShaderPath, TextureAtlas diffuseDiffuseTexture)
    {
        this.shader = new Shader(vertexShaderPath, fragmentShaderPath);
        this.diffuseDiffuseTexture = diffuseDiffuseTexture;
    }
    
    public Material(string vertexShaderPath, string fragmentShaderPath, Texture texture, Vector3 shapeColor)
    {
        this.shader = new Shader(vertexShaderPath, fragmentShaderPath);
        this.baseTexture = texture;
    }
    
    public Material(string vertexShaderPath, string fragmentShaderPath, TextureAtlas diffuseDiffuseTexture
        ,TextureAtlas normalMapTexture)
    {
        this.shader = new Shader(vertexShaderPath, fragmentShaderPath);
        this.diffuseDiffuseTexture = diffuseDiffuseTexture;
        this.normalMapTexture = normalMapTexture;
    }
    
    public Material(string vertexShaderPath, string fragmentShaderPath, TextureAtlas diffuseDiffuseTexture,
        Vector3 shapeColor)
    {
        this.shader = new Shader(vertexShaderPath, fragmentShaderPath);
        this.diffuseDiffuseTexture = diffuseDiffuseTexture;
        this.ShapeColor = shapeColor;
    }
    
    public Material(string vertexShaderPath, string fragmentShaderPath, TextureAtlas diffuseDiffuseTexture,
        TextureAtlas normalMapTexture, Vector3 shapeColor)
    {
        this.shader = new Shader(vertexShaderPath, fragmentShaderPath);
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
    public TextureAtlas DiffuseTexture { get => diffuseDiffuseTexture; set => diffuseDiffuseTexture = value; }
    public TextureAtlas NormalMapTexture { get => normalMapTexture; set => normalMapTexture = value; }
    public TextureAtlas DisplacementMapTexture { get => displacementMapTexture; set => displacementMapTexture = value; }
    public Texture BaseTexture { get => baseTexture; set => baseTexture = value; }
    public SkyboxTexture SkyBoxTexture { get => skyboxTexure; set => skyboxTexure = value; }
    public Vector3 ShapeColor { get => shapeColor; set => shapeColor = value; }
    public string Name { get => name; }

}