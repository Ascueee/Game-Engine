using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Textures;

namespace ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;

public class CubeRenderer : IComponent
{
    string name = "CubeRenderer";
    private bool canRender = true;
    
    int vertexBufferObject;
    int textureCoordinateBufferObject;
    int vertexBufferObjectInstance;
    int vertexArrayObject;
    private float[] vertices;
    
    private Vector2[] texCoords = new Vector2[6];
    
    public CubeRenderer()
    {
        vertices = new float[]
        {
            // Front face 
            -0.5f, -0.5f, -0.5f, 0f, 0f, -1f,
            0.5f, -0.5f, -0.5f, 0f, 0f, -1f,
            0.5f,  0.5f, -0.5f, 0f, 0f, -1f,
            0.5f,  0.5f, -0.5f, 0f, 0f, -1f,
            -0.5f,  0.5f, -0.5f, 0f, 0f, -1f,
            -0.5f, -0.5f, -0.5f, 0f, 0f, -1f,

            // Back face 
            -0.5f, -0.5f,  0.5f, 0f, 0f, 1f,
            -0.5f,  0.5f,  0.5f, 0f, 0f, 1f,
            0.5f,  0.5f,  0.5f,  0f, 0f, 1f,
            0.5f,  0.5f,  0.5f,  0f, 0f, 1f,
            0.5f, -0.5f,  0.5f,  0f, 0f, 1f,
            -0.5f, -0.5f,  0.5f, 0f, 0f, 1f,

            // Left face 
            -0.5f, -0.5f, -0.5f, -1f, 0f, 0f,
            -0.5f,  0.5f, -0.5f, -1f, 0f, 0f,
            -0.5f,  0.5f,  0.5f, -1f, 0f, 0f,
            -0.5f,  0.5f,  0.5f, -1f, 0f, 0f,
            -0.5f, -0.5f,  0.5f, -1f, 0f, 0f,
            -0.5f, -0.5f, -0.5f, -1f, 0f, 0f,

            // Right face 
            0.5f, -0.5f, -0.5f, 1f, 0f, 0f,
            0.5f, -0.5f,  0.5f, 1f, 0f, 0f,
            0.5f,  0.5f,  0.5f, 1f, 0f, 0f,
            0.5f,  0.5f,  0.5f, 1f, 0f, 0f,
            0.5f,  0.5f, -0.5f, 1f, 0f, 0f,
            0.5f, -0.5f, -0.5f, 1f, 0f, 0f,

            // Bottom face
            -0.5f, -0.5f, -0.5f, 0f, -1f, 0f,
            -0.5f, -0.5f,  0.5f, 0f, -1f, 0f,
            0.5f, -0.5f,  0.5f, 0f, -1f, 0f,
            0.5f, -0.5f,  0.5f, 0f, -1f, 0f,
            0.5f, -0.5f, -0.5f, 0f, -1f, 0f,
            -0.5f, -0.5f, -0.5f, 0f, -1f, 0f,

            // Top face
            -0.5f,  0.5f, -0.5f, 0f, 1f, 0f,
            0.5f,  0.5f, -0.5f,  0f, 1f, 0f,
            0.5f,  0.5f,  0.5f,  0f, 1f, 0f,
            0.5f,  0.5f,  0.5f,  0f, 1f, 0f,
            -0.5f,  0.5f,  0.5f, 0f, 1f, 0f,
            -0.5f,  0.5f, -0.5f, 0f, 1f, 0f
        };
    }
    public CubeRenderer(TextureAtlas textureAtlas, int textureCellX, int textureCellY)
    {
        texCoords = textureAtlas.GetTextureCoords(textureCellX, textureCellY);
        vertices = new float[]
        {
            // Front face
            -0.5f, -0.5f, -0.5f, texCoords[0].X, texCoords[0].Y, 0f, 0f, -1f,  // Bottom-left
            0.5f, -0.5f, -0.5f,  texCoords[1].X, texCoords[1].Y, 0f, 0f, -1f,  // Bottom-right
            0.5f,  0.5f, -0.5f,  texCoords[5].X, texCoords[5].Y, 0f, 0f, -1f,  // Top-right
            0.5f,  0.5f, -0.5f,  texCoords[5].X, texCoords[5].Y, 0f, 0f, -1f,  // Top-right
            -0.5f,  0.5f, -0.5f,  texCoords[3].X, texCoords[3].Y, 0f, 0f, -1f,  // Top-left
            -0.5f, -0.5f, -0.5f, texCoords[0].X, texCoords[0].Y, 0f, 0f, -1f,  // Bottom-left

            // Back face
            -0.5f, -0.5f,  0.5f,  texCoords[0].X, texCoords[0].Y, 0f, 0f, 1f,  // Bottom-left
            -0.5f,  0.5f,  0.5f,  texCoords[1].X, texCoords[1].Y, 0f, 0f, 1f,  // Top-left
            0.5f,  0.5f,  0.5f,  texCoords[5].X, texCoords[5].Y, 0f, 0f, 1f,  // Top-right
            0.5f,  0.5f,  0.5f,  texCoords[5].X, texCoords[5].Y, 0f, 0f, 1f,  // Top-right
            0.5f, -0.5f,  0.5f,  texCoords[3].X, texCoords[3].Y, 0f, 0f, 1f,  // Bottom-right
            -0.5f, -0.5f,  0.5f, texCoords[0].X, texCoords[0].Y, 0f, 0f, 1f,  // Bottom-left

            // Left face
            -0.5f, -0.5f, -0.5f,  texCoords[0].X, texCoords[0].Y, -1f, 0f, 0f,  // Bottom-left
            -0.5f,  0.5f, -0.5f,  texCoords[1].X, texCoords[1].Y, -1f, 0f, 0f,  // Top-left
            -0.5f,  0.5f,  0.5f,  texCoords[5].X, texCoords[5].Y, -1f, 0f, 0f,  // Top-right
            -0.5f,  0.5f,  0.5f,  texCoords[5].X, texCoords[5].Y, -1f, 0f, 0f,  // Top-right
            -0.5f, -0.5f,  0.5f,  texCoords[3].X, texCoords[3].Y, -1f, 0f, 0f,  // Bottom-right
            -0.5f, -0.5f, -0.5f, texCoords[0].X, texCoords[0].Y, -1f, 0f, 0f, // Bottom-left

            // Right face
            0.5f, -0.5f, -0.5f,  texCoords[0].X, texCoords[0].Y, 1f, 0f, 0f,  // Bottom-left
            0.5f, -0.5f,  0.5f,  texCoords[1].X, texCoords[1].Y, 1f, 0f, 0f,  // Bottom-right
            0.5f,  0.5f,  0.5f,  texCoords[5].X, texCoords[5].Y, 1f, 0f, 0f,  // Top-right
            0.5f,  0.5f,  0.5f,  texCoords[5].X, texCoords[5].Y, 1f, 0f, 0f, // Top-right
            0.5f,  0.5f, -0.5f,  texCoords[3].X, texCoords[3].Y, 1f, 0f, 0f,  // Top-left
            0.5f, -0.5f, -0.5f,  texCoords[0].X, texCoords[0].Y, 1f, 0f, 0f,  // Bottom-left

            // Bottom face
            -0.5f, -0.5f, -0.5f,  texCoords[0].X, texCoords[0].Y, 0f, -1f, 0f,  // Bottom-left
            -0.5f, -0.5f,  0.5f,  texCoords[1].X, texCoords[1].Y, 0f, -1f, 0f,  // Bottom-right
            0.5f, -0.5f,  0.5f,  texCoords[5].X, texCoords[5].Y, 0f, -1f, 0f,  // Top-right
            0.5f, -0.5f,  0.5f,  texCoords[5].X, texCoords[5].Y, 0f, -1f, 0f,  // Top-right
            0.5f, -0.5f, -0.5f,  texCoords[3].X, texCoords[3].Y, 0f, -1f, 0f,  // Top-left
            -0.5f, -0.5f, -0.5f, texCoords[0].X, texCoords[0].Y, 0f, -1f, 0f,  // Bottom-left

            // Top face
            -0.5f,  0.5f, -0.5f, texCoords[0].X, texCoords[0].Y, 0f, -1f, 0f, // Bottom-left
            0.5f,  0.5f, -0.5f, texCoords[1].X, texCoords[1].Y, 0f, -1f, 0f, // Bottom-right
            0.5f,  0.5f,  0.5f, texCoords[5].X, texCoords[5].Y, 0f, -1f, 0f, // Top-right
            0.5f,  0.5f,  0.5f, texCoords[5].X, texCoords[5].Y, 0f, -1f, 0f,// Top-right
            -0.5f,  0.5f,  0.5f, texCoords[3].X, texCoords[3].Y, 0f, -1f, 0f, // Top-left
            -0.5f,  0.5f, -0.5f, texCoords[0].X, texCoords[0].Y, 0f, -1f, 0f, // Bottom-left
        };
    }
    
