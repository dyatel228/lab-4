using System;
using System.IO;

public static class Task5S
{
    public static void Task5()
    {
        string filePath = "applicants.txt";
        CreateApplicantsFile(filePath);
        ProcessApplicants(filePath);
    }

    private static void CreateApplicantsFile(string filePath)
    {
        try
        {
            Console.WriteLine("Заполнение файла с данными абитуриентов:");
            int n = InputHelper.ReadPositiveInt("Введите количество абитуриентов: ");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(n);
                WriteApplicantsData(writer, n);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
        }
    }

    private static void WriteApplicantsData(StreamWriter writer, int applicantCount)
    {
        for (int i = 0; i < applicantCount; i++)
        {
            string applicantData = ReadApplicantData(i + 1);
            writer.WriteLine(applicantData);
        }
    }

    private static string ReadApplicantData(int applicantNumber)
    {
        while (true)
        {
            Console.Write($"Абитуриент {applicantNumber} (Фамилия Имя Балл1 Балл2): ");
            string input = Console.ReadLine();

            if (ValidateApplicantInput(input, out string validatedData))
            {
                return validatedData;
            }
        }
    }

    private static bool ValidateApplicantInput(string input, out string validatedData)
    {
        validatedData = string.Empty;

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Ошибка! Ввод не может быть пустым.");
            return false;
        }

        // Проверка формата ввода - должно быть ровно 3 пробела
        int spaceCount = CountSpaces(input);
        if (spaceCount != 3)
        {
            Console.WriteLine("Ошибка! Неверный формат. Должно быть: Фамилия Имя Балл1 Балл2 (разделено одним пробелом)");
            return false;
        }

        string[] parts = input.Split(' ');

        // Проверяем, что после разбиения получилось 4 части
        if (parts.Length != 4)
        {
            Console.WriteLine("Ошибка! Неверный формат. Должно быть: Фамилия Имя Балл1 Балл2 (разделено одним пробелом)");
            return false;
        }

        if (parts[0].Length > 20)
        {
            Console.WriteLine($"Ошибка! Фамилия не должна превышать 20 символов. Введено: {parts[0].Length} символов.");
            return false;
        }

        if (parts[1].Length > 15)
        {
            Console.WriteLine($"Ошибка! Имя не должно превышать 15 символов. Введено: {parts[1].Length} символов.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
        {
            Console.WriteLine("Ошибка! Фамилия и имя не могут быть пустыми.");
            return false;
        }

        if (!int.TryParse(parts[2], out int score1) || !int.TryParse(parts[3], out int score2))
        {
            Console.WriteLine("Ошибка! Баллы должны быть числами.");
            return false;
        }

        if (score1 < 0 || score1 > 100 || score2 < 0 || score2 > 100)
        {
            Console.WriteLine("Ошибка! Баллы должны быть от 0 до 100.");
            return false;
        }

        validatedData = $"{parts[0]} {parts[1]} {score1} {score2}";
        return true;
    }

    private static int CountSpaces(string input)
    {
        int count = 0;
        foreach (char c in input)
        {
            if (c == ' ')
            {
                count++;
            }
        }
        return count;
    }

    private static void ProcessApplicants(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не найден!");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            if (!ValidateFileFormat(lines, out int applicantCount)) return;

            SortedList<string, List<Dictionary<string, object>>> applicants = ParseApplicantsData(lines, applicantCount);
            DisplayFailedApplicants(applicants);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке файла: {ex.Message}");
        }
    }

    private static bool ValidateFileFormat(string[] lines, out int applicantCount)
    {
        applicantCount = 0;

        if (lines.Length == 0)
        {
            Console.WriteLine("Файл пуст!");
            return false;
        }

        if (!int.TryParse(lines[0], out applicantCount) || applicantCount <= 0)
        {
            Console.WriteLine("Ошибка в формате файла: неверное количество абитуриентов");
            return false;
        }

        if (lines.Length < applicantCount + 1)
        {
            Console.WriteLine("Ошибка: в файле недостаточно данных");
            return false;
        }

        return true;
    }

