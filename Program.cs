using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Michiel_Duyvejonck
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hoeveel plaatsen bevat je stock");
            int plaatsen = 10;

            Stock stock;
            if (int.TryParse(Console.ReadLine(), out plaatsen))
            {
                stock = new Stock(plaatsen);
            }
            else
            {
                Console.WriteLine("standaard 10 plaatsen");
                stock = new Stock(plaatsen);
            }
            stock.StockVol += Stock_StockVol;
            stock.StockWithDraw += Stock_StockWithDraw;
            stock.KritiekeHoeveelheid += StockPlaats_KritiekeHoeveelheid;
            mainLoop(stock);
            
        }
        private static void StockPlaats_KritiekeHoeveelheid(StockPlaats sender, int e)
        {
            Console.WriteLine("Er is minder dan 20procent beschikbaar van {0}, er is nog {1} beschikbaar", sender.Item.Name, sender._hoeveelheidInPlaats);
        }
        private static void Stock_StockWithDraw(Item thesender, string aantal)
        {
            if (thesender == null)
            {
                Console.WriteLine("The item you asked for does not exist in this stock");
            }
            else
            {
                Console.WriteLine("For {0} there is not enough in stock to withdraw{1}",thesender.Name, aantal);
            }
        }

        public static void Stock_StockVol(Item sender, int e)
        {
            Console.WriteLine("Je probeert van {0}, {1} items in de stock te stoppen.\nMaar de stock zit vol", sender.Name, e);
        }
        public static void StockPlaats_PlaatsIsVol(StockPlaats sender, int e)
        {
            Console.WriteLine("Deze stockplaats zit nu vol met het item: {0}, {1} hoeveelheid is er over",sender.Item.Name, e.ToString());
        }

        public static void mainLoop(Stock stock)
        {
            string exit = "";
            while (exit.ToLower() != "exit")
            {
                Console.WriteLine("Type exit to stop the application." +
                    "\n1) To add an Item to the stock" +
                    "\n2) To withdraw an amount form the stock");
                exit = Console.ReadLine();
                if (exit.ToLower() == "1")
                {
                    Console.WriteLine("What item do you want to add: Name");
                    string name = Console.ReadLine();
                    Console.WriteLine("Whats the maximum amount");
                    int maxamount = int.Parse(Console.ReadLine());

                    Console.WriteLine("Whats the amount");
                    int amount = int.Parse(Console.ReadLine());
                    stock.add(new Item(maxamount, name), amount);
                }
                else if (exit.ToLower() == "2")
                {
                    Console.WriteLine("What item do you want to remove: Name");
                    string name = Console.ReadLine();
                    Console.WriteLine("Whats the amount to be removed");
                    int amount = int.Parse(Console.ReadLine());
                    stock.removeAmount(name.ToLower(),amount);
                }
                else
                {
                    Console.WriteLine("Geen keuze gekozen");
                }
            }
        }
    }
}

