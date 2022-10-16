using System;

namespace Bank {

  class Client {
    string name;
    string surname;
    string patronymic; 
    int age;
    string work_place; 
    int[] account_numbers;

    public string Name { get { return name; } set { name = value; } }
    public string Surname { get { return surname; } set { surname = value; } }
    public string Patronymic { get { return patronymic; } set { patronymic = value; } }
    public int Age { get { return age; } set { age = value; } }
    public string Work_place { get { return work_place; } set { work_place = value; } }
    public int[] Account_numbers { get { return account_numbers; } }
    public Client(string _name, string _surname, string _patronymic, int _age, string _work_place) {
      name = _name;
      surname = _surname;
      patronymic = _patronymic;
      age = _age;
      work_place = _work_place;
      account_numbers = new int[0];
    }

    public int[] add_account(int acc_num) {
      int prev_length = account_numbers.Length;
      Array.Resize(ref account_numbers, account_numbers.Length + 1);
      account_numbers[prev_length] = acc_num;
      return account_numbers;
    }

    public int find_account_number(int number) {
      int i = 0;
      while(i != account_numbers.Length && account_numbers[i] != number)
        i++;
      if (i != account_numbers.Length)
        return account_numbers[i];
      else
        return -1;
    }
  }

  class Account {
    int number;
    int balance;
    string[] history;
    bool status;

    public int Number { get { return number; } }
    public bool Status { set { status = value; } }
    public int Balance { get { return balance; } }
    public string[] History { get { return history; } }
    public Account(int _number) {
      number = _number;
      balance = 0;
      history = new string[0];
      status = true;
    }

    public int increase_balance(int sum) {
      add_history_note("increase", sum);
      return balance += sum;
    }

    public int decrease_balance(int sum) {
      add_history_note("decrease", sum);
      return balance -= sum;
    }

    public void add_history_note(string type, int sum) {
      int prev_length = history.Length;
      Array.Resize(ref history, history.Length + 1);
      history[prev_length] = $"{type} {sum}";
    }
  }

  class ConsoleInterface {
    static Client[] clients = new Client[0];
    static Account[] accounts = new Account[0];
    static Client current_client = null;
    static int system_acc_number = 0;
    static void Main(string[] args) {
      Console.WriteLine("Добро пожаловать в Банк!");
      main_menu();
      Console.WriteLine("Спасибо что воспользовались нашим приложением! До новых встреч!");
    }

    static void main_menu() {
      string[] main_actions = {"1 - Новый пользователь", "2 - Войти в систему", "3 - Выход"};
      string input = "1";
      while (input != "3") {
        Console.WriteLine("Главное меню");
        Console.WriteLine("Выберите цифру из предложенных в списке:");
        for(int i = 0; i < main_actions.Length; i++)
          Console.WriteLine(main_actions[i]);
        Console.Write("Ввод: ");
        input = Console.ReadLine();
        switch (input) {
          case "1":
            create_new_client();
            break;
          case "2": 
            string[] client_args = input_client_data();
            int age;
            string name = client_args[0];
            string surname = client_args[1];
            string patronymic = client_args[2];
            int.TryParse(client_args[3], out age);
            string work_place = client_args[4];
            current_client = login(name, surname, patronymic, age, work_place);
            break;
        }
        if (input == "3")
          break;
        if (current_client != null)
          show_client_menu();
        else 
          Console.WriteLine("Такого пользователя не существует, проверьте введенные данные!");
      }
    }

    static void show_client_menu() {
      string[] client_menu = { "1 - Открыть новый счет", "2 - Закрыть счет", "3 - Сделать вклад", "4 - Снять деньги", "5 - Текущий баланс", "6 - Просмотреть историю", "7 - Выход" };
      Console.WriteLine($"Добро пожаловать в систему, {current_client.Name}!");
      string input = "1";
      while (input != "7") {
        for(int i = 0; i < client_menu.Length; i++)
          Console.WriteLine(client_menu[i]);
        Console.Write("Ввод: ");
        input = Console.ReadLine();
        switch (input) {
          case "1":
            create_new_account();
            break;
          case "2":
            close_account();
            break;
          case "3":
            deposit_money();
            break;
          case "4":
            withdraw_money();
            break;
          case "5":
            get_current_balance();
            break;
          case "6":
            get_history();
            break;
          case "7":
            current_client = null;
            break;
        }
      }
    }

    static void create_new_client() {
      int clients_size = clients.Length;
      Array.Resize(ref clients, clients.Length + 1);
      Console.WriteLine("Введите личные данные");
      int age;
      string[] client_args = input_client_data();
      string name = client_args[0];
      string surname = client_args[1];
      string patronymic = client_args[2];
      int.TryParse(client_args[3], out age);
      string work_place = client_args[4];
      Client client = new Client(name, surname, patronymic, age, work_place);
      clients[clients_size] = client;
      Console.WriteLine("Пользователь успешно создан!");
      current_client = login(name, surname, patronymic, age, work_place);
    }

