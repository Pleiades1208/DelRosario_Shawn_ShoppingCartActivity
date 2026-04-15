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
                new Product(104, "Chicken", 250, 200),
                new Product(105, "Pork", 330, 200),
                new Product(106, "Beef", 450, 200),
                new Product(107, "Sardines", 40, 250),
                new Product(108, "Carrots", 80, 100),
                new Product(109, "Garlic", 100, 100),
                new Product(110, "Onions", 80, 100),
                new Product(111, "Potato", 85, 100),
                new Product(112, "Tomato", 70, 100)
            };

            int[] cartIds = new int[10];
            int[] cartQty = new int[10];

            Console.WriteLine("ID     NAME        PRICE       STOCK");
            for (int i = 0; i < products.Length; i++)
            {
                products[i].DisplayProduct();
            }

            string choice = "Y";

            do
            {
                int inputId;
                Console.Write("\nEnter product ID: ");

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

                int qty;
                Console.Write("Enter quantity: ");

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
                    bool added = false;

                    for (int i = 0; i < cartIds.Length; i++)
                    {
                        if (cartIds[i] == 0)
                        {
                            cartIds[i] = inputId;
                            cartQty[i] = qty;
                            added = true;
                            break;
                        }
                    }

                    if (!added)
                    {
                        Console.WriteLine("Cart is full.");
                        continue;
                    }
                }

                products[index].RemainingStock -= qty;

                Console.WriteLine("Item added to cart.");

                Console.Write("\nAdd more? (Y/N): ");
                choice = (Console.ReadLine() ?? "").Trim().ToUpper();

            } while (choice != "N");

            // ✅ NEW PART
            double total = 0;

            Console.WriteLine("\nRECEIPT:");
            Console.WriteLine("NAME     PRICE    QTY    TOTAL");

            for (int i = 0; i < cartIds.Length; i++)
            {
                if (cartIds[i] == 0) break;

                for (int j = 0; j < products.Length; j++)
                {
                    if (products[j].Id == cartIds[i])
                    {
                        double itemTotal = products[j].GetItemTotal(cartQty[i]);
                        Console.WriteLine($"{products[j].Name,-8} {products[j].Price,6:N2} x {cartQty[i],3} = {itemTotal,8:N2}");
                        total += itemTotal;
                    }
                }
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