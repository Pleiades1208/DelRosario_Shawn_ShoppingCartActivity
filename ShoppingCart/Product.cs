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
        public string Category;

        public Product(int id, string name, double price, int stock, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = stock;
            Category = category;
        }

        public void DisplayProduct()
        {
            Console.WriteLine($"{Id,3}   {Name,-10}   {Price,8:N2}   {RemainingStock,5}   {Category}");
        }

        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }
    }
}
