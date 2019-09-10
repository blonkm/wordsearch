using System;

  /* row and column position on the board
  */
class Position {
  const char north = 'N';
  const char south = 'S';
  const char west = 'W';
  const char east = 'E';

  public int col { get; set; }
  public int row { get; set; }

  public Position(int col, int row) {
    this.col = col;
    this.row = row;
  }

  public Position ShallowCopy()
  {
      return (Position) this.MemberwiseClone();
  }

  public void MoveTo(string direction) {
    if (direction.Contains(north))
      this.row--;
    if (direction.Contains(south))
      this.row++;
    if (direction.Contains(west))
      this.col--;
    if (direction.Contains(east))
      this.col++;
  }

  public void Print() {
    Console.WriteLine(this.ToString());
  }

  public override string ToString() {
    return String.Format("col = {0}, row = {1}", col+1, row+1); // 0-based array
  }
}
