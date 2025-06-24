using HoneyEngine.Engine;
using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Components;
using ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;
using ProjectLS.Engine.EntityComponentSystem.Textures;

namespace ProjectLS.LootNShoot;

/// <summary>
/// Holds a registry of all the entities that are going to be used in the game
/// INCLUDES:
///     THE SCENE <-- Entities get fed into it to get sorted
/// </summary>
public class WorldEntities
{
    private Scene worldScene;
    
    //TEXTURES
    private static TextureAtlas spriteSheet = new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\sprite_sheet.png",
        16, 16);
    private static TextureAtlas HDSprite = new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\HDDiffuse.png",
        1024, 1024);
    private static TextureAtlas HDSpriteNormal = new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\HDNormal.png",
        1024, 1024);
    
    SkyboxTexture skybox = new SkyboxTexture(new string[]
    {
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\SkyboxTextures\\px.png", 
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\SkyboxTextures\\nx.png", 
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\SkyboxTextures\\py.png", 
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\SkyboxTextures\\nz.png", 
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\SkyboxTextures\\pz.png", 
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\SkyboxTextures\\nz.png"  
    });

    private static TextureAtlas modelDiffuse = new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\ModelDiffuse.png"
        , 512, 512);
    private static TextureAtlas modelNormal =
        new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\ModelNormal.png",
            512, 512);

    private static TextureAtlas animationTestSprites = 
        new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\Owlet_Monster_Attack1_4.png", 
        32, 32);
    
    
    
    //TRANSFORMS
    private Transform cubeTransform = new Transform(Vector3.Zero, Vector3.Zero,
        new Vector3(2f, 2f, 2f));
    
    private Transform modelTransform = new Transform(new Vector3(3, 1, 3), Vector3.Zero,
        new Vector3(0.1f, 0.1f, 0.1f));
    
    private Transform primitiveCubeTransform = new Transform(Vector3.Zero, Vector3.Zero,
        new Vector3(100f, 2f, 100f));
    
    private Transform quadTransform = new Transform(new Vector3(3, 1, 3), Vector3.Zero,
        new Vector3(12f, 12f, 12f));

    private Transform DirectionalLightTransform = new Transform(new Vector3(0, 1, 0),
        Vector3.Zero, new Vector3(10f, 10f, 10f));
    
    private Transform pointLightOneTransform = new Transform(new Vector3(50, 4, 0),
        Vector3.Zero, Vector3.One);
    
    private Transform pointLightTwoTransform = new Transform(new Vector3(0, 25, 20),
        Vector3.Zero, Vector3.One);
    
    //MATERIALS
    private Material cubeMaterial = new Material(
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\Lighting.vert",
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\Lighting.frag",
        spriteSheet, new Vector3(1f, 1f, 1f));
    
    private Material primitiveCubeMaterial = new Material(
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\Lighting.vert",
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\Lighting.frag",
        HDSprite, HDSpriteNormal, new Vector3(1f, 1f, 1f));

    private Material modelMaterial = new Material(
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\Lighting.vert",
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\Lighting.frag",
        modelDiffuse, modelNormal, new Vector3(1f, 1f, 1f));
    
    Material animationTestMaterial = new Material("D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\quadShader.vert",
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\quadShader.frag", animationTestSprites
        , new Vector3(1f, 1f, 1f));
    public WorldEntities(Scene worldScene)
    {
        this.worldScene = worldScene;
    }
    
    public void InitializeEntities()
    {
        worldScene.CreateEntity(0, "Air Block");
        
        worldScene.CreateCube(1, "Grass Block", 16,16, 34,16,
            cubeTransform, cubeMaterial);
        worldScene.CreateCube(2, "Dirt Block", 0, 0,
            cubeTransform, cubeMaterial);
        worldScene.CreateCube(3, "Stone Block", 16,0, cubeTransform, cubeMaterial);
        
        worldScene.CreateCube(4, "CobbleStone Block",0,16, cubeTransform, cubeMaterial);
        
        worldScene.CreateSkyBox(5, "SkyBox", skybox);
        
        worldScene.CreateDirectionalLight(6, "Directional Light", DirectionalLightTransform, 
            new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.25f, 0.25f, 0.25f), new Vector3(0.25f, 0.25f, 0.25f),
            new Vector3(0.2f, 0.2f, 0.2f));
        
        worldScene.CreatePointLight(7, "Point Light One",spriteSheet, 0, 0,
            pointLightOneTransform, new Vector3(0f, 1f, 0f), new Vector3(0.5f, 0.5f, 0.5f), 
            new Vector3(1f, 1f, 1f), new Vector3(1f, 0.5f, 0f),
            1.0f,  0.02f, 0.001f);
        
        worldScene.CreatePointLight(8, "Point Light Two",spriteSheet, 0, 0,
            pointLightTwoTransform, new Vector3(1f, 1f, 1f), new Vector3(1f, 1f,1f), 
            new Vector3(1f, 0f, 0f), new Vector3(1f, 0.5f, 0f),
            1.0f,  0.02f, 0.001f);
        
        worldScene.CreatePrimitiveCube(9, "Primitive Cube", 0, 0,
            primitiveCubeTransform, primitiveCubeMaterial);
        
        worldScene.CreateModel(10,"Evelyn Model",
            "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\3D Models\\Evelynn Tango.obj",
             0,0, modelTransform, modelMaterial);
        
        worldScene.CreateQuad(11, "Pokemon", 0,0, quadTransform, animationTestMaterial, false);
        
    }
   
    public TextureAtlas SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
    public TextureAtlas HDDiffuse {get => HDSprite; set => HDSprite = value; }
    public TextureAtlas HDNormal {get => HDSpriteNormal; set => HDSpriteNormal = value; }
    public SkyboxTexture SkyBoxTexture { get => skybox; set => skybox = value; }
    public TextureAtlas ModelDiffuse { get => modelDiffuse; set => modelDiffuse = value; }
    public TextureAtlas ModelNormal { get => modelNormal; set => modelNormal = value; }
    public TextureAtlas AnimationTest { get => animationTestSprites; set => animationTestSprites = value; }

}