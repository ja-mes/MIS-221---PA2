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

        public int Render()
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
                return Render();
            }

            return selection;
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
            switch (menu.Render())
            {
                case 1:
                    Init();
                    break;
                case 2:
                    MainScreen.Render();
                    break;
            }
        }

        void Init()
        {
            GetFood();
            GetAlcohol();
            Header();
        }

        void Header()
        {
            Utils.Header("resturant pos", "Calculate Bill");

            Utils.P();
            Utils.P($"Food:\t\t${Convert.ToString(foodTotal)}");
            Utils.P($"Alcohol:\t${Convert.ToString(alcoholTotal)}");
            Utils.P($"Gratuity:\t${Convert.ToString(gratuityTotal)}");
            Utils.P($"Total Due:\t${Convert.ToString(totalDue)}");
            Utils.P("\n\n");

        }

        void GetFood(bool error = false)
        {
            Header();

            if(error)
            {
                Utils.P("Invalid entry");
            }

            Utils.P("Enter the food total: $", false);

            if(double.TryParse(Console.ReadLine(), out foodTotal)) {
                Calculate();
            }
            else
            {
                GetFood(true);
            }

        }
        void GetAlcohol(bool error = false)
        {
            Header();
	
            if(error)
            {
                Utils.P("Invalid entry");
            }

            Utils.P("Enter the alchohol total: $", false);

            if(double.TryParse(Console.ReadLine(), out alcoholTotal)) {
                Calculate();
            }
            else
            {
                GetAlcohol(true);

            }
        }

        void Calculate()
        {
            gratuityTotal = Math.Round(foodTotal * 0.18);
            totalDue = Math.Round(gratuityTotal + foodTotal + alcoholTotal, 2);
            totalDue = Math.Round(totalDue * 1.09, 2);
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

            switch (menu.Render())
            {
                case 1:
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