using HoneyEngine.Engine;
using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Components;
using ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;
using ProjectLS.Engine.EntityComponentSystem.Textures;

namespace ProjectLS.LootNShoot;

/// <summary>
/// Holds a registry of all the entities that are going to be used in the game
/// INCLUDES:
///     BLOCKS
///     ITEMS
///     THE SCENE <-- Entities get fed into it to get sorted
/// </summary>
public class WorldEntities
{
    private Scene worldScene;
    
    //TEXTURES
    private static TextureAtlas spriteSheet = new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\sprite_sheet.png",
        16, 16);
    private static TextureAtlas HDSprite = new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\HDDiffuse.png",
        1024, 1024);
    private static TextureAtlas HDSpriteNormal = new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\HDNormal.png",
        1024, 1024);
    
    SkyboxTexture skybox = new SkyboxTexture(new string[]
    {
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\SkyboxTextures\\px.png", 
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\SkyboxTextures\\nx.png", 
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\SkyboxTextures\\py.png", 
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\SkyboxTextures\\nz.png", 
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\SkyboxTextures\\pz.png", 
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\SkyboxTextures\\nz.png"  
    });

    private static TextureAtlas modelDiffuse = new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\ModelDiffuse.png"
        , 512, 512);
    private static TextureAtlas modelNormal =
        new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\ModelNormal.png",
            512, 512);
    private static TextureAtlas quadTexture = new TextureAtlas(
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\grass.png",
        512, 512);
    
    
    //TRANSFORMS
    private Transform cubeTransform = new Transform(Vector3.Zero, Vector3.Zero,
        new Vector3(2f, 2f, 2f));
    
    private Transform modelTransform = new Transform(new Vector3(0, 1, 0), Vector3.Zero,
        new Vector3(0.25f, 0.25f, 0.25f));
    
    private Transform primitiveCubeTransform = new Transform(Vector3.Zero, Vector3.Zero,
        new Vector3(100f, 2f, 100f));
    
    private Transform quadTransform = new Transform(new Vector3(0, 0, 0), Vector3.Zero,
        new Vector3(12f, 12f, 12f));

    private Transform DirectionalLightTransform = new Transform(new Vector3(0, 1, 0),
        Vector3.Zero, new Vector3(10f, 10f, 10f));
    
    private Transform pointLightOneTransform = new Transform(new Vector3(50, 4, 0),
        Vector3.Zero, Vector3.One);
    
    private Transform pointLightTwoTransform = new Transform(new Vector3(0, 25, 20),
        Vector3.Zero, Vector3.One);
    
    //MATERIALS
    private Material cubeMaterial = new Material(
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\Lighting.vert",
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\Lighting.frag",
        spriteSheet, new Vector3(1f, 1f, 1f));
    
    private Material quadMaterial = new Material(
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\baseShader.vert",
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\baseShader.frag",
        quadTexture, new Vector3(1f, 1f, 1f));

    private Material primitiveCubeMaterial = new Material(
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\Lighting.vert",
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\Lighting.frag",
        HDSprite, HDSpriteNormal, new Vector3(1f, 1f, 1f));

    private Material modelMaterial = new Material(
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\Lighting.vert",
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\Lighting.frag",
        modelDiffuse, modelNormal, new Vector3(1f, 1f, 1f));
    public WorldEntities(Scene worldScene)
    {
        this.worldScene = worldScene;
    }
    
    //Creates all the blocks for the scene to use
    public void InitializeBlocks()
    {
        worldScene.CreateEntity(0, "Air Block");
        
        worldScene.CreateCube(1, "Grass Block", 16,16, 34,16,
            cubeTransform, cubeMaterial);
        worldScene.CreateCube(2, "Dirt Block", 0, 0,
            cubeTransform, cubeMaterial);
        worldScene.CreateCube(3, "Stone Block", 16,0, cubeTransform, cubeMaterial);
        
        worldScene.CreateCube(4, "CobbleStone Block",0,16, cubeTransform, cubeMaterial);
        
        worldScene.CreateSkyBox(5, "SkyBox", skybox);
        
        worldScene.CreateDirectionalLight(6, "LightOne", DirectionalLightTransform, 
            new Vector3(1f, 1f, 1f), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1f, 1f, 1f),
            new Vector3(0.2f, 0.2f, 0.2f));
        
        worldScene.CreatePointLight(7, "LightTwo",spriteSheet, 0, 0,
            pointLightOneTransform, new Vector3(0f, 1f, 0f), new Vector3(0.5f, 0.5f, 0.5f), 
            new Vector3(1f, 1f, 1f), new Vector3(1f, 0.5f, 0f),
            1.0f,  0.02f, 0.001f);
        
        worldScene.CreatePointLight(8, "LightThree",spriteSheet, 0, 0,
            pointLightTwoTransform, new Vector3(1f, 0f, 0f), new Vector3(1f, 1f,1f), 
            new Vector3(1f, 1f, 1f), new Vector3(1f, 0.5f, 0f),
            1.0f,  0.02f, 0.001f);
        
        worldScene.CreatePrimitiveCube(9, "Primitive Cube", 1024, 0,
            primitiveCubeTransform, primitiveCubeMaterial);
        
        worldScene.CreateModel(10,"Test Model",
            "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\TestProject\\Assets\\3D Models\\Evelynn Tango.obj",
             0,0, modelTransform, modelMaterial);
        
        worldScene.CreateQuad(11, "Grass",0,0, quadTransform, quadMaterial);
    }
    
    public TextureAtlas SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
    public TextureAtlas HDDiffuse {get => HDSprite; set => HDSprite = value; }
    public TextureAtlas HDNormal {get => HDSpriteNormal; set => HDSpriteNormal = value; }
    public TextureAtlas QuadTexture {get => quadTexture; set => quadTexture = value; }
    public SkyboxTexture SkyBoxTexture { get => skybox; set => skybox = value; }
    
    public TextureAtlas ModelDiffuse { get => modelDiffuse; set => modelDiffuse = value; }
    public TextureAtlas ModelNormal { get => modelNormal; set => modelNormal = value; }
}