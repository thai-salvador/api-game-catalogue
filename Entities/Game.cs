using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogue.Entities
{
    //This class will be a copy of our GameViewModel
    public class Game
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Producer { get; set; }
        public double Price { get; set; }
    }
}
