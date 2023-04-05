using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombermen
{
    public class Element : Cell
    {
        public Element(int X, int Y) : base(X,Y)
        {     }
        public override bool Equals(object obj)
        {
            Element other = (Element)obj;
            if (X == other.X && Y == other.Y)
                return true;
            else
            {
                return false;
            }

        }
        public int GetX()
        { return X; }
        public int GetY()
        { return Y; }
        public int manhattan(Player pl)
        {
            return Math.Abs(X - pl.X) + Math.Abs(Y - pl.Y);
        }
    }
}
