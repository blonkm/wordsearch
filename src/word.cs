using System;

  /* a word with its position and direction on the board */
class Word {
  const string defaultDirection = "E";

  public string text { get; set; }
  public Position position { get; set; }
  public string direction { get; set; }
  public bool placed { get; set; }
  public int errors { get; set; }

  public Word(string text, Position position = null, string direction = defaultDirection) {
    this.text = text.ToUpper();
    this.position = position ?? new Position(0,0);
    this.direction = direction;
    this.placed = false;
  }

  public int Length() {
    return this.text.Length;
  }
  public void Print() {
    Console.WriteLine(this);
  }

  public override string ToString() {
    return String.Format("{0} {1} @ {2}, {3}", placed?'T':'F', text, position, direction);
  }

  public bool Equals( Word other )
  {
    if (other==null)
      return false;
    return this.text == other.text;
  }
}
