using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace Lab_4
{
    internal class Program
    {
        private static List<BoardGame> games = new List<BoardGame>();

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            int choice;
            do
            {
                Console.WriteLine("\n=== МЕНЮ ЛР-4 (Настiльнi iгри) ===");
                Console.WriteLine("1 – Додати гру");
                Console.WriteLine("2 – Переглянути всi iгри");
                Console.WriteLine("3 – Знайти гру");
                Console.WriteLine("4 – Продемонструвати поведiнку");
                Console.WriteLine("5 – Видалити гру");
                Console.WriteLine("6 – Продемонструвати static-методи");
                Console.WriteLine("0 – Вийти");
                Console.Write("Ваш вибiр: ");

                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        AddGame();
                        break;
                    case 2:
                        ShowAll();
                        break;
                    case 3:
                        FindGame();
                        break;
                    case 4:
                        DemonstrateBehaviour();
                        break;
                    case 5:
                        DeleteGame();
                        break;
                    case 6:
                        DemonstrateStatic();
                        break;
                    case 0:
                        Console.WriteLine("Вихiд iз програми...");
                        break;
                    default:
                        Console.WriteLine("Некоректний вибiр!");
                        break;
                }
            } while (choice != 0);
        }

        //========== 1. Додати гру ==========

        private static void AddGame()
        {
            Console.WriteLine("\n--- Додавання гри ---");
            Console.WriteLine("Оберiть спосiб:");
            Console.WriteLine("1 – Через конструктори");
            Console.WriteLine("2 – Через рядок (TryParse)");
            int.TryParse(Console.ReadLine(), out int mode);

            if (mode == 2)
            {
                AddGameFromString();
                return;
            }

            Console.WriteLine("Оберiть конструктор:");
            Console.WriteLine("1 – Без параметрiв");
            Console.WriteLine("2 – З одним параметром");
            Console.WriteLine("3 – З трьома параметрами");
            Console.WriteLine("4 – Повна iнiцiалiзацiя");

            int.TryParse(Console.ReadLine(), out int c);
            BoardGame g;

            switch (c)
            {
                case 1:
                    g = new BoardGame();
                    break;

                case 2:
                    Console.Write("Назва: ");
                    string t = Console.ReadLine();
                    g = new BoardGame(t);
                    break;

                case 3:
                    Console.Write("Назва: ");
                    string n = Console.ReadLine();
                    Console.Write("Жанр: ");
                    string gen = Console.ReadLine();
                    Console.Write("Цiна: ");
                    decimal pr = decimal.Parse(Console.ReadLine() ?? "0");
                    g = new BoardGame(n, gen, pr);
                    break;

                case 4:
                    Console.Write("Назва: ");
                    string title = Console.ReadLine();
                    Console.Write("Жанр: ");
                    string genre = Console.ReadLine();
                    Console.Write("Мiн. гравцiв: ");
                    int min = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Макс. гравцiв: ");
                    int max = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Тривалiсть (хв): ");
                    int dur = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Вiк (вiд): ");
                    int age = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Цiна: ");
                    decimal price = decimal.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Видавець: ");
                    string pub = Console.ReadLine();
                    g = new BoardGame(title, genre, min, max, dur, age, price, pub);
                    break;

                default:
                    g = new BoardGame();
                    break;
            }

            games.Add(g);
            Console.WriteLine("Гру додано (через конструктор)!");
        }

        private static void AddGameFromString()
        {
            Console.WriteLine("\nВведiть рядок у форматi:");
            Console.WriteLine("Назва;Жанр;MinPlayers;MaxPlayers;Тривалiсть;MinAge;Цiна;Видавець");
            Console.WriteLine("Наприклад:");
            Console.WriteLine("Каркассон;Стратегiя;2;5;60;8;800;Hobby World");

            string input = Console.ReadLine();

            if (BoardGame.TryParse(input, out BoardGame game))
            {
                games.Add(game);
                Console.WriteLine("Гру успiшно додано за допомогою TryParse().");
            }
            else
            {
                Console.WriteLine("Гру НЕ додано (рядок некоректний).");
            }
        }

        //========== 2. Переглянути всi iгри ==========

        private static void ShowAll()
        {
            Console.WriteLine("\n--- Список iгор ---");
            if (games.Count == 0)
            {
                Console.WriteLine("Список порожнiй!");
                return;
            }

            // сортування по назві
            games.Sort((a, b) =>
                string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));

            foreach (var g in games)
            {
                g.DisplayInfo("short");
            }

            Console.WriteLine($"\nВсього коректно створених iгор: {BoardGame.Counter}");
            Console.WriteLine(
                $"Клуб: {BoardGame.ClubName}, поточна знижка: {BoardGame.DiscountRate:P0}");
        }

        //========== 3. Знайти гру ==========

        private static void FindGame()
        {
            Console.Write("\nВведiть назву гри: ");
            string search = Console.ReadLine();

            var found = games.Find(
                g => g.Name.Equals(search, StringComparison.OrdinalIgnoreCase));

            if (found != null)
            {
                Console.WriteLine("Знайдено гру:");
                found.DisplayInfo(true);
                Console.WriteLine("Рядок ToString():");
                Console.WriteLine(found.ToString());
            }
            else
            {
                Console.WriteLine("Не знайдено.");
            }
        }

        //========== 5. Видалити гру ==========

        private static void DeleteGame()
        {
            Console.Write("\nВведiть назву для видалення: ");
            string name = Console.ReadLine();

            int removed = games.RemoveAll(
                g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (removed > 0)
                Console.WriteLine("Гру видалено!");
            else
                Console.WriteLine("Такої гри не знайдено!");
        }

        //========== 4. Продемонструвати поведiнку (з Lab-3) ==========

        private static void DemonstrateBehaviour()
        {
            Console.WriteLine("\n--- Демонстрацiя перевантажень (не static) ---");

            BoardGame demo1 = new BoardGame();
            BoardGame demo2 = new BoardGame("Монополiя");
            BoardGame demo3 = new BoardGame("Каркассон", "Стратегiя", 550);
            BoardGame demo4 = new BoardGame(
                "Ticket to Ride", "Подорожi", 2, 5, 60, 8, 1100, "Days of Wonder");

            Console.WriteLine("\n1) Звичайний вивiд:");
            demo3.DisplayInfo();

            Console.WriteLine("\n2) Вивiд з видавцем:");
            demo3.DisplayInfo(true);

            Console.WriteLine("\n3) Короткий формат:");
            demo4.DisplayInfo("short");

            Console.WriteLine("\n4) Демонстрацiя поведiнки:");
            demo4.PlayDemo();
        }

        //========== 6. Продемонструвати static-методи ==========

        private static void DemonstrateStatic()
        {
            Console.WriteLine("\n--- Демонстрацiя static-членiв ---");

            Console.WriteLine($"Клуб: {BoardGame.ClubName}");
            Console.WriteLine($"Поточна знижка: {BoardGame.DiscountRate:P0}");
            Console.WriteLine($"Лiчильник iгор: {BoardGame.Counter}");

            BoardGame sample;

            if (games.Count > 0)
            {
                Console.WriteLine("\nВикористаємо першу гру зi списку:");
                sample = games[0];
            }
            else
            {
                Console.WriteLine("\nСписок порожнiй – створимо тестову гру:");
                sample = new BoardGame(
                    "Каркассон", "Стратегiя", 2, 5, 60, 8, 800, "Hobby World");
            }

            sample.DisplayInfo("short");
            decimal discounted = BoardGame.GetDiscountedPrice(sample);
            Console.WriteLine(
                $"Цiна з урахуванням знижки {BoardGame.DiscountRate:P0}: {discounted} грн");

            Console.WriteLine("\n=== Демонстрацiя Parse() ===");
            string str = "Монополiя;Економiчна;2;6;90;8;1000;Hasbro";
            Console.WriteLine("Рядок: " + str);

            try
            {
                BoardGame parsed = BoardGame.Parse(str);
                Console.WriteLine("Пiсля Parse():");
                parsed.DisplayInfo("short");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка Parse: " + ex.Message);
            }

            Console.WriteLine("\n=== Демонстрацiя TryParse() з некоректним рядком ===");
            string bad = "поганий рядок";
            if (!BoardGame.TryParse(bad, out BoardGame badGame))
                Console.WriteLine("TryParse коректно повернув false.");
        }
    }
}
