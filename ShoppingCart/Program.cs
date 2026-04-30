using System;

namespace ShoppingCartSystem
{
    class Program
    {
        static int receiptCounter = 1;
        static string[] orderHistory = new string[100];
        static int orderCount = 0;

        static void Main(string[] args)
        {
            Product[] products = new Product[]
            {
                new Product(101, "Eggs",     230, 200, "Food"),
                new Product(102, "Bread",     75, 100, "Food"),
                new Product(103, "Milk",     150, 100, "Food"),
                new Product(104, "Chicken",  250, 200, "Food"),
                new Product(105, "Pork",     330, 200, "Food"),
                new Product(106, "Beef",     450, 200, "Food"),
                new Product(107, "Sardines",  40, 250, "Food"),
                new Product(108, "Carrots",   80, 100, "Food"),
                new Product(109, "Garlic",   100, 100, "Food"),
                new Product(110, "Onions",    80, 100, "Food"),
                new Product(111, "Potato",    85, 100, "Food"),
                new Product(112, "Tomato",    70, 100, "Food"),
                new Product(113, "Keyboard", 750,  10, "Electronics"),
                new Product(114, "Mouse",    500,  10, "Electronics"),
                new Product(115, "T-Shirt",  299,  30, "Clothing"),
                new Product(116, "Jeans",    799,  20, "Clothing")
            };

            int[] cartIds = new int[10];
            int[] cartQty = new int[10];

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== MAIN MENU =====");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Add Item to Cart");
                Console.WriteLine("3. Search Product by Name");
                Console.WriteLine("4. Filter Products by Category");
                Console.WriteLine("5. Cart Menu");
                Console.WriteLine("6. View Order History");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                string menuInput = (Console.ReadLine() ?? "").Trim();

