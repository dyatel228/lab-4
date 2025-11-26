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
                    Task1S.Task1();
                    break;
                case "2":
                    Task2S.Task2();
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