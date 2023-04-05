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
    class Wall : Element
    {
        public Wall(int x, int y) : base(x,y)
        {
            Rectangle rect = new Rectangle();
            rect.Width = size;
            rect.Height = size;
            rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Wall.png", UriKind.Relative)) };

            Uielement = rect;
            X = x;
            Y = y;
            sym = 'o';
        }
    }
}
