using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Michiel_Duyvejonck
{

    public delegate void StockPlaatsEvent(StockPlaats sender, int e);
    public class StockPlaats
    {
        public event StockPlaatsEvent PlaatsIsVol;
        
        public Item Item { get; set; }
        public int _hoeveelheidInPlaats;

        public StockPlaats(Item item, int hoeveelHeidInPlaats = 0)
        {
            Item = item;
            this._hoeveelheidInPlaats = hoeveelHeidInPlaats;
            this.PlaatsIsVol += Program.StockPlaats_PlaatsIsVol;
            

        }



        ~StockPlaats()
        {
            this.PlaatsIsVol -= Program.StockPlaats_PlaatsIsVol;
        }



        public void addHoeveelheid(int hoeveelHeidToAdd)
        {
            if (_hoeveelheidInPlaats + hoeveelHeidToAdd > Item.MaximumHoeveelheid)
            {
                _hoeveelheidInPlaats += Item.MaximumHoeveelheid - hoeveelHeidToAdd;
                PlaatsIsVol?.Invoke(this, Item.MaximumHoeveelheid - hoeveelHeidToAdd);
            }
            else
            {
                _hoeveelheidInPlaats += hoeveelHeidToAdd; 
            }
        }

        public override string ToString()
        {
            return this.Item.Name;
        }
    }
}