                switch (menuInput)
                {
                    case "1":
                        DisplayMenu(products);
                        break;
                    case "2":
                        AddToCart(products, cartIds, cartQty);
                        break;
                    case "3":
                        SearchProduct(products);
                        break;
                    case "4":
                        FilterByCategory(products);
                        break;
                    case "5":
                        CartMenu(products, cartIds, cartQty);
                        break;
                    case "6":
                        ViewOrderHistory();
                        break;
                    case "7":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please enter 1 to 7.");
                        break;
                }
            }
        }

        static void DisplayMenu(Product[] products)
        {
            Console.WriteLine("\nID     NAME        PRICE       STOCK    CATEGORY");
            for (int i = 0; i < products.Length; i++)
            {
                products[i].DisplayProduct();
            }
        }

        static void SearchProduct(Product[] products)
        {
            Console.Write("\nEnter product name to search: ");
            string keyword = (Console.ReadLine() ?? "").Trim().ToLower();

            if (keyword == "")
            {
                Console.WriteLine("Search keyword cannot be empty.");
                return;
            }

            bool found = false;
            Console.WriteLine("\nSearch Results:");

            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Name.ToLower().Contains(keyword))
                {
                    Console.WriteLine($"{products[i].Id}. {products[i].Name} - Php {products[i].Price:N2} - Stock: {products[i].RemainingStock}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No products found matching that name.");
            }
        }

        static void FilterByCategory(Product[] products)
        {
            Console.WriteLine("\nSelect a category:");
            Console.WriteLine("1. Food");
            Console.WriteLine("2. Electronics");
            Console.WriteLine("3. Clothing");
            Console.Write("Choose an option: ");

            string input = (Console.ReadLine() ?? "").Trim();
            string selectedCategory = "";

            switch (input)
            {
                case "1":
                    selectedCategory = "Food";
                    break;
                case "2":
                    selectedCategory = "Electronics";
                    break;
                case "3":
                    selectedCategory = "Clothing";
                    break;
                default:
                    Console.WriteLine("Invalid category option.");
                    return;
            }

            bool found = false;
            Console.WriteLine($"\nProducts in '{selectedCategory}':");
            Console.WriteLine("ID     NAME        PRICE       STOCK");

            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Category == selectedCategory)
                {
                    Console.WriteLine($"{products[i].Id,3}   {products[i].Name,-10}   {products[i].Price,8:N2}   {products[i].RemainingStock,5}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No products found in this category.");
            }
        }

        static void AddToCart(Product[] products, int[] cartIds, int[] cartQty)
        {
            DisplayMenu(products);

            int inputId;
            Console.Write("\nEnter product ID: ");

            if (!int.TryParse(Console.ReadLine(), out inputId))
            {
                Console.WriteLine("Invalid input.");
                return;
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
                return;
            }

            if (products[index].RemainingStock == 0)
            {
                Console.WriteLine("Out of stock.");
                return;
            }

            int qty;
            Console.Write("Enter quantity: ");

            if (!int.TryParse(Console.ReadLine(), out qty) || qty <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            if (qty > products[index].RemainingStock)
            {
                Console.WriteLine($"Not enough stock available. Only {products[index].RemainingStock} left.");
                return;
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
                    return;
                }
            }

            products[index].RemainingStock -= qty;

            if (exists)
            {
                Console.WriteLine("Cart updated.");
            }
            else
            {
                Console.WriteLine("Item added to cart.");
            }
        }

        static void CartMenu(Product[] products, int[] cartIds, int[] cartQty)
        {
            bool inCart = true;

            while (inCart)
            {
                Console.WriteLine("\n===== CART MENU =====");
                Console.WriteLine("1. View Cart");
                Console.WriteLine("2. Update Item Quantity");
                Console.WriteLine("3. Remove Item");
                Console.WriteLine("4. Clear Cart");
                Console.WriteLine("5. Checkout");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choose an option: ");

                string cartInput = (Console.ReadLine() ?? "").Trim();

                switch (cartInput)
                {
                    case "1":
                        ViewCart(products, cartIds, cartQty);
                        break;
                    case "2":
                        UpdateQuantity(products, cartIds, cartQty);
                        break;
                    case "3":
                        RemoveItem(products, cartIds, cartQty);
                        break;
                    case "4":
                        ClearCart(products, cartIds, cartQty);
                        break;
                    case "5":
                        Checkout(products, cartIds, cartQty);
                        inCart = false;
                        break;
                    case "6":
                        inCart = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please enter 1 to 6.");
                        break;
                }
            }
        }

        static void ViewCart(Product[] products, int[] cartIds, int[] cartQty)
        {
            bool empty = true;

            Console.WriteLine("\nCART:");
            Console.WriteLine($"{"NAME",-12} {"QTY",5} {"PRICE",10} {"SUBTOTAL",10}");

            for (int i = 0; i < cartIds.Length; i++)
            {
                if (cartIds[i] == 0) continue;

                for (int j = 0; j < products.Length; j++)
                {
                    if (products[j].Id == cartIds[i])
                    {
                        double subtotal = products[j].GetItemTotal(cartQty[i]);
                        Console.WriteLine($"{products[j].Name,-12} {cartQty[i],5} {products[j].Price,10:N2} {subtotal,10:N2}");
                        empty = false;
                    }
                }
            }

            if (empty)
            {
                Console.WriteLine("Your cart is empty.");
            }
        }

        static void UpdateQuantity(Product[] products, int[] cartIds, int[] cartQty)
        {
            ViewCart(products, cartIds, cartQty);

            int inputId;
            Console.Write("\nEnter product ID to update: ");

            if (!int.TryParse(Console.ReadLine(), out inputId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            int cartIndex = -1;
            for (int i = 0; i < cartIds.Length; i++)
            {
                if (cartIds[i] == inputId)
                {
                    cartIndex = i;
                    break;
                }
            }

            if (cartIndex == -1)
            {
                Console.WriteLine("Item not found in cart.");
                return;
            }

            int productIndex = -1;
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Id == inputId)
                {
                    productIndex = i;
                    break;
                }
            }

            int newQty;
            Console.Write("Enter new quantity: ");

            if (!int.TryParse(Console.ReadLine(), out newQty) || newQty <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            int diff = newQty - cartQty[cartIndex];

            if (diff > products[productIndex].RemainingStock)
            {
                Console.WriteLine($"Not enough stock. Only {products[productIndex].RemainingStock} additional units available.");
                return;
            }

            products[productIndex].RemainingStock -= diff;
            cartQty[cartIndex] = newQty;

            Console.WriteLine("Quantity updated.");
        }

        static void RemoveItem(Product[] products, int[] cartIds, int[] cartQty)
        {
            ViewCart(products, cartIds, cartQty);

            int inputId;
            Console.Write("\nEnter product ID to remove: ");

            if (!int.TryParse(Console.ReadLine(), out inputId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            for (int i = 0; i < cartIds.Length; i++)
            {
                if (cartIds[i] == inputId)
                {
                    for (int j = 0; j < products.Length; j++)
                    {
                        if (products[j].Id == inputId)
                        {
                            products[j].RemainingStock += cartQty[i];
                            break;
                        }
                    }

                    cartIds[i] = 0;
                    cartQty[i] = 0;
                    Console.WriteLine("Item removed from cart.");
                    return;
                }
            }

            Console.WriteLine("Item not found in cart.");
        }

        static void ClearCart(Product[] products, int[] cartIds, int[] cartQty)
        {
            string confirm;
            Console.Write("Are you sure you want to clear the cart? (Y/N): ");
            confirm = (Console.ReadLine() ?? "").Trim().ToUpper();

            while (confirm != "Y" && confirm != "N")
            {
                Console.Write("Invalid input. Please enter Y or N only: ");
                confirm = (Console.ReadLine() ?? "").Trim().ToUpper();
            }

            if (confirm == "N")
            {
                Console.WriteLine("Cart not cleared.");
                return;
            }

            for (int i = 0; i < cartIds.Length; i++)
            {
                if (cartIds[i] != 0)
                {
                    for (int j = 0; j < products.Length; j++)
                    {
                        if (products[j].Id == cartIds[i])
                        {
                            products[j].RemainingStock += cartQty[i];
                            break;
                        }
                    }

                    cartIds[i] = 0;
                    cartQty[i] = 0;
                }
            }

            Console.WriteLine("Cart cleared.");
        }

        static void Checkout(Product[] products, int[] cartIds, int[] cartQty)
        {
            bool empty = true;
            for (int i = 0; i < cartIds.Length; i++)
            {
                if (cartIds[i] != 0)
                {
                    empty = false;
                    break;
                }
            }

            if (empty)
            {
                Console.WriteLine("Your cart is empty. Nothing to checkout.");
                return;
            }

            double total = 0;
            string receiptNo = receiptCounter.ToString("D4");
            string dateTime = DateTime.Now.ToString("MMMM dd, yyyy h:mm tt");

            Console.WriteLine("\n===== RECEIPT =====");
            Console.WriteLine($"Receipt No: {receiptNo}");
            Console.WriteLine($"Date: {dateTime}");
            Console.WriteLine($"\n{"NAME",-12} {"PRICE",8} {"QTY",5} {"TOTAL",10}");

            for (int i = 0; i < cartIds.Length; i++)
            {
                if (cartIds[i] == 0) continue;

                for (int j = 0; j < products.Length; j++)
                {
                    if (products[j].Id == cartIds[i])
                    {
                        double itemTotal = products[j].GetItemTotal(cartQty[i]);
                        Console.WriteLine($"{products[j].Name,-12} {products[j].Price,8:N2} {cartQty[i],5} {itemTotal,10:N2}");
                        total += itemTotal;
                    }
                }
            }

            Console.WriteLine($"\nGrand Total: Php {total:N2}");

            double discount = 0;
            if (total >= 5000)
            {
                discount = total * 0.10;
                Console.WriteLine($"Discount (10%): Php {discount:N2}");
            }

            double finalTotal = total - discount;
            Console.WriteLine($"Final Total: Php {finalTotal:N2}");

            double payment = 0;
            while (true)
            {
                Console.Write("Enter payment: Php ");

                if (!double.TryParse(Console.ReadLine(), out payment) || payment <= 0)
                {
                    Console.WriteLine("Invalid payment amount.");
                    continue;
                }

                if (payment < finalTotal)
                {
                    Console.WriteLine("Insufficient payment.");
                    continue;
                }

                break;
            }

            double change = payment - finalTotal;
            Console.WriteLine($"Payment: Php {payment:N2}");
            Console.WriteLine($"Change: Php {change:N2}");

            Console.WriteLine("\nLOW STOCK ALERT:");
            bool anyLow = false;
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].RemainingStock <= 5)
                {
                    Console.WriteLine($"{products[i].Name} has only {products[i].RemainingStock} stock left.");
                    anyLow = true;
                }
            }
            if (!anyLow)
            {
                Console.WriteLine("All products have sufficient stock.");
            }

            Console.WriteLine("\nUPDATED STOCK:");
            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine($"{products[i].Name,-10} {products[i].RemainingStock}");
            }

            orderHistory[orderCount] = $"Receipt #{receiptNo} - Final Total: Php {finalTotal:N2}";
            orderCount++;
            receiptCounter++;

            for (int i = 0; i < cartIds.Length; i++)
            {
                cartIds[i] = 0;
                cartQty[i] = 0;
            }
        }

        static void ViewOrderHistory()
        {
            if (orderCount == 0)
            {
                Console.WriteLine("\nNo orders yet.");
                return;
            }

            Console.WriteLine("\n===== ORDER HISTORY =====");
            for (int i = 0; i < orderCount; i++)
            {
                Console.WriteLine(orderHistory[i]);
            }
        }
    }
}