using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Michiel_Duyvejonck
{
    public delegate void StockEventHandler(Item thesender, int aantal);
    public delegate void StockWithdrawEvent(Item thesender, string aantal);
    public class Stock
    {
        public event StockEventHandler StockVol;
        public event StockWithdrawEvent StockWithDraw;
        public event StockPlaatsEvent KritiekeHoeveelheid;
        public List<StockPlaats> _plaatsen;
        public int _aantalPlaatsen;
        public Stock(int aantalPlaatsen)
        {
            _plaatsen = new List<StockPlaats>(aantalPlaatsen);
            this._aantalPlaatsen = aantalPlaatsen;

        }
        private void AddExistingItemsToStock(StockPlaats plaats, int aantal)
        {
            plaats.addHoeveelheid(aantal);
        }
        public void add(Item itemToAdd, int aantal)
        {
            foreach (StockPlaats plaats in _plaatsen)
            {
                if (plaats.Item.Name.ToLower() == itemToAdd.Name.ToLower())
                {
                    AddExistingItemsToStock(plaats, aantal);
                }
                else
                {
                    AddNewItemToStock(itemToAdd, aantal);
                }
            }
            Console.WriteLine("Item was added to the stock");
        }

        private void AddNewItemToStock(Item itemToAdd, int aantal)
        {
            if (_plaatsen.Count < this._aantalPlaatsen)
            {
                _plaatsen.Add(new StockPlaats(itemToAdd, aantal));
            }
            else
            {
                StockVol?.Invoke(itemToAdd, aantal);
            }
        }

        public void removeAmount(string item, int amount)
        {
            foreach (StockPlaats plaats in _plaatsen)
            {
                if (plaats.Item.Name.ToLower() == item)
                {
                    if (plaats._hoeveelheidInPlaats > amount)
                    {
                        plaats._hoeveelheidInPlaats -= amount;
                        Console.WriteLine("{0}, {1} items where removed", plaats.Item.Name, amount.ToString());
                        if (plaats._hoeveelheidInPlaats/plaats.Item.MaximumHoeveelheid * 100<= 20)
                        {
                            KritiekeHoeveelheid?.Invoke(plaats, 0);
                        }
                    }
                    else
                    {
                        StockWithDraw?.Invoke(plaats.Item, amount.ToString());
                    }
                }
                else
                {
                    StockWithDraw?.Invoke(null, amount.ToString());
                }
            }
        }

    }
}
