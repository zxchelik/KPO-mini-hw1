using Core;
using Core.Enums;
using Core.Interfaces;
using Core.Models;

namespace Tests;

// Тестовая реализация IVetClinic, позволяющая задать результат проверки здоровья
public class FakeVetClinic(HealthStates expectedState) : IVetClinic
{
    public bool CheckHealth(Animal animal, HealthStates state)
    {
        return state == expectedState;
    }
}

public class ZooTests
{
    [Fact]
    public void AdmitAnimal_HealthyAnimal_ShouldBeAcceptedAndAssignedNumber()
    {
        // Arrange
        var vetClinic = new FakeVetClinic(HealthStates.Healthy);
        var zoo = new Zoo(vetClinic);
        var rabbit = new Rabbit("Test Rabbit", 5, 7); // 5 кг еды, 7 - уровень доброты
        
        bool result = zoo.AdmitAnimal(rabbit, () => HealthStates.Healthy);

        // Assert
        Assert.True(result);
        Assert.NotEqual(0, rabbit.Number); // номер должен быть назначен
        Assert.Contains(rabbit, zoo.Inventories);
    }

    [Fact]
    public void AdmitAnimal_UnhealthyAnimal_ShouldNotBeAccepted()
    {
        // Arrange
        var vetClinic = new FakeVetClinic(HealthStates.Healthy);
        var zoo = new Zoo(vetClinic);
        var tiger = new Tiger("Test Tiger", 10); // 10 кг еды
        
        bool result = zoo.AdmitAnimal(tiger, () => HealthStates.Unhealthy);

        // Assert
        Assert.False(result);
        Assert.DoesNotContain(tiger, zoo.Inventories);
    }

    [Fact]
    public void TotalFoodConsumption_ShouldReturnSumOfFoodForAnimals()
    {
        // Arrange
        var vetClinic = new FakeVetClinic(HealthStates.Healthy);
        var zoo = new Zoo(vetClinic);
        var rabbit = new Rabbit("Rabbit", 2, 6); // 2 кг еды
        var tiger = new Tiger("Tiger", 8); // 8 кг еды
        zoo.AdmitAnimal(rabbit, () => HealthStates.Healthy);
        zoo.AdmitAnimal(tiger, () => HealthStates.Healthy);

        // Act
        int totalFood = zoo.TotalFoodConsumption();

        // Assert
        Assert.Equal(10, totalFood); // 2 + 8 = 10 кг
    }

    [Fact]
    public void GetInteractiveAnimals_ShouldReturnOnlyHerboWithHighKindness()
    {
        // Arrange
        var vetClinic = new FakeVetClinic(HealthStates.Healthy);
        var zoo = new Zoo(vetClinic);
        var rabbit1 = new Rabbit("Rabbit1", 2, 7); // интерактивное (доброта = 7)
        var rabbit2 = new Rabbit("Rabbit2", 3, 4); // не интерактивное (доброта = 4)
        zoo.AdmitAnimal(rabbit1, () => HealthStates.Healthy);
        zoo.AdmitAnimal(rabbit2, () => HealthStates.Healthy);

        // Act
        var interactiveAnimals = zoo.GetInteractiveAnimals().ToList();

        // Assert
        Assert.Single(interactiveAnimals);
        Assert.Contains(rabbit1, interactiveAnimals);
        Assert.DoesNotContain(rabbit2, interactiveAnimals);
    }

    [Fact]
    public void PrintInventory_ShouldReturnCorrectString()
    {
        // Arrange
        var vetClinic = new FakeVetClinic(HealthStates.Healthy);
        var zoo = new Zoo(vetClinic);
        var rabbit = new Rabbit("Rabbit", 2, 7);
        var tiger = new Tiger("Tiger", 8);
        zoo.AdmitAnimal(rabbit, () => HealthStates.Healthy);
        zoo.AdmitAnimal(tiger, () => HealthStates.Healthy);

        // Act
        string output = zoo.PrintInventory();

        // Assert – проверяем, что строка содержит имена животных и их номера
        Assert.Contains("Rabbit", output);
        Assert.Contains("Tiger", output);
        Assert.Contains("№", output);
    }
}