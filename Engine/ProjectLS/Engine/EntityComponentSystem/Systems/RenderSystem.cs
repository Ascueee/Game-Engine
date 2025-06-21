using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Components;
using ProjectLS.Engine.EntityComponentSystem.Components.RenderComponents;
namespace ProjectLS.Engine.EntityComponentSystem.Systems;

public class RenderSystem
{
    List<Entity> cubeEntities = new List<Entity>();
    List<Entity> skyboxEntities = new List<Entity>();
    List<Entity> modelEntities = new List<Entity>();
    List<Entity> quadEntities = new List<Entity>();
    List<Entity> directionalLightEntities = new List<Entity>();
    List<Entity> pointLightEntities = new List<Entity>();
    private bool inWireFrameMode = false;
    
    private string[] potentialComponents =
    {
        "CubeRenderer", "SkyboxRenderer",  "ModelRenderer", "QuadRenderer",
        "DirectionalLight", "PointLight"
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

    public void LoadQuad()
    {
        for (int i = 0; i < quadEntities.Count; i++)
        {
            Material material = (Material)quadEntities[i].GetComponent("Material");
            QuadRenderer quadRenderer = (QuadRenderer)quadEntities[i].GetComponent("QuadRenderer");
            
            material.Shader.LoadShader();
            quadRenderer.VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, quadRenderer.VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, quadRenderer.Vertices.Length * sizeof(float),
                quadRenderer.Vertices, BufferUsageHint.StaticDraw);
            
            quadRenderer.VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(quadRenderer.VertexArrayObject);
            
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false,
                5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false,
                5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            
            quadRenderer.VertexBufferObjectInstance = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, quadRenderer.VertexBufferObjectInstance);
            GL.BufferData(BufferTarget.ArrayBuffer, quadEntities[i].EntityInstance.InstancePositions.Count * Vector3.SizeInBytes,
                quadEntities[i].EntityInstance.InstancePositions.ToArray(), BufferUsageHint.StaticDraw);
           
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribDivisor(2, 1);
            
        }
    }
    
    public void LoadModel()
    {
        for (int i = 0; i < modelEntities.Count; i++)
        {
            Console.WriteLine("Loading The model");
            Material material = (Material)modelEntities[i].GetComponent("Material");
            ModelRenderer modelRenderer = (ModelRenderer)modelEntities[i].GetComponent("ModelRenderer");
            
            material.Shader.LoadShader();
            
            modelRenderer.VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, modelRenderer.VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, modelRenderer.Vertices.Count * sizeof(float), modelRenderer.Vertices.ToArray(), BufferUsageHint.StaticDraw);
      
            modelRenderer.VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(modelRenderer.VertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);              
            GL.EnableVertexAttribArray(0);
        
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float)); 
            GL.EnableVertexAttribArray(1);
        
            //NORMALS
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float)); 
            GL.EnableVertexAttribArray(2);
        
            modelRenderer.ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, modelRenderer.ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, modelRenderer.Indices.Count * sizeof(int), modelRenderer.Indices.ToArray(), BufferUsageHint.StaticDraw);
            
            modelRenderer.VertexBufferObjectInstance = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, modelRenderer.VertexBufferObjectInstance);
            GL.BufferData(BufferTarget.ArrayBuffer, modelEntities[i].EntityInstance.InstancePositions.Count * Vector3.SizeInBytes,
                modelEntities[i].EntityInstance.InstancePositions.ToArray(), BufferUsageHint.StaticDraw);
           
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
                
            material.Shader.SetMat4("model", transform.Model);
            material.Shader.SetMat4("view", camera.GetViewMatrix());
            material.Shader.SetMat4("projection", camera.GetProjectionMatrix());
            material.Shader.SetMat3("inverseTranspose", inverseTranspose);
            material.Shader.SetVec3("viewPos", camera.Position);
                
            //lighting shader struct setting for mat and light
            material.Shader.SetVec3("shapeColor", material.ShapeColor);
            material.Shader.SetVec3("mat.ambient", material.ShapeColor);
            material.Shader.SetVec3("mat.diffuse", material.ShapeColor);
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

            for (int i2 = 0; i2 < directionalLightEntities.Count; i2++)
            {
                DirectionalLightComponent directionalLightComponent = 
                    (DirectionalLightComponent)directionalLightEntities[i2].GetComponent("DirectionalLight");
                Transform lightingTransform = (Transform)directionalLightEntities[i2].GetComponent("Transform");
                
                material.Shader.SetVec3("dirLight.direction", lightingTransform.Position);
                material.Shader.SetVec3("dirLight.color", directionalLightComponent.LightColor);
                material.Shader.SetVec3("dirLight.ambient", directionalLightComponent.LightAmbient);
                material.Shader.SetVec3("dirLight.diffuse", directionalLightComponent.LightDiffuse);
                material.Shader.SetVec3("dirLight.specular", directionalLightComponent.LightSpecular);
                material.Shader.SetVec3("mat.specular", directionalLightComponent.LightSpecular);

            }


            for (int i3 = 0; i3 < pointLightEntities.Count; i3++)
            {
                PointLightComponent pointLightComponent = 
                    (PointLightComponent)pointLightEntities[i3].GetComponent("PointLight");
                Transform lightingTransform = (Transform)pointLightEntities[i3].GetComponent("Transform");
                    
                material.Shader.SetVec3($"pointLights[{i3}].position", lightingTransform.Position);
                material.Shader.SetVec3($"pointLights[{i3}].color", pointLightComponent.LightColor);
                material.Shader.SetVec3($"pointLights[{i3}].ambient", pointLightComponent.LightAmbient);
                material.Shader.SetVec3($"pointLights[{i3}].diffuse", pointLightComponent.LightDiffuse);
                material.Shader.SetVec3($"pointLights[{i3}].specular", pointLightComponent.LightSpecular);
                material.Shader.SetFloat($"pointLights[{i3}].constant", pointLightComponent.Constant);
                material.Shader.SetFloat($"pointLights[{i3}].linear", pointLightComponent.Linear);
                material.Shader.SetFloat($"pointLights[{i3}].quadratic", pointLightComponent.Quadratic);

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

    public void DrawQuad(DebugCamera camera)
    {
        for (int i = 0; i < quadEntities.Count; i++)
        {
            Material material = (Material)quadEntities[i].GetComponent("Material");
            Transform transform = (Transform)quadEntities[i].GetComponent("Transform");
            QuadRenderer quadRenderer = (QuadRenderer)quadEntities[i].GetComponent("QuadRenderer");
            
            GL.BindVertexArray(quadRenderer.VertexArrayObject);
            material.Shader.Use();
            
            if (material.DiffuseTexture is not null)
            {
                material.Shader.SetInt("baseTex", 0);
                material.DiffuseTexture.Use(TextureUnit.Texture0);
            }
            
            material.Shader.SetMat4("model", transform.Model);
            material.Shader.SetMat4("view", camera.GetViewMatrix());
            material.Shader.SetMat4("projection", camera.GetProjectionMatrix());
            
            if (inWireFrameMode == true)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
            else
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }
            
            GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, quadRenderer.Vertices.Length / 3,
                quadEntities[i].EntityInstance.InstancePositions.Count);
            
        }
    }
    
    public void DrawModel(DebugCamera camera)
    {
        for (int i = 0; i < modelEntities.Count; i++)
        {

            Material material = (Material)modelEntities[i].GetComponent("Material");
            Transform transform = (Transform)modelEntities[i].GetComponent("Transform");
            
            ModelRenderer modelRenderer = (ModelRenderer)modelEntities[i].GetComponent("ModelRenderer");
            //Console.WriteLine($"Number of entities Visible: {visibleInstanceIndices.Count}");
            GL.BindVertexArray(modelRenderer.VertexArrayObject);
            material.Shader.Use();

            
            Matrix4 transposedInvertedModelMatrix = Matrix4.Transpose(Matrix4.Invert(transform.GetModelMatrix()));  

            Matrix3 inverseTranspose = new Matrix3(
                transposedInvertedModelMatrix.M11, transposedInvertedModelMatrix.M12, transposedInvertedModelMatrix.M13,
                transposedInvertedModelMatrix.M21, transposedInvertedModelMatrix.M22, transposedInvertedModelMatrix.M23,
                transposedInvertedModelMatrix.M31, transposedInvertedModelMatrix.M32, transposedInvertedModelMatrix.M33
            );
            
            material.Shader.SetMat4("model", transform.Model);
            material.Shader.SetMat4("view", camera.GetViewMatrix());
            material.Shader.SetMat4("projection", camera.GetProjectionMatrix());
            material.Shader.SetMat3("inverseTranspose", inverseTranspose);
            material.Shader.SetVec3("viewPos", camera.Position);
            
            material.Shader.SetVec3("shapeColor", material.ShapeColor);
            material.Shader.SetVec3("mat.ambient", material.ShapeColor);
            material.Shader.SetVec3("mat.diffuse", material.ShapeColor);
           
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
            

            for (int i2 = 0; i2 < directionalLightEntities.Count; i2++)
            {
                DirectionalLightComponent directionalLightComponent = 
                    (DirectionalLightComponent)directionalLightEntities[i2].GetComponent("DirectionalLight");
                Transform lightingTransform = (Transform)directionalLightEntities[i2].GetComponent("Transform");
                
                material.Shader.SetVec3("dirLight.direction", lightingTransform.Position);
                material.Shader.SetVec3("dirLight.color", directionalLightComponent.LightColor);
                material.Shader.SetVec3("dirLight.ambient", directionalLightComponent.LightAmbient);
                material.Shader.SetVec3("dirLight.diffuse", directionalLightComponent.LightDiffuse);
                material.Shader.SetVec3("dirLight.specular", directionalLightComponent.LightSpecular);
                material.Shader.SetVec3("mat.specular", directionalLightComponent.LightSpecular);

            }


            for (int i3 = 0; i3 < pointLightEntities.Count; i3++)
            {
                PointLightComponent pointLightComponent = 
                    (PointLightComponent)pointLightEntities[i3].GetComponent("PointLight");
                Transform lightingTransform = (Transform)pointLightEntities[i3].GetComponent("Transform");
                    
                material.Shader.SetVec3($"pointLights[{i3}].position", lightingTransform.Position);
                material.Shader.SetVec3($"pointLights[{i3}].color", pointLightComponent.LightColor);
                material.Shader.SetVec3($"pointLights[{i3}].ambient", pointLightComponent.LightAmbient);
                material.Shader.SetVec3($"pointLights[{i3}].diffuse", pointLightComponent.LightDiffuse);
                material.Shader.SetVec3($"pointLights[{i3}].specular", pointLightComponent.LightSpecular);
                material.Shader.SetFloat($"pointLights[{i3}].constant", pointLightComponent.Constant);
                material.Shader.SetFloat($"pointLights[{i3}].linear", pointLightComponent.Linear);
                material.Shader.SetFloat($"pointLights[{i3}].quadratic", pointLightComponent.Quadratic);

            }
            
            if (inWireFrameMode == true)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
            else
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }
            
            GL.DrawElementsInstanced(PrimitiveType.Triangles, modelRenderer.Indices.Count, DrawElementsType.UnsignedInt, IntPtr.Zero, 
                modelEntities[i].EntityInstance.InstancePositions.Count 
            );
        }
    }

    public void DrawSkyBox(DebugCamera camera){
        
        for (int i = 0; i < skyboxEntities.Count; i++)
        {
            Material material = (Material)skyboxEntities[i].GetComponent("Material");
            
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
    
    public List<Entity> DirectionalLightEntities { get => directionalLightEntities; set => directionalLightEntities = value; }
    public List<Entity> PointLightEntities { get => pointLightEntities; set => pointLightEntities = value; }
    public List<Entity> SkyboxEntities { get => skyboxEntities; set => skyboxEntities = value; }
    public List<Entity> CubeEntities { get => cubeEntities; set => cubeEntities = value; }
    public List<Entity> QuadEntities { get => quadEntities; set => quadEntities = value; }
    public List<Entity> ModelEntities { get => modelEntities; set => modelEntities = value; }
    public string[] PotentialComponents {get => potentialComponents;}
    public bool InWireFrameMode { get => inWireFrameMode; set => inWireFrameMode = value; }
}