using System;

public static class Task2S<T>
{
    public static void Task2()
    {
        int count = InputHelper.ReadPositiveInt("Введите количество элементов в связном списке: ");
        if (count < 2)
        {
            Console.WriteLine("Слишком мало элементов! Нужно как минимум 2 элемента.");
            return;
        }

        LinkedList<T> list = new LinkedList<T>();

        Console.WriteLine($"Введите {count} элементов:");
        for (int i = 0; i < count; i++)
        {
            T element = ReadElement($"Элемент {i + 1}: ");
            list.AddLast(element);
        }

        bool found = CheckEqualNeighborsWithLoop(list);

        if (found)
        {
            Console.WriteLine("В списке есть равные соседние элементы");
        }
        else
        {
            Console.WriteLine("В списке нет равных соседних элементов");
        }
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

    private static bool CheckEqualNeighborsWithLoop(LinkedList<T> list)
    {
        bool found = false;
        LinkedListNode<T> current = list.First;

        while (current != null)
        {
            LinkedListNode<T> next;

            if (current.Next == null)
            {
                next = list.First;
            }
            else
            {
                next = current.Next;
            }

            if (EqualityComparer<T>.Default.Equals(current.Value, next.Value))
            {
                found = true;
                break;
            }

            current = current.Next;
        }

        return found;
    }
}
