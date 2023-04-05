using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bombermen
{
     public abstract class Cell
     {
        [JsonProperty]
        protected int x;
        [JsonProperty]
        protected int y;
        public char sym;
        public bool explode = false;

        protected const int size = 20;
        [JsonIgnore]
        public UIElement Uielement { get; set; }
        public int X
        {
            get
            {
                return x;
            }
            protected set
            {
                x = value;
            }
        }
        public int Y
        { 
            get
            {
                return y;
            }
            protected set
            {
                y = value;
            }
        }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
     }
}
