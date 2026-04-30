using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartSystem
{
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
