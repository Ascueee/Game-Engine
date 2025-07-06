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
    PhysicsSystem physicsSystem = new PhysicsSystem();
    
    int counter = 0;
    private float engineElapsedTime;
    private float engineDeltaTime;
    
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
        
        SortIntoSystems();
    }
    
    //sorts all the componenets into the given systems
    public void SortIntoSystems()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            //TODO: COME BACK TO THIS FOR A BETTER SOLUTION THIS IS LAZY
            
            //FOR THE RENDERER
            if (entities[i].Components.ContainsKey("CubeRenderer"))
                renderSystem.CubeEntities.Add(entities[i]);
            else if (entities[i].Components.ContainsKey("SkyboxRenderer"))
                renderSystem.SkyboxEntities.Add(entities[i]);
            else if(entities[i].Components.ContainsKey("ModelRenderer"))
                renderSystem.ModelEntities.Add(entities[i]);
            else if(entities[i].Components.ContainsKey("QuadRenderer"))
                renderSystem.QuadEntities.Add(entities[i]);
            else if (entities[i].Components.ContainsKey("DirectionalLight"))
                renderSystem.DirectionalLightEntities.Add(entities[i]);
            else if(entities[i].Components.ContainsKey("PointLight"))
                renderSystem.PointLightEntities.Add(entities[i]);
            
            //PHYSICS
            if(entities[i].Components.ContainsKey("BoxCollider"))
                physicsSystem.PhysicsEntities.Add(entities[i]);
                
            
        }
        
        
        //Loads The Render System
        renderSystem.LoadEngine();
    }

    public void RenderUse()
    {
        renderSystem.Render(scene.Camera, engineElapsedTime);
    }

    public void PhysicsUse()
    {
        physicsSystem.Update(engineDeltaTime);
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
    public float EngineElapsedTime { get => engineElapsedTime; set => engineElapsedTime = value; }
    public float EngineDeltaTime {get => engineDeltaTime; set => engineDeltaTime = value; }
}