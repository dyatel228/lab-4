using System;
using System.IO;
public static class Collections
{
    private static int ReadInt(string prompt)
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

    private static int ReadPositiveInt(string prompt)
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
    private static string ReadNonEmptyString(string prompt)
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

    // Задание 1: List - удаляем элемент после каждого вхождения E
    public static void Task1()
    {
        int count = ReadPositiveInt("Введите количество чисел в списке: ");
        List<int> list = new List<int>();
        Console.WriteLine($"Введите {count} чисел:");
        for (int i = 0; i < count; i++)
        {
            int number = ReadInt($"Число {i + 1}: ");
            list.Add(number);
        }

        int E = ReadInt("Введите элемент E: ");

        List<int> result = new List<int>();

        for (int i = 0; i < list.Count; i++)
        {
            result.Add(list[i]);

            if (list[i] == E && i + 1 < list.Count && list[i + 1] != E)
            {
                i++; // Пропускаем следующий элемент
            }
        }

        Console.WriteLine("\nИсходный список: " + string.Join(" ", list));
        Console.WriteLine("Результат: " + string.Join(" ", result));
    }

    // Задание 2: LinkedList - проверяем, есть ли элемент равный следующему
    public static void Task2()
    {
        int count = ReadPositiveInt("Введите количество элементов в связном списке: ");
        if (count < 2)
        {
            Console.WriteLine("Слишком мало элементов! Нужно как минимум 2 элемента.");
            return;
        }
        LinkedList<int> list = new LinkedList<int>();
        Console.WriteLine($"Введите {count} элементов:");
        for (int i = 0; i < count; i++)
        {
            int number = ReadInt($"Элемент {i + 1}: ");
            list.AddLast(number);
        }

        bool found = false;
        LinkedListNode<int> current = list.First;

        while (current != null)
        {
            LinkedListNode<int> next;
            if (current.Next == null)
            {
                next = list.First;
            }
            else
            {
                next = current.Next;
            }

            if (current.Value == next.Value)
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

    // Задание 3: HashSet - анализ стран по туристам
    public static void Task3()
    {
        int countryCount = ReadPositiveInt("Введите количество стран: ");
        HashSet<string> allCountries = new HashSet<string>();
        Console.WriteLine($"Введите {countryCount} названий стран:");
        for (int i = 0; i < countryCount; i++)
        {
            while (true)
            {
                string country = ReadNonEmptyString($"Страна {i + 1}: ");
                // Проверяем, что страна не повторяется
                if (allCountries.Contains(country))
                {
                    Console.WriteLine("Ошибка! Такая страна уже была введена.");
                    continue;
                }

                allCountries.Add(country);
                break;
            }
        }

        // Пользователь вводит количество туристов
        int touristCount = ReadPositiveInt("Введите количество туристов: ");

        Dictionary<string, HashSet<string>> tourists = new Dictionary<string, HashSet<string>>();

        for (int i = 0; i < touristCount; i++)
        {
            string name = ReadNonEmptyString($"Введите имя туриста {i + 1}: ");
            int visitedCount = ReadPositiveInt($"Сколько стран посетил {name}? ");
            HashSet<string> visited = new HashSet<string>();
            Console.WriteLine($"Введите {visitedCount} стран, которые посетил {name}:");

            for (int j = 0; j < visitedCount; j++)
            {
                while (true)
                {
                    string country = ReadNonEmptyString($"Страна {j + 1}: ");
                    // Проверяем, что страна существует в общем списке
                    if (!allCountries.Contains(country))
                    {
                        Console.WriteLine($"Ошибка! Страна '{country}' не найдена в общем списке.");
                        Console.WriteLine("Доступные страны: " + string.Join(", ", allCountries));
                        continue;
                    }

                    // Проверяем, что страна не повторяется у этого туриста
                    if (visited.Contains(country))
                    {
                        Console.WriteLine("Ошибка! Эту страну уже вводили для этого туриста.");
                        continue;
                    }

                    visited.Add(country);
                    break;
                }
            }

            tourists[name] = visited;
        }

        // Собираем все страны, которые посетил хотя бы один турист
        HashSet<string> allVisitedCountries = new HashSet<string>();
        foreach (HashSet<string> touristVisits in tourists.Values)
        {
            allVisitedCountries.UnionWith(touristVisits);
        }

        // Страны, которые посетили ВСЕ туристы
        HashSet<string> visitedByAll = new HashSet<string>(allCountries);
        foreach (HashSet<string> touristVisits in tourists.Values)
        {
            visitedByAll.IntersectWith(touristVisits);
        }

        // Страны, которые никто не посетил
        HashSet<string> visitedByNone = new HashSet<string>(allCountries);
        visitedByNone.ExceptWith(allVisitedCountries);

        // Страны, которые посетили некоторые
        HashSet<string> visitedBySome = new HashSet<string>(allVisitedCountries);
        visitedBySome.ExceptWith(visitedByAll);

        Console.WriteLine("\nРезультаты анализа:");

        if (visitedByAll.Count > 0)
        {
            Console.WriteLine("Страны, которые посетили ВСЕ туристы:");
            foreach (string country in visitedByAll)
            {
                Console.WriteLine($"- {country}");
            }
        }
        else
        {
            Console.WriteLine("Нет стран, которые посетили ВСЕ туристы.");
        }

        if (visitedBySome.Count > 0)
        {
            Console.WriteLine("\nСтраны, которые посетили НЕКОТОРЫЕ туристы:");
            foreach (string country in visitedBySome)
            {
                Console.WriteLine($"- {country}");
            }
        }
        else
        {
            Console.WriteLine("\nНет стран, которые посетили НЕКОТОРЫЕ туристы.");
        }

        if (visitedByNone.Count > 0)
        {
            Console.WriteLine("\nСтраны, которые НИКТО не посетил:");
            foreach (string country in visitedByNone)
            {
                Console.WriteLine($"- {country}");
            }
        }
        else
        {
            Console.WriteLine("\nНет стран, которые НИКТО не посетил.");
        }
    }

    // Задание 4: HashSet - чтение из файла
    public static void Task4()
    {
        string filePath = "text.txt";
        try
        {
            string text = File.ReadAllText(filePath).ToLower();
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Файл пуст!");
                return;
            }

            Console.WriteLine($"Прочитан текст: {text}");
            char[] separators = { ' ', ',', '.', '!', '?', ';', ':', '-', '\n', '\r', '\t', '(', ')', '"' };
            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
            {
                Console.WriteLine("В файле нет слов для анализа!");
                return;
            }

            HashSet<char> deafConsonants = new HashSet<char> { 'п', 'ф', 'к', 'т', 'ш', 'с', 'х', 'ц', 'ч', 'щ' };
            Dictionary<char, int> consonantCount = new Dictionary<char, int>();

            foreach (char consonant in deafConsonants)
            {
                int count = 0;
                foreach (string word in words)
                {
                    if (word.Contains(consonant))
                    {
                        count++;
                    }
                }
                consonantCount[consonant] = count;
            }

            // Собираем согласные, которые встречаются не ровно в одном слове
            HashSet<char> result = new HashSet<char>();
            foreach (char consonant in deafConsonants)
            {
                if (consonantCount[consonant] != 1)
                {
                    result.Add(consonant);
                }
            }

            // Сортируем по алфавиту
            List<char> sortedList = result.ToList();
            sortedList.Sort();

            Console.WriteLine("\nГлухие согласные, которые не входят ровно в одно слово:");
            foreach (char consonant in sortedList)
            {
                Console.WriteLine($"- '{consonant}' (встречается в {consonantCount[consonant]} словах)");
            }

            if (result.Count == 0)
            {
                Console.WriteLine("Таких согласных не найдено.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
        }
    }

    // Задание 5: SortedList - ввод данных пользователем
    public static void Task5()
    {
        // SortedList автоматически сортирует по ключу (фамилии)
        SortedList<string, List<Student>> students = new SortedList<string, List<Student>>();
        int n = ReadPositiveInt("Введите количество абитуриентов: ");
        Console.WriteLine("Введите данные абитуриентов (Фамилия Имя Балл 1 Балл 2):");
        for (int i = 0; i < n; i++)
        {
            while (true)
            {
                Console.Write($"Абитуриент {i + 1}: ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ошибка! Ввод не может быть пустым.");
                    continue;
                }

                string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 4)
                {
                    Console.WriteLine("Ошибка! Недостаточно данных. Формат: Фамилия Имя Балл1 Балл2");
                    continue;
                }

                if (!int.TryParse(parts[2], out int score1) || !int.TryParse(parts[3], out int score2))
                {
                    Console.WriteLine("Ошибка! Баллы должны быть числами.");
                    continue;
                }

                if (score1 < 0 || score1 > 100 || score2 < 0 || score2 > 100)
                {
                    Console.WriteLine("Ошибка! Баллы должны быть от 0 до 100.");
                    continue;
                }

                string lastName = parts[0];
                string firstName = parts[1];

                Student student = new Student(lastName, firstName, score1, score2);

                // Если фамилия уже есть в списке, добавляем к существующему списку
                if (students.ContainsKey(lastName))
                {
                    students[lastName].Add(student);
                }
                else
                {
                    // Если фамилии нет, создаем новый список
                    students[lastName] = new List<Student> { student };
                }
                break;
            }
        }

        bool found = false;

        Console.WriteLine("\nАбитуриенты, не прошедшие в первый поток (отсортировано по фамилии):");

        foreach (KeyValuePair<string, List<Student>> studentGroup in students)
        {
            foreach (Student student in studentGroup.Value)
            {
                if (student.Score1 < 30 || student.Score2 < 30)
                {
                    Console.WriteLine($"{student.LastName} {student.FirstName} - {student.Score1}, {student.Score2}");
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("Все абитуриенты прошли в первый поток!");
        }
    }
}

public class Student
{
    public string LastName;
    public string FirstName;
    public int Score1;
    public int Score2;

    public Student(string lastName, string firstName, int score1, int score2)
    {
        LastName = lastName;
        FirstName = firstName;
        Score1 = score1;
        Score2 = score2;
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            ShowMenu();
            string choice = ReadMenuChoice();
            switch (choice)
            {
                case "1":
                    Collections.Task1();
                    break;
                case "2":
                    Collections.Task2();
                    break;
                case "3":
                    Collections.Task3();
                    break;
                case "4":
                    Collections.Task4();
                    break;
                case "5":
                    Collections.Task5();
                    break;
                case "0":
                    return;
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("МЕНЮ:");
        Console.WriteLine("1. Задание 1 - Удаление элемента после E");
        Console.WriteLine("2. Задание 2 - Поиск равных соседей");
        Console.WriteLine("3. Задание 3 - Анализ стран по туристам");
        Console.WriteLine("4. Задание 4 - Глухие согласные из файла");
        Console.WriteLine("5. Задание 5 - Анализ абитуриентов");
        Console.WriteLine("0. Выход");
    }

    static string ReadMenuChoice()
    {
        while (true)
        {
            Console.Write("\nВыберите задание (1-5) или 0 для выхода: ");
            string choice = Console.ReadLine();
            if (choice == "0" || choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5")
            {
                return choice;
            }
            Console.WriteLine("Ошибка! Введите число от 0 до 5.");
        }
    }
}