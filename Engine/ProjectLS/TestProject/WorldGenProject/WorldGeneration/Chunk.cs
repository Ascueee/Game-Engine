using OpenTK.Mathematics;
using ProjectLS.Engine.Math;

namespace ProjectLS.LootNShoot;

public class Chunk
{
    private int chunkWidth;
    int chunkHeight;
    private Block[,,] chunkBlocks;

    
    
    public Chunk(int chunkWidth, int chunkHeight)
    {
        this.chunkWidth = chunkWidth;
        this.chunkHeight = chunkHeight;
        
        //Creates a 3D array of blocks which stores the position of all blocks in the chunk
        chunkBlocks = new Block[chunkWidth, chunkHeight, chunkWidth];
    }
    
    
    public void GenerateChunk(int xPosStart, int zPosStart)
    {
        PerlinNoise noise = new PerlinNoise(22);
        
        //Console.WriteLine($"The chunk starts at: {xPosStart}, {zPosStart}");
        ///TODO: ADD SPLINES FOR BETTER LOOKING TERRAIN
        for (int x = 0; x < chunkWidth; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                for (int z = 0; z <  chunkWidth; z++)
                {
                    float frequency = 0.2f;
                    float amplitude = 100f;
                    int terrainHeight = 2;
                    
                    float xOffset = MathF.Sin(x * frequency) * amplitude;
                    float zOffset = MathF.Sin(z * frequency) * amplitude;
                    
                    
                    //float surfaceY = terrainHeight + xOffset + zOffset;
                    //float surfaceY = terrainHeight + noise.Noise(x * frequency,z * frequency) * 5;
                    float perlinNoise = noise.OctaveNoise((xPosStart + x) * frequency, (zPosStart + z) * frequency, 8);
                    float surfaceY = terrainHeight + perlinNoise * amplitude;
                    
                    
                    if (y < surfaceY)
                    {
                        chunkBlocks[x, y, z] = new Block("Stone Block", new Vector3(xPosStart + x, y, zPosStart + z));
                    }
                    else
                    {
                        chunkBlocks[x, y, z] = new Block("Air Block", new Vector3(xPosStart + x, y, zPosStart + z));
                    }
                }
            }
        }
        
    }

    public void PlaceBlock(int blockID)
    {
        
    }


    /// <summary>
    /// When called checks the block array to see which block is visible to render in the scene
    /// Counts how many blocks there are in the scene to render
    /// TODO:
    ///     CHECK IF THERE IS AN AIRBLOCK ABOVE TO RENDER IF THERE IS THE BLOCK IS VISIBLE SO RENDER IF NOT DONT RENDER BLOCK HIDDEN
    ///     FIX THIS SYSTEM TO MAKE NEATER AND MORE EFFICIENT CODE
    /// </summary>
    public void RenderChunk(Scene gameWorldScene)
    {
        for (int x = 0; x < chunkWidth; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                for (int z = 0; z <  chunkWidth; z++)
                {
                    if (chunkBlocks[x, y, z].BlockName == "Sand Block")
                    {
                        gameWorldScene.Entities[2].Instance(chunkBlocks[x,y,z].BlockWorldPos);
                    }
                    else if (chunkBlocks[x, y, z].BlockName == "Stone Block")
                    {
                        gameWorldScene.Entities[3].Instance(chunkBlocks[x,y,z].BlockWorldPos);
                    }
                    else if (chunkBlocks[x, y, z].BlockName == "Grass Block")
                    {
                        gameWorldScene.Entities[1].Instance(chunkBlocks[x,y,z].BlockWorldPos);
                    }
                    else if (chunkBlocks[x, y, z].BlockName == "CobbleStone Block")
                    {
                        gameWorldScene.Entities[4].Instance(chunkBlocks[x,y,z].BlockWorldPos);
                    }
                    else if (chunkBlocks[x, y, z].BlockName == "Primitive Cube")
                    {
                        gameWorldScene.Entities[7].Instance(chunkBlocks[x,y,z].BlockWorldPos);
                    }
                    
                }
            }
        }
    }
    
        
    public void PrintChunKBlocks()
    {
        for (int x = 0; x < chunkWidth; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                for (int z = 0; z <  chunkWidth; z++)
                {
                    Console.WriteLine($"{chunkBlocks[x, y, z].BlockName}: {chunkBlocks[x, y, z].BlockWorldPos}");
                }
            }
        }
    }
    public int ChunkWidth { get => chunkWidth; set => chunkWidth = value; }
}