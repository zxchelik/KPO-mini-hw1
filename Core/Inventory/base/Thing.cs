using Core.Interfaces;

namespace Core.Inventory;

public class Thing : InventoryItem
{
    public string Name { get; set; }

    public Thing(string name)
    {
        Name = name;
    }

    public override string ToString() => $"{Name} (№{Number})";
}