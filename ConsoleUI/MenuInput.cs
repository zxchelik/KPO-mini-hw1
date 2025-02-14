namespace ConsoleUI;

/// <summary>
/// Осуществляет выбор пользователя из некоторых опций.
/// </summary>
public class MenuInput
{
    private string Header { get; } = "";
    private string[] Buttons { get; } = [];
    private DateTime LastNum { get; set; }
    private int MaxRow { get; set; }
    private int Choice { get; set; }
    private int TopIndex { get; set; }
    private int Temp { get; set; }
    private bool IsNumerable { get; }


    private int RowsQuantity => (Console.WindowHeight - 1) / MaxRow;

    private int BotIndex
    {
        set => TopIndex = Math.Max(0, value - RowsQuantity + 1);
        get => Math.Min(TopIndex + RowsQuantity - 1, Buttons.Length - 1);
    }

    public MenuInput(string header, string[] buttons, bool isNumerable = false)
    {
        Header = header;
        Buttons = buttons;
        IsNumerable = isNumerable;
        MaxRow = GetMaxRowsQuantity();
    }


    public MenuInput()
    {
    }

    /// <summary>
    /// Считает сколько элеметнов помещается на экране
    /// </summary>
    /// <returns>Количество элементов</returns>
    private int GetMaxRowsQuantity()
    {
        int max = -10;

        foreach (string button in Buttons)
        {
            max = max > button.Split("\n").Length ? max : button.Split("\n").Length;
        }

        return max;
    }

    
    /// <summary>
    /// Функция ответственная за отображение данных.
    /// </summary>
    private void Print()
    {
        while (BotIndex == -1)
        {
            Console.Clear();
            Console.WriteLine("Окно консоли слишком маленькое. Увеличте его и нажмите любую клавишу.");
            Console.ReadKey();
        }

        Console.Clear();
        Console.WriteLine(Header);

        for (int i = TopIndex; i < BotIndex + 1; i++)
        {
            if (i == Choice)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(IsNumerable ? $"[{i + 1}] " : "\u25b6 ");
                Console.WriteLine(Buttons[i]);
                Console.ResetColor();
            }
            else
            {
                Console.Write(IsNumerable ? $"[{i + 1}]" : " ");
                Console.WriteLine(Buttons[i]);
            }
        }
    }

    /// <summary>
    /// Считывает и обрабатывает нажатия пользователя
    /// </summary>
    /// <returns>Индекс выбранного элемента</returns>
    public int Process()
    {
        Console.CursorVisible = false;
        do
        {
            Print();

            var key = Console.ReadKey().Key;

            if (key is >= ConsoleKey.D0 and <= ConsoleKey.D9 && IsNumerable)
            {
                if ((DateTime.Now - LastNum).TotalSeconds <= 1)
                {
                    Temp = (Temp % 10) * 10 + key - ConsoleKey.D0;
                }
                else
                {
                    Temp = key - ConsoleKey.D0;
                }

                LastNum = DateTime.Now;

                Choice = Temp <= Buttons.Length && Temp > 0 ? Temp - 1 : Choice;
                if (Choice > BotIndex) BotIndex = Choice;
                if (Choice < TopIndex) TopIndex = Choice;
                continue;
            }

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (Choice == TopIndex)
                    {
                        TopIndex = TopIndex == 0 ? 0 : TopIndex - 1;
                        Choice = TopIndex;
                    }
                    else
                    {
                        Choice--;
                    }

                    break;
                case ConsoleKey.DownArrow:
                    if (Choice == BotIndex)
                    {
                        BotIndex = BotIndex != Buttons.Length - 1 ? BotIndex + 1 : Buttons.Length - 1;
                        Choice = BotIndex;
                    }
                    else
                    {
                        Choice++;
                    }

                    break;
                case ConsoleKey.Enter:
                    Console.CursorVisible = true;
                    Console.Clear();
                    return Choice;
            }
        } while (true);
    }
}