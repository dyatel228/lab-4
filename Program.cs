using System;

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
                    ExecuteTask1();
                    break;
                case "2":
                    ExecuteTask2();
                    break;
                case "3":
                    Task3S.Task3();
                    break;
                case "4":
                    Task4S.Task4();
                    break;
                case "5":
                    Task5S.Task5();
                    break;
                case "0":
                    return;
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private static void ExecuteTask1()
    {
        Console.WriteLine("Выберите тип данных для задания 1:");
        Console.WriteLine("1. Целые числа (int)");
        Console.WriteLine("2. Строки (string)");
        Console.WriteLine("3. Дробные числа (double)");

        string typeChoice = ReadTypeChoice();
        switch (typeChoice)
        {
            case "1":
                Task1S<int>.Task1();
                break;
            case "2":
                Task1S<string>.Task1();
                break;
            case "3":
                Task1S<double>.Task1(); 
                break;
        }
    }

    private static void ExecuteTask2()
    {
        Console.WriteLine("Выберите тип данных для задания 2:");
        Console.WriteLine("1. Целые числа (int)");
        Console.WriteLine("2. Строки (string)");
        Console.WriteLine("3. Дробные числа (double)");

        string typeChoice = ReadTypeChoice();
        switch (typeChoice)
        {
            case "1":
                Task2S<int>.Task2();
                break;
            case "2":
                Task2S<string>.Task2();
                break;
            case "3":
                Task2S<double>.Task2(); 
                break;
        }
    }

    private static string ReadTypeChoice()
    {
        while (true)
        {
            Console.Write("Выберите тип (1-3): ");
            string choice = Console.ReadLine();
            if (choice == "1" || choice == "2" || choice == "3")
            {
                return choice;
            }
            Console.WriteLine("Ошибка! Введите число от 1 до 3.");
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
