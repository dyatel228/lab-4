using System;

public static class Task1S<T>
{
    public static void Task1()
    {
        int count = InputHelper.ReadPositiveInt("Введите количество элементов в списке: ");
        List<T> list = new List<T>();

        Console.WriteLine($"Введите {count} элементов:");
        for (int i = 0; i < count; i++)
        {
            T element = ReadElement($"Элемент {i + 1}: ");
            list.Add(element);
        }

        T E = ReadElement("Введите элемент E: ");
        List<T> result = ProcessList(list, E);

        Console.WriteLine("\nИсходный список: " + string.Join(", ", list));
        Console.WriteLine("Результат: " + string.Join(", ", result));
    }

    private static T ReadElement(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            try
            {
                T result = (T)Convert.ChangeType(input, typeof(T));
                return result;
            }
            catch (Exception)
            {
                Console.WriteLine($"Ошибка! Введите элемент корректного типа ({typeof(T).Name}).");
            }
        }
    }

    private static List<T> ProcessList(List<T> list, T E)
    {
        List<T> result = new List<T>();

        for (int i = 0; i < list.Count; i++)
        {
            result.Add(list[i]);

            if (list[i].Equals(E) && i + 1 < list.Count && !list[i + 1].Equals(E))
            {
                i++;
            }
        }

        return result;
    }
}
