using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace ProjectLS.Engine.Shaders;

public class Shader
{
    private int handle;
    bool disposed = false;
    string vertexShaderPath;
    string fragmentShaderPath;
    
    public Shader(string vertexShaderPath, string fragmentShaderPath)
    {
        this.vertexShaderPath = vertexShaderPath;
        this.fragmentShaderPath = fragmentShaderPath;
    }
    public void LoadShader()
    {
        int vertexShader;
        int fragmentShader;
        
        //Reads the shader source code from the file paths
        string vertexShaderSource = File.ReadAllText(vertexShaderPath);
        string fragmentShaderSource = File.ReadAllText(fragmentShaderPath);
        
        vertexShader = GL.CreateShader(ShaderType.VertexShader);
        fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        
        vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        
        fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        
        //compiles the shaders to be used and checks for shader compilation errors
        CompileShader(vertexShader);
        CompileShader(fragmentShader);
        
        
        //creates a program
        handle = GL.CreateProgram();
        
        LinkProgram(vertexShader, fragmentShader);
        
        //Detach and the delete the shader for clean up
        GL.DetachShader(handle, vertexShader);
        GL.DetachShader(handle, fragmentShader);
        GL.DeleteShader(fragmentShader);
        GL.DeleteShader(vertexShader);
    }

    
    public void Use()
    {
        GL.UseProgram(handle);
    }

    
    /// <summary>
    /// Compiles the shader and checks if there are any errors with the compilation
    /// </summary>
    /// <param name="shader"></param> represents the shader that's getting passed in for compilation
    void CompileShader(int shader)
    {
        GL.CompileShader(shader);
        GL.GetShader(shader, ShaderParameter.CompileStatus, out int result);

        if (result == 0)
        {
            string infoLog = GL.GetShaderInfoLog(shader);
            Console.WriteLine(infoLog);
        }
        else
        {
            Console.Write(result);
            Console.WriteLine("Compiled Successfully");
        }
    }

    /// <summary>
    /// Attaches the shaders to the program as well as checks for any linking errors
    /// </summary>
    /// <param name="vertexShader"></param>
    /// <param name="fragmentShader"></param>
    void LinkProgram(int vertexShader, int fragmentShader)
    {
        GL.AttachShader(handle, vertexShader);
        GL.AttachShader(handle, fragmentShader);
        
        GL.LinkProgram(handle);
        
        GL.GetProgram(handle, GetProgramParameterName.LinkStatus, out int result);
        if (result == 0)
        {
            string infoLog = GL.GetProgramInfoLog(handle);
            Console.WriteLine(infoLog);
        }
    }

    public void SetVec4(string name, Vector4 vec)
    {
        int location = GL.GetUniformLocation(handle, name);
        
        GL.Uniform4(location, vec.X, vec.Y, vec.Z, vec.W);
    }
    public void SetVec3(string name, Vector3 vec)
    {
        int location = GL.GetUniformLocation(handle, name);
        GL.Uniform3(location, vec.X, vec.Y, vec.Z);
    }

    public void SetVec2(string name, Vector2 vec)
    {
        int location = GL.GetUniformLocation(handle, name);
        GL.Uniform2(location, vec.X, vec.Y);
    }
    

    public void SetBool(string name, bool val)
    {
        int location = GL.GetUniformLocation(handle, name);
        GL.Uniform1(location, val ? 1 : 0);
    }
    
    public void SetFloat(string name, float val)
    {
        int location = GL.GetUniformLocation(handle, name);
        GL.Uniform1(location, val);
    }
    
    public void SetInt(string name, int val)
    {
        int location = GL.GetUniformLocation(handle, name);
        GL.Uniform1(location, val);
    }
    


    public void SetMat4(string name, Matrix4 mat)
    {
        int location = GL.GetUniformLocation(handle, name);
        GL.UniformMatrix4(location, false, ref mat);
    }
    
    public void SetMat3(string name, Matrix3 mat)
    {
        int location = GL.GetUniformLocation(handle, name);
        GL.UniformMatrix3(location, false, ref mat);
    }
    
    //Clean up methods to check if the shader has been properly disposed of
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
        {
            GL.DeleteProgram(handle);

            disposed = true;
        }
    }

    ~Shader()
    {
        if (disposed == false)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}