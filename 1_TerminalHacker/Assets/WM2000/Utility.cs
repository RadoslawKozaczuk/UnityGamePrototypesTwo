using UnityEngine;

public static class StringExtension
{
    public static string Anagram(this string str)
    {
        string attempt = Shuffle(str);
        while (attempt == str)
        {
            attempt = Shuffle(str);
        }
        return attempt;
    }

    // Based on something we got from the web, not re-written for clarity
    private static string Shuffle(string str)
    {
        char[] characters = str.ToCharArray();
        int numberOfCharacters = characters.Length;
		int index = 0;

		while (numberOfCharacters > 1)
        {
            numberOfCharacters--;
            index = Random.Range(0, numberOfCharacters + 1);
            var value = characters[index];
            characters[index] = characters[numberOfCharacters];
            characters[numberOfCharacters] = value;
        }

        return new string(characters);
    }
}