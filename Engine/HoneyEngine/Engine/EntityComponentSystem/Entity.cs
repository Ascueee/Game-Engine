using OpenTK.Mathematics;
using ProjectLS.Engine.EntityComponentSystem.Components;

namespace ProjectLS.Engine.EntityComponentSystem;


/// <summary>
/// A usable object that can be used in the game world
/// </summary>
public class Entity
{
    private int id;
    private string entityName;
    Dictionary<string, IComponent> components = new Dictionary<string, IComponent>();
    private Instance entityInstance = new Instance();
    public Entity(string entityName)
    {
        this.entityName = entityName;
    }
    //Creates an instance by saving its position to be used for rendering later
    public void Instance(Vector3 instancePosition)
    {
        entityInstance.InstancePositions.Add(instancePosition);
    }
    public void AddComponent(IComponent component)
    {
        components.Add(component.Name, component);
    }

    public void UpdateComponent(IComponent component)
    {
        components[component.Name] = component;
    }
    
    public IComponent GetComponent(string componentName)
    {
        return components[componentName];
    }
    public int Id { get => id; set => id = value; }
    public string EntityName { get => entityName; set => entityName = value; }
    public Instance EntityInstance { get => entityInstance; set => entityInstance = value; }
    public Dictionary<string, IComponent> Components { get => components; set => components = value; }
    
}