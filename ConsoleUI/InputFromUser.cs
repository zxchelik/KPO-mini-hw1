namespace ConsoleUI;

public static class InputFromUser<T>
{
    /// <summary>
    /// Преобразует строку к необходимому типу
    /// </summary>
    /// <param name="input">Строка которую надо преобразовать</param>
    /// <param name="res"> результат</param>
    /// <returns>Удачно ли получилось преобразовать</returns>
    /// <exception cref="ArgumentException">Если не поддерживается тип T</exception>
    private static bool TryParse(string? input, out T res)
    {
        if (typeof(T) == typeof(double))
        {
            if (double.TryParse(input, out var intValue))
            {
                res = (T)(object)intValue;
                return true;
            }

            res = (T)(object)0;
            return false;
        }

        if (typeof(T) == typeof(int))
        {
            if (int.TryParse(input, out var intValue))
            {
                res = (T)(object)intValue;
                return true;
            }

            res = (T)(object)0;
            return false;
        }

        if (typeof(T) == typeof(string))
        {
            res = (T)(object)(input ?? "");
            return true;
        }


        throw new ArgumentException("Unsupported type");
    }

    /// <summary>
    /// Осуществляет интерфейс пользователя
    /// </summary>
    /// <param name="text">Текст, который видит пользователь</param>
    public static T Input(string text)
    {
        Console.Clear();
        Console.WriteLine(text);
        do
        {
            Console.Write(">>");
            try
            {
                if (TryParse(Console.ReadLine(), out var res)) return res;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(">>input error");
                Console.ResetColor();

                return res;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(">>input error");
                Console.ResetColor();
            }
        } while (true);
    }
}