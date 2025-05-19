using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem;
using ProjectLS.Engine.EntityComponentSystem.Components;
using ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;

namespace ProjectLS.LootNShoot;

/// <summary>
/// DEMO WORLD GEN FOR TESTING THE LOGIC OF A CHUNK
/// NEEDS OPTIMIZATIONS AND A WAY TO STORE BLOCKS IN THE WORLD
/// OPTIMIZATIONS NEED TO BE DONE IN THE CHUNK AND WORLD GEN TO MAKE SURE THERE IS A ENTITY COUNT
/// </summary>
public class GenerateWorldTest
{
    private Scene scene = new Scene();

    //when called create the entities and store them in the scene list
    public void GenerateWorld(int chunkX, int chunkY, int chunkZ)
    {
        //creates the entities
        scene.Entities.Add(new Entity("Grass Block"));
        scene.Entities.Add(new Entity("Dirt Block"));
        scene.Entities.Add(new Entity("Stone Block"));
        scene.Entities.Add(new Entity("Physics Stone"));
        
        scene.Entities[0].AddComponent(new Transform());
        //renders the entity as a cube and assigns the texture relating to the specifc atlas
        scene.Entities[0].AddComponent(new CubeRenderer(scene.SpriteSheet, 50, 0, 32, 0));
        scene.Entities[0].AddComponent(new Material("D:\\Projects\\ProjectLS\\ProjectLS\\Engine\\Shaders\\baseShader.vert",
            "D:\\Projects\\ProjectLS\\ProjectLS\\Engine\\Shaders\\baseShader.frag", scene.SpriteSheet));
        //scene.Entities[0].AddComponent(new BoxCollider());
        
        
        scene.Entities[1].AddComponent(new Transform());
        scene.Entities[1].AddComponent(new CubeRenderer(scene.SpriteSheet, 0, 0));
        scene.Entities[1].AddComponent(new Material("D:\\Projects\\ProjectLS\\ProjectLS\\Engine\\Shaders\\baseShader.vert",
            "D:\\Projects\\ProjectLS\\ProjectLS\\Engine\\Shaders\\baseShader.frag", scene.SpriteSheet));
        
        scene.Entities[2].AddComponent(new Transform());
        scene.Entities[2].AddComponent(new CubeRenderer(scene.SpriteSheet, 16, 0));
        scene.Entities[2].AddComponent(new Material("D:\\Projects\\ProjectLS\\ProjectLS\\Engine\\Shaders\\baseShader.vert",
            "D:\\Projects\\ProjectLS\\ProjectLS\\Engine\\Shaders\\baseShader.frag", scene.SpriteSheet));
        
        //physcis Cobblestone object
        scene.Entities[3].AddComponent(new Transform());
        scene.Entities[3].AddComponent(new CubeRenderer(scene.SpriteSheet, 16, 0));
        scene.Entities[3].AddComponent(new Material("D:\\Projects\\ProjectLS\\ProjectLS\\Engine\\Shaders\\baseShader.vert",
            "D:\\Projects\\ProjectLS\\ProjectLS\\Engine\\Shaders\\baseShader.frag", scene.SpriteSheet));
        //scene.Entities[3].AddComponent(new BoxCollider());
        
        int boarderX = chunkX - 1;
        int boarderY = chunkY - 1;
        int boarderZ = chunkZ - 1;
        
        for (int x = 0; x < chunkX; x++)
        {
            for (int y = 0; y < chunkY; y++)
            {
                for (int z = 0; z < chunkZ; z++)
                {
                    if(y > (boarderY - 1))
                        scene.Entities[0].Instance(new Vector3(x, y, z));
                    else if(y > (boarderY - 15))
                        scene.Entities[1].Instance(new Vector3(x, y, z));
                    else
                        scene.Entities[2].Instance(new Vector3(x, y, z));
                }
            }
        }
        
        //spawns an instance of a stone block affected by gravity
        scene.Entities[3].Instance(new Vector3(5, 60, 5));
    }
    
    public Scene Scene{get => scene; set => scene = value; }
}