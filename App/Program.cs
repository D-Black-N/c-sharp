// See https://aka.ms/new-console-template for more information
using System;

namespace Application {
  class MyArrays {
    public static void Main(string[] args) {
      int[] a = { 1, 8, 2, 10, 6 };
      int[] b = { 7, 9, 10, 12 };
      Array.Sort(a);
      Array.Sort(b);
      Array.Reverse(b);
      foreach (var item in a) {
        Console.Write($"{item}, ");
      }
      Console.WriteLine();
      foreach (var item in b) {
        Console.Write($"{item}, ");
      }
      Console.WriteLine();
      int[] c = a.Concat(b).ToArray();
      foreach(var item in c) {
        Console.Write($"{item}, ");
      }
      Console.WriteLine();
    }
  }
}