using Core.Interfaces;

namespace Core.Inventory;

public abstract class InventoryItem : IInventory
{

    // Номер назначается автоматически при создании экземпляра
    public int Number { get; set; } = 0;

}