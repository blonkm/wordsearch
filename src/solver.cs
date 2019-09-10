using System;
using System.Collections.Generic;
using System.Linq;

// a game of wordsearch
class Solver {
  const int defaultNumCompositions = 100;
  const int defaultNumWords = 20;
  const int defaultWidth = 20;
  const int defaultHeight = 20;
  public List<Composition> compositions;
  public int numCompositions { get; set; }

  public Solver(int width = defaultWidth, int height = defaultHeight, int numWords = defaultNumWords, int numCompositions = defaultNumCompositions) {
    Composition composition;
    string noun;
    
    compositions = new List<Composition>();
    for (int c = 0; c < numCompositions; c++) {
      List<Word> wordlist = new List<Word>();
      for (int n=0; n < numWords; n++) {
        noun = Randomizer.Noun();
        wordlist.Add(new Word(noun));
      }
      Board board = new Board(width, height);
      Candidate candidate = new Candidate(wordlist);
      composition = new Composition(board, candidate);
      compositions.Add(composition);
    }
  }

  public void Test() {
    this.compositions[0].Test();
  }

  public void Solve() {
    double maxScore = 0;
    Composition winner = null;

    // try to solve
    foreach (Composition composition in this.compositions) {
      composition.Solve();
    }

    winner = compositions.First();
    // find best solution
    foreach (Composition composition in this.compositions) {
      if (maxScore < composition.score) {
        winner = composition;
        maxScore = composition.score;
      }
    }
    winner.board.FillRandom();
    winner.Print();
    winner.SaveAsHtml();
  }
}
