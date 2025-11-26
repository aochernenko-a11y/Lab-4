using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Lab_4
{
    public sealed class BoardGame : Product
    {
        //========== СТАТИЧНI ПОЛЯ ТА ВЛАСТИВОСТI ==========

        private static int counter;

        private static decimal discountRate = 0.10m;

        private static string clubName = "Клуб настiльних iгор";

        public static int Counter => counter;

        public static decimal DiscountRate
        {
            get => discountRate;
            set
            {
                if (value >= 0m && value <= 0.5m)
                    discountRate = value;
                else
                    discountRate = 0.10m;   // значення за замовчуванням
            }
        }

        public static string ClubName
        {
            get => clubName;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    clubName = value;
            }
        }

        //========== ЗВИЧАЙНI ВЛАСТИВОСТI ОБ'ЄКТА ==========

        public string Genre { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int DurationMinutes { get; set; }
        public int MinAge { get; set; }
        public string Publisher { get; set; }

        private void IncrementCounter()
        {
            counter++;
        }

        //========== ПЕРЕВАНТАЖЕНI КОНСТРУКТОРИ ==========

        // 1. Без параметрiв
        public BoardGame()
        {
            Name = "Без назви";
            Genre = "Сiмейна";
            MinPlayers = 2;
            MaxPlayers = 4;
            DurationMinutes = 30;
            MinAge = 8;
            Price = 0m;
            Publisher = "Невiдомо";

            IncrementCounter();
            Console.WriteLine("[Конструктор 1] Без параметрiв");
        }

        // 2. З одним параметром
        public BoardGame(string title) : this()
        {
            Name = title;
            Console.WriteLine("[Конструктор 2] З одним параметром");
        }

        // 3. З кiлькома параметрами
        public BoardGame(string title, string genre, decimal price) : this(title)
        {
            Genre = genre;
            Price = price;
            Console.WriteLine("[Конструктор 3] З кiлькома параметрами");
        }

        // 4. З усiма параметрами
        public BoardGame(
            string title,
            string genre,
            int minPlayers,
            int maxPlayers,
            int duration,
            int minAge,
            decimal price,
            string publisher)
            : this(title, genre, price)
        {
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            DurationMinutes = duration;
            MinAge = minAge;
            Price = price;
            Publisher = publisher;

            Console.WriteLine("[Конструктор 4] Повна iнiцiалiзацiя");
        }

        //========== ПЕРЕВАНТАЖЕНI МЕТОДИ ВИВЕДЕННЯ ==========

        public override void DisplayInfo()
        {
            Console.WriteLine($"Назва: {Name}");
            Console.WriteLine($"Жанр: {Genre}");
            Console.WriteLine($"Гравцiв: {MinPlayers}-{MaxPlayers}");
            Console.WriteLine($"Тривалiсть: {DurationMinutes} хв");
            Console.WriteLine($"Вiк: {MinAge}+");
            Console.WriteLine($"Цiна: {Price} грн");
        }

        public void DisplayInfo(bool withPublisher)
        {
            DisplayInfo();
            if (withPublisher)
                Console.WriteLine($"Видавець: {Publisher}");
        }

        public void DisplayInfo(string format)
        {
            if (format == "short")
                Console.WriteLine($"{Name} ({Genre}) – {Price} грн");
            else
                DisplayInfo(true);
        }

        public void PlayDemo()
        {
            Console.WriteLine(
                $"Гра '{Name}' розпочалась! ({MinPlayers}-{MaxPlayers} гравцiв, тривалiсть {DurationMinutes} хв)");
        }

        //========== СТАТИЧНИЙ ПРЕДМЕТНИЙ МЕТОД ==========

        public static decimal GetDiscountedPrice(BoardGame game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            return Math.Round(game.Price * (1 - DiscountRate), 2);
        }

        //========== ToString(), Parse(), TryParse() ==========

        public override string ToString()
        {
            return $"{Name};{Genre};{MinPlayers};{MaxPlayers};{DurationMinutes};{MinAge};{Price};{Publisher}";
        }

        public static BoardGame Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentNullException(nameof(s), "Рядок не може бути порожнiм.");

            string[] parts = s.Split(';');

            if (parts.Length != 8)
                throw new FormatException(
                    "Невiрний формат рядка. Очiкується 8 полiв, роздiлених крапкою з комою.");

            string name = parts[0].Trim();
            string genre = parts[1].Trim();

            if (string.IsNullOrWhiteSpace(name))
                throw new FormatException("Назва гри не може бути порожньою.");

            if (!int.TryParse(parts[2], out int minPlayers) || minPlayers <= 0)
                throw new FormatException("Мiнiмальна кiлькiсть гравцiв задана некоректно.");

            if (!int.TryParse(parts[3], out int maxPlayers) || maxPlayers < minPlayers)
                throw new FormatException("Максимальна кiлькiсть гравцiв задана некоректно.");

            if (!int.TryParse(parts[4], out int duration) || duration <= 0)
                throw new FormatException("Тривалiсть гри задана некоректно.");

            if (!int.TryParse(parts[5], out int minAge) || minAge < 0)
                throw new FormatException("Мiнiмальний вiк заданий некоректно.");

            if (!decimal.TryParse(parts[6], out decimal price) || price < 0)
                throw new FormatException("Цiна задана некоректно.");

            string publisher = parts[7].Trim();
            if (string.IsNullOrWhiteSpace(publisher))
                publisher = "Невiдомо";

            // конструктор з повною iнiцiалiзацiєю
            return new BoardGame(name, genre, minPlayers, maxPlayers, duration, minAge, price, publisher);
        }

        public static bool TryParse(string s, out BoardGame game)
        {
            game = null;
            bool valid = false;

            try
            {
                game = Parse(s);
                valid = true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Помилка формату: " + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("TryParse: " + ex.Message);
            }

            return valid;
        }
    }
}
