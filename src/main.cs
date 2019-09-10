using System;
using System.Collections.Generic;

class App {
  const int numWords = 12;
  const int rows = 10;
  const int columns = 10;
  const int numCompositions = 50;

  public static void Main (string[] args) {
    Solver solver;
    Log.debug = false;
    solver = new Solver(rows, columns, numWords, numCompositions); 
    solver.Solve();
  }
}