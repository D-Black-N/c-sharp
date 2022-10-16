namespace Application {
  class MyArrays {
    public static void Main(string[] args) {
      int rows;
      int columns;
      string input;
      do {
        Console.Write("Type the number of rows: ");
        input = Console.ReadLine();
        Console.WriteLine();
      } while (!Int32.TryParse(input, out rows) || rows < 1);

      do {
        Console.Write("Type the number of columns: ");
        input = Console.ReadLine();
        Console.WriteLine();
      } while (!Int32.TryParse(input, out columns) || columns < 1);

      var rand = new Random();
      int[,] matrix = new int[rows, columns];
      for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
          matrix[i, j] = rand.Next(10);

      Console.WriteLine("Original matrix:");
      Print(matrix, rows, columns);

      Console.WriteLine("Sorted by rows: ");
      int[,] row_sorted_matr = Sort(matrix, true, rows, columns);
      Print(row_sorted_matr, rows, columns);

      Console.WriteLine("Sorted by columns: ");
      int[,] col_sorted_matr = Sort(matrix, false, rows, columns);
      Print(col_sorted_matr, rows, columns);

    }

    static void Print(int[,] matr, int row_length, int col_length) {
      for(int i = 0; i < row_length; i++) {
        for(int j = 0; j < col_length; j++)
          Console.Write($"{matr[i, j]} ");
        Console.WriteLine();
      }
    }

    static int[,] Sort(int[,] matr, bool isRow, int row_length, int col_length) {
      int[,] new_matr = new int[row_length, col_length]; 
      if (isRow) { // если isRow, то сортируем по строкам, иначе по столбцам
        for(int i = 0; i < row_length; i++) {
          int[] row = new int[col_length];
          for(int j = 0; j < col_length; j++) {
            row[j] = matr[i, j]; // Выборка строки матрицы
          }
          Array.Sort(row); // Сортировка строки матрицы
          for(int j = 0; j < col_length; j++)
            new_matr[i, j] = row[j]; // Запись строки в новую матрицу
        }
      }
      else {
        for(int j = 0; j < col_length; j++) {
          int[] col = new int[row_length];
          for(int i = 0; i < row_length; i++) {
            col[i] = matr[i, j]; // Выборка столбца матрицы
          }
          Array.Sort(col); // Сортировка столбца матрицы
          for(int i = 0; i < row_length; i++)
            new_matr[i, j] = col[i]; // Запись строки в новую матрицу
        }
      }
      return new_matr;
    }
  }
}