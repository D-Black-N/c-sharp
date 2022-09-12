// See https://aka.ms/new-console-template for more information
using System;

namespace Application {
  class MyArrays {
    public static void Main(string[] args) {
      int rows;
      int columns; 
      string input;
      Console.WriteLine("Введите размерность матрицы:");

      do {
        Console.Write("Введите количество строк матрицы: ");
        input = Console.ReadLine();
        Console.WriteLine();
      } while(!Int32.TryParse(input, out rows) || rows < 1);

      do {
        Console.Write("Введите количество столбцов матрицы: ");
        input = Console.ReadLine();
        Console.WriteLine();
      } while(!Int32.TryParse(input, out columns) || columns < 1);

      var rand = new Random();
      int[,] matrix = new int[rows, columns]; 
      for(int i = 0; i < rows; i++) {
        for(int j = 0; j < columns; j++) {
          matrix[i, j] = rand.Next(10);
        }
      }

      Console.WriteLine("Сформирована рандомная матрица:");
      for(int i = 0; i < rows; i++) {
        for(int j = 0; j < columns; j++) {
          Console.Write($"{matrix[i, j]} ");
        }
        Console.WriteLine();
      }
    }
  }
}