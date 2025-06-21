using HoneyEngine.Engine;
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
    DebugCamera camera = new DebugCamera(new Vector3(0,0,0), (float)1920 / 1080);
    private WorldEntities gameWorldEntities;
    
    public Scene()
    {
        this.gameWorldEntities = new WorldEntities(this);
    }

    public void LoadScene()
    {
        gameWorldEntities.InitializeEntities();
    }

    public void GenerateTextureAtlas()
    {
                
        gameWorldEntities.SpriteSheet.GenerateAtlasTexture();
        gameWorldEntities.SkyBoxTexture.GenerateTexture();
        gameWorldEntities.ModelDiffuse.GenerateAtlasTexture();
        gameWorldEntities.ModelNormal.GenerateAtlasTexture();
        gameWorldEntities.QuadTexture.GenerateAtlasTexture();
        gameWorldEntities.HDDiffuse.GenerateAtlasTexture();
        gameWorldEntities.HDNormal.GenerateAtlasTexture();
    }
    
    public void CreateEntity(int entityID, string entityName)
    {
        entities.Add(new Entity(entityName));
        entities[entityID].AddComponent(new Transform());
    }
    
    public void CreateCube(int entityID, string entityName,int textureX, int textureY,
        Transform transform, Material material)
    {
        entities.Add(new Entity(entityName));
        entities[entityID].AddComponent(transform);
        entities[entityID].AddComponent(new CubeRenderer(material.DiffuseTexture, textureX, textureY));
        entities[entityID].AddComponent(material);
    }
    
    public void CreateCube(int entityID, string entityName,int textureX, int textureY,
        int textureTwoX, int textureTwoY, Transform transform, Material material)
    {
        entities.Add(new Entity(entityName));
        
        entities[entityID].AddComponent(transform);
        entities[entityID].AddComponent(new CubeRenderer(material.DiffuseTexture, textureX, textureY, textureTwoX, textureTwoY));
        entities[entityID].AddComponent(material);
    }

    public void CreateQuad(int entityID, string entityName, int textureX, int textureY,
        Transform transform, Material material)
    {
        entities.Add(new Entity(entityName));
        entities[entityID].AddComponent(transform);
        entities[entityID].AddComponent(new QuadRenderer(material.DiffuseTexture, textureX, textureY, true));
        entities[entityID].AddComponent(material);
        
        
        // //PLACES NUMBER OF BLADES OF GRASS
        // Random rand = new Random();
        // float xOffSet = 10f;
        // float yOffSet = 25f;
        //
        // float max = 18;
        // float min = -18;
        // int numOfGrass = 20000;
        //     
        // for (int i = 0; i < numOfGrass; i++)
        // {
        //     float x = (float)(rand.NextDouble() * (max - min) + min);
        //     float z = (float)(rand.NextDouble() * (max - min) + min);
        //     entities[blockID].Instance(new Vector3(x, 0.5f, z));
        // }

    }
    
    public void CreatePrimitiveCube(int entityID, string entityName, 
        int textureX, int textureY, Transform transform, Material material)
    {
        entities.Add(new Entity(entityName));
        entities[entityID].AddComponent(transform);
        entities[entityID].AddComponent(new CubeRenderer(material.DiffuseTexture, textureX, textureY));
        entities[entityID].AddComponent(material);
        
        entities[entityID].Instance(new Vector3(0, 0, 0));
        entities[entityID].Instance(new Vector3(1, 0, 0));
        entities[entityID].Instance(new Vector3(2, 0, 0));
        
        entities[entityID].Instance(new Vector3(0, 0, 1));
        entities[entityID].Instance(new Vector3(1, 0, 1));
        entities[entityID].Instance(new Vector3(2, 0, 1));
        
        entities[entityID].Instance(new Vector3(0, 0, 2));
        entities[entityID].Instance(new Vector3(1, 0, 2));
        entities[entityID].Instance(new Vector3(2, 0, 2));
        
                
        entities[entityID].Instance(new Vector3(0, 0, -1));
        entities[entityID].Instance(new Vector3(1, 0, -1));
        entities[entityID].Instance(new Vector3(2, 0, -1));
        
        entities[entityID].Instance(new Vector3(0, 0, -2));
        entities[entityID].Instance(new Vector3(1, 0, -2));
        entities[entityID].Instance(new Vector3(2, 0, -2));
    }

    public void CreateModel(int entityID, string entityName,string modelFilePath, int textureX, int textureY,
        Transform transform, Material material)
    {
        entities.Add(new Entity(entityName));
        entities[entityID].AddComponent(transform);
        entities[entityID].AddComponent(new ModelRenderer(modelFilePath, material.DiffuseTexture, textureX, textureY));
        entities[entityID].AddComponent(material);
        
        entities[entityID].Instance(transform.Position);
    }

    public void CreateSkyBox(int entityID, string entityName, SkyboxTexture skyBox)
    {
        entities.Add(new Entity(entityName));
        
        entities[entityID].AddComponent(new SkyboxRenderer());
        entities[entityID].AddComponent(new Material("D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\SkyBox.vert",
            "D:\\Repos\\Game-Engine\\Engine\\ProjectLS\\Engine\\Shaders\\SkyBox.frag", skyBox));
    }

    public void CreateDirectionalLight(int entityID, string entityName, Transform transform, Vector3 lightColor, Vector3 lightAmbient,
        Vector3 lightDiffuse, Vector3 lightSpecular)
    {
        entities.Add(new Entity(entityName));
        entities[entityID].AddComponent(transform);
        entities[entityID].AddComponent(new DirectionalLightComponent(lightColor, lightAmbient, lightDiffuse, lightSpecular)); 
    }
    
    public void CreatePointLight(int entityID, string entityName,TextureAtlas texture, int textureX, int textureY,
        Transform transform, Vector3 lightColor, Vector3 lightAmbient, Vector3 lightDiffuse, Vector3 lightSpecular,
        float constant, float linear, float quadratic)
    {
        entities.Add(new Entity(entityName));
        entities[entityID].AddComponent(transform);
        entities[entityID].AddComponent(new PointLightComponent(lightColor, lightAmbient, lightDiffuse,
            lightSpecular, constant, linear, quadratic));
    }
    
    public DebugCamera Camera {get => camera; set => camera = value; }
    public List<Entity> Entities { get => entities; set => entities = value; }
    public WorldEntities GameWorldEntities { get => gameWorldEntities; set => gameWorldEntities = value; }
    
}