    private static SortedList<string, List<Dictionary<string, object>>> ParseApplicantsData(string[] lines, int applicantCount)
    {
        SortedList<string, List<Dictionary<string, object>>> applicants = new SortedList<string, List<Dictionary<string, object>>>();

        for (int i = 1; i <= applicantCount; i++)
        {
            ParseApplicantLine(lines[i], i, applicants);
        }

        return applicants;
    }

    private static void ParseApplicantLine(string line, int lineNumber, SortedList<string, List<Dictionary<string, object>>> applicants)
    {
        // Проверяем формат строки - должно быть ровно 3 пробела
        int spaceCount = CountSpaces(line);
        if (spaceCount != 3)
        {
            Console.WriteLine($"Ошибка в строке {lineNumber}: неверный формат. Должно быть ровно 3 пробела.");
            return;
        }

        string[] parts = line.Split(' ');

        // Проверяем, что после разбиения получилось 4 части
        if (parts.Length != 4)
        {
            Console.WriteLine($"Ошибка в строке {lineNumber}: неверный формат данных.");
            return;
        }

        // Проверка длины фамилии при чтении из файла
        if (parts[0].Length > 20)
        {
            Console.WriteLine($"Ошибка в строке {lineNumber}: фамилия превышает 20 символов.");
            return;
        }

        // Проверка длины имени при чтении из файла
        if (parts[1].Length > 15)
        {
            Console.WriteLine($"Ошибка в строке {lineNumber}: имя превышает 15 символов.");
            return;
        }

        if (!int.TryParse(parts[2], out int score1) || !int.TryParse(parts[3], out int score2))
        {
            Console.WriteLine($"Ошибка в строке {lineNumber}: неверный формат баллов");
            return;
        }

        string lastName = parts[0];
        string firstName = parts[1];

        Dictionary<string, object> applicant = CreateApplicantDictionary(firstName, score1, score2);
        AddApplicantToCollection(applicants, lastName, applicant);
    }

    private static Dictionary<string, object> CreateApplicantDictionary(string firstName, int score1, int score2)
    {
        return new Dictionary<string, object>
        {
            { "FirstName", firstName },
            { "Score1", score1 },
            { "Score2", score2 }
        };
    }

    private static void AddApplicantToCollection(SortedList<string, List<Dictionary<string, object>>> applicants, string lastName, Dictionary<string, object> applicant)
    {
        if (applicants.ContainsKey(lastName))
        {
            applicants[lastName].Add(applicant);
        }
        else
        {
            applicants[lastName] = new List<Dictionary<string, object>> { applicant };
        }
    }

    private static void DisplayFailedApplicants(SortedList<string, List<Dictionary<string, object>>> applicants)
    {
        Console.WriteLine("\nАбитуриенты, не прошедшие в первый поток (отсортировано по фамилии):");
        bool found = false;

        foreach (string lastName in applicants.Keys)
        {
            foreach (Dictionary<string, object> applicant in applicants[lastName])
            {
                if (IsApplicantFailed(applicant))
                {
                    DisplayApplicantInfo(lastName, applicant);
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("Все абитуриенты прошли в первый поток!");
        }
    }

    private static bool IsApplicantFailed(Dictionary<string, object> applicant)
    {
        int score1 = (int)applicant["Score1"];
        int score2 = (int)applicant["Score2"];
        return score1 < 30 || score2 < 30;
    }

    private static void DisplayApplicantInfo(string lastName, Dictionary<string, object> applicant)
    {
        int score1 = (int)applicant["Score1"];
        int score2 = (int)applicant["Score2"];
        string firstName = (string)applicant["FirstName"];
        Console.WriteLine($"{lastName} {firstName} - {score1}, {score2}");
    }
}
