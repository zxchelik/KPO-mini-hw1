using Core;
using Core.Enums;
using Core.Inventory;
using Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace KPO_mini_hw1;

using ConsoleUI;

public class Menu
{
    private readonly ServiceProvider _serviceProvider = new ServiceCollection()
        .AddSingleton<Core.Interfaces.IVetClinic, Core.Services.VetClinic>()
        .AddSingleton<Zoo>()
        .BuildServiceProvider();

    public Menu()
    {
        var zoo = _serviceProvider.GetService<Zoo>()!;

        // Добавим несколько объектов инвентаря
        zoo.Inventories.Add(new Table("Столовая"));
        zoo.Inventories.Add(new Computer("Рабочий компьютер"));
        zoo.Inventories.Add(new Monkey("Монке", 10, 4));
        zoo.Inventories.Add(new Rabbit("Монке", 20, 10));
        zoo.Inventories.Add(new Tiger("АйТигр", 10));
    }

    public void Run()
    {
        var zoo = _serviceProvider.GetService<Zoo>()!;
        Console.Clear();
        const string header = "--- Меню ---";
        string[] buttons =
        [
            "Принять новое животное",
            "Вывести общий отчёт по потребляемой еде",
            "Вывести список животных для контактного зоопарка",
            "Вывести инвентарный учёт",
            "Выход"
        ];
        var menu = new MenuInput(header, buttons);
        var isDone = false;
        do
        {
            switch (menu.Process())
            {
                case 0:
                    AdmitAnimalMenu();
                    break;
                case 1:
                    PrintInfo($"Общее количество еды для животных: {zoo.TotalFoodConsumption()} кг/день");
                    break;
                case 2:
                    var interactiveAnimals = zoo.GetInteractiveAnimals();
                    var info = interactiveAnimals.Aggregate(
                        "Животные для контактного зоопарка:",
                        (current, animal) => current + $"\n{animal}"
                    );
                    PrintInfo(info);
                    break;
                case 3:
                    PrintInfo(zoo.PrintInventory());
                    break;
                case 4:
                    isDone = true;
                    break;
            }
        } while (!isDone);
    }

    private static void PrintInfo(string info)
    {
        Console.Clear();
        Console.WriteLine(info);
        Console.Write("\nНажмите любую клавишу чтобы продолжить...");
        Console.ReadKey();
    }

    private void AdmitAnimalMenu()
    {
        var animalType = GetAnimalType();
        if (animalType is null) return;

        var name = ConsoleUI.InputFromUser<string>.Input("Введите имя");
        var food = ConsoleUI.InputFromUser<int>.Input("Введите количество еды (кг/день)");
        Animal animal = null!;
        switch (animalType)
        {
            case AnimalTypes.Monkey:
                animal = new Monkey(name, food, GetKindness());
                break;
            case AnimalTypes.Rabbit:
                animal = new Rabbit(name, food, GetKindness());
                break;
            case AnimalTypes.Tiger:
                animal = new Tiger(name, food);
                break;
            case AnimalTypes.Wolf:
                animal = new Wolf(name, food);
                break;
        }

        var zoo = _serviceProvider.GetService<Zoo>()!;

        var info = zoo.AdmitAnimal(animal, GetHealthStates)
            ? $"Животное {animal.Name} принято в зоопарк с номером {animal.Number}."
            : $"Животное {animal.Name} не соответствует требованиям здоровья.";
        PrintInfo(info);
    }

    private static AnimalTypes? GetAnimalType()
    {
        const string header = "Выберите вид животного";
        string[] buttons =
        [
            "Обезьяна",
            "Кролик",
            "Тигр",
            "Волк",
            "Назад"
        ];
        var menu = new MenuInput(header, buttons);
        var select = menu.Process();
        if (select == 4) return null;
        return (AnimalTypes)select;
    }

    private static HealthStates GetHealthStates()
    {
        const string header = "Выберите больной/здоровый";
        string[] buttons =
        [
            "Больной",
            "Здоровый"
        ];
        var menu = new MenuInput(header, buttons);
        return (HealthStates)menu.Process();
    }

    private static int GetKindness()
    {
        var kindness = 0;
        do
        {
            kindness = ConsoleUI.InputFromUser<int>.Input("Введите уровень доброты(0-10)");
        } while (kindness is < 0 or > 10);

        return kindness;
    }
}