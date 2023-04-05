using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bombermen
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Main : Window
    {
        List<World> levels;
        World world;
        Player pl;
        public List<Enemy> enemy = new List<Enemy> ();
        int en_id = 0;
        int i = 0;
        bool Continue = true;
        private MediaPlayer player = new MediaPlayer();
        public MediaPlayer bomb_player = new MediaPlayer();
        public int calculate = 0;
        Random rand  = new Random();
        private DispatcherTimer timer = null;

        public Main()
        {
            InitializeComponent();
            Prepare();
          
            world = levels[i];
            Run(world);

            player.Open(new Uri("music.mp3", UriKind.Relative));
            player.Play();
            bomb_player.Open(new Uri("TickingBomb.mp3", UriKind.Relative));
            checkalive();
        }

        public Main(bool Only1, int num)
        {
            InitializeComponent();
            Prepare();
            world = levels[num];
            Run(world);
            Continue = false;
            player.Open(new Uri("music.mp3", UriKind.Relative));
            player.Play();
            bomb_player.Open(new Uri("TickingBomb.mp3", UriKind.Relative));
            checkalive();
        }

        private void checkalive()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (!pl.IsAlive)
            {
                player.Stop();
                foreach (var en in enemy)
                {
                    en.isAlive = false;
                }
                MessageBox.Show("You Died! Press enter to return to the menu");

                MainWindow menu = new MainWindow();
                menu.Show();
                Close();

                timer.Stop();
            }
        }

        public void Run(World world)
        {
            try
            {
                game_canv.Width = world.Columns * 20;
                game_canv.Height = world.Rows * 20;
                this.Width = world.Columns * 20 + 200;
                this.Height = game_canv.Height = world.Rows * 20 + 400;

                pl = new Player(20, 20);
                game_canv.Children.Add(pl.Uielement);
                Canvas.SetLeft(pl.Uielement, pl.X);
                Canvas.SetTop(pl.Uielement, pl.Y);
                Draw(world);
                Spawn_Enemy(true);
                Thread.Sleep(107);
                Spawn_Enemy(true);
                Thread.Sleep(107);
                Spawn_Enemy(true);
                Thread.Sleep(107);
                Spawn_Enemy(true);
                Thread.Sleep(107);
                Spawn_Enemy(false);
                Thread.Sleep(107);
                Spawn_Enemy(false);
                Thread.Sleep(107);
                Spawn_Enemy(false);
                Thread.Sleep(107);
                Spawn_Enemy(false);
                //better way to spawn enemy
                /*int num_enemy = rand.Next(3, 6);
                for (int i = 0; i < num_enemy; i++)
                {
                    if (rand.Next() % 2 == 1)
                    {
                        Spawn_Enemy(true);
                        Thread.Sleep(57);
                    }

                    else Spawn_Enemy(false);
                }*/
            }
            catch(Exception ex)
            {
                Run(world);
            }
        }

        public void Draw_Player( Player pl)
        {
            game_canv.Children.Add(pl.Uielement);
            Canvas.SetLeft(pl.Uielement, pl.X);
            Canvas.SetTop(pl.Uielement, pl.Y);
        }

        public  void Draw(World world)
        {
            for (int i = 0; i < world.Rows; i++)
            {
                for (int j = 0; j < world.Columns; j++)
                {
                    #region Wave
                    if (world[i * 20, j * 20].sym == '>')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Wave_Right.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == '<')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Wave_Left.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == '-')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Wave_Down.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == '+')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Wave_Up.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == ' ')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = Brushes.Green };
                    if (world[i * 20, j * 20].sym == '.')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Wave_Center.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == 'K')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"key.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == 'P')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"poweruprange.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == 'Q')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"powerupnumber.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == '&')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Finish.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == 'n')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = Brushes.Green };
                    #endregion

                    if (!game_canv.Children.Contains(world[i * 20, j * 20].Uielement))
                    {
                        game_canv.Children.Add(world[i * 20, j * 20].Uielement);
                        Canvas.SetLeft(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].X);
                        Canvas.SetTop(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].Y);
                       
                    }
                }
            }
            if(game_canv.Children.Contains(pl.Uielement))
            {
                game_canv.Children.Remove(pl.Uielement);
                Draw_Player(pl);
                if (enemy.Count > 0)
                {
                    RefreshEnemy();
                } 
            }
          
        }

        public void RefreshEnemy()
        {
            foreach(var en in enemy)
            {
                game_canv.Children.Remove(en.Uielement);
                game_canv.Children.Add(en.Uielement);
                Canvas.SetLeft(en.Uielement, en.X);
                Canvas.SetTop(en.Uielement, en.Y);
                world[en.Y, en.X].sym = 'n';
            }
        }

        public void RemoveEnemy(int id)
        {
            int place = 0;
            if (enemy.Count > 0)
            {
                foreach (var item in enemy)
                {
                    if(item.id == id)
                    {
                        game_canv.Children.Remove(item.Uielement);
                        world[item.Y, item.X].sym = ' ';
                        break;
                    }
                    place++;
                }
                enemy.RemoveAt(place);
            } 
        }

        public void Spawn_Enemy(bool mode)
        {
            int x = rand.Next(3, world.Columns);
            int y = rand.Next(3, world.Rows);
            if(world[y*20,x*20].sym ==' ' && world.GetEmptNeighb(x*20, y*20)!=null)
            {
                var en = new Enemy(x*20, y*20, this, world, pl);
                en.id = en_id++;
                en.walk_mode = mode;
                enemy.Add(en);
                world[en.Y, en.X].sym = 'n';
                return;
            }
            else
            {
                List<Element> el = world.GetEmptNeighb(x * 20, y * 20);
                int i = rand.Next(el.Count);
                var en = new Enemy(el[i].X, el[i].Y, this, world, pl);
                en.id = en_id++;
                en.walk_mode = mode;
                enemy.Add(en);
                world[en.Y, en.X].sym = 'n';
            }
        }

        private void Key_Released(object sender, KeyEventArgs e)
        {
            /*if(!pl.IsAlive)
            {
                MainWindow menu = new MainWindow();
                menu.Show();
                Close();
                player.Stop();
            }*/

            pl.Handlkey(e);
            if (pl.direct == Directions.SPACE && world[pl.Y, pl.X].sym != '&')
            {
                calculate = calculate + 1;
                if (pl.haspowerupnumber == true)
                {
                    if (calculate <= 2)
                    {
                        if (world[pl.Y, pl.X].sym == '&') return;
                        Bomb b = new Bomb(pl.X, pl.Y, this, world, pl, enemy);
                        world[pl.Y, pl.X].sym = 'n';
                        game_canv.Children.Add(b.Uielement);
                        Canvas.SetLeft(b.Uielement, b.X);
                        Canvas.SetTop(b.Uielement, b.Y);
                        player.Pause();
                        MediaPlayer bomb_player = new MediaPlayer();
                        bomb_player.Open(new Uri("TickingBomb.mp3", UriKind.Relative));

                        player.Play();
                    }
                    else
                        calculate = calculate - 1;
                }
                else
                {
                    if (calculate <= 1)
                    {
                        if (world[pl.Y, pl.X].sym == '&') return;
                        Bomb b = new Bomb(pl.X, pl.Y, this, world, pl, enemy);
                        world[pl.Y, pl.X].sym = 'n';
                        game_canv.Children.Add(b.Uielement);
                        Canvas.SetLeft(b.Uielement, b.X);
                        Canvas.SetTop(b.Uielement, b.Y);
                        player.Pause();
                        MediaPlayer bomb_player = new MediaPlayer();
                        bomb_player.Open(new Uri("TickingBomb.mp3", UriKind.Relative));

                        player.Play();
                    }
                    else
                        calculate = calculate - 1;
                }
            }
            else
            {
                Canvas.SetLeft(pl.Uielement, pl.X);
                Canvas.SetTop(pl.Uielement, pl.Y);

                pl.Move(world);
                foreach(var item in enemy)
                {
                    if (pl.X == item.X && pl.Y == item.Y)
                        pl.IsAlive = false;
                }
                Canvas.SetLeft(pl.Uielement, pl.X);
                Canvas.SetTop(pl.Uielement, pl.Y);
            }
        }

        private void Prepare()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sr = new StreamReader("mylevel.json"))
            {
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    levels = (List<World>)serializer.Deserialize(reader, typeof(List<World>));
                }
            }

            Random r = new Random();
            foreach(var i in levels)
            {
                //generate bricks
                int num_bricks=0;
                for (int j = 1; j < i.Rows; j++)
                {
                    for (int k = 1; k < i.Columns; k++)
                    {
                        if (i.world[j, k].sym == ' ')
                        {
                            // you can controll the number of bricks here
                            if (r.Next() % 4 == 0)
                            {
                                i.world[j, k].sym = '#';
                                num_bricks++;
                            }
                        }
                        if (i.world[1, 1].sym == '#') num_bricks--;
                        if (i.world[1, 2].sym == '#') num_bricks--;
                        if (i.world[2, 1].sym == '#') num_bricks--;
                        i.world[1, 1].sym = ' ';
                        i.world[1, 2].sym = ' ';
                        i.world[2, 1].sym = ' ';
                    }
                }
                //hide keys
                int brick_pos = r.Next() % (num_bricks - 1);
                //score.Text = num_bricks.ToString();
                int pos = -1;
                for (int j = 1; j < i.Rows; j++)
                {
                    for (int k = 1; k < i.Columns; k++)
                    {
                        if (i.world[j, k].sym == '#')
                        {
                            pos++;
                            if (pos == brick_pos)
                            { 
                                i.world[j, k].sym = '$';
                                tbkey.Text = j.ToString() + " " + k.ToString();
                            }
                        }
                    }
                }
                //hide poweruprange
                int poweruprange = r.Next() % (num_bricks - 1);
                while (true)
                {
                    if (poweruprange != brick_pos) break;
                    poweruprange = r.Next() % (num_bricks - 1);
                }
                pos = -1;
                for (int j = 1; j < i.Rows; j++)
                {
                    for (int k = 1; k < i.Columns; k++)
                    {
                        if (i.world[j, k].sym == '#' || i.world[j, k].sym == '$')
                        {
                            pos++;
                            if (pos == poweruprange)
                            { 
                                i.world[j, k].sym = '*';
                                tbur.Text = j.ToString() + " " + k.ToString();
                            } 
                        }
                    }
                }
                //hide powerupnumber
                int powerupnumber = r.Next() % (num_bricks - 1);
                while (true)
                {
                    if ((powerupnumber != brick_pos) && (powerupnumber != poweruprange)) break;
                    powerupnumber = r.Next() % (num_bricks - 1);
                }
                pos = -1;
                for (int j = 1; j < i.Rows; j++)
                {
                    for (int k = 1; k < i.Columns; k++)
                    {
                        if (i.world[j, k].sym == '#' || i.world[j, k].sym == '$' || i.world[j, k].sym == '*')
                        {
                            pos++;
                            if (pos == powerupnumber)
                            {
                                i.world[j, k].sym = '%';
                                tbun.Text = j.ToString() + " " + k.ToString();
                            }
                        }
                    }
                }
                //generate door
                int door = r.Next() % (num_bricks - 1);
                while (true)
                {
                    if ((door != brick_pos) && (door != poweruprange) && (door != powerupnumber)) break;
                    door = r.Next() % (num_bricks - 1);
                }
                pos = -1;
                for (int j = 1; j < i.Rows; j++)
                {
                    for (int k = 1; k < i.Columns; k++)
                    {
                        if (i.world[j, k].sym == '#' || i.world[j, k].sym == '$' || i.world[j, k].sym == '*' || i.world[j, k].sym == '%')
                        {
                            pos++;
                            if (pos == door)
                            {
                                i.world[j, k].sym = 'D';
                                tbdoor.Text = j.ToString() + " " + k.ToString();
                            }
                        }
                    }
                }
                //for test only
                //i.world[3, 3].sym = '$';
                //i.world[4, 4].sym = '*';
                //i.world[5, 5].sym = '%';

                i.Validate();
            }
        }

        private void Key_Pressed(object sender, KeyEventArgs e)
        {

            if (!pl.IsAlive)
            {
                MainWindow menu = new MainWindow();
                menu.Show();
                Close();
                player.Stop();
            }
            if (pl.Check_Finish(world))
            {

                player.Stop();

                player = new MediaPlayer();

                player.Open(new Uri("finish.mp3", UriKind.Relative));
                player.Play();

                MessageBox.Show("Congratulations");
                foreach(var en in enemy)
                    en.isAlive = false;

                i++;  
                game_canv.Children.Clear();

                if(i < levels.Count() && Continue == true)
                {
                    if (enemy.Count > 0)
                    {
                        foreach (var item in enemy)
                        {
                            game_canv.Children.Remove(item.Uielement);
                        }
                    }
                    enemy.RemoveAll(data => enemy.Contains(data));
                    world = levels[i];
                    Run(world);
                    player.Open(new Uri("music.mp3", UriKind.Relative));
                    player.Play();
                }                         
                else if(!Continue)
                {
                    MessageBox.Show("Congratulation");
                    MainWindow menu = new MainWindow();
                    menu.Show();
                    player.Stop();
                    Close();
                }
                else
                {
                    MessageBox.Show("You have passed the game \n You can create your own map in the constructor \n" +
                        "for the greatest pleasure");
                    if (enemy.Count > 0)
                    {
                        foreach (var item in enemy)
                        {
                            game_canv.Children.Remove(item.Uielement);
                        }
                    }
                    enemy.RemoveAll(data => enemy.Contains(data));
                    MainWindow menu = new MainWindow();        
                    player.Stop();
                    Close();
                    menu.Show();
                }
            }
            pl.Check_keys(world, this);
            pl.Check_poweruprange(world, this);
            pl.Check_powerupnumber(world, this);
        }

        private void quit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            player.Stop();
            Close();
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            player.Stop();
            Close();
            main.Show();
        }
    }
}
