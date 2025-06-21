using OpenTK.Mathematics;

namespace ProjectLS.LootNShoot;


/// <summary>
/// Holds Data of a block in the scene
/// </summary>
public class Block
{
    private string blockName;
    private Vector3 blockWorldPos;

    public Block(string blockName, Vector3 blockWorldPos)
    {
        this.blockName = blockName;
        this.blockWorldPos = blockWorldPos;
    }
    
    public string BlockName { get => blockName; set => blockName = value; }
    public Vector3 BlockWorldPos { get => blockWorldPos; set => blockWorldPos = value; }
}