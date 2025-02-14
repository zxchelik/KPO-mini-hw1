namespace Core.Models;

public abstract class Herbo(string name, int food, int number, int kindness)
    : Animal(name, food, number)
{
    public int Kindness { get; init; } = kindness; // Уровень доброты

    public override string ToString()
    {
        return base.ToString() + $", доброта: {Kindness}";
    }
}