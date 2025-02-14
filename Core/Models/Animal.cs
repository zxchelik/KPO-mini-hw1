using Core.Interfaces;

namespace Core.Models;

public abstract class Animal(string name, int food, int number) : IAlive, IInventory
{
    public string Name { get; set; } = name;
    public int Food { get; set; } = food;
    public int Number { get; set; } = number;
    public bool IsHealthy { get; set; } = true;

    public override string ToString()
    {
        return $"{Name} (№{Number}) – еда: {Food} кг/день";
    }
}