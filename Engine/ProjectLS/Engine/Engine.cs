using OpenTK.Windowing.GraphicsLibraryFramework;
using ProjectLS.Engine.EntityComponentSystem;
using ProjectLS.Engine.EntityComponentSystem.Systems;
using ProjectLS.LootNShoot;

namespace ProjectLS.Engine;

/// <summary>
/// IS A ENTITY COMPONENET SYSTEM BASED ENGINE:
/// Handles sorting all the entities in the right systems
/// ENTITY: HOLDS AN ID AND COMPONENETS
/// SYSTEM: HOLDS LOGIG SURROUNDING COMPONENTS
/// COMPONENETS: STORES DATA TO BE USED BY THE ENTITY AND SYSTEM
/// </summary>
public class Engine
{
    private Dictionary<int, Entity> entities = new Dictionary<int, Entity>(); // cant hold duplicates
    private Scene scene;
    
    //ENGINE SYSTEMS
    RenderSystem renderSystem = new RenderSystem();
    
    int counter = 0;
    public Engine(Scene scene)
    {
        this.scene = scene;
    }
    
    public void LoadEntities(List<Entity> entitiesList)
    {
        for (int i = 0; i < entitiesList.Count; i++)
        {
            entitiesList[i].Id = i;
            entities.Add(i, entitiesList[i]);
        }
    }
    
    //sorts all the componenets into the given systems
    public void SortIntoSystems()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            for (int r2 = 0; r2 < renderSystem.PotentialComponents.Length; r2++)
            {
                //TODO: COME BACK TO THIS FOR A BETTER SOLUTION THIS IS LAZY
                if (entities[i].Components.ContainsKey(renderSystem.PotentialComponents[r2]))
                {
                    Console.WriteLine($"The Component is: {entities[i].Components[renderSystem.PotentialComponents[r2]]}");
                    if (renderSystem.PotentialComponents[r2].Equals("CubeRenderer"))
                        renderSystem.CubeEntities.Add(entities[i]);
                    else if (renderSystem.PotentialComponents[r2].Equals("SkyboxRenderer"))
                        renderSystem.SkyboxEntities.Add(entities[i]);
                    else if(renderSystem.PotentialComponents[r2].Equals("ModelRenderer"))
                        renderSystem.ModelEntities.Add(entities[i]);
                    else if(renderSystem.PotentialComponents[r2].Equals("QuadRenderer"))
                        renderSystem.QuadEntities.Add(entities[i]);
                    else if (renderSystem.PotentialComponents[r2].Equals("DirectionalLight"))
                        renderSystem.DirectionalLightEntities.Add(entities[i]);
                    else if(renderSystem.PotentialComponents[r2].Equals("PointLight"))
                        renderSystem.PointLightEntities.Add(entities[i]);

                }
            }
        
        }
        
        renderSystem.LoadSkyBox();
        renderSystem.LoadCube();
        renderSystem.LoadModel();
        renderSystem.LoadQuad();
    }

    public void RenderUse()
    {
        //renderSystem.DrawRenderEntities(scene.Camera);
        renderSystem.DrawSkyBox(scene.Camera);
        renderSystem.DrawCube(scene.Camera);
        renderSystem.DrawQuad(scene.Camera);
        renderSystem.DrawModel(scene.Camera);
        

    }
    
    
    //Prints all the entities being used by the ECS
    public void PrintEntities()
    {
        for (int i = 0; i < entities.Count; i++)
            Console.WriteLine($"Entity Id: {entities[i].Id} |  Entity Name: {entities[i].EntityName}");
    }

    public void SetSystemState(KeyboardState input)
    {
        if (input.IsKeyPressed(Keys.L))
            counter++;

        if (counter % 2 == 0)
            renderSystem.InWireFrameMode = false;
        else
            renderSystem.InWireFrameMode = true;
    }

    public RenderSystem RenderSystem => renderSystem;

}