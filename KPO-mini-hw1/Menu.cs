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
        zoo.Inventories.Add(new Table("Столовая", 101));
        zoo.Inventories.Add(new Computer("Рабочий компьютер", 102));
    }
    
    public void Run()
    {
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
                    PrintInfo("1");
                    break;
                case 2:
                    PrintInfo("2");
                    break;
                case 3:
                    PrintInfo("3");
                    break;
                case 4:
                    isDone = true;
                    break;
            }
        } while (!isDone);
    }

    private void PrintInfo(string info)
    {
    }

    private void AdmitAnimalMenu()
    {
        var animalType = GetAnimalType();
        if (animalType is null) return;

        var name = ConsoleUI.InputFromUser<string>.Input("Введите имя");
        var number = ConsoleUI.InputFromUser<int>.Input("Введите инвентаризационный номер");
        var food = ConsoleUI.InputFromUser<int>.Input("Введите количество еды (кг/день)");
        Animal? animal = null;
        switch (animalType)
        {
            case AnimalTypes.Monkey:
                animal = new Monkey(name, number, food, GetKindness());
                break;
            case AnimalTypes.Rabbit:
                animal = new Rabbit(name, number, food, GetKindness());
                break;
            case AnimalTypes.Tiger:
                animal = new Tiger(name, number, food);
                break;
            case AnimalTypes.Wolf:
                animal = new Wolf(name, number, food);
                break;
        }
        
        var zoo = _serviceProvider.GetService<Zoo>()!;
        
        var admitted = zoo.AdmitAnimal(animal!,GetHealthStates);
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