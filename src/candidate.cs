using System;
using System.Collections.Generic;
using System.Linq;

// a collection of words, their positions and directions
// which might be a solution to the wordsearch puzzle
class Candidate {
  public List<Word> words { get; set; }

  public Candidate(List<Word> words) {
    this.words = words;
    this.words.Sort((x,y) => y.Length() - x.Length()); // sort by length DESC
  }

  public void SortAlphabetical() {
    this.words.Sort((x,y) => x.text.CompareTo(y.text)); 
  }

  public void Print() {
    foreach (Word w in this.words) {
      Console.WriteLine(w.ToString());
    }
  }

  public int TotalLength() {
    return words.Sum(x => x.Length());
  }

  public string ToHtml() {
    string text = "";
    foreach (Word word in this.words)
        text += String.Format("\n<li>{0}</li>", word.text);
    return text;
  }

  public override string ToString() {
    return string.Join("\n", words);
  }
}

