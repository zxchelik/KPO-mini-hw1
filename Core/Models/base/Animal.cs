using Core.Interfaces;
using Core.Inventory;

namespace Core.Models;

public abstract class Animal(string name, int food) : InventoryItem, IAlive
{
    public string Name { get; set; } = name;
    public int Food { get; set; } = food;
    public bool IsHealthy { get; set; } = true;

    public override string ToString()
    {
        return $"{Name} (№{Number}) – еда: {Food} кг/день";
    }
}