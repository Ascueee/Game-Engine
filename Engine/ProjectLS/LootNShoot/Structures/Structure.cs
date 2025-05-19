using OpenTK.Mathematics;

namespace ProjectLS.LootNShoot.Structures;

public class Structure
{
    private string structureName;
    public Vector3[] structureBlocks;
    public int structureSpawnHeight;

    public Structure(string structureName, Vector3[] structureBlocks, int structureSpawnHeight)
    {
        this.structureName = structureName;
        this.structureBlocks = structureBlocks;
        this.structureSpawnHeight = structureSpawnHeight;
    }
    
}