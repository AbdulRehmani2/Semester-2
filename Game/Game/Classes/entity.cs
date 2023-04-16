using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Classes
{
    class entity
    {
        public char[,] model;
        public int x;
        public int y;
        public int health;
        public List<List<int>> bullets;
    }
}
