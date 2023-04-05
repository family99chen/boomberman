using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Bombermen
{
    class Empty_Cell : Element
    {
        public Empty_Cell(int x, int y): base(x,y)
        {
            Rectangle rect = new Rectangle();
            rect.Width = size;
            rect.Height = size;
            rect.Fill = Brushes.Green;
            Uielement = rect;
            sym = ' ';
        }
    }
}
