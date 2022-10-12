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

      Console.WriteLine("Сортировка по строкам: ");
      int[] row = new int[columns];
      for (int i = 0; i < rows; i++) {
        for (int j = 0; j < columns; j++)
          row[j] = matrix[i, j];
        BubbleSort(row);
        Insert(true, i, row, matrix);
      }
      PrintArray(matrix);

      Console.WriteLine("Сортировка по столбцам: ");
      int[] col = new int[rows];
      for (int i = 0; i < columns; i++) {
        for (int j = 0; j < rows; j++)
          col[j] = matrix[j, i];
        BubbleSort(col);
        Insert(false, i, col, matrix);
      }
      PrintArray(matrix);
    }

    static void BubbleSort(int[] inArray) {
      for (int i = 0; i < inArray.Length; i++)
        for (int j = 0; j < inArray.Length - i - 1; j++) {
          if (inArray[j] > inArray[j + 1]) {
              int temp = inArray[j];
              inArray[j] = inArray[j + 1];
              inArray[j + 1] = temp;
          }
        }
    }
    public static void Insert(bool isRow, int dim, int[] source, int[,] dest) {
      for (int k = 0; k < source.Length; k++){
        if (isRow)
          dest[dim, k] = source[k];
        else
          dest[k, dim] = source[k];
      }
    }

    public static void PrintArray(int[,] array) {
      for (int a = 0; a < array.GetLength(0); a++) {
        for (int b = 0; b < array.GetLength(1); b++)
          Console.Write(array[a, b] + " ");
        Console.WriteLine();
      }
    }
  }
}