using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Lab_4
{
    public class Product
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; } = "Без назви";
        public decimal Price { get; set; } = 0m;

        public Product() { }

        public Product(string name)
        {
            Name = name;
        }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Назва: {Name}, Цiна: {Price} грн");
        }
    }
}
