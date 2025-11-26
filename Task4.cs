using System;
using System.IO;

public static class Task4S
{
    public static void Task4()
    {
        string filePath = "text.txt";
        try
        {
            string text = ReadTextFromFile(filePath);
            if (string.IsNullOrWhiteSpace(text)) return;

            string[] words = SplitTextIntoWords(text);
            if (words.Length == 0) return;

            HashSet<char> resultConsonants = AnalyzeConsonants(words);
            DisplayResults(resultConsonants, words);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
        }
    }

    private static string ReadTextFromFile(string filePath)
    {
        string text = File.ReadAllText(filePath).ToLower();
        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Файл пуст!");
        }
        else
        {
            Console.WriteLine($"Прочитан текст: {text}");
        }
        return text;
    }

    private static string[] SplitTextIntoWords(string text)
    {
        char[] separators = { ' ', ',', '.', '!', '?', ';', ':', '-', '\n', '\r', '\t', '(', ')', '"' };
        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        if (words.Length == 0)
        {
            Console.WriteLine("В файле нет слов для анализа!");
        }

        return words;
    }

    private static HashSet<char> AnalyzeConsonants(string[] words)
    {
        HashSet<char> deafConsonants = new HashSet<char> { 'п', 'ф', 'к', 'т', 'ш', 'с', 'х', 'ц', 'ч', 'щ' };
        HashSet<char> consonantsInExactlyOneWord = FindConsonantsInExactlyOneWord(deafConsonants, words);

        return CalculateResultConsonants(deafConsonants, consonantsInExactlyOneWord);
    }

    private static HashSet<char> FindConsonantsInExactlyOneWord(HashSet<char> deafConsonants, string[] words)
    {
        HashSet<char> consonantsInExactlyOneWord = new HashSet<char>();

        foreach (char consonant in deafConsonants)
        {
            if (IsConsonantInExactlyOneWord(consonant, words))
            {
                consonantsInExactlyOneWord.Add(consonant);
            }
        }

        return consonantsInExactlyOneWord;
    }

    private static bool IsConsonantInExactlyOneWord(char consonant, string[] words)
    {
        HashSet<string> wordsWithConsonant = new HashSet<string>();

        foreach (string word in words)
        {
            if (word.Contains(consonant))
            {
                wordsWithConsonant.Add(word);
            }
        }

        return wordsWithConsonant.Count == 1;
    }

    private static HashSet<char> CalculateResultConsonants(HashSet<char> deafConsonants, HashSet<char> consonantsInExactlyOneWord)
    {
        HashSet<char> resultConsonants = new HashSet<char>(deafConsonants);
        resultConsonants.ExceptWith(consonantsInExactlyOneWord);
        return resultConsonants;
    }

    private static void DisplayResults(HashSet<char> resultConsonants, string[] words)
    {
        char[] sortedConsonants = SortConsonants(resultConsonants);

        Console.WriteLine("\nГлухие согласные, которые не входят ровно в одно слово:");
        if (sortedConsonants.Length > 0)
        {
            foreach (char consonant in sortedConsonants)
            {
                int wordCount = CountWordsWithConsonant(consonant, words);
                Console.WriteLine($"- '{consonant}' (встречается в {wordCount} словах)");
            }
        }
        else
        {
            Console.WriteLine("Таких согласных не найдено.");
        }
    }

    private static char[] SortConsonants(HashSet<char> consonants)
    {
        char[] resultArray = new char[consonants.Count];
        consonants.CopyTo(resultArray);
        BubbleSort(resultArray);
        return resultArray;
    }

    private static int CountWordsWithConsonant(char consonant, string[] words)
    {
        HashSet<string> wordsWithConsonant = new HashSet<string>();
        foreach (string word in words)
        {
            if (word.Contains(consonant))
            {
                wordsWithConsonant.Add(word);
            }
        }
        return wordsWithConsonant.Count;
    }

    private static void BubbleSort(char[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    char temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }
}
