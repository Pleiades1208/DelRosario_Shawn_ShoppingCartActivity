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
                new Product(103, "Milk", 150, 100),
                new Product(104, "Chicken", 250, 200)
            };

            int[] cartIds = new int[10];
            int[] cartQty = new int[10];

            string choice = "Y";

            do
            {
                Console.WriteLine("\nID     NAME        PRICE       STOCK");
                for (int i = 0; i < products.Length; i++)
                {
                    products[i].DisplayProduct();
                }

                Console.Write("\nEnter product ID: ");
                int inputId;

                if (!int.TryParse(Console.ReadLine(), out inputId))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                int index = -1;

                for (int i = 0; i < products.Length; i++)
                {
                    if (products[i].Id == inputId)
                    {
                        index = i;
                        break;
                    }
                }

                if (index == -1)
                {
                    Console.WriteLine("Product not found.");
                    continue;
                }

                if (products[index].RemainingStock == 0)
                {
                    Console.WriteLine("Out of stock.");
                    continue;
                }

                Console.Write("Enter quantity: ");
                int qty;

                if (!int.TryParse(Console.ReadLine(), out qty) || qty <= 0)
                {
                    Console.WriteLine("Invalid quantity.");
                    continue;
                }

                if (qty > products[index].RemainingStock)
                {
                    Console.WriteLine("Not enough stock available.");
                    continue;
                }

                bool exists = false;

                for (int i = 0; i < cartIds.Length; i++)
                {
                    if (cartIds[i] == inputId)
                    {
                        cartQty[i] += qty;
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    for (int i = 0; i < cartIds.Length; i++)
                    {
                        if (cartIds[i] == 0)
                        {
                            cartIds[i] = inputId;
                            cartQty[i] = qty;
                            break;
                        }
                    }
                }

                products[index].RemainingStock -= qty;

                Console.WriteLine("Item added to cart.");

                Console.Write("\nAdd more? (Y/N): ");
                choice = Console.ReadLine().ToUpper();

            } while (choice == "Y");
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