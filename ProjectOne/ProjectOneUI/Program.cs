// See https://aka.ms/new-console-template for more information
global using Serilog;
using ProjectOneUI;
using ProjectOneDL;
using ProjectOneBL;
using Microsoft.Extensions.Configuration;


Log.Logger = new LoggerConfiguration()
    .WriteTo.File("./logs/user.txt") 
    .CreateLogger();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string _connectionString = configuration.GetConnectionString("Reference2DB");


bool repeat = true;
IMenu menu = new MainMenu();

while (repeat)
{
    Console.Clear();
    
    menu.Display();
    string ans = menu.UserChoice();

    switch (ans)
    {
        case "ReplenishInventory":
            Log.Information("Displaying Replenish Inventory Menu to user");
            menu = new ReplenishInvMenu(new CustomerBL(new SQLRepository(_connectionString)));
            break;
        case "ViewOrderHistory":
            Log.Information("Displaying View Order History Menu to user");
            menu = new ViewOrderHistMenu(new CustomerBL(new SQLRepository(_connectionString)));
            break;
        case "PlaceOrder_2":
            Log.Information("Displaying Place Order part 2 Menu to user");
            menu = new PlaceOrderMenu_2(new CustomerBL(new SQLRepository(_connectionString)));
            break;
        case "PlaceOrder_1":
            Log.Information("Displaying Place Order part 1 Menu to user");
            menu = new PlaceOrderMenu_1();
            break;
        case "ViewStoreInventory":
            Log.Information("Displaying the View Store Inventory Menu to user");
            menu = new ViewStoreInvMenu(new CustomerBL(new SQLRepository(_connectionString)));
            break;
        case "SearchCustomer":
            Log.Information("Displaying Search Customer Menu to user");
            menu = new SearchCustMenu(new CustomerBL(new SQLRepository(_connectionString)));
            break;
        case "AddCustomer":
            Log.Information("Displaying Add Customer Menu to user");
            menu = new AddCustMenu(new CustomerBL(new SQLRepository(_connectionString)));
            break;
        case "MainMenu":
            Log.Information("Displaying Main Menu to user");
            menu = new MainMenu();
            break;
        case "Exit":
            repeat = false;
            break;
        default:
            Console.WriteLine("Page Does Not Exist");
            Console.WriteLine("Please press Enter to continue");
            Console.ReadLine();
            break;
    }






}
