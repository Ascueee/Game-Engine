using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem;
using ProjectLS.Engine.EntityComponentSystem.Components;
using ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;
using ProjectLS.Engine.EntityComponentSystem.Textures;

namespace ProjectLS.LootNShoot;

/// <summary>
/// Holds all the data used for a specific scene
/// </summary>
public class Scene
{
    List<Entity> entities = new List<Entity>();
    
    //creates a texture atlas with each texture having a dimension of 16x16
    private TextureAtlas spriteSheet = new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\LootNShoot\\Assets\\sprite_sheet.png",
        16, 16);
    private TextureAtlas HDSprite = new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\LootNShoot\\Assets\\HDDiffuse.png",
        1024, 1024);
    private TextureAtlas HDSpriteNormal = new TextureAtlas("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\LootNShoot\\Assets\\HDNormal.png",
        1024, 1024);
    
    DebugCamera camera = new DebugCamera(new Vector3(0,0,0), (float)1920 / 1080);
    
    SkyboxTexture skybox = new SkyboxTexture(new string[]
    {
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\LootNShoot\\Assets\\SkyboxTextures\\px.png", 
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\LootNShoot\\Assets\\SkyboxTextures\\nx.png", 
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\LootNShoot\\Assets\\SkyboxTextures\\py.png", 
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\LootNShoot\\Assets\\SkyboxTextures\\nz.png", 
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\LootNShoot\\Assets\\SkyboxTextures\\pz.png", 
        "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\LootNShoot\\Assets\\SkyboxTextures\\nz.png"  
    });
    
    /// Creates a block entity that uses one texture
    public void CreateEntity(int blockID, string Blockname)
    {
        entities.Add(new Entity(Blockname));
        entities[blockID].AddComponent(new Transform());
    }
    
    public void CreateCube(int blockID, string blockName, int textureX, int textureY)
    {
        entities.Add(new Entity(blockName));
        
        entities[blockID].AddComponent(new Transform(Vector3.Zero, Vector3.Zero, new Vector3(2f, 2f, 2f)));
        entities[blockID].AddComponent(new CubeRenderer(spriteSheet, textureX, textureY));
        entities[blockID].AddComponent(new Material("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\Lighting.vert",
            "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\Lighting.frag",
            spriteSheet ,new Vector3(1f,1f,1f)));
    }
    
    public void CreateCube(int blockID, string blockName, int textureX, int textureY, int textureTwoX, int textureTwoY)
    {
        entities.Add(new Entity(blockName));
        
        entities[blockID].AddComponent(new Transform(Vector3.Zero, Vector3.Zero, new Vector3(2f, 2f, 2f)));
        entities[blockID].AddComponent(new CubeRenderer(spriteSheet, textureX, textureY, textureTwoX, textureTwoY));
        entities[blockID].AddComponent(new Material("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\Lighting.vert",
            "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\Lighting.frag",
            spriteSheet ,new Vector3(1f,1f,1f)));
    }

    public void CreateSkyBox(int blockID, string blockName)
    {
        entities.Add(new Entity(blockName));
        
        entities[blockID].AddComponent(new SkyboxRenderer());
        entities[blockID].AddComponent(new Material("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\SkyBox.vert",
            "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\SkyBox.frag", skybox));
    }

    public void CreateLightObject(int blockID, string blockName, Vector3 lightPos, Vector3 lightColor, Vector3 lightAmbient,
        Vector3 lightDiffuse)
    {
        entities.Add(new Entity(blockName));
        entities[blockID].AddComponent(new Transform(lightPos, Vector3.Zero, new Vector3(0.5f, 0.5f, 0.5f)));
        entities[blockID].AddComponent(new LightingObject(lightColor, lightAmbient, lightDiffuse)); 
    }

    public void CreatePrimitiveCube(int blockID, string blockName, int textureX, int textureY)
    {
        entities.Add(new Entity(blockName));
        entities[blockID].AddComponent(new Transform(Vector3.Zero, Vector3.Zero, new Vector3(100f,2f, 100f)));
        entities[blockID].AddComponent(new CubeRenderer(HDSprite, textureX, textureY));
        entities[blockID].AddComponent(new Material("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\Lighting.vert",
            "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\Lighting.frag",
            HDSprite, HDSpriteNormal, new Vector3(1f,1f,1f)));
        
        
        entities[blockID].Instance(new Vector3(0, 0, 0));
    }
    
    public DebugCamera Camera {get => camera; set => camera = value; }
    public List<Entity> Entities { get => entities; set => entities = value; }
    public TextureAtlas SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
    public TextureAtlas HDDiffuse {get => HDSprite; set => HDSprite = value; }
    public TextureAtlas HDNormal {get => HDSpriteNormal; set => HDSpriteNormal = value; }
    public SkyboxTexture SkyBoxTexture { get => skybox; set => skybox = value; }
}