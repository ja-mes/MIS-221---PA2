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
            Utils.P();

            for (int i = 0; i < menuItems.Length; i++)
            {
                string output = $"({Convert.ToString(i + 1)}) {menuItems[i]}";
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
        Menu menu;

        string[] menuOptions =
        {
            ""
        };

        void Header()
        {
            Utils.Header("Convert currencies");
        }
        public void Render()
        {
            Header();
            GetFromCurrencyType();
        }

        void GetFromCurrencyType()
        {
            string[] fromCurrencyOptions =
            {
                "(C)anadian Dollar",
                "(E)Uro",
                "(I)ndian Rupee",
                "(J)apense Yen",
                "(M)exican Peso",
                "(B)ritish Pound",
            };
            Menu fromCurrency = new Menu(fromCurrencyOptions);

            switch(fromCurrency.GetInput())
            {

            } 
            
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

            if(error)
            {
                Utils.P("Invalid entry");
            }

            Utils.P("Enter the food total: $", false);

            if(double.TryParse(Console.ReadLine(), out foodTotal)) {
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
	
            if(error)
            {
                Utils.P("Invalid entry");
            }

            Utils.P("Enter the alchohol total: $", false);

            if(double.TryParse(Console.ReadLine(), out alcoholTotal)) {
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

            switch(finishMenu.GetInput())
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