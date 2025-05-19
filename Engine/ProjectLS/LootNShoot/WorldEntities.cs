using OpenTK.Mathematics;

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


    public WorldEntities(Scene worldScene)
    {
        this.worldScene = worldScene;
    }
    
    //Creates all the blocks for the scene to use
    public void InitializeBlocks()
    {
        worldScene.CreateEntity(0, "Air Block");
        worldScene.CreateCube(1, "Grass Block", 16,16, 34,16);
        worldScene.CreateCube(2, "Dirt Block", 0, 0);
        worldScene.CreateCube(3, "Stone Block", 16,0);
        worldScene.CreateCube(4, "CobbleStone Block", 0,16);
        
        worldScene.CreateSkyBox(5, "SkyBox");
        worldScene.CreateLightObject(6, "WhiteLight", new Vector3(-5, 3, 1), 
            new Vector3(1, 1, 1), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.5f, 0.5f, 0.5f));
        worldScene.CreatePrimitiveCube(7, "Primitive Cube", 0, 0);
    }
}