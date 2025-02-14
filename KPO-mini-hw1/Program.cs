using Microsoft.Extensions.DependencyInjection;
using Core;
using Core.Inventory;
using Core.Models;

namespace KPO_mini_hw1;

internal abstract class Program
    {
        private static void Main()
        {
            // Настройка DI-контейнера
            var serviceProvider = new ServiceCollection()
                .AddSingleton<Core.Interfaces.IVetClinic, Core.Services.VetClinic>()
                .AddSingleton<Zoo>()
                .BuildServiceProvider();
        
            var zoo = serviceProvider.GetService<Zoo>()!;
        
            // Добавим несколько объектов инвентаря
            zoo.Inventories.Add(new Table("Столовая", 101));
            zoo.Inventories.Add(new Computer("Рабочий компьютер", 102));
        
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- Меню ---");
                Console.WriteLine("1. Принять новое животное");
                Console.WriteLine("2. Вывести общий отчёт по потребляемой еде");
                Console.WriteLine("3. Вывести список животных для контактного зоопарка");
                Console.WriteLine("4. Вывести инвентарный учёт");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();
        
                switch (choice)
                {
                    case "1":
                        AdmitAnimalMenu(zoo);
                        break;
                    case "2":
                        Console.WriteLine($"Общее количество еды для животных: {zoo.TotalFoodConsumption()} кг/день");
                        break;
                    case "3":
                        var interactiveAnimals = zoo.GetInteractiveAnimals();
                        Console.WriteLine("Животные для контактного зоопарка:");
                        foreach (var animal in interactiveAnimals)
                        {
                            Console.WriteLine(animal);
                        }
                        break;
                    case "4":
                        zoo.PrintInventory();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }
            }
        }
        
        // Меню для добавления нового животного
        private static void AdmitAnimalMenu(Zoo zoo)
        {
            Console.WriteLine("Введите тип животного (Rabbit, Monkey, Tiger, Wolf):");
            string type = Console.ReadLine()?.Trim();
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.Write("Введите инвентаризационный номер: ");
            int number = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Введите количество еды (кг/день): ");
            int food = int.Parse(Console.ReadLine() ?? "0");
        
            Animal animal = null;
        
            switch (type?.ToLower())
            {
                case "rabbit":
                    Console.Write("Введите уровень доброты: ");
                    int kindnessR = int.Parse(Console.ReadLine() ?? "0");
                    animal = new Rabbit(name, number, food, kindnessR);
                    break;
                case "monkey":
                    Console.Write("Введите уровень доброты: ");
                    int kindnessM = int.Parse(Console.ReadLine() ?? "0");
                    animal = new Monkey(name, number, food, kindnessM);
                    break;
                case "tiger":
                    animal = new Tiger(name, number, food);
                    break;
                case "wolf":
                    animal = new Wolf(name, number, food);
                    break;
                default:
                    Console.WriteLine("Неизвестный тип животного.");
                    return;
            }

            // bool admitted = zoo.AdmitAnimal(animal,); 
            // if (admitted)
            // {
            //     // Добавляем животное в инвентаризацию
            //     zoo.Inventories.Add(animal);
            // }
            // var menu = new Menu();
            // menu.Run();
        }
    }