    static string[] input_client_data() {
      string[] result = new string[5];
      Console.Write("Введите имя: ");
      result[0] = Console.ReadLine();

      Console.Write("Введите фамилию: ");
      result[1] = Console.ReadLine();

      Console.Write("Введите отчество: ");
      result[2] = Console.ReadLine();

      Console.Write("Введите возраст: ");
      result[3] = Console.ReadLine();

      Console.Write("Введите место работы: ");
      result[4] = Console.ReadLine();
      return result;
    }

    static Client login(string name, string surname, string patronymic, int age, string work_place) {
      int i = 0;
      while (clients[i].Name != name && clients[i].Surname != surname && clients[i].Patronymic != patronymic && clients[i].Age != age && clients[i].Work_place != work_place)
        i++;
      if (i != clients.Length) {
        return clients[i];
      }
      else return null;
    }

    static void create_new_account() {
      int acc_length = accounts.Length;
      Array.Resize<Account>(ref accounts, acc_length + 1);
      accounts[acc_length] = new Account(system_acc_number);
      current_client.add_account(system_acc_number);
      Console.WriteLine($"Счет успешно создан! Номер счета: {system_acc_number}");
      system_acc_number++;
    }

    static void close_account() {
      int acc_number = find_account_in_user();
      if (acc_number == -1)
        Console.WriteLine("Введено неверное значение! Вы не можете получить доступ к данному аккаунту");
      else {
        Account current_account = Array.Find<Account>(accounts, acc => acc.Number == acc_number);
        current_account.Status = false;
        Console.WriteLine($"Счет с номером {current_account.Number} успешно закрыт!");
      }
    }

    static void deposit_money() {
      int acc_number = find_account_in_user();
      if (acc_number == -1)
        Console.WriteLine("Введено неверное значение! Вы не можете получить доступ к данному аккаунту");
      else {
        Account current_account = Array.Find<Account>(accounts, acc => acc.Number == acc_number);
        Console.Write("Введите сумму пополнения: ");
        string input = Console.ReadLine();
        int sum;
        int.TryParse(input, out sum);
        current_account.increase_balance(sum);
        Console.WriteLine($"На счет номер {current_account.Number} поступил платеж на сумму {sum}");
      }
    }
    static void withdraw_money() {
      int acc_number = find_account_in_user();
      if (acc_number == -1)
        Console.WriteLine("Введено неверное значение! Вы не можете получить доступ к данному аккаунту");
      else {
        Account current_account = Array.Find<Account>(accounts, acc => acc.Number == acc_number);
        Console.Write("Введите сумму снятия: ");
        string input = Console.ReadLine();
        int sum;
        int.TryParse(input, out sum);
        if (sum > current_account.Balance)
          Console.WriteLine($"Сумма снятия не может превышать сумму счета! Текущий баланс счета номер {current_account.Number} составляет {current_account.Balance}");
        else {
          current_account.decrease_balance(sum);
          Console.WriteLine($"Со счета номер {current_account.Number} были сняты средства в размере {sum}. Текущий баланс счета {current_account.Balance}");
        }
      }
    }
    static void get_current_balance() {
      int acc_number = find_account_in_user();
      if (acc_number == -1)
        Console.WriteLine("Введено неверное значение! Вы не можете получить доступ к данному аккаунту");
      else {
        Account current_account = Array.Find<Account>(accounts, acc => acc.Number == acc_number);
        Console.WriteLine($"Текущий баланс счета {current_account.Number}: {current_account.Balance}");
      }
    }
    static void get_history() {
      int acc_number = find_account_in_user();
      if (acc_number == -1)
        Console.WriteLine("Введено неверное значение! Вы не можете получить доступ к данному аккаунту");
      else {
        Account current_account = Array.Find<Account>(accounts, acc => acc.Number == acc_number);
        string[] history = current_account.History;
        for(int i = 0; i < history.Length; i++)
          Console.WriteLine(history[i]);
      }
    }

    static int find_account_in_user() {
      Console.WriteLine("Введи номер доступного вам аккаунта, счет которого желаете открыть");
      Console.Write($"Список доступных аккаунтов: ");
      for(int i = 0; i < current_client.Account_numbers.Length; i++)
        Console.Write($"{current_client.Account_numbers[i]}  ");
      Console.WriteLine("");
      Console.Write("Ввод: ");
      string input = Console.ReadLine();
      int number;
      int.TryParse(input, out number);
      int acc_number = current_client.find_account_number(number);
      return acc_number;
    }
  }
}