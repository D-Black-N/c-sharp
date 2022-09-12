// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using System;

namespace Application {
  class Calculator {
    static void Main(string[] args) {
      float x1;
      float x2;
      Console.WriteLine("Вы вошли в приложение калькулятор!");
      while(true) {
        Console.WriteLine();
        Console.Write("Введите 'exit' для выхода из приложения или первый операнд: ");
        string str = Console.ReadLine();
        if (str == "exit") {
          return;
        }
        while (!float.TryParse(str, out x1)) {
          Console.WriteLine("Ошибка! Вы ввели не число!");
          Console.Write("Введите первый операнд: ");
          str = Console.ReadLine();
        }
        Console.Write("Введите второй операнд: ");
        str = Console.ReadLine();
        while (!float.TryParse(str, out x2)) {
          Console.WriteLine("Ошибка! Вы ввели не число!");
          Console.Write("Введите второй операнд: ");
          str = Console.ReadLine();
        }
        Console.Write("Введите операцию из предложенных (* / + -): ");
        string op = Console.ReadLine();
        switch (op) {
          case "+": 
            Console.WriteLine($"Результат: {x1 + x2}");
            break;
          case "-":
            Console.WriteLine($"Результат: {x1 - x2}");
            break;
          case "*":
            Console.WriteLine($"Результат: {x1 * x2}");
            break;
          case "/": 
            if (x2 == 0) {
              Console.WriteLine("Деление на 0 невозможно!");
              break;
            }
            Console.WriteLine($"Результат: {x1 / x2}");
            break;
        }
      }
    }
  }
}
