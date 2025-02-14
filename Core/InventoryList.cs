using Core.Interfaces;

namespace Core;

public class InventoryList : List<IInventory>
{
    private static int _counter = 1;

    // Переопределяем метод Add
    public new void Add(IInventory item)
    {
        // Если номер ещё не назначен (например, равен 0)
        if(item.Number == 0)
        {
            item.Number = _counter++;
        }
        base.Add(item);
    }
}