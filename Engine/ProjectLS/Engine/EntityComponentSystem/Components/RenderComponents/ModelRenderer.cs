using Assimp;
using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Textures;

namespace ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;

public class ModelRenderer : IComponent
{
    string name = "ModelRenderer";
    List<float> vertices = new List<float>();
    List<int> indices = new List<int>();
    
    int vertexBufferObject;
    int vertexArrayObject;
    int elementBufferObject;
    int vertexBufferObjectInstance;
    

public ModelRenderer(string filePath, TextureAtlas textureAtlas, int textureCellX, int textureCellY)
{
    AssimpContext importer = new AssimpContext();
    Scene scene = importer.ImportFile(filePath, PostProcessSteps.Triangulate | PostProcessSteps.GenerateSmoothNormals);
    
    int pixelX = textureCellX * textureAtlas.TextureAtlasWidth;
    int pixelY = textureCellY * textureAtlas.TextureAtlasHeight;
    
    Vector2[] atlasCoords = textureAtlas.GetTextureCoords(pixelX, pixelY);
    
    float u0 = atlasCoords[0].X;
    float v0 = atlasCoords[0].Y;
    float u1 = atlasCoords[5].X;
    float v1 = atlasCoords[5].Y;

    foreach (var mesh in scene.Meshes)
    {
        for (int i = 0; i < mesh.Vertices.Count; i++)
        {
            vertices.Add(mesh.Vertices[i].X);
            vertices.Add(mesh.Vertices[i].Y);
            vertices.Add(mesh.Vertices[i].Z);
            
            if (mesh.HasNormals)
            {
                vertices.Add(mesh.Normals[i].X);
                vertices.Add(mesh.Normals[i].Y);
                vertices.Add(mesh.Normals[i].Z);
            }
            else
            {
                vertices.Add(0.0f);
                vertices.Add(0.0f);
                vertices.Add(0.0f);
            }
            
            if (mesh.HasTextureCoords(0))
            {
                float modelU = mesh.TextureCoordinateChannels[0][i].X;
                float modelV = mesh.TextureCoordinateChannels[0][i].Y;
                
                float atlasU = u0 + modelU * (u1 - u0);
                float atlasV = v0 + modelV * (v1 - v0);

                vertices.Add(atlasU);
                vertices.Add(atlasV);
            }
            else
            {
                vertices.Add(0.0f);
                vertices.Add(0.0f);
            }
        }

        // Indices remain unchanged
        foreach (var face in mesh.Faces)
        {
            indices.AddRange(face.Indices);
        }
    }
}
    
    public string Name => name;
    public int VertexBufferObject { get => vertexBufferObject; set => vertexBufferObject = value; }
    public int VertexArrayObject { get => vertexArrayObject; set => vertexArrayObject = value; }
    public int ElementBufferObject  { get => elementBufferObject; set => elementBufferObject = value; }
    public int VertexBufferObjectInstance { get => vertexBufferObjectInstance; set => vertexBufferObjectInstance = value; }
    
    public List<float> Vertices { get => vertices; set => vertices = value ?? throw new ArgumentNullException(nameof(value)); }
    public List<int> Indices { get => indices; set => indices = value ?? throw new ArgumentNullException(nameof(value)); }
}