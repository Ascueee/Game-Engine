using OpenTK.Graphics.OpenGL;
using StbImageSharp;

namespace ProjectLS.Engine.EntityComponentSystem.Textures;

public class SkyboxTexture
{
    private int handle;
    private string[] faces;

    public SkyboxTexture(string[] faces)
    {
        this.faces = faces;
    }

    public void GenerateTexture()
    {
        handle = GL.GenTexture();
        GL.BindTexture(TextureTarget.TextureCubeMap, handle);

        for (int i = 0; i < faces.Length; i++)
        {
            using (var stream = File.OpenRead(faces[i]))
            {
                var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                
                
                byte[] flippedData = FlipImageVertically(image.Data, image.Width, image.Height, 4);
                
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgba, 
                    image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, flippedData);
            }
        }

        GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
    }
    
    public byte[] FlipImageVertically(byte[] imageData, int width, int height, int channels)
    {
        byte[] flippedData = new byte[imageData.Length];
        int rowSize = width * channels; 
        
        for (int y = 0; y < height; y++)
        {
            int sourceOffset = y * rowSize;
            int destOffset = (height - 1 - y) * rowSize;
            
            
            Array.Copy(imageData, sourceOffset, flippedData, destOffset, rowSize);
        }

        return flippedData;
    }
    public void Use(TextureUnit unit)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, handle);
    }
}