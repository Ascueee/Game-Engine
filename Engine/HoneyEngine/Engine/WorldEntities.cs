using HoneyEngine.Engine;
using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Components;
using ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;
using ProjectLS.Engine.EntityComponentSystem.Textures;
using ProjectLS.Engine.Shaders;

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
    
    //SHADERS
    private static Shader basicLightingShader = new Shader(
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\Lighting.vert",
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\Lighting.frag");
    
    private static Shader debugShader = new Shader(
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\DebugShape.vert",
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\DebugShape.frag",
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\DebugShape.geom");
    
    private static Shader quadShader = new Shader(
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\quadShader.vert",
        "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\Engine\\Shaders\\quadShader.frag");
    
    //TRANSFORMS
    private Transform modelTransform = new Transform(new Vector3(3, 200, 3), Vector3.Zero,
        new Vector3(0.1f, 0.1f, 0.1f));
    
    private Transform primitiveCubeTransform = new Transform(Vector3.Zero, Vector3.Zero,
        new Vector3(100f, 2f, 100f));
    
    private Transform quadTransform = new Transform(new Vector3(2, 1.5f, 2), Vector3.Zero,
        new Vector3(12f, 12f, 12f));

    private Transform DirectionalLightTransform = new Transform(new Vector3(0, 1, 0),
        Vector3.Zero, new Vector3(10f, 10f, 10f));
    
    private Transform pointLightOneTransform = new Transform(new Vector3(50, 4, 0),
        Vector3.Zero, Vector3.One);
    
    private Transform pointLightTwoTransform = new Transform(new Vector3(0, 25, 20),
        Vector3.Zero, Vector3.One);
    
    private Transform pointLightThreeTransform = new Transform(new Vector3(10, 1, 4),
        Vector3.Zero, Vector3.One);

    
    //MATERIALS
    private Material Debug = new Material(basicLightingShader, debugShader,
        HDSprite, HDSpriteNormal, new Vector3(1f, 1f, 1f));
    
    private Material primitiveCubeMaterial = new Material(basicLightingShader,
        HDSprite, HDSpriteNormal, new Vector3(1f, 1f, 1f));

    private Material modelMaterial = new Material(basicLightingShader, debugShader,
        modelDiffuse, modelNormal, new Vector3(1f, 1f, 1f));
    
    
    Material animationTestMaterial = new Material(quadShader, animationTestSprites
        , new Vector3(1f, 1f, 1f));
    public WorldEntities(Scene worldScene)
    {
        this.worldScene = worldScene;
    }
    
    public void InitializeEntities()
    {
        worldScene.CreateSkyBox(0, "SkyBox", skybox);
        
        worldScene.CreateDirectionalLight(1, "Directional Light", DirectionalLightTransform, 
            new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.25f, 0.25f, 0.25f), new Vector3(0.25f, 0.25f, 0.25f),
            new Vector3(0.2f, 0.2f, 0.2f));
        
        worldScene.CreatePointLight(2, "Point Light One",spriteSheet, 0, 0,
            pointLightOneTransform, new Vector3(0f, 1f, 0f), new Vector3(0.5f, 0.5f, 0.5f), 
            new Vector3(1f, 1f, 1f), new Vector3(1f, 0.5f, 0f),
            1.0f,  0.02f, 0.001f);
        
        worldScene.CreatePointLight(3, "Point Light Two",spriteSheet, 0, 0,
            pointLightTwoTransform, new Vector3(1f, 0f, 1f), new Vector3(1f, 1f,1f), 
            new Vector3(1f, 0f, 0f), new Vector3(1f, 0.5f, 0f),
            1.0f,  0.02f, 0.001f);
        
        worldScene.CreatePointLight(4, "Point Light Three",spriteSheet, 0, 0,
            pointLightThreeTransform, new Vector3(0f, 1f, 1f), new Vector3(1f, 1f,1f), 
            new Vector3(1f, 0f, 0f), new Vector3(1f, 0.5f, 0f),
            1.0f,  0.02f, 0.001f);
        
        worldScene.CreatePrimitiveCube(5, "Primitive Cube", 1024, 0,
            primitiveCubeTransform, Debug);
        
        worldScene.CreateModel(6,"Evelyn Model",
            "D:\\Repos\\Game-Engine\\Engine\\HoneyEngine\\TestProject\\Assets\\3D Models\\Evelynn Tango.obj",
             0,0, modelTransform, modelMaterial);
        
        worldScene.CreateQuad(7, "Pokemon", 0,0, quadTransform, animationTestMaterial, false);
        
    }
   
    public TextureAtlas SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
    public TextureAtlas HDDiffuse {get => HDSprite; set => HDSprite = value; }
    public TextureAtlas HDNormal {get => HDSpriteNormal; set => HDSpriteNormal = value; }
    public SkyboxTexture SkyBoxTexture { get => skybox; set => skybox = value; }
    public TextureAtlas ModelDiffuse { get => modelDiffuse; set => modelDiffuse = value; }
    public TextureAtlas ModelNormal { get => modelNormal; set => modelNormal = value; }
    public TextureAtlas AnimationTest { get => animationTestSprites; set => animationTestSprites = value; }

}