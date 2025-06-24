using HoneyEngine.Engine;
using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Textures;

namespace ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;

public class QuadRenderer : IComponent
{
    string name = "QuadRenderer";
    private int vertexBufferObject;
    private int vertexArrayObject;
    private int vertexBufferObjectInstance;
    
    private float[] vertices;
    
    private Vector2[] texCoords = new Vector2[6];

    public QuadRenderer(TextureAtlas textureAtlas, int textureCellX, int textureCellY, bool crossQuadTrue)
    {
        texCoords = textureAtlas.GetTextureCoords(textureCellX, textureCellY);
        float halfOffset = 0.25f;

        if (crossQuadTrue)
        {
            vertices = new float[]
            {
                -halfOffset, -0.5f, 0.0f, texCoords[0].X, texCoords[0].Y,
                halfOffset, -0.5f, 0.0f, texCoords[1].X, texCoords[1].Y,
                halfOffset,  0.5f, 0.0f, texCoords[5].X, texCoords[5].Y,

                halfOffset,  0.5f, 0.0f, texCoords[5].X, texCoords[5].Y,
                -halfOffset,  0.5f, 0.0f, texCoords[3].X, texCoords[3].Y,
                -halfOffset, -0.5f, 0.0f, texCoords[0].X, texCoords[0].Y,
            
                0.0f, -0.5f, -halfOffset, texCoords[0].X, texCoords[0].Y,
                0.0f, -0.5f,  halfOffset, texCoords[1].X, texCoords[1].Y,
                0.0f,  0.5f,  halfOffset, texCoords[2].X, texCoords[2].Y,

                0.0f,  0.5f,  halfOffset, texCoords[2].X, texCoords[2].Y,
                0.0f,  0.5f, -halfOffset, texCoords[3].X, texCoords[3].Y,
                0.0f, -0.5f, -halfOffset, texCoords[0].X, texCoords[0].Y
            };
        }
        else
        {
            vertices = new float[]
            {
                -0.5f, -0.5f, 0.0f, texCoords[0].X, texCoords[0].Y,
                0.5f, -0.5f, 0.0f, texCoords[1].X, texCoords[1].Y,
                0.5f,  0.5f, 0.0f, texCoords[5].X, texCoords[5].Y,
                
                0.5f,  0.5f, 0.0f, texCoords[5].X, texCoords[5].Y,
                -0.5f,  0.5f, 0.0f, texCoords[3].X, texCoords[3].Y,
                -0.5f, -0.5f, 0.0f, texCoords[0].X, texCoords[0].Y,
            };
        }


    }
    
    public int VertexBufferObjectInstance { get => vertexBufferObjectInstance; set => vertexBufferObjectInstance = value; }
    public int VertexBufferObject { get => vertexBufferObject; set => vertexBufferObject = value; }
    public int VertexArrayObject { get => vertexArrayObject; set => vertexArrayObject = value; }
    public float[] Vertices {get => vertices;}
    public string Name { get => name; set => name = value; }
}