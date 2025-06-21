using OpenTK.Graphics.OpenGL;
using StbImageSharp;
namespace HoneyEngine.Engine;

public class Texture
{
    private int handle;
    private string filePath;

    public Texture(string filePath)
    {
        this.filePath = filePath;
    }

    public void LoadTexture()
    {
        handle = GL.GenTexture();

        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, handle);
        
        
        StbImage.stbi_set_flip_vertically_on_load(1);
        
        
        using (Stream stream = File.OpenRead(filePath))
        {
            ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

            GL.TexImage2D(TextureTarget.Texture2D, 0, (PixelInternalFormat)PixelInternalFormat.Rgba,
                image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        }
        
        GL.TexParameter(TextureTarget.Texture2D, 
            (TextureParameterName)TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, 
            (TextureParameterName)TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);


        GL.TexParameter(TextureTarget.Texture2D, 
            (TextureParameterName)TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, 
            (TextureParameterName)TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
    }
    
    public void Use(TextureUnit unit)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, handle);
    }
}