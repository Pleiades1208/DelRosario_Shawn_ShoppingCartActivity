# Shopping Cart System Program


Shawn Cedric C. Del Rosario BSIT-1-1

## About the Project
A console-based Shopping Cart System built in C# using classes, objects, and arrays. The program allows users to browse products, manage a cart, and complete a checkout with receipt generation.

---

## Part 1 Features
- Product menu display with ID, name, price, and stock
- Add items to cart with full input validation
- Duplicate item handling (updates existing cart entry)
- Cart full detection
- 10% discount applied when grand total is Php 5,000 or more
- Discount line only displays when a discount is actually applied
- Receipt display with itemized totals
- Updated stock display after checkout
- Separate file-per-class organization (Product.cs and Program.cs)

---

## Part 2 Features
- **Cart Management Menu** — view cart, update item quantity, remove an item, clear cart, and proceed to checkout all from a dedicated cart menu
- **Product Search** — search products by name using a keyword; displays matching results with price and stock
- **Product Categories** — products are tagged with a category (Food, Electronics, Clothing); users can filter the product list by category
- **Payment Validation** — user enters payment amount; must be numeric and greater than or equal to the final total; re-prompts if insufficient
- **Change Computation** — displays exact change after valid payment is entered
- **Receipt Details** — each receipt includes a receipt number, checkout date and time, itemized purchases, grand total, discount (if applicable), final total, payment, and change
- **Low Stock Alert** — after checkout, any product with 5 or fewer remaining units is flagged with a warning message
- **Order History** — completed transactions are stored and viewable from the main menu at any time during the program run
- **Strict Y/N Validation** — all yes/no prompts re-prompt continuously until a valid Y or N is entered


## AI Usage

### Part 1
I used AI mainly when I was stuck on how to properly use arrays with objects in C#. It helped me understand how to store multiple Product objects and access their fields without getting confused. It also gave me a clearer idea of how the structure works, especially coming from a Python background.

I also used AI when I wanted to clean up how my program looks on the console. My first output was hard to read, so I asked for help on how to align everything properly. After that, I improved the layout of my menu, receipt, and stock display so they look more organized and easier to follow. I still made small adjustments myself to match what I wanted.

I also asked AI for some help in making the flowchart, namely in the flow and just the looks of it. There were also moments where I wasn't sure about parts of the logic like calculating totals or handling repeated items in the cart, so I used AI to guide me through the structure. I made sure to understand everything before applying it.

**Prompts/questions I asked:**
- "How do I store multiple objects in an array and access their values in C#?"
- "How can I make console output look aligned and easier to read?"
- "How do I calculate totals using methods inside a class?"
- "How do I properly handle cart items when the same product is added again?"

### Part 2
For Part 2, I used AI more extensively since the feature set was significantly larger. I used it to help plan the overall structure of the enhanced program, particularly how to organize the cart management menu and how to break the program into separate static methods to keep the code readable.

I also used AI to help implement features I hadn't worked with before, such as generating a formatted receipt with a receipt number and live date/time, storing order history in an array, and computing change from a payment. After getting the structure from AI, I made sure to trace through the logic myself and adjust parts to match my existing code style and product list.

**Prompts/questions I asked:**
- "How do I add a cart management menu with view, update, remove, and clear options?"
- "How do I search products by name using a keyword in C#?"
- "How do I add a Category field to my Product class and filter by it?"
- "How do I generate a receipt number and display the current date and time in C#?"
- "How do I validate payment and compute change?"
- "How do I store completed transactions in an array for order history?"
- "How do I show a low stock alert after checkout for products with 5 or fewer units?"

**What I changed or improved after using AI:**
- Adjusted the product list to include Electronics and Clothing items so the category filter is actually demonstrable
- Kept the same code style I used in Part 1 (full if/else blocks, no ternary expressions) for consistency
- Reviewed and traced through all logic before finalizing to make sure I understood each part
