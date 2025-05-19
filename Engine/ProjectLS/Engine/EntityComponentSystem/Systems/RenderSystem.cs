
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Components;
using ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;
namespace ProjectLS.Engine.EntityComponentSystem.Systems;

public class RenderSystem
{
    List<Entity> cubeEntities = new List<Entity>();
    List<Entity> skyboxEntities = new List<Entity>();
    List<Entity> lightEntities = new List<Entity>();
   
    private bool inWireFrameMode = false;
    
    
    private string[] potentialComponents =
    {
        "CubeRenderer", "SkyboxRenderer", "LightingObject"
    };

    public void LoadCube()
    {
        for (int i = 0; i < cubeEntities.Count; i++)
        {
            int widthOfArray = 8;
            Material material = (Material)cubeEntities[i].GetComponent("Material");
            CubeRenderer cubeRenderer = (CubeRenderer)cubeEntities[i].GetComponent("CubeRenderer");

            //Creates and binds a VBO for the vertices of the cube
            material.Shader.LoadShader();
            cubeRenderer.VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, cubeRenderer.VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, cubeRenderer.Vertices.Length * sizeof(float),
                cubeRenderer.Vertices, BufferUsageHint.StaticDraw);

            //Set up VAO
            cubeRenderer.VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(cubeRenderer.VertexArrayObject);
            
            Console.WriteLine(cubeRenderer.Vertices.Length);
            if (cubeRenderer.Vertices.Length == 216)
            {
                widthOfArray = 6;
            }
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false
                , widthOfArray * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false
                , widthOfArray * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
                
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false
                , widthOfArray * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(2);
            
            cubeRenderer.VertexBufferObjectInstance = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, cubeRenderer.VertexBufferObjectInstance);
            GL.BufferData(BufferTarget.ArrayBuffer, cubeEntities[i].EntityInstance.InstancePositions.Count * Vector3.SizeInBytes,
                cubeEntities[i].EntityInstance.InstancePositions.ToArray(), BufferUsageHint.StaticDraw);
           
            GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
            GL.EnableVertexAttribArray(3);
            GL.VertexAttribDivisor(3, 1);
            

        }
    }

    public void LoadSkyBox()
    {
        for (int i = 0; i < skyboxEntities.Count; i++)
        {
            Material material = (Material)skyboxEntities[i].GetComponent("Material");
            material.Shader.LoadShader();
         
            if (skyboxEntities[i].Components.ContainsKey("SkyboxRenderer"))
            {

                SkyboxRenderer skyboxRenderer = (SkyboxRenderer)skyboxEntities[i].GetComponent("SkyboxRenderer");

                skyboxRenderer.VertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, skyboxRenderer.VertexBufferObject);
                
                
                GL.BufferData(BufferTarget.ArrayBuffer, skyboxRenderer.Vertices.Length * sizeof(float), 
                    skyboxRenderer.Vertices, BufferUsageHint.StaticDraw);

                skyboxRenderer.VertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(skyboxRenderer.VertexArrayObject);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            }
        }

    }

    public void DrawCube(DebugCamera camera)
    {
        for (int i = 0; i < cubeEntities.Count; i++)
        {

            Material material = (Material)cubeEntities[i].GetComponent("Material");
            Transform transform = (Transform)cubeEntities[i].GetComponent("Transform");

            if (cubeEntities[i].Components.ContainsKey("CubeRenderer"))
            {
                CubeRenderer cubeRenderer = (CubeRenderer)cubeEntities[i].GetComponent("CubeRenderer");
                //Console.WriteLine($"Number of entities Visible: {visibleInstanceIndices.Count}");
                GL.BindVertexArray(cubeRenderer.VertexArrayObject);
                material.Shader.Use();

                
                Matrix4 transposedInvertedModelMatrix = Matrix4.Transpose(Matrix4.Invert(transform.GetModelMatrix()));  

                Matrix3 inverseTranspose = new Matrix3(
                    transposedInvertedModelMatrix.M11, transposedInvertedModelMatrix.M12, transposedInvertedModelMatrix.M13,
                    transposedInvertedModelMatrix.M21, transposedInvertedModelMatrix.M22, transposedInvertedModelMatrix.M23,
                    transposedInvertedModelMatrix.M31, transposedInvertedModelMatrix.M32, transposedInvertedModelMatrix.M33
                );
                material.Shader.SetMat4("model", transform.GetModelMatrix());
                material.Shader.SetMat4("view", camera.GetViewMatrix());
                material.Shader.SetMat4("projection", camera.GetProjectionMatrix());
                material.Shader.SetMat3("inverseTranspose", inverseTranspose);
                material.Shader.SetVec3("viewPos", camera.Position);
                
                //lighting shader struct setting for mat and light
                material.Shader.SetVec3("shapeColor", material.ShapeColor);
                material.Shader.SetVec3("mat.ambient", material.ShapeColor);
                material.Shader.SetVec3("mat.diffuse", material.ShapeColor);
                material.Shader.SetVec3("mat.specular", new Vector3(0.3f, 0.3f, 0.3f));
                material.Shader.SetFloat("mat.shininess", 8f);
                
                if (material.DiffuseTexture is not null)
                {
                    material.Shader.SetInt("mat.diffuse", 0);
                    material.DiffuseTexture.Use(TextureUnit.Texture0);
                }
                
                if (material.NormalMapTexture is not null)
                {
                    material.Shader.SetInt("mat.normal", 1);
                    material.NormalMapTexture.Use(TextureUnit.Texture1);
                }
                

                
                foreach (Entity entity in LightEntities)
                {
                    LightingObject lightingObject = (LightingObject)entity.GetComponent("LightingObject");
                    Transform lightingTransform = (Transform)entity.GetComponent("Transform");
                    material.Shader.SetVec3("lightObj.lightPos", lightingTransform.Position);
                    material.Shader.SetVec3("lightObj.ambient", lightingObject.LightAmbient);
                    material.Shader.SetVec3("lightObj.diffuse", lightingObject.LightDiffuse);
                }
                
                if (inWireFrameMode == true)
                {
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                }
                else
                {
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                }
                
                GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, cubeRenderer.Vertices.Length / 3, cubeEntities[i].EntityInstance.InstancePositions.Count);
            }
        }
    }

    public void DrawSkyBox(DebugCamera camera){
        
        for (int i = 0; i < skyboxEntities.Count; i++)
        {
            //Console.WriteLine("Rendering a skybox");
            Material material = (Material)skyboxEntities[i].GetComponent("Material");
            
            if (skyboxEntities[i].Components.ContainsKey("SkyboxRenderer"))
            {
                SkyboxRenderer skyboxRenderer = (SkyboxRenderer)skyboxEntities[i].GetComponent("SkyboxRenderer");
                GL.DepthFunc(DepthFunction.Lequal);
                GL.DepthMask(false);
                GL.BindVertexArray(skyboxRenderer.VertexArrayObject);
                material.Shader.Use();
                material.SkyBoxTexture.Use(TextureUnit.Texture0);

                Matrix4 view = camera.GetViewMatrixSkyBox();
                material.Shader.SetMat4("view", view );
                material.Shader.SetMat4("projection",camera.GetProjectionMatrix());
                
                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
                GL.BindVertexArray(0);
                GL.DepthMask(true);  
                GL.DepthFunc(DepthFunction.Less); 
            }
        }
    }



    public List<Entity> LightEntities { get => lightEntities; set => lightEntities = value; }
    public List<Entity> SkyboxEntities { get => skyboxEntities; set => skyboxEntities = value; }
    public List<Entity> CubeEntities { get => cubeEntities; set => cubeEntities = value; }
    public string[] PotentialComponents {get => potentialComponents;}
    public bool InWireFrameMode { get => inWireFrameMode; set => inWireFrameMode = value; }
}