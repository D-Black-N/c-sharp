using System;

namespace DZ1 {

  struct Leksema { //Структура, описывающая любое число или операцию
	  public char type; // 0 для чисел, "+" для операции сложения и т.д.
	  public double value; //Значение (только для чисел). У операций значение всегда "0"
  };
  class Calculator {
    string statement;

    public string Statement { get { return statement; } }

    public Calculator(string param) {
      statement = param;
    }

    public bool calculate(Stack<Leksema> Stack_n, Stack<Leksema> Stack_o, Leksema item) {
      double a, b, c;
	    a = Stack_n.Pop().value; //Берется верхнее число из стека с числами
	    switch (Stack_o.Peek().type) {  //Проверяется тип верхней операции из стека с операциями
	      case '+': //Если тип верхней операции из стека с операциями сложение
	      	b = Stack_n.Pop().value;
	      	c = a + b;
	      	item.type = '0';
	      	item.value = c;
	      	Stack_n.Push(item); //Результат операции кладется обратно в стек с числами
	      	Stack_o.Pop();
	      	break;

	      case '-':
	      	b = Stack_n.Pop().value;
	      	c = b - a;
	      	item.type = '0';
	      	item.value = c;
	      	Stack_n.Push(item);
	      	Stack_o.Pop();
	      	break;

	      case '*':
	      	b = Stack_n.Pop().value;
	      	c = a * b;
	      	item.type = '0';
	      	item.value = c;
	      	Stack_n.Push(item);
	      	Stack_o.Pop();
	      	break;

	      case '/':
	      	b = Stack_n.Pop().value;
	      	if (a == 0) {
            Console.WriteLine("На 0 делить нельзя!");
            return false;
	      	}
	      	else {
	      		c = (b / a);
	      		item.type = '0';
	      		item.value = c;
	      		Stack_n.Push(item);
	      		Stack_o.Pop();
	          break;
	      	}
      }
      return true;
    }

    int getRang(char Ch) { //Функция возвращает приоритет операции: "1" для сложения и вычитания, "2" для умножения и деления и т.д.
	    if (Ch == '+' || Ch == '-')
        return 1;
	    if (Ch == '*' || Ch == '/')
        return 2;
	    else 
        return 0;
    }

    public double maths(string expression) {
      char Ch; //Переменная, в которую будет записываться текущий обрабатываемый символ
      double value;
      bool flag = true; //Нужен для того, чтобы программа смогла отличить унарный минус (-5) от вычитания (2-5)
      Stack<Leksema> Stack_n = new Stack<Leksema>(); //Стек с числами
      Stack<Leksema> Stack_o = new Stack<Leksema>(); //Стек с операциями
      Leksema item = new Leksema(); //Объект типа Leksema
      int i = 0;
      while (true) {
        Ch = expression[i]; //Смотрим на первый символ
        if (Ch == '=') {
          break;
        }
        if (Ch == ' ') { //Игнорирование пробелов
          i++;
          continue;
        }
        if (Ch >= '0' && Ch <= '9' || Ch == '-' && flag == true) { //Если прочитано число
            string str = "";
            if (Ch == '-') {
              i++;
              Ch = expression[i];
              value = -1;
            }
            else value = 1;
            //string str = Ch.ToString();
            while (Ch >= '0' && Ch <= '9') {
              str = str + Ch.ToString();
              i++;
              Ch = expression[i];
            }
            value = value * double.Parse(str);
            item.type = '0';
            item.value = value;
            Stack_n.Push(item); //Число кладется в стек с числами
            flag = false;
            continue;
        }
        if (Ch == '+' || Ch == '-' && flag == false || Ch == '*' || Ch == '/') { //Если прочитана операция
          if (Stack_o.Count == 0) { //Если стек с операциями пуст
            item.type = Ch;
            item.value = 0;
            Stack_o.Push(item); //Операция кладется в стек с операциями
            i++;
            continue;
          }
          if (Stack_o.Count != 0 && getRang(Ch) > getRang(Stack_o.Peek().type)) { //Если стек с операциями НЕ пуст, но приоритет текущей операции выше верхней в стеке с операциями
            item.type = Ch;
            item.value = 0;
            Stack_o.Push(item); //Операция кладется в стек с операциями
            i++;
            continue;
          }
          if (Stack_o.Count != 0 && getRang(Ch) <= getRang(Stack_o.Peek().type)) {//Если стек с операциями НЕ пуст, но приоритет текущей операции ниже либо равен верхней в стеке с операциями
            if (calculate(Stack_n, Stack_o, item) == false) { //Если функция вернет "false", то прекращаем работу
              return 0;
            }
            continue;
          }
        }
        if (Ch == '(') { //Если прочитана открывающаяся скобка
          item.type = Ch;
          item.value = 0;
          Stack_o.Push(item); //Операция кладется в стек с операциями
          i++;
          if (expression[i] == '-') flag = true;
          continue;
        }
        if (Ch == ')') { //Если прочитана закрывающаяся скобка
          while (Stack_o.Peek().type != '(') {
            if (calculate(Stack_n, Stack_o, item) == false) { //Если функция вернет "false", то прекращаем работу
              return 0;
            } else continue; //Если все хорошо
          }
          Stack_o.Pop();
          i++;
          if (expression[i] == '-') flag = false;
          continue;
        } 
        else { //Если прочитан какой-то странный символ
          // cout << "\nНеверно введено выражение!\n";
          return 0;
        }
      }
      while (Stack_o.Count != 0) { //Вызываем матем. функцию до тех пор, пока в стеке с операциями не будет 0 элементов
        if (calculate(Stack_n, Stack_o, item) == false) { //Если функция вернет "false", то прекращаем работу
          return 0.987654321;
        } else continue; //Если все хорошо
      }
      return Stack_n.Pop().value;
    }
  }
}
