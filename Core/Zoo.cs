using Core.Enums;
using Core.Interfaces;
using Core.Models;

namespace Core;

public class Zoo(IVetClinic vetClinic)
{
    private List<Animal> Animals { get; } = [];
    public List<IInventory> Inventories { get; } = [];

    // Метод для принятия нового животного в зоопарк
    public bool AdmitAnimal(Animal animal, Func<HealthStates> getAnimalHealth)
    {
        if (!vetClinic.CheckHealth(animal, getAnimalHealth())) return false;
        Animals.Add(animal);
        return true;
    }

    // Подсчёт общего количества еды
    public int TotalFoodConsumption() => Animals.Sum(a => a.Food);

    // Список животных, пригодных для контактного зоопарка (травоядные с добротой > 5)
    public IEnumerable<Herbo> GetInteractiveAnimals() =>
        Animals.OfType<Herbo>().Where(a => a.Kindness > 5);

    // Вывод инвентаризации
    public string PrintInventory()
    {
        var output = "Инвентарный учёт объектов зоопарка:";

        foreach (var item in Inventories)
        {
            // Если это животное, выводим его название и номер
            if (item is Animal animal)
            {
                 output += $"\n{animal}";
            }
            else
            {
                output += $"\n{item}";
            }
        }

        return output;
    }
}