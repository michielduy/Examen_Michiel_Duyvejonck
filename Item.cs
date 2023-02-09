using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Michiel_Duyvejonck
{
    public class Item
    {
        //basic item class
        public int MaximumHoeveelheid { get; set; }
        public string Name { get; set; }

        public Item(int maximum, string Name)
        {
            MaximumHoeveelheid = maximum;
            this.Name = Name;
        }
    }
}
