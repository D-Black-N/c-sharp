using DZ1;
class Program {
  static void Main(string[] args) {
    Console.WriteLine("Добро пожаловать в консольный калькулятор!");
    Console.WriteLine("Доступные символы: + - * / ( )");
    Console.WriteLine("Введите выражение: ");
    string expression = Console.ReadLine();
    Calculator calculator = new Calculator(expression);
    double result = calculator.maths(expression);
    if (result != 0.987654321)
      Console.WriteLine($"Результат: {result}");
  }
}
