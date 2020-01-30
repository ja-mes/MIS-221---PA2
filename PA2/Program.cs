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
            Utils.Divider('-', 50);
            for(int i = 0; i < menuItems.Length; i++)
            {
                string output = $"({Convert.ToString(i + 1)}) {menuItems[i]}";
                Utils.P(output);
            }
            Utils.Divider('-', 50);

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
                Generate();
            }

            return selection;
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

            int userOption;

            Menu optionsMenu = new Menu(menuItems);

            userOption = optionsMenu.Generate();

            switch (userOption) {
                case 1:
                    // Convert Currencies
                    break;
                case 2:
                    // Restuarant POS
                    break;
                case 3:
                    Exit();
                    break;
            }

            Console.ReadKey();
        }

        static void Exit()
        {
            System.Environment.Exit(0);
        }
    }
}