    public CubeRenderer(TextureAtlas textureAtlas, int sideCellX, int sideCellY, int topCellX, int topCellY)
    {
        Vector2[] sideUVs = textureAtlas.GetTextureCorners(sideCellX, sideCellY); 
        Vector2[] topUVs = textureAtlas.GetTextureCorners(topCellX, topCellY);   
        vertices = new float[]
        {
            -0.5f, -0.5f, -0.5f, sideUVs[0].X, sideUVs[0].Y, 0, 0, -1,
             0.5f, -0.5f, -0.5f, sideUVs[1].X, sideUVs[1].Y, 0, 0, -1,
             0.5f,  0.5f, -0.5f, sideUVs[2].X, sideUVs[2].Y, 0, 0, -1,
             0.5f,  0.5f, -0.5f, sideUVs[2].X, sideUVs[2].Y, 0, 0, -1,
            -0.5f,  0.5f, -0.5f, sideUVs[3].X, sideUVs[3].Y, 0, 0, -1,
            -0.5f, -0.5f, -0.5f, sideUVs[0].X, sideUVs[0].Y, 0, 0, -1,
            
             0.5f, -0.5f,  0.5f, sideUVs[0].X, sideUVs[0].Y, 0, 0, 1,
            -0.5f, -0.5f,  0.5f, sideUVs[1].X, sideUVs[1].Y, 0, 0, 1,
            -0.5f,  0.5f,  0.5f, sideUVs[2].X, sideUVs[2].Y, 0, 0, 1,
            -0.5f,  0.5f,  0.5f, sideUVs[2].X, sideUVs[2].Y, 0, 0, 1,
             0.5f,  0.5f,  0.5f, sideUVs[3].X, sideUVs[3].Y, 0, 0, 1,
             0.5f, -0.5f,  0.5f, sideUVs[0].X, sideUVs[0].Y, 0, 0, 1,

            // Left face (X-)
            -0.5f, -0.5f,  0.5f, sideUVs[0].X, sideUVs[0].Y, -1, 0, 0,
            -0.5f, -0.5f, -0.5f, sideUVs[1].X, sideUVs[1].Y, -1, 0, 0,
            -0.5f,  0.5f, -0.5f, sideUVs[2].X, sideUVs[2].Y, -1, 0, 0,
            -0.5f,  0.5f, -0.5f, sideUVs[2].X, sideUVs[2].Y, -1, 0, 0,
            -0.5f,  0.5f,  0.5f, sideUVs[3].X, sideUVs[3].Y, -1, 0, 0,
            -0.5f, -0.5f,  0.5f, sideUVs[0].X, sideUVs[0].Y, -1, 0, 0,

            // Right face (X+)
             0.5f, -0.5f, -0.5f, sideUVs[0].X, sideUVs[0].Y, 1, 0, 0,
             0.5f, -0.5f,  0.5f, sideUVs[1].X, sideUVs[1].Y, 1, 0, 0,
             0.5f,  0.5f,  0.5f, sideUVs[2].X, sideUVs[2].Y, 1, 0, 0,
             0.5f,  0.5f,  0.5f, sideUVs[2].X, sideUVs[2].Y, 1, 0, 0,
             0.5f,  0.5f, -0.5f, sideUVs[3].X, sideUVs[3].Y, 1, 0, 0,
             0.5f, -0.5f, -0.5f, sideUVs[0].X, sideUVs[0].Y, 1, 0, 0,

            // Bottom face (Y-)
            -0.5f, -0.5f,  0.5f, topUVs[0].X, topUVs[0].Y, 0, -1, 0,
             0.5f, -0.5f,  0.5f, topUVs[1].X, topUVs[1].Y, 0, -1, 0,
             0.5f, -0.5f, -0.5f, topUVs[2].X, topUVs[2].Y, 0, -1, 0,
             0.5f, -0.5f, -0.5f, topUVs[2].X, topUVs[2].Y, 0, -1, 0,
            -0.5f, -0.5f, -0.5f, topUVs[3].X, topUVs[3].Y, 0, -1, 0,
            -0.5f, -0.5f,  0.5f, topUVs[0].X, topUVs[0].Y, 0, -1, 0,

            // Top face (Y+)
            -0.5f,  0.5f, -0.5f, topUVs[0].X, topUVs[0].Y, 0, 1, 0,
             0.5f,  0.5f, -0.5f, topUVs[1].X, topUVs[1].Y, 0, 1, 0,
             0.5f,  0.5f,  0.5f, topUVs[2].X, topUVs[2].Y, 0, 1, 0,
             0.5f,  0.5f,  0.5f, topUVs[2].X, topUVs[2].Y, 0, 1, 0,
            -0.5f,  0.5f,  0.5f, topUVs[3].X, topUVs[3].Y, 0, 1, 0,
            -0.5f,  0.5f, -0.5f, topUVs[0].X, topUVs[0].Y, 0, 1, 0,
        };
    }
    
    public int VertexBufferObjectInstance { get => vertexBufferObject; set => vertexBufferObject = value; }
    public int VertexBufferObject { get => vertexBufferObject; set => vertexBufferObject = value; }
    public int VertexArrayObject { get => vertexArrayObject; set => vertexArrayObject = value; }
    public float[] Vertices {get => vertices;}
    public string Name => name;
}