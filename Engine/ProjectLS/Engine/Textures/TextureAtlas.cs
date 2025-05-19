using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using StbImageSharp;

namespace ProjectLS.Engine.EntityComponentSystem.Textures;

/// <summary>
/// At the start of the game generates a texture atlas from a png image for the material component to use to render
/// textures onto blocks
/// </summary>
public class TextureAtlas
{
    private int handle;
    private string textureAtlasPath;
    
    //dimensions for the atlas
    int textureAtlasWidth;
    int textureAtlasHeight;
    
    //dimensions for each individual texture
    private int textureWidth;
    private int textureHeight;

    public TextureAtlas(string textureAtlasPath, int textureWidth, int textureHeight)
    {
        this.textureAtlasPath = textureAtlasPath;
        this.textureWidth = textureWidth;
        this.textureHeight = textureHeight;
    }
    
    public void GenerateAtlasTexture()
    {
        handle = GL.GenTexture();
        
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, handle);
        
        
        StbImage.stbi_set_flip_vertically_on_load(1);
        using (Stream stream = File.OpenRead(textureAtlasPath))
        {
            ImageResult textureAtlas = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            
            textureAtlasWidth = textureAtlas.Width;
            textureAtlasHeight = textureAtlas.Height;
            
            //uploads the texture
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, textureAtlas.Width, textureAtlas.Height,
                0, PixelFormat.Rgba, PixelType.UnsignedByte, textureAtlas.Data);
        }
        
        //texture settings
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
    }

    public void Use(TextureUnit unit)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }
    
    public Vector2[] GetTextureCoords(int x, int y)
    {
        float u0 = ((float)x) / textureAtlasWidth;
        float v0 = ((float)y) / textureAtlasHeight;
        float u1 = ((float)(x + textureWidth)) / textureAtlasWidth;
        float v1 = ((float)(y + textureHeight)) / textureAtlasHeight;
        
        return new Vector2[]
        {
            new Vector2(u0, v0), // Bottom-left
            new Vector2(u1, v0), // Bottom-right
            new Vector2(u0, v1), // Top-left

            new Vector2(u0, v1), // Top-left
            new Vector2(u1, v0), // Bottom-right
            new Vector2(u1, v1)  // Top-right
        };
    }
    
    public Vector2[] GetTextureCorners(int x, int y)
    {
        float u0 = (float)x / textureAtlasWidth;
        float v0 = (float)(y + textureHeight) / textureAtlasHeight; 
        float u1 = (float)(x + textureWidth) / textureAtlasWidth;
        float v1 = (float)y / textureAtlasHeight; 

        return new Vector2[]
        {
            new Vector2(u0, v1), // Bottom-left
            new Vector2(u1, v1), // Bottom-right
            new Vector2(u1, v0), // Top-right
            new Vector2(u0, v0)  // Top-left
        };
    }
    
    public int Handle {get => handle; set => handle = value;}
}