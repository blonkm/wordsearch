using System;
using System.IO;
using System.Text;

// a combination of a board and a list of words
// forming a possible solution to the puzzle
class Composition {
  const int maxIterations = 1000;

  public double score { get; set;} 
  public double percentageSolved { get; set; }
  public Board board;
  public Candidate candidate;

  public Composition(Board board, Candidate candidate) {
    this.board = board;
    this.candidate = candidate;
    this.score = 0.0;
    this.percentageSolved = 0.0;
    this.FillBoard('.');
  }

  public void Test() {
    this.FillBoard('.');
    this.board.Reposition(candidate.words[0]);
    this.PlaceWords();
    this.Print();
  }

  public void PlaceWords() {
    foreach (Word w in candidate.words)
      if (board.Fitness(w) >= 1.0)
        board.PlaceWord(w);    
  }

  public void Solve() {
    foreach (Word w in candidate.words) {
      int n = 0;
      bool done = false;
      while (!done) {
        double f = board.Fitness(w); 
        if (w.errors == 0) {
          done = true;
          Log.Post(w + " can be placed");
          this.score += f;
          this.percentageSolved++;
          board.PlaceWord(w);
        }
        else
          board.Reposition(w);
        if (n > maxIterations) {
          Log.Post(w + " cannot be placed");
          this.score = 0.0;
          done = true;
        }
        n++;
      }
    }
    this.percentageSolved /= candidate.words.Count;
  }

  public void FillBoard(char withChar = ' ') {
    this.board.Fill(withChar);
  }

  public int NumCharsLeft() {
    return this.board.CharsLeft();
  }

  public int NumCharsOverlapping() {
    return NumCharsLeft() + candidate.TotalLength() - board.Size();
  }

  public void Print() {    
    Console.WriteLine("\nscore: {0:##.##}", score) ;
    Console.WriteLine("\nsolved: {0:P0}", percentageSolved); 
    Console.WriteLine("\nchars left: {0}", NumCharsLeft());
    Console.WriteLine("\nchars overlapping: {0}", NumCharsOverlapping());
    Console.WriteLine("\nBOARD:");
    this.board.Print();
    Console.WriteLine("\nSolution:");
    this.candidate.SortAlphabetical();
    this.candidate.Print();
  }

  public string BoardToHtml() {
    return this.board.ToHtml();
  }

  public string CandidateToHtml() {
    return this.candidate.ToHtml();
  }

  public void SaveAsHtml() {
    string path = "solution.html";
    string template = File.ReadAllText("template.html");
    string html = String.Format(template, this.BoardToHtml(), this.CandidateToHtml());

    if (File.Exists(path))
      File.Delete(path);
    File.WriteAllText(path, html);
  }
}