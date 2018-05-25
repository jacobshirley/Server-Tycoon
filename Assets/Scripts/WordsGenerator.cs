using System.Globalization;
using System.IO;
using UnityEngine;

public static class WordsGenerator
{
    private static string[] nouns = File.ReadAllLines("Assets/JSON/nouns.txt");
    private static string[] adjectives = File.ReadAllLines("Assets/JSON/adjectives.txt");

    public static string[] GetRandomNouns(int number)
    {
        string[] result = new string[number];
        for (int i = 0; i < number; i++)
        {
            result[i] = nouns[(int)(Random.value * nouns.Length)];
        }

        return result;
    }

    public static string[] GetRandomAdjectives(int number)
    {
        string[] result = new string[number];
        for (int i = 0; i < number; i++)
        {
            result[i] = adjectives[(int)(Random.value * adjectives.Length)];
        }

        return result;
    }

    public static string GetInterestingWord(int adjectiveAmount)
    {
        return string.Join(" ", GetRandomAdjectives(adjectiveAmount)) + " " + GetRandomNouns(1)[0];
    }

    public static string GetJoinedWord(int adjectiveAmount)
    {
        return ToTitleCase(GetInterestingWord(adjectiveAmount)).Replace(" ", "");
    }

    public static string ToTitleCase(string title)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title);
    }
}
