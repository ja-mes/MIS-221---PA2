using System;

namespace PA2
{
    static class Utils
    {
        public static void P(string s = "", bool newLine = true)
        {
            if (newLine)
                Console.WriteLine(s);
            else
                Console.Write(s);
        }

        public static void Divider(char c, int length)
        {
            string outputStr = "";
            for (int i = 0; i < length; i++)
                outputStr += c;

            P(outputStr);
        }

        public static void Header(string title, string subHeader=null)
        {
            Console.Clear();
            Divider('-', 50);
            P($"\n{title.ToUpper()}", false);

            if (subHeader != null)
                P($" / {subHeader}");
            else
                P();

            P();
            Divider('-', 50);
        }
        public static void Exit()
        {
            P("\n\nThank you for using Hospitality Management Software!\n");
            System.Environment.Exit(0);
        }

    }

    class Menu
    {
        private bool error;
        private string[] menuItems;

        public Menu(string[] items)
        {
            menuItems = items;
        }

        public int GetInput()
        {
            Utils.P(); // make sure the menu has a blank line about it

            for (int i = 0; i < menuItems.Length; i++)
            {
                string output;
                output = $"({Convert.ToString(i + 1)}) {menuItems[i]}";

                Utils.P(output);
            }
            Utils.Divider('_', 50);

            if (error)
            {
                Utils.P($"Invalid Input. Select a value between {1} and {menuItems.Length}");
            }

            Utils.P("Please select an option: ", false);
            int selection;
            bool valid = int.TryParse(Console.ReadLine(), out selection);

            // Ensure that input is a valid menu item
            if (!valid || selection < 1 || selection > menuItems.Length)
            {
                error = true;
                return GetInput();
            }

            return selection;
        }
    }

    class ConvertCurriences
    {
        double fromCurrencyAmount;
        double toCurrencyAmount;

        string fromCurrencyType;
        string toCurrencyType;


        void Header()
        {
            Utils.Header("Convert currencies");
            Utils.P("\n");

            if(fromCurrencyType != null && toCurrencyType != null)
            {
                Utils.P($"From {fromCurrencyType} to {toCurrencyType}");

                Utils.P("\n");
            }
        }
        public void Render()
        {
            Header();
            GetFromCurrencyType();
        }

        void GetFromCurrencyType()
        {
            string[] currencyOptions =
            {
                "US Dollar",
                "Canadian Dollar",
                "Euro",
                "Indian Rupee",
                "Japense Yen",
                "Mexican Peso",
                "British Pound",
            };
            Menu currencyMenu = new Menu(currencyOptions);

            int selection;

            Utils.P("Convert from:");
            selection = currencyMenu.GetInput();
            fromCurrencyType = currencyOptions[selection - 1];
            Header(); // Rerender screen  

            Utils.P($"Convert from {fromCurrencyType} to:");
            selection = currencyMenu.GetInput();
            toCurrencyType = currencyOptions[selection - 1];


            GrabCurrency();
        }

        void GrabCurrency(bool fromCurrency = true, bool error = false)
        {
            Header(); // Rerender

            if (error)
            {
                Console.WriteLine("Invalid entry.");
            }

            string displayVal = fromCurrency ? fromCurrencyType : toCurrencyType;
            Utils.P($"Enter amount in {displayVal}: ", false);

            double value;
            if (double.TryParse(Console.ReadLine(), out value)) {
                if(fromCurrency)
                {
                    fromCurrencyAmount = value;
                    GrabCurrency(false); // re-run the function to get to value 
                }
                else
                {
                    toCurrencyAmount = value;
                    ConvertCurrency(); // we now have both values. time to run the conversion 
                }

            }
            else
            {
                GrabCurrency(fromCurrency, true);
            }
        }

        void ConvertCurrency()
        {
            Utils.P(fromCurrencyAmount.ToString());
            Utils.P(toCurrencyAmount.ToString());
        }

    }

    class ResturantPOS
    {
        Menu menu;
        string[] menuOptions =
        {
            "Calculate Bill",
            "Back",
        };

        double foodTotal = 0;
        double alcoholTotal = 0;
        double gratuityTotal = 0;
        double totalDue = 0;

        public void Render()
        {
            Utils.Header("Resturant POS");
            menu = new Menu(menuOptions);
            switch (menu.GetInput())
            {
                case 1:
                    CalculateBill();
                    break;
                case 2:
                    MainScreen.Render();
                    break;
            }
        }

        void CalculateBill()
        {
            GetFood();
            GetAlcohol();
            Finish();
        }

        void DisplayBill()
        {
            Utils.Header("resturant pos", "Calculate Bill");

            Utils.P();
            Utils.P($"Food:\t\t${foodTotal.ToString("F")}");
            Utils.P($"Alcohol:\t${alcoholTotal.ToString("F")}");
            Utils.P($"Gratuity:\t${gratuityTotal.ToString("F")}");
            Utils.P($"Total Due:\t${totalDue.ToString("F")}");
            Utils.P("\n\n");

        }

        void GetFood(bool error = false)
        {
            DisplayBill();

            if (error)
            {
                Utils.P("Invalid entry");
            }

            Utils.P("Enter the food total: $", false);

            if (double.TryParse(Console.ReadLine(), out foodTotal))
            {
                ComputeTotal();
            }
            else
            {
                GetFood(true);
            }

        }
        void GetAlcohol(bool error = false)
        {
            DisplayBill();

            if (error)
            {
                Utils.P("Invalid entry");
            }

            Utils.P("Enter the alchohol total: $", false);

            if (double.TryParse(Console.ReadLine(), out alcoholTotal))
            {
                ComputeTotal();
            }
            else
            {
                GetAlcohol(true);
            }
        }

        void ComputeTotal()
        {
            gratuityTotal = Math.Round(foodTotal * 0.18);
            totalDue = Math.Round(gratuityTotal + foodTotal + alcoholTotal, 2);
            totalDue = Math.Round(totalDue * 1.09, 2);
        }

        void Finish()
        {
            DisplayBill();

            // need to reset these in case the user wants to calculate another bill
            foodTotal = 0;
            alcoholTotal = 0;
            gratuityTotal = 0;
            totalDue = 0;

            string[] finishItems =
            {
                "Calculate Another Bill",
                "Back"
            };
            Menu finishMenu = new Menu(finishItems);

            switch (finishMenu.GetInput())
            {
                case 1:
                    CalculateBill();
                    break;
                case 2:
                    Render();
                    break;
            }

        }

    }

    static class MainScreen
    {
        static Menu menu;

        static string[] menuItems = {
            "Convert currencies",
            "Restaurant POS",
            "Exit"
        };

        public static void Render()
        {
            Utils.Header("Hospitality Management Software");

            menu = new Menu(menuItems);

            switch (menu.GetInput())
            {
                case 1:
                    new ConvertCurriences().Render();
                    break;
                case 2:
                    new ResturantPOS().Render();
                    break;
                case 3:
                    Utils.Exit();
                    break;
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MainScreen.Render();
            Console.ReadKey();
        }
    }
}