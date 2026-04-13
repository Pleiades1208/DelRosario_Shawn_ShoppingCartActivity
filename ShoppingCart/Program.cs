using System;

namespace ShoppingCartSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] products = new Product[]
            {
                new Product(101, "Eggs", 230, 200),
                new Product(102, "Bread", 75, 100),
                new Product(103, "Milk", 150, 100)
            };

            Console.WriteLine("ID     NAME        PRICE       STOCK");

            for (int i = 0; i < products.Length; i++)
            {
                products[i].DisplayProduct();
            }
        }
    }

    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;

        public Product(int id, string name, double price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = stock;
        }

        public void DisplayProduct()
        {
            Console.WriteLine($"{Id,3}   {Name,-10}   {Price,8:N2}   {RemainingStock,5}");
        }

        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }
    }
}