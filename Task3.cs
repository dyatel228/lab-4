using System;

public static class Task3S
{
    public static void Task3()
    {
        HashSet<string> allCountries = ReadCountries();
        if (allCountries.Count == 0) return;

        HashSet<string>[] touristsVisits = ReadTouristsVisits(allCountries);
        AnalyzeAndDisplayResults(allCountries, touristsVisits);
    }

    private static HashSet<string> ReadCountries()
    {
        HashSet<string> allCountries = new HashSet<string>();
        Console.WriteLine("Ввод стран (для завершения введите 'стоп'):");
        int countryIndex = 1;

        while (true)
        {
            string country = InputHelper.ReadNonEmptyString($"Страна {countryIndex}: ");

            if (country.ToLower() == "стоп")
                break;

            allCountries.Add(country);
            countryIndex++;
        }

        if (allCountries.Count == 0)
        {
            Console.WriteLine("Не введено ни одной страны!");
        }

        return allCountries;
    }

    private static HashSet<string>[] ReadTouristsVisits(HashSet<string> allCountries)
    {
        int touristCount = InputHelper.ReadPositiveInt("\nВведите количество туристов: ");
        HashSet<string>[] touristsVisits = new HashSet<string>[touristCount];

        for (int touristIndex = 0; touristIndex < touristCount; touristIndex++)
        {
            touristsVisits[touristIndex] = ReadTouristVisits(touristIndex + 1, allCountries);
        }

        return touristsVisits;
    }

    private static HashSet<string> ReadTouristVisits(int touristNumber, HashSet<string> allCountries)
    {
        HashSet<string> visited = new HashSet<string>();
        Console.WriteLine($"Введите страны, которые посетил турист {touristNumber} (для завершения введите 'стоп'):");
        int visitedCountryIndex = 1;

        while (true)
        {
            string country = InputHelper.ReadNonEmptyString($"Страна {visitedCountryIndex}: ");

            if (country.ToLower() == "стоп")
                break;

            if (!allCountries.Contains(country))
            {
                Console.WriteLine($"Страна '{country}' не найдена в общем списке!");
                Console.WriteLine("Доступные страны: " + string.Join(", ", allCountries));
                continue;
            }

            visited.Add(country);
            visitedCountryIndex++;
        }

        return visited;
    }

    private static void AnalyzeAndDisplayResults(HashSet<string> allCountries, HashSet<string>[] touristsVisits)
    {
        HashSet<string> visitedByAll = CalculateCountriesVisitedByAll(allCountries, touristsVisits);
        HashSet<string> visitedByAtLeastOne = CalculateCountriesVisitedByAtLeastOne(touristsVisits);
        HashSet<string> visitedBySome = CalculateCountriesVisitedBySome(visitedByAll, visitedByAtLeastOne);
        HashSet<string> visitedByNone = CalculateCountriesVisitedByNone(allCountries, visitedByAtLeastOne);

        DisplayResults(visitedByAll, visitedBySome, visitedByNone);
    }

    private static HashSet<string> CalculateCountriesVisitedByAll(HashSet<string> allCountries, HashSet<string>[] touristsVisits)
    {
        HashSet<string> visitedByAll = new HashSet<string>(allCountries);
        foreach (HashSet<string> touristVisits in touristsVisits)
        {
            visitedByAll.IntersectWith(touristVisits); // Общие страны
        }
        return visitedByAll;
    }

    private static HashSet<string> CalculateCountriesVisitedByAtLeastOne(HashSet<string>[] touristsVisits)
    {
        HashSet<string> visitedByAtLeastOne = new HashSet<string>();
        foreach (HashSet<string> touristVisits in touristsVisits)
        {
            visitedByAtLeastOne.UnionWith(touristVisits); // Все страны всех туристов
        }
        return visitedByAtLeastOne;
    }

    private static HashSet<string> CalculateCountriesVisitedBySome(HashSet<string> visitedByAll, HashSet<string> visitedByAtLeastOne)
    {
        HashSet<string> visitedBySome = new HashSet<string>(visitedByAtLeastOne);
        visitedBySome.ExceptWith(visitedByAll); // Удаление стран всеми посещенных туристов
        return visitedBySome;
    }

    private static HashSet<string> CalculateCountriesVisitedByNone(HashSet<string> allCountries, HashSet<string> visitedByAtLeastOne)
    {
        HashSet<string> visitedByNone = new HashSet<string>(allCountries);
        visitedByNone.ExceptWith(visitedByAtLeastOne); // Удаление страны хотя бы с одним туристом
        return visitedByNone;
    }

    private static void DisplayResults(HashSet<string> visitedByAll, HashSet<string> visitedBySome, HashSet<string> visitedByNone)
    {
        Console.WriteLine("\nРезультаты анализа:");

        DisplayCountryGroup("Страны, которые посетили ВСЕ туристы:", visitedByAll);
        DisplayCountryGroup("Страны, которые посетили НЕКОТОРЫЕ туристы:", visitedBySome);
        DisplayCountryGroup("Страны, которые НИКТО не посетил:", visitedByNone);
    }

    private static void DisplayCountryGroup(string title, HashSet<string> countries)
    {
        Console.WriteLine(title);
        if (countries.Count > 0)
        {
            // Выводим страны в том порядке, в котором они хранятся в HashSet
            foreach (string country in countries)
            {
                Console.WriteLine($"- {country}");
            }
        }
        else
        {
            Console.WriteLine("(нет таких стран)");
        }
    }
}

