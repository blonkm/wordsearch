using System;
using System.Net;
using System.IO;

// static class to generate random stuff like ints and chars
static class Randomizer {
  const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  const string api = "https://api.wordnik.com/v4/words.json/randomWords?hasDictionaryDef=true&maxCorpusCount=-1&minDictionaryCount=1&maxDictionaryCount=-1&minLength=5&maxLength=-1&limit=10";
  static Random rand;
  static string[] words;
  private static int wordIndex;

  static Randomizer() {
    rand = new Random();
    words = File.ReadAllText("dictionary.txt").Split(',');
    ShuffleWords();
  }

  public static void ShuffleWords() {
        Random rand = new Random();

        for (int i = 0; i < words.Length - 1; i++)
        {
            int j = rand.Next(i, words.Length);
            string temp = words[i];
            words[i] = words[j];
            words[j] = temp;
        }
  }

  public static char GetLetter()
  {
      int num = rand.Next(0, alphabet.Length -1);
      return alphabet[num];
  }  

  public static int GetInt(int low, int high)
  {
      return rand.Next(low, high+1); // Rand.Next is exclusive high value
  }  

  public static string PickFromList(string[] s)
  {
      int num = rand.Next(0, s.Length -1);
      return s[num];
  }  

  public static string Noun() {
    string text = words[wordIndex];
    wordIndex = (wordIndex+1) % (words.Length);
    return text;
  }

  public static string NounFromWeb() {
    WebClient client = new WebClient();
    string key = "";
    string noun = client.DownloadString(api + "&api_key=" + key);
    return noun;
  }
}
