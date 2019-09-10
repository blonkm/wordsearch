using System;

static class Log {
  public static bool debug { get; set; }

  static Log() {
    debug = true;
  }

  public static void Post(string s) {
    if (debug)
      Console.WriteLine(s);
  }
}