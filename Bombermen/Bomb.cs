using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bombermen
{
    class Bomb : Element
    {
        Main Main;
        World map;
        Player Pl;
        int im = 1;
        private DispatcherTimer timer = null;
        private DispatcherTimer timer2 = null;
        List<Enemy> enemyList;
        bool isexplode = false;

        //place for key
        int kx;
        int ky;
        int urx;
        int ury;
        int unx;
        int uny;
        int dx;
        int dy;
        
        public Bomb(int x, int y, Main main, World w, Player pl, List<Enemy> enemy) : base(x,y)
        {
            Pl = pl;
            enemyList = enemy;
            Rectangle rect = new Rectangle();
            rect.Width = size;
            rect.Height = size;
            rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Bomb1.png", UriKind.Relative)) };

            Uielement = rect;
            X = x;
            Y = y;
            sym = '@';
            Main = main;
            map = w;
            checkexplode();
            Explosion(map,Pl);
            
        }
        async public void Explosion(World map, Player pl)
        {
            timerStart();
            await Task.Run(() => Clear_Map(map, pl));
            if (isexplode == true)
            {
                map[Y, X].sym = ' ';
                isexplode = false;
                timer.Stop();
                timer2.Stop();
                return;
            }
            Main.bomb_player.Open(new Uri("TickingBomb.mp3", UriKind.Relative));
            Main.bomb_player.Play();
            Main.Draw(map);
            await Task.Run(() => Clear_Dots(map));
            //map refresh function
            Main.Draw(map);
            if(enemyList.Count > 0)
            {
                Main.game_canv.Children.Remove(Pl.Uielement);
                Main.Draw_Player(Pl);

            }
            //Main.RefreshEnemy();
            //Main.game_canv.Children.Remove(Pl.Uielement);
            //Main.Draw_Player(Pl);
            Main.calculate--;
            map[Y, X].explode = false;
            map[Y, X].sym = ' ';
            isexplode = false;
            timer.Stop();
            timer2.Stop();
        }

        async public void Explosion_immediate(World map, Player pl)
        {
            await Task.Run(() => Clear_Map_no_sleep(map, pl));
            Main.bomb_player.Open(new Uri("TickingBomb.mp3", UriKind.Relative));
            Main.bomb_player.Play();
            Main.Draw(map);
            await Task.Run(() => Clear_Dots(map));
            //map refresh function
            Main.Draw(map);
            if (enemyList.Count > 0)
            {
                Main.game_canv.Children.Remove(Pl.Uielement);
                Main.Draw_Player(Pl);

            }
            //Main.RefreshEnemy();
            //Main.game_canv.Children.Remove(Pl.Uielement);
            //Main.Draw_Player(Pl);
            Main.calculate--;
            Main.game_canv.Children.Remove(Uielement);
            map[Y, X].explode = false;
        }

        public void Clear_Map(World world, Player pl)
        {
            
            Thread.Sleep(2000);
            if (isexplode == true) return;
            if (X + 20 < world.Columns * 20 && world[y, X + 20].sym != 'o' && world[y, X + 20].sym != '&')
            {
                if (world[y, X + 20].sym == '$')
                {
                    kx = X + 20;
                    ky = y;
                    //world[y, X + 20].sym = 'K';
                }
                if (world[y, X + 20].sym == '*') //* double range hidden
                {
                    urx = X + 20;
                    ury = y;
                }
                if (world[y, X + 20].sym == '%') //* double number hidden
                {
                    unx = X + 20;
                    uny = y;
                }
                if (world[y, X + 20].sym == 'D') // door hidden
                {
                    dx = X + 20;
                    dy = y;
                }
                if (world[y, X + 20].sym == 'K')
                {
                    pl.IsAlive = false;
                }

                if (y == pl.Y && x + 20 == pl.X)
                { pl.IsAlive = false; }

                if (enemyList.Count > 0)
                {
                    foreach (var en in enemyList)
                    {
                        if (y == en.Y && x + 20 == en.X)
                        { en.isAlive = false; }
                    }
                }

                if (pl.haspoweruprange)
                {
                    if (X + 40 < world.Columns * 20 && world[y, X + 40].sym != 'o' && world[y, X + 40].sym != '&' &&
                        world[y, X + 20].sym != '$' && world[y, X + 20].sym != '#' && world[y, X + 20].sym != '*' && world[y, X + 20].sym != '%')
                    {
                        if (world[y, X + 40].sym == '$')
                        {
                            kx = X + 40;
                            ky = y;
                            //world[y, X + 20].sym = 'K';
                        }
                        if (world[y, X + 40].sym == '*') //* double range hidden
                        {
                            urx = X + 40;
                            ury = y;
                        }
                        if (world[y, X + 40].sym == '%') //* double number hidden
                        {
                            unx = X + 40;
                            uny = y;
                        }
                        if (world[y, X + 40].sym == 'D') // door hidden
                        {
                            dx = X + 40;
                            dy = y;
                        }
                        if (world[y, X + 40].sym == 'K')
                        {
                            pl.IsAlive = false;
                        }

                        if (y == pl.Y && x + 40 == pl.X)
                        { pl.IsAlive = false; }

                        if (enemyList.Count > 0)
                        {
                            foreach (var en in enemyList)
                            {
                                if (y == en.Y && x + 40 == en.X)
                                { en.isAlive = false; }
                            }
                        }
                        world[y, X + 40].sym = '>';
                    }
                }
                world[y, X + 20].sym = '>';

            }
            if (x - 20 >= 0 && world[y, x - 20].sym != 'o' && world[y, x - 20].sym != '&')
            {
                if (world[y, X - 20].sym == '$')
                {
                    kx = X - 20;
                    ky = y;
                    //world[ky, X - 20].sym = 'K';
                }
                if (world[y, X - 20].sym == '*') //* double range hidden
                {
                    urx = X - 20;
                    ury = y;
                }
                if (world[y, X - 20].sym == '%') //* double number hidden
                {
                    unx = X - 20;
                    uny = y;
                }
                if (world[y, X - 20].sym == 'D') // door hidden
                {
                    dx = X - 20;
                    dy = y;
                }
                if (world[y, X - 20].sym == 'K')
                {
                    pl.IsAlive = false;
                }

                if (y == pl.Y && x - 20 == pl.X)
                { pl.IsAlive = false; }

                if (enemyList.Count > 0)
                {
                    foreach (var en in enemyList)
                    {
                        if (y == en.Y && x - 20 == en.X)
                        { en.isAlive = false; }
                    }
                }

                if (pl.haspoweruprange)
                {
                    if (X - 40 < world.Columns * 20 && world[y, X - 40].sym != 'o' && world[y, X - 40].sym != '&'
                        && world[y, X - 20].sym != '$' && world[y, X - 20].sym != '#' && world[y, X - 20].sym != '*' && world[y, X - 20].sym != '%')
                    {
                        if (world[y, X - 40].sym == '$')
                        {
                            kx = X - 40;
                            ky = y;
                            //world[y, X + 20].sym = 'K';
                        }
                        if (world[y, X - 40].sym == '*') //* double range hidden
                        {
                            urx = X - 40;
                            ury = y;
                        }
                        if (world[y, X - 40].sym == '%') //* double number hidden
                        {
                            unx = X - 40;
                            uny = y;
                        }
                        if (world[y, X - 40].sym == 'D') // door hidden
                        {
                            dx = X - 40;
                            dy = y;
                        }
                        if (world[y, X - 40].sym == 'K')
                        {
                            pl.IsAlive = false;
                        }

                        if (y == pl.Y && x - 40 == pl.X)
                        { pl.IsAlive = false; }

                        if (enemyList.Count > 0)
                        {
                            foreach (var en in enemyList)
                            {
                                if (y == en.Y && x - 40 == en.X)
                                { en.isAlive = false; }
                            }
                        }
                        world[y, X - 40].sym = '<';
                    }
                }
                world[y, x - 20].sym = '<';
            }
            if (y + 20 < world.Rows * 20 && world[y + 20, x].sym != 'o' && world[y + 20, x].sym != '&')
            {
                if (world[y + 20, x].sym == '$')
                {
                    kx = x;
                    ky = y + 20;
                    //world[ky, kx].sym = 'K';
                }
                if (world[y + 20, x].sym == '*') //* double range hidden
                {
                    urx = x;
                    ury = y + 20;
                }
                if (world[y + 20, x].sym == '%') //* double number hidden
                {
                    unx = x;
                    uny = y + 20;
                }
                if (world[y + 20, x].sym == 'D') // door hidden
                {
                    dx = x;
                    dy = y + 20;
                }
                if (world[y + 20, x].sym == 'K')
                {
                    pl.IsAlive = false;
                }

                if (y + 20 == pl.Y && x == pl.X)
                { pl.IsAlive = false; }

                if (enemyList.Count > 0)
                {
                    foreach (var en in enemyList)
                    {
                        if (y + 20 == en.Y && x == en.X)
                        { en.isAlive = false; }
                    }
                }

                if (pl.haspoweruprange)
                {
                    if (Y + 40 < world.Columns * 20 && world[y + 40, X].sym != 'o' && world[y + 40, X].sym != '&'
                        && world[y + 20, X].sym != '$' && world[y + 20, X].sym != '#' && world[y + 20, X].sym != '*' && world[y + 20, X].sym != '%')
                    {
                        if (world[y + 40, X].sym == '$')
                        {
                            kx = X;
                            ky = y + 40;
                            //world[y, X + 20].sym = 'K';
                        }
                        if (world[y + 40, X].sym == '*') //* double range hidden
                        {
                            urx = X;
                            ury = y + 40;
                        }
                        if (world[y + 40, X].sym == '%') //* double number hidden
                        {
                            unx = X;
                            uny = y + 40;
                        }
                        if (world[y + 40, X].sym == 'D') // door hidden
                        {
                            dx = X;
                            dy = y + 40;
                        }
                        if (world[y + 40, X].sym == 'K')
                        {
                            pl.IsAlive = false;
                        }

                        if (y + 40 == pl.Y && x == pl.X)
                        { pl.IsAlive = false; }

                        if (enemyList.Count > 0)
                        {
                            foreach (var en in enemyList)
                            {
                                if (y + 40 == en.Y && x == en.X)
                                { en.isAlive = false; }
                            }
                        }
                        world[y + 40, X].sym = '-';
                    }
                }
                world[y + 20, x].sym = '-';
            }
            if (y - 20 >= 0 && world[y - 20, x].sym != 'o' && world[y - 20, x].sym != '&')
            {
                if (world[y - 20, x].sym == '$')
                {
                    kx = x;
                    ky = y - 20;
                    //world[ky, kx].sym = 'K';
                }
                if (world[y - 20, x].sym == '*') //* double range hidden
                {
                    urx = x;
                    ury = y - 20;
                }
                if (world[y - 20, x].sym == '%') //* double number hidden
                {
                    unx = x;
                    uny = y - 20;
                }
                if (world[y - 20, x].sym == 'D') // door hidden
                {
                    dx = x;
                    dy = y - 20;
                }
                if (world[y - 20, x].sym == 'K')
                {
                    pl.IsAlive = false;
                }

                if (y - 20 == pl.Y && x == pl.X)
                { pl.IsAlive = false; }

                if (enemyList.Count > 0)
                {
                    foreach (var en in enemyList)
                    {
                        if (y - 20 == en.Y && x == en.X)
                        { en.isAlive = false; }
                    }
                }

                if (pl.haspoweruprange)
                {
                    if (Y - 40 < world.Columns * 20 && world[y - 40, X].sym != 'o' && world[y - 40, X].sym != '&'
                        && world[y - 20, X].sym != '$' && world[y - 20, X].sym != '#' && world[y - 20, X].sym != '*' && world[y - 20, X].sym != '%')
                    {
                        if (world[y - 40, X].sym == '$')
                        {
                            kx = X;
                            ky = y - 40;
                            //world[y, X + 20].sym = 'K';
                        }
                        if (world[y - 40, X].sym == '*') //* double range hidden
                        {
                            urx = X;
                            ury = y - 40;
                        }
                        if (world[y - 40, X].sym == '%') //* double number hidden
                        {
                            unx = X;
                            uny = y - 40;
                        }
                        if (world[y - 40, X].sym == 'D') // door hidden
                        {
                            dx = X;
                            dy = y - 40;
                        }
                        if (world[y - 40, X].sym == 'K')
                        {
                            pl.IsAlive = false;
                        }

                        if (y - 40 == pl.Y && x == pl.X)
                        { pl.IsAlive = false; }

                        if (enemyList.Count > 0)
                        {
                            foreach (var en in enemyList)
                            {
                                if (y - 40 == en.Y && x == en.X)
                                { en.isAlive = false; }
                            }
                        }
                            
                        world[y - 40, X].sym = '+';
                    }
                }
                world[y - 20, x].sym = '+';
            }
            world[y, x].sym = '.';
            
            if (y == pl.Y && x == pl.X)
            { pl.IsAlive = false; }

            //Enemy
            if (enemyList.Count > 0)
            {
                foreach (var en in enemyList)
                {
                    if (y == en.Y && x == en.X)
                    { en.isAlive = false; }
                }
            }


        }

        public void Clear_Map_no_sleep(World world, Player pl)
        {
            //Thread.Sleep(2000);

            if (X + 20 < world.Columns * 20 && world[y, X + 20].sym != 'o' && world[y, X + 20].sym != '&')
            {
                if (world[y, X + 20].sym == '$')
                {
                    kx = X + 20;
                    ky = y;
                    //world[y, X + 20].sym = 'K';
                }
                if (world[y, X + 20].sym == '*') //* double range hidden
                {
                    urx = X + 20;
                    ury = y;
                }
                if (world[y, X + 20].sym == '%') //* double number hidden
                {
                    unx = X + 20;
                    uny = y;
                }
                if (world[y, X + 20].sym == 'D') // door hidden
                {
                    dx = X + 20;
                    dy = y;
                }
                if (world[y, X + 20].sym == 'K')
                {
                    pl.IsAlive = false;
                }

                if (y == pl.Y && x + 20 == pl.X)
                { pl.IsAlive = false; }

                if (enemyList.Count > 0)
                {
                    foreach (var en in enemyList)
                    {
                        if (y == en.Y && x + 20 == en.X)
                        { en.isAlive = false; }
                    }
                }

                if (pl.haspoweruprange)
                {
                    if (X + 40 < world.Columns * 20 && world[y, X + 40].sym != 'o' && world[y, X + 40].sym != '&' &&
                        world[y, X + 20].sym != '$' && world[y, X + 20].sym != '#' && world[y, X + 20].sym != '*' && world[y, X + 20].sym != '%')
                    {
                        if (world[y, X + 40].sym == '$')
                        {
                            kx = X + 40;
                            ky = y;
                            //world[y, X + 20].sym = 'K';
                        }
                        if (world[y, X + 40].sym == '*') //* double range hidden
                        {
                            urx = X + 40;
                            ury = y;
                        }
                        if (world[y, X + 40].sym == '%') //* double number hidden
                        {
                            unx = X + 40;
                            uny = y;
                        }
                        if (world[y, X + 40].sym == 'D') // door hidden
                        {
                            dx = X + 40;
                            dy = y;
                        }
                        if (world[y, X + 40].sym == 'K')
                        {
                            pl.IsAlive = false;
                        }

                        if (y == pl.Y && x + 40 == pl.X)
                        { pl.IsAlive = false; }

                        if (enemyList.Count > 0)
                        {
                            foreach (var en in enemyList)
                            {
                                if (y == en.Y && x + 40 == en.X)
                                { en.isAlive = false; }
                            }
                        }
                        world[y, X + 40].sym = '>';
                    }
                }
                world[y, X + 20].sym = '>';

            }
            if (x - 20 >= 0 && world[y, x - 20].sym != 'o' && world[y, x - 20].sym != '&')
            {
                if (world[y, X - 20].sym == '$')
                {
                    kx = X - 20;
                    ky = y;
                    //world[ky, X - 20].sym = 'K';
                }
                if (world[y, X - 20].sym == '*') //* double range hidden
                {
                    urx = X - 20;
                    ury = y;
                }
                if (world[y, X - 20].sym == '%') //* double number hidden
                {
                    unx = X - 20;
                    uny = y;
                }
                if (world[y, X - 20].sym == 'D') // door hidden
                {
                    dx = X - 20;
                    dy = y;
                }
                if (world[y, X - 20].sym == 'K')
                {
                    pl.IsAlive = false;
                }

                if (y == pl.Y && x - 20 == pl.X)
                { pl.IsAlive = false; }

                if (enemyList.Count > 0)
                {
                    foreach (var en in enemyList)
                    {
                        if (y == en.Y && x - 20 == en.X)
                        { en.isAlive = false; }
                    }
                }

                if (pl.haspoweruprange)
                {
                    if (X - 40 < world.Columns * 20 && world[y, X - 40].sym != 'o' && world[y, X - 40].sym != '&'
                        && world[y, X - 20].sym != '$' && world[y, X - 20].sym != '#' && world[y, X - 20].sym != '*' && world[y, X - 20].sym != '%')
                    {
                        if (world[y, X - 40].sym == '$')
                        {
                            kx = X - 40;
                            ky = y;
                            //world[y, X + 20].sym = 'K';
                        }
                        if (world[y, X - 40].sym == '*') //* double range hidden
                        {
                            urx = X - 40;
                            ury = y;
                        }
                        if (world[y, X - 40].sym == '%') //* double number hidden
                        {
                            unx = X - 40;
                            uny = y;
                        }
                        if (world[y, X - 40].sym == 'D') // door hidden
                        {
                            dx = X - 40;
                            dy = y;
                        }
                        if (world[y, X - 40].sym == 'K')
                        {
                            pl.IsAlive = false;
                        }

                        if (y == pl.Y && x - 40 == pl.X)
                        { pl.IsAlive = false; }

                        if (enemyList.Count > 0)
                        {
                            foreach (var en in enemyList)
                            {
                                if (y == en.Y && x - 40 == en.X)
                                { en.isAlive = false; }
                            }
                        }
                        world[y, X - 40].sym = '<';
                    }
                }
                world[y, x - 20].sym = '<';
            }
            if (y + 20 < world.Rows * 20 && world[y + 20, x].sym != 'o' && world[y + 20, x].sym != '&')
            {
                if (world[y + 20, x].sym == '$')
                {
                    kx = x;
                    ky = y + 20;
                    //world[ky, kx].sym = 'K';
                }
                if (world[y + 20, x].sym == '*') //* double range hidden
                {
                    urx = x;
                    ury = y + 20;
                }
                if (world[y + 20, x].sym == '%') //* double number hidden
                {
                    unx = x;
                    uny = y + 20;
                }
                if (world[y + 20, x].sym == 'D') // door hidden
                {
                    dx = x;
                    dy = y + 20;
                }
                if (world[y + 20, x].sym == 'K')
                {
                    pl.IsAlive = false;
                }

                if (y + 20 == pl.Y && x == pl.X)
                { pl.IsAlive = false; }

                if (enemyList.Count > 0)
                {
                    foreach (var en in enemyList)
                    {
                        if (y + 20 == en.Y && x == en.X)
                        { en.isAlive = false; }
                        
                    }
                }

                if (pl.haspoweruprange)
                {
                    if (Y + 40 < world.Columns * 20 && world[y + 40, X].sym != 'o' && world[y + 40, X].sym != '&'
                        && world[y + 20, X].sym != '$' && world[y + 20, X].sym != '#' && world[y + 20, X].sym != '*' && world[y + 20, X].sym != '%')
                    {
                        if (world[y + 40, X].sym == '$')
                        {
                            kx = X;
                            ky = y + 40;
                            //world[y, X + 20].sym = 'K';
                        }
                        if (world[y + 40, X].sym == '*') //* double range hidden
                        {
                            urx = X;
                            ury = y + 40;
                        }
                        if (world[y + 40, X].sym == '%') //* double number hidden
                        {
                            unx = X;
                            uny = y + 40;
                        }
                        if (world[y + 40, X].sym == 'D') // door hidden
                        {
                            dx = X;
                            dy = y + 40;
                        }
                        if (world[y + 40, X].sym == 'K')
                        {
                            pl.IsAlive = false;
                        }

                        if (y + 40 == pl.Y && x == pl.X)
                        { pl.IsAlive = false; }

                        if (enemyList.Count > 0)
                        {
                            foreach (var en in enemyList)
                            {
                                if (y + 40 == en.Y && x == en.X)
                                { en.isAlive = false; }
                            }
                        }
                        world[y + 40, X].sym = '-';
                    }
                }
                world[y + 20, x].sym = '-';
            }
            if (y - 20 >= 0 && world[y - 20, x].sym != 'o' && world[y - 20, x].sym != '&')
            {
                if (world[y - 20, x].sym == '$')
                {
                    kx = x;
                    ky = y - 20;
                    //world[ky, kx].sym = 'K';
                }
                if (world[y - 20, x].sym == '*') //* double range hidden
                {
                    urx = x;
                    ury = y - 20;
                }
                if (world[y - 20, x].sym == '%') //* double number hidden
                {
                    unx = x;
                    uny = y - 20;
                }
                if (world[y - 20, x].sym == 'D') // door hidden
                {
                    dx = x;
                    dy = y - 20;
                }
                if (world[y - 20, x].sym == 'K')
                {
                    pl.IsAlive = false;
                }

                if (y - 20 == pl.Y && x == pl.X)
                { pl.IsAlive = false; }

                if (enemyList.Count > 0)
                {
                    foreach (var en in enemyList)
                    {
                        if (y - 20 == en.Y && x == en.X)
                        { en.isAlive = false; }
                    }
                }

                if (pl.haspoweruprange)
                {
                    if (Y - 40 < world.Columns * 20 && world[y - 40, X].sym != 'o' && world[y - 40, X].sym != '&'
                        && world[y - 20, X].sym != '$' && world[y - 20, X].sym != '#' && world[y - 20, X].sym != '*' && world[y - 20, X].sym != '%')
                    {
                        if (world[y - 40, X].sym == '$')
                        {
                            kx = X;
                            ky = y - 40;
                            //world[y, X + 20].sym = 'K';
                        }
                        if (world[y - 40, X].sym == '*') //* double range hidden
                        {
                            urx = X;
                            ury = y - 40;
                        }
                        if (world[y - 40, X].sym == '%') //* double number hidden
                        {
                            unx = X;
                            uny = y - 40;
                        }
                        if (world[y - 40, X].sym == 'D') // door hidden
                        {
                            dx = X;
                            dy = y - 40;
                        }
                        if (world[y - 40, X].sym == 'K')
                        {
                            pl.IsAlive = false;
                        }

                        if (y - 40 == pl.Y && x == pl.X)
                        { pl.IsAlive = false; }

                        if (enemyList.Count > 0)
                        {
                            foreach (var en in enemyList)
                            {
                                if (y - 40 == en.Y && x == en.X)
                                { en.isAlive = false; }
                            }
                        }

                        world[y - 40, X].sym = '+';
                    }
                }
                world[y - 20, x].sym = '+';
            }
            world[y, x].sym = '.';

            if (y == pl.Y && x == pl.X)
            { pl.IsAlive = false; }

            //Enemy
            if (enemyList.Count > 0)
            {
                foreach (var en in enemyList)
                {
                    if (y == en.Y && x == en.X)
                    { en.isAlive = false; }
                }

            }
        }

        public void Clear_Dots(World world)
        {
            Thread.Sleep(200);
            for (int i = 0; i < world.Rows; i++)
            {
                for (int j = 0; j < world.Columns; j++)
                {
                    if (world[i * 20, j * 20].sym == '.' || world[i * 20, j * 20].sym == '>' || world[i * 20, j * 20].sym == '<' || world[i * 20, j * 20].sym == '+' || world[i * 20, j * 20].sym == '-')
                        world[i * 20, j * 20].sym = ' ';
                }
            }
            if (kx != 0 && ky != 0)
            {
                world[ky, kx].sym = 'K';
            }
            if (urx != 0 && ury != 0)
            {
                world[ury, urx].sym = 'P';
            }
            if (unx != 0 && uny != 0)
            {
                world[uny, unx].sym = 'Q';
            }
            if (dx != 0 && dy != 0)
            {
                world[dy, dx].sym = '&';
            }
        }
        private void timerStart()
        {
            timer = new DispatcherTimer();  
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
        }

        private void checkexplode()
        {
            timer2 = new DispatcherTimer();
            timer2.Tick += new EventHandler(timerTick2);
            //check time 5ms
            timer2.Interval = new TimeSpan(0, 0, 0, 0, 5);
            timer2.Start();
        }

        private void timerTick2(object sender, EventArgs e)
        {
            Main.score.Text = map[20, 20].sym.ToString();
            if (!Pl.haspoweruprange)
            {
                if (map[Y, X + 20].explode == true || map[Y, X - 20].explode == true || map[Y + 20, X].explode == true || map[Y - 20, X].explode == true)
                {
                    Explosion_immediate(map, Pl);
                    Main.game_canv.Children.Remove(Uielement);
                    timer.Stop();
                    timer2.Stop();
                    isexplode = true;
                }
            }
            else
            {
                if (map[Y, X + 20].explode == true || map[Y, X - 20].explode == true || map[Y + 20, X].explode == true || map[Y - 20, X].explode == true || (X + 40 < 20 * 20 && map[Y, X + 40].explode == true)
                    || (X - 40 >= 0 && map[Y, X - 40].explode == true) || (Y + 40 <= 20*20 && map[Y + 40, X].explode == true) || (Y - 40 >= 0 && map[Y - 40, X].explode == true))
                {
                    if (X + 40 <= 20 * 20 && map[Y, X + 40].explode == true && (map[Y, X + 20].sym == 'o' || map[Y, X + 20].sym == '&')) return;
                    if (X - 40 >= 0 && map[Y, X - 40].explode == true && (map[Y, X - 20].sym == 'o' || map[Y, X - 20].sym == '&')) return;
                    if (Y + 40 <= 20 * 20 && map[Y + 40, X].explode == true && (map[Y + 20, X].sym == 'o' || map[Y + 20, X].sym == '&')) return;
                    if (Y - 40 >= 0 && map[Y - 40, X].explode == true && (map[Y - 20, X].sym == 'o' || map[Y - 20, X].sym == '&')) return;
                    Explosion_immediate(map, Pl);
                    Main.game_canv.Children.Remove(Uielement);
                    timer.Stop();
                    timer2.Stop();
                    isexplode = true;
                }
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (im == 4)
            {
                timer.Stop();
                Main.game_canv.Children.Remove(Uielement);
                map[Y, X].explode = true;
            }
            else
            {
                Main.game_canv.Children.Remove(Uielement);
                Rectangle rect = new Rectangle();
                rect.Width = size;
                rect.Height = size;
                rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Bomb" + im + ".png", UriKind.Relative)) };
                Uielement = rect;
                Main.game_canv.Children.Add(Uielement);
                Canvas.SetLeft(Uielement, X);
                Canvas.SetTop(Uielement, Y);
                im++;
            }
        }
    }
}
