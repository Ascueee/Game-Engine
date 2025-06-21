
namespace ProjectLS.LootNShoot;
public class GameManager
{
    private static Scene gameWorldScene = new Scene();
    private WorldGenerator worldGenerator;

    private int worldSize;
    private int chunkWidth;
    private int chunkHeight;
    
    public GameManager(int worldSize, int chunkWidth, int chunkHeight)
    {
        this.worldSize = worldSize;
        this.chunkWidth = chunkWidth;
        this.chunkHeight = chunkHeight;
        worldGenerator = new WorldGenerator(worldSize, gameWorldScene);
    }
    
    //Loads all game features
    public void InstantiateGame()
    {
        gameWorldScene.GenerateTextureAtlas();
        gameWorldScene.LoadScene();
        //worldGenerator.GenerateWorld(chunkWidth, chunkHeight);
    }
    
    public Scene GameWorldScene { get => gameWorldScene; set => gameWorldScene = value; }
    

}