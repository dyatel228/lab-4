using System;

public static class InputHelper
{
    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            Console.WriteLine("Ошибка! Введите целое число.");
        }
    }

    public static int ReadPositiveInt(string prompt)
    {
        while (true)
        {
            int result = ReadInt(prompt);
            if (result > 0)
            {
                return result;
            }
            Console.WriteLine("Ошибка! Число должно быть > 0.");
        }
    }

    public static string ReadNonEmptyString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }
            Console.WriteLine("Ошибка! Ввод не может быть пустым.");
        }
    }
}
