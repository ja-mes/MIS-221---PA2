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
            Utils.P("foo");
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
            string[] menuItems = {
                "Convert currencies",
                "Restaurant POS",
                "Exit"
             };

            Menu optionsMenu = new Menu(menuItems);
        }
    }
}