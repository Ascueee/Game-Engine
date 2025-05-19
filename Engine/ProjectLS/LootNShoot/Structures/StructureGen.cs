using OpenTK.Mathematics;

namespace ProjectLS.LootNShoot.Structures;

/// <summary>
/// Needs an array that holds blocks that can be used in the textures
/// Sends those block positions to the chunk generator to use to generate them
/// Rarity has a rarity scale to limit the amount of the structure spawned
/// </summary>
public class StructureGen
{
    private int structureSpawnHeight;
    private Structure structure;

    public StructureGen(string structureName)
    {
        Vector3[] vectorArray = ReadStructureData(structureName);
        structure = new Structure(structureName, vectorArray, 49);
    }
    
    /// <summary>
    /// Returns the block that needs to be placed which is depending on the 
    /// </summary>
    /// <returns></returns>
    public String GenerateStructureBlocks(int index)
    {
        if (structure.structureBlocks[index].Y > 3)
            return "Leaf Block";
        else 
            return "Dirt Block";
    }

    Vector3[] ReadStructureData(string structureName)
    {
        string filePath = $"D:\\Projects\\ProjectLS\\ProjectLS\\LootNShoot\\Structures\\{structureName}.txt";
        List<Vector3> vectors = new List<Vector3>();

        try
        {
            foreach (string line in File.ReadLines(filePath))
            {
                string[] parts = line.Split(',');

                if (parts.Length == 3 &&
                    float.TryParse(parts[0], out float x) &&
                    float.TryParse(parts[1], out float y) &&
                    float.TryParse(parts[2], out float z))
                {
                    vectors.Add(new Vector3(x, y, z));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
        
        return vectors.ToArray();
    }
    
    public Structure Structure { get => structure; set => structure = value; }


}