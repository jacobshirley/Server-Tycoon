using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public static class WordsGenerator
{
    private static string[] nouns = Regex.Split(Resources.Load<TextAsset>("JSON/nouns").text, "\n");
    private static string[] adjectives = Regex.Split(Resources.Load<TextAsset>("JSON/adjectives").text, "\n");

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

    public static string ToTitleCase(string s)
    {
        if (s == null) return s;

        string[] words = s.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length == 0) continue;

            char firstChar = char.ToUpper(words[i][0]);
            string rest = "";
            if (words[i].Length > 1)
            {
                rest = words[i].Substring(1).ToLower();
            }
            words[i] = firstChar + rest;
        }
        return string.Join(" ", words);
    }
    }
