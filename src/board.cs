using System;
using System.Linq;

// the board with width and height
class Board {
  
  public readonly string[] directions = {"N","NE","E","SE","S","SW","W","NW"};
  const int defaultWidth = 20;
  const int defaultHeight = 20;

  private char defaultChar = 'x';
  public char [,] board;
  public bool [,] occupied;
  public int width { get; set; }
  public int height { get; set; }
  
  public Board(int width = defaultWidth, int height = defaultHeight ) {
    this.width = width;
    this.height = height;
    this.board = new char[height, width];
    this.occupied = new bool[height, width];
    for(int row = 0;row < height; row++)
      for(int col = 0;col < width; col++) {
        this.board[row, col] = defaultChar;
        this.occupied[row, col] = true;
      }
  }

  public int Size() {
    return width * height;
  }

  public void Print() {
    for(int row = 0;row < height; ++row) {
        for(int col = 0;col < width; ++col) {
            Console.Write(this.board[row, col]);
        }
        Console.WriteLine();
    }
  }

  public string ToHtml() {
    string text = "";
    for(int row = 0;row < height; ++row) {
        text += "\n<tr>";
        for(int col = 0;col < width; ++col) {
            text += String.Format("<td>{0}</td>", this.board[row, col]);
        }
        text += "\n</tr>";
    }
    return text;
  }

  public string ToHtmlWithTemplate() {
    string text = "\n<table>";
    text += this.ToHtml();
    text += "\n</table>";
    string style = "table { margin:1em; width:75%; font-size:2em;  } td { border-radius: 5%; border:1px solid black; text-align:center; background: SkyBlue; padding:.1em .2em; }";
    string html = "<html>\n<head><style>{0}</style></head>\n<body>\n{1}</body>\n</html>";
    return String.Format(html, style, text);
  }

  public void Fill(char withChar = 'x') {
    if (withChar != 'x')
      this.defaultChar = withChar;
    for(int row = 0;row < height; ++row)
        for(int col = 0;col < width; ++col)
              this.board[row, col] = defaultChar;
  }

  public void FillRandom() {
    for(int row = 0;row < height; ++row)
        for(int col = 0;col < width; ++col)
            if (this.board[row, col] == defaultChar) {
              this.board[row, col] = Randomizer.GetLetter();
              this.occupied[row, col] = false;
            }
  }

  public int CharsLeft() {
    return (from bool item in occupied
            where !item
            select item).Count();
  }

  private bool IsOutOfBounds(Position p) {
    return p.col < 0 || p.col > width - 1 || p.row < 0 || p.row > height - 1;
  }

  private bool PositionFilled(Position p, char ch) {
    char current = board[p.row, p.col];
    return !(current == ch || current == defaultChar);
  }

  private bool Overlapping(Position p, char ch) {
    char current = board[p.row, p.col];
    return current == ch;
  }

  public double Fitness(Word w) {
    double fitness = 0.0;
    Position p = w.position.ShallowCopy();
    foreach (char ch in w.text) {
      bool outside = IsOutOfBounds(p);

      // bad: if the position is filled by another word
      // or the word doesn't fit on the board
      if (outside || PositionFilled(p, ch)) {
        w.errors++;
        fitness--;
      }
      else
        fitness++;
      // good: extra points if there is overlap with other words
      if (!outside && Overlapping(p, ch))
        fitness++;
      p.MoveTo(w.direction);
    }
    return fitness / w.Length();
  }

  public void PlaceWord(Word w) {
    Position p = w.position.ShallowCopy();
    foreach (char ch in w.text) {
      this.board[p.row, p.col] = ch;
      p.MoveTo(w.direction);
      w.placed = true;
    }
  }

  // move the word to another position and change direction
  public void Reposition(Word w) {
      w.position.col = Randomizer.GetInt(0, this.width-1);
      w.position.row = Randomizer.GetInt(0, this.height-1);
      w.direction = Randomizer.PickFromList(directions);
      w.errors = 0;
  }
}
