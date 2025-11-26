using System;

public static class Task2S
{
    public static void Task2()
    {
        int count = InputHelper.ReadPositiveInt("Введите количество элементов в связном списке: ");
        if (count < 2)
        {
            Console.WriteLine("Слишком мало элементов! Нужно как минимум 2 элемента.");
            return;
        }

        LinkedList<string> list = new LinkedList<string>();
        Console.WriteLine($"Введите {count} элементов:");
        for (int i = 0; i < count; i++)
        {
            string element = InputHelper.ReadNonEmptyString($"Элемент {i + 1}: ");
            list.AddLast(element);
        }

        bool found = false;
        LinkedListNode<string> current = list.First;

        while (current != null)
        {
            LinkedListNode<string> next;
            if (current.Next == null)
            {
                // Для последнего элемента следующий - первый (кольцевая проверка)
                next = list.First;
            }
            else
            {
                next = current.Next;
            }

            if (current.Value.Equals(next.Value))
            {
                found = true;
                break;
            }

            current = current.Next;
        }

        if (found)
        {
            Console.WriteLine("В списке есть равные соседние элементы");
        }
        else
        {
            Console.WriteLine("В списке нет равных соседних элементов");
        }
    }
}