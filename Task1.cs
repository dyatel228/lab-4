using System;

public static class Task1S
{
    public static void Task1()
    {
        int count = InputHelper.ReadPositiveInt("Введите количество элементов в списке: ");
        List<string> list = new List<string>();

        Console.WriteLine($"Введите {count} элементов:");
        for (int i = 0; i < count; i++)
        {
            string element = InputHelper.ReadNonEmptyString($"Элемент {i + 1}: ");
            list.Add(element);
        }

        string E = InputHelper.ReadNonEmptyString("Введите элемент E: ");

        List<string> result = new List<string>();

        for (int i = 0; i < list.Count; i++)
        {
            result.Add(list[i]);

            // Если текущий элемент равен E, и есть следующий элемент, и он не равен E
            if (list[i].Equals(E) && i + 1 < list.Count && !list[i + 1].Equals(E))
            {
                i++; // Пропускаем следующий элемент
            }
        }

        Console.WriteLine("\nИсходный список: " + string.Join(", ", list));
        Console.WriteLine("Результат: " + string.Join(", ", result));
    }
}
