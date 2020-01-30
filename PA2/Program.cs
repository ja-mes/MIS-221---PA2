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

        public static void Header(string title)
        {
            Console.Clear();
            Divider('-', 50);
            P($"\n{title}\n");
            Divider('-', 50);
        }
        public static void Exit()
        {
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

        public int Generate()
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
                return Generate();
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

        public ResturantPOS()
        {
            menu = new Menu(menuOptions);
        }
        public void Display()
        {
            Utils.Header("Resturant POS");
            menu.Generate();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // add any additional menu items to this array
            string[] menuItems = {
                "Convert currencies",
                "Restaurant POS",
                "Exit"
             };

            Utils.Header("Hospitality Management Software");

            Menu optionsMenu = new Menu(menuItems);

            switch (optionsMenu.Generate())
            {
                case 1:
                    break;
                case 2:
                    ResturantPOS pos = new ResturantPOS();
                    pos.Display();
                    break;
                case 3:
                    Utils.Exit();
                    break;
            }

            Console.ReadKey();
        }
    }
}