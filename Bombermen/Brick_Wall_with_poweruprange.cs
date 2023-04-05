using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bombermen
{
    class Brick_Wall_with_poweruprange : Element
    {
        public Brick_Wall_with_poweruprange(int x, int y) : base(x,y)
        {
            Rectangle rect = new Rectangle();
            rect.Width = size;
            rect.Height = size;
            rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Brick.png", UriKind.Relative)) };
           
            Uielement = rect;
            X = x;
            Y = y;
            sym = '*';
        }
    }
}
