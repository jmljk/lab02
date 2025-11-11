using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore
{
    class Program
    {
        struct Game
        {
            public string Name;
            public double Price;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<Game> games = new List<Game>()
            {
                new Game { Name = "S.T.A.L.K.E.R 2", Price = 1399 },
                new Game { Name = "Cyberpunk 2077", Price = 799 },
                new Game { Name = "Battlefield 6", Price = 1699 },
                new Game { Name = "Zomboid", Price = 415 },
                new Game { Name = "TitanFall 2", Price = 799 }
            };

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("~~~~ MY GAMES STORE ~~~~");
                Console.ResetColor();

                Console.WriteLine("1. Каталог ігор");
                Console.WriteLine("2. Пошук гри");
                Console.WriteLine("3. Кошик");
                Console.WriteLine("4. Оформити покупку");
                Console.WriteLine("5. Акаунт користувача");
                Console.WriteLine("6. Адміністрування");
                Console.WriteLine("0. Вихід");
                Console.WriteLine("-----------------------------");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ShowCatalog(games);
                        break;
                    case "2":
                        SearchGame(games);
                        break;
                    case "3":
                        CartMenu();
                        break;
                    case "4":
                        DirectCheckout(games);
                        break;
                    case "5":
                        UserAccountMenu();
                        break;
                    case "6":
                        AdminMenu();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Дякуємо за відвідування магазину!");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Невірний вибір. Натисніть будь-яку клавішу...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ===== 1. Каталог з цінами =====
        static void ShowCatalog(List<Game> games)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("========= КАТАЛОГ ІГОР =========");
            Console.ResetColor();

            for (int i = 0; i < games.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {games[i].Name} - {games[i].Price} грн");
            }

            Console.WriteLine("\n0. Назад у головне меню");
            Console.Write("Натисніть 0 для повернення: ");

            while (Console.ReadLine() != "0")
            {
                Console.Write("❌ Невірний ввід. Натисніть 0, щоб повернутись: ");
            }
        }

        // ===== 2. Пошук гри =====
        static void SearchGame(List<Game> games)
        {
            Console.Clear();
            Console.Write("Введіть назву гри для пошуку: ");
            string query = (Console.ReadLine() ?? "").ToLower();

            var found = games.FindAll(g => g.Name.ToLower().Contains(query));

            if (found.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Гру не знайдено. Функція в розробці...");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✅ Знайдено ігри (функція в розробці)...");
                Console.ResetColor();
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутись...");
            Console.ReadKey();
        }

        // ===== 3. Кошик з підгрупами (заглушки) =====
        static void CartMenu()
        {
            bool inCart = true;
            while (inCart)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== КОШИК ===");
                Console.ResetColor();

                Console.WriteLine("1. Переглянути кошик");
                Console.WriteLine("2. Очистити кошик");
                Console.WriteLine("0. Назад");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "1":
                    case "2":
                        Console.WriteLine("⚙️  Функція в розробці...");
                        Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутись...");
                        Console.ReadKey();
                        break;
                    case "0":
                        inCart = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Невірний вибір!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ===== 4. Оформлення покупки (оновлено) =====
        static void DirectCheckout(List<Game> games)
        {
            bool continueShopping = true;

            while (continueShopping)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("=== ОФОРМЛЕННЯ ПОКУПКИ ===");
                Console.ResetColor();

                List<(Game game, int qty)> cart = new List<(Game, int)>();
                bool addingItems = true;

                while (addingItems)
                {
                    Console.WriteLine("\nОберіть гру для покупки:");
                    for (int i = 0; i < games.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {games[i].Name} - {games[i].Price} грн");
                    }
                    Console.WriteLine("0. Завершити вибір і розрахувати");

                    Console.Write("Ваш вибір: ");
                    string input = Console.ReadLine() ?? "";
                    if (int.TryParse(input, out int choice))
                    {
                        if (choice == 0)
                        {
                            addingItems = false;
                            break;
                        }

                        if (choice > 0 && choice <= games.Count)
                        {
                            Game selected = games[choice - 1];
                            Console.Write($"Введіть кількість для \"{selected.Name}\": ");
                            if (int.TryParse(Console.ReadLine(), out int qty) && qty > 0)
                            {
                                cart.Add((selected, qty));
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"✅ Додано {qty} шт. гри \"{selected.Name}\"");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("❌ Невірна кількість!");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("❌ Невірний вибір гри!");
                            Console.ResetColor();
                        }
                    }
                }

                // Підрахунок суми
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=== ПІДСУМОК ПОКУПКИ ===");
                Console.ResetColor();

                double total = 0;
                foreach (var item in cart)
                {
                    double sum = item.game.Price * item.qty;
                    Console.WriteLine($"{item.game.Name} × {item.qty} = {sum} грн");
                    total += sum;
                }

                Console.WriteLine("-----------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Загальна сума: {total} грн");
                Console.ResetColor();

                Console.Write("\nПідтвердити покупку? (y/n): ");
                string confirm = (Console.ReadLine() ?? "").ToLower();

                if (confirm == "y")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Покупку оформлено! Дякуємо за замовлення!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ Покупку скасовано.");
                    Console.ResetColor();
                }

                Console.Write("\nБажаєте почати нову покупку? (y/n): ");
                string again = (Console.ReadLine() ?? "").ToLower();
                if (again != "y")
                {
                    continueShopping = false;
                }
            }
        }

        // ===== 5. Акаунт користувача (заглушка) =====
        static void UserAccountMenu()
        {
            bool inAccount = true;
            while (inAccount)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== АКАУНТ КОРИСТУВАЧА ===");
                Console.ResetColor();

                Console.WriteLine("1. Вхід");
                Console.WriteLine("2. Реєстрація");
                Console.WriteLine("3. Переглянути профіль");
                Console.WriteLine("4. Історія покупок");
                Console.WriteLine("0. Назад");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        Console.WriteLine("⚙️  Функція в розробці...");
                        Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутись...");
                        Console.ReadKey();
                        break;
                    case "0":
                        inAccount = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Невірний вибір!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ===== 6. Адміністрування (заглушка) =====
        static void AdminMenu()
        {
            bool inAdmin = true;
            while (inAdmin)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=== 🛠 АДМІНІСТРУВАННЯ ===");
                Console.ResetColor();

                Console.WriteLine("1. Додати нову гру");
                Console.WriteLine("2. Видалити гру");
                Console.WriteLine("3. Змінити ціну гри");
                Console.WriteLine("4. Переглянути статистику продажів");
                Console.WriteLine("0. Назад");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        Console.WriteLine("⚙️  Функції адміністратора в розробці...");
                        Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутись...");
                        Console.ReadKey();
                        break;
                    case "0":
                        inAdmin = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Невірний вибір!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
