﻿using System;
using System.Collections.Generic;

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


        public static void BuildScreen(string title, string subHeader = null)
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

    /*
     * Exchange rates uses a collection of dictionaries to store the exchange rates of the supported currencies
     */
    static class ExchangeRates
    {
        public static Dictionary<string, double> US = new Dictionary<string, double>()
        {
            { "US Dollar", 1 },
            { "Canadian Dollar", 1.323400},
            { "Euro", 0.902254},
            { "Indian Rupee", 71.534161},
            { "Japense Yen", 108.466743},
            { "Mexican Peso", 18.858311},
            { "British Pound", 0.757946},
        };

        public static Dictionary<string, double> Canadian = new Dictionary<string, double>()
        {
            { "Canadian Dollar", 1 },
            { "US Dollar", 0.755467},
            { "Euro", 0.681584},
            { "Indian Rupee", 54.040256},
            { "Japense Yen", 81.915460},
            { "Mexican Peso", 14.247736},
            { "British Pound", 0.573290},
        };

        public static Dictionary<string, double> Euro = new Dictionary<string, double>()
        {
            { "Euro", 1 },
            { "Canadian Dollar", 1.467085},
            { "US Dollar", 1.108352},
            { "Indian Rupee", 79.282554},
            { "Japense Yen", 120.200349},
            { "Mexican Peso", 20.895462},
            { "British Pound", 0.841332},
        };

        public static Dictionary<string, double> Indian = new Dictionary<string, double>()
        {
            { "US Dollar", 0.013980 },
            { "Canadian Dollar", 0.018504},
            { "Euro", 0.012614},
            { "Indian Rupee", 1},
            { "Japense Yen", 1.516199},
            { "Mexican Peso", 0.263740},
            { "British Pound", 0.010606},
        };

        public static Dictionary<string, double> Japense = new Dictionary<string, double>()
        {
            { "US Dollar", 0.009219},
            { "Canadian Dollar", 0.012204},
            { "Euro", 0.008314},
            { "Indian Rupee", 0.659389},
            { "Japense Yen", 1 },
            { "Mexican Peso", 0.173930},
            { "British Pound", 0.006993},
        };

        public static Dictionary<string, double> Mexican = new Dictionary<string, double>()
        {
            { "US Dollar", 0.053169 },
            { "Canadian Dollar", 0.070683},
            { "Euro", 0.048059},
            { "Indian Rupee", 3.791722},
            { "Japense Yen", 5.778462},
            { "Mexican Peso", 1},
            { "British Pound", 0.040907},
        };

        public static Dictionary<string, double> British = new Dictionary<string, double>()
        {
            { "US Dollar", 1.299683 },
            { "Canadian Dollar", 1.727779},
            { "Euro", 1.174894},
            { "Indian Rupee", 92.677850 },
            { "Japense Yen", 141.260915},
            { "Mexican Peso", 24.438507},
            { "British Pound", 1},
        };
    }

    class ConvertCurriences
    {
        readonly string[] currencyOptions =
        {
            "US Dollar",
            "Canadian Dollar",
            "Euro",
            "Indian Rupee",
            "Japense Yen",
            "Mexican Peso",
            "British Pound",
        };

        // use to calculate the exchance
        double fromCurrencyAmount;
        string fromCurrencyType;
        string toCurrencyType;

        void Intialize()
        {
            Utils.BuildScreen("Convert currencies");
            Utils.P("\n");

            if (fromCurrencyType != null && toCurrencyType != null)
            {
                Utils.P($"From {fromCurrencyType} to {toCurrencyType}");

                Utils.P("\n");
            }
        }
        public void Render()
        {
            Intialize();
            GetCurrencyType();
        }

        void GetCurrencyType()
        {
            Menu fromMenu = new Menu(currencyOptions);

            int selection;

            // get from currency
            Utils.P("Convert from:");
            selection = fromMenu.GetInput();
            fromCurrencyType = currencyOptions[selection - 1];
            Intialize(); // Rerender screen  


            // get to currency type

            // we don't want the from currency showing up in the options. remove it
            var list = new List<string>(currencyOptions);
            list.Remove(fromCurrencyType);
            string[] toCurrencyOptions = list.ToArray();

            Menu toMenu = new Menu(toCurrencyOptions);

            Utils.P($"Convert from {fromCurrencyType} to:");
            selection = toMenu.GetInput();
            toCurrencyType = toCurrencyOptions[selection - 1];


            GetValue();
        }

        void GetValue(bool error = false)
        {
            Intialize(); // Rerender

            if (error)
            {
                Console.WriteLine("Invalid entry.");
            }

            Utils.P($"Enter amount in {fromCurrencyType}: ", false);

            if (double.TryParse(Console.ReadLine(), out fromCurrencyAmount))
            {
                ConvertCurrency(); // we now have a value. time to run the conversion
            }
            else
            {
                GetValue(true);
            }
        }

        void ConvertCurrency()
        {
            Intialize(); // Rerender
            double rate = 1;

            switch (fromCurrencyType)
            {
                case "US Dollar":
                    rate = ExchangeRates.US[toCurrencyType];
                    break;
                case "Canadian Dollar":
                    rate = ExchangeRates.Canadian[toCurrencyType];
                    break;
                case "Euro":
                    rate = ExchangeRates.Euro[toCurrencyType];
                    break;
                case "Indian Rupee":
                    rate = ExchangeRates.Indian[toCurrencyType];
                    break;
                case "Japense Yen":
                    rate = ExchangeRates.Japense[toCurrencyType];
                    break;
                case "Mexican Peso":
                    rate = ExchangeRates.Mexican[toCurrencyType];
                    break;
                case "British Pound":
                    rate = ExchangeRates.British[toCurrencyType];
                    break;
            }

            double result = rate * fromCurrencyAmount;

            // output conversion to console
            Utils.P($"{fromCurrencyAmount} {fromCurrencyType} = {result.ToString("F")} {toCurrencyType}");

            string[] restartMenuOptions =
            {
                $"Convert {fromCurrencyType} to {toCurrencyType} again",
                "Select Another Conversion",
                "Back",
            };

            Utils.P("\n");
            Menu restartMenu = new Menu(restartMenuOptions);

            switch (restartMenu.GetInput())
            {
                case 1:
                    GetValue();
                    break;
                case 2:
                    new ConvertCurriences().Render();
                    break;
                case 3:
                    MainScreen.Render();
                    break;
            }


        }

    }

    class ResturantPOS
    {
        Menu menu;

        readonly string[] menuOptions =
        {
            "Calculate Bill",
            "Back",
        };

        const double TAX_RATE = 1.09;
        const double GRATUITY_PERCENTAGE = 0.18;

        double foodTotal, alcoholTotal, gratuityTotal, billTotal, amountPaid, changeDue, totalDue;

        public void Render()
        {
            Utils.BuildScreen("Resturant POS");
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
        void Initialize()
        {
            Utils.BuildScreen("resturant pos", "Calculate Bill");

            // output the calculations. round with to string
            Console.WriteLine("");
            Console.WriteLine($"Food:\t\t${foodTotal.ToString("F")}");
            Console.WriteLine($"Alcohol:\t${alcoholTotal.ToString("F")}");
            Console.WriteLine($"Gratuity:\t${gratuityTotal.ToString("F")}");
            Console.WriteLine($"Bill Total: \t${billTotal.ToString("F")}");
            Console.WriteLine("\n");
            Console.WriteLine($"Amount Paid:\t${amountPaid.ToString("F")}");
            Console.WriteLine($"Amount Due:\t${totalDue.ToString("F")}");
            Console.WriteLine($"Change Due:\t${changeDue.ToString("F")}");
            Console.WriteLine("\n\n");

        }

        void CalculateBill()
        {
            // make sure everything is zero before we start running calculations
            foodTotal = 0;
            alcoholTotal = 0;
            changeDue = 0;
            gratuityTotal = 0;
            billTotal = 0;
            amountPaid = 0;
            totalDue = 0;

            GetFood();
            GetAlcohol();
            GetAmountPaid();
            Finish();
        }

        void GetFood(bool error = false)
        {
            Initialize();

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
            Initialize();

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

        void GetAmountPaid(bool error = false, bool notEnough = false)
        {
            Initialize();

            if (error)
            {
                Console.WriteLine("Invalid Entry");
            }
            else if(notEnough)
            {
                Console.WriteLine($"${amountPaid.ToString("F")} has been paid but ${totalDue.ToString("F")} is still due!");
            }

            Console.Write("Enter amount paid: $");

            double pmtAmount;
            if (double.TryParse(Console.ReadLine(), out pmtAmount))
            {

                if (pmtAmount < totalDue) { 
                    ComputeTotal(pmtAmount);
                    GetAmountPaid(false, true);
                }

                ComputeTotal(pmtAmount);
            }
            else
            {
                GetAmountPaid(true);
            }

        }

        void ComputeTotal(double pmtAmount = 0)
        {
            // there is no need to round these numbers because they are rounded with ToString during output

            // calculate gratuity on food only using the specified percentage. 
            gratuityTotal = foodTotal * GRATUITY_PERCENTAGE;

            // add specified tax rate to food and alcohol. don't tax gratuity
            billTotal = (foodTotal + alcoholTotal) * TAX_RATE;
            billTotal += gratuityTotal;

            totalDue = billTotal;

            amountPaid += pmtAmount;
            totalDue -= amountPaid;

            if(amountPaid > totalDue)
            {
                changeDue = amountPaid - totalDue;
            }
            else
            {
                changeDue = 0;
            }
        }

        void Finish()
        {
            Initialize();

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
            Utils.BuildScreen("Hospitality Management Software");

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