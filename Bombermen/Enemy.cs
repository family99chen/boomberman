using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;

namespace Bombermen
{
    public class Enemy : Element
    {
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer animation = new DispatcherTimer();
        Main game;
        World map;
        Player pl;
        Element prev;
        int frame = 1;
        int direction;
        public int id;
        public bool isAlive = true;
        Stack<Element> path;
        Rectangle rect;
        public bool walk_mode = true; //straight_walk = false random_walk = true
        Element next;

        public Enemy(int x, int y, Main g, World w, Player player) : base(x, y)
        {
            game = g;
            map= w;
            pl = player;
            rect = new System.Windows.Shapes.Rectangle();
            rect.Width = size;
            rect.Height = size;
            if(!walk_mode)
                rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"StraightEnemy1.png", UriKind.Relative)) };
            else
                rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Enemy1.png", UriKind.Relative)) };

            Uielement = rect;
            timerStart();
            animateStart();
        }

        private void animateStart()
        {
            animation = new DispatcherTimer();
            animation.Tick += new EventHandler(Animate);
            animation.Interval = new TimeSpan(0, 0, 0, 0, 100);
            animation.Start();
        }

        private void Animate(object sender, EventArgs e)
        {
            if (frame == 5)
                frame = 1;

            if (!walk_mode)
                rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"StraightEnemy"+frame+".png", UriKind.Relative)) };
            else
                rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Enemy" + frame + ".png", UriKind.Relative)) };
            frame++;
            Uielement = rect;
        }

        private void timerStart()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (isAlive)
            {
                if (walk_mode)
                {
                    Move();
                }
                else
                {
                    Straight_Move();
                }
                CheckPlayer();
            }
            else
            {
                timer.Stop();
                animation.Stop(); 
                
                game.RemoveEnemy(id);
            }
        }

        public int set_direction()
        {
            if (prev.X == X)
            {
                if (Y > prev.Y)
                {
                    return 0; //up
                }
                else
                {
                    return 1; //down
                }
            }
            else
            {
                if (X > prev.X)
                {
                    return 2; //right
                }
                else
                {
                    return 3; //left
                }
            }
        }

        public void get_next()
        {
            switch (direction)
            {
                case 0:
                    next = map[Y + 20, X];
                    break;
                case 1:
                    next  = map[Y - 20, X];
                    break;
                case 2:
                    next = map[Y, X + 20];
                    break;
                case 3:
                    next = map[Y, X - 20];
                    break;
            }
        }
        
        public void Move()
        {
            List<Element> empt = map.GetEmptNeighb(X, Y);
            if (empt.Count == 0) return;
            if (empt.Count == 1)
            {
                prev = map[Y, X];
                X = empt[0].X;
                Y = empt[0].Y;
            }
            else if (empt != null && empt.Count > 1)
            {
                Random r = new Random();
                empt = empt.OrderBy(item => r.Next()).ToList();
                if (prev != null)
                    empt = empt.Where(x => !x.Equals(prev)).ToList();
                int index = r.Next(empt.Count());

                prev = map[Y, X];
                X = empt[index].X;
                Y = empt[index].Y;
            }
            if (prev != null) map[prev.Y, prev.X].sym = ' ';
            game.RefreshEnemy();
        }

        public void Straight_Move()
        {
            if (prev == null)
            {
                List<Element> empt = map.GetEmptNeighb(X, Y);
                if (empt.Count == 0) return;

                if (empt.Count == 1)
                {
                    prev = map[Y, X];
                    X = empt[0].X;
                    Y = empt[0].Y;
                    direction = set_direction();
                }
                else if (empt != null && empt.Count > 1)
                {
                    Random r = new Random();
                    empt = empt.OrderBy(item => r.Next()).ToList();
                    if (prev != null)
                        empt = empt.Where(x => !x.Equals(prev)).ToList();
                    int index = r.Next(empt.Count());

                    prev = map[Y, X];
                    X = empt[index].X;
                    Y = empt[index].Y;
                    direction = set_direction();
                }
            }
            else
            {
                List<Element> empt = map.GetEmptNeighb(X, Y);
                if (empt.Count == 0) return;
                get_next();
                if (next.sym == ' ')
                {
                    prev = map[Y, X];
                    X = next.X;
                    Y = next.Y;
                }
                else if (next.sym != ' ' && prev.sym != ' ') return;
                else
                {
                    var tmp = prev;
                    prev = map[Y, X];
                    X = tmp.X;
                    Y = tmp.Y;
                    if (direction > 1)
                        direction = (direction + 1) % 2 + 2;
                    else
                        direction = (direction + 1) % 2;
                }
            }
            if (prev != null) map[prev.Y, prev.X].sym = ' ';
            game.RefreshEnemy();
        }

        public void CheckPlayer()
        {
            if (pl != null && X == pl.X && Y == pl.Y)
                pl.IsAlive = false;
        }
    }
}
