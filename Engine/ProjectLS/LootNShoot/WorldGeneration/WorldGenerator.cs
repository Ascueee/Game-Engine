namespace ProjectLS.LootNShoot;

/// Generates a world that is consistant of chunks and stores the chunks in a 2D array
public class WorldGenerator
{
    private Chunk[,] worldChunks;
    private Scene gameWorldScene;
    private int worldSize;
    
    public WorldGenerator(int worldSize, Scene gameScene)
    {
        this.worldSize = worldSize;
        this.gameWorldScene = gameScene;
        worldChunks = new Chunk[worldSize, worldSize];
        
    }

    
    /// When called generates chunks and stores in a 2D array
    public void GenerateWorld(int chunkWidth, int chunkHeight)
    {
        
        //itterates through each chunk to generate and save them into the array
        for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                worldChunks[x, y] = new Chunk(chunkWidth, chunkHeight);
                worldChunks[x,y].GenerateChunk(x * worldChunks[x,y].ChunkWidth, y * worldChunks[x,y].ChunkWidth);
                worldChunks[x,y].RenderChunk(gameWorldScene);
                
            }
        }

    }
}