using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bombermen
{
    /// <summary>
    ///
    /// </summary>
    public partial class Tasks_Window : Window
    {
        List<World> levels;
        World world;

        public Tasks_Window()
        {
            InitializeComponent();
            Prepare();
            world = levels[0];
            world.Validate();
            Draw(world);
        }
        
        private void Prepare()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sr = new StreamReader("Levels.json"))
            {
                using (JsonTextReader reader = new JsonTextReader(sr))
                {

                    levels = (List<World>)serializer.Deserialize(reader, typeof(List<World>));

                }
            }
            foreach (var i in levels)
            {
                i.Validate();
            }
        }

        public void Draw(World world)
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
                    #endregion

                    if (!task_canv.Children.Contains(world[i * 20, j * 20].Uielement))
                    {
                        task_canv.Children.Add(world[i * 20, j * 20].Uielement);
                        Canvas.SetLeft(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].X);
                        Canvas.SetTop(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].Y);

                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string el = One.SelectedItem.ToString().Substring(37).Trim();
            
            task_canv.Children.Clear();
            switch (el)
            {
                case "Brick":
                    task_canv.Children.Clear();
                    for (int i = 0; i < world.Rows; i++)
                    {
                        for (int j = 0; j < world.Columns; j++)
                        {
                            if (world[i * 20, j * 20] .sym =='#')
                            {
                                task_canv.Children.Add(world[i * 20, j * 20].Uielement);
                                Canvas.SetLeft(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].X);
                                Canvas.SetTop(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].Y);

                            }
                        }
                    }
                    break;
                case "Empty":
                    task_canv.Children.Clear();
                    for (int i = 0; i < world.Rows; i++)
                    {
                        for (int j = 0; j < world.Columns; j++)
                        {
                            if ( world[i * 20, j * 20] is Empty_Cell)
                            {
                                task_canv.Children.Add(world[i * 20, j * 20].Uielement);
                                Canvas.SetLeft(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].X);
                                Canvas.SetTop(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].Y);

                            }
                        }
                    }
                    break;
                case "Wall":
                    task_canv.Children.Clear();
                    for (int i = 0; i < world.Rows; i++)
                    {
                        for (int j = 0; j < world.Columns; j++)
                        {
                            if ( world[i * 20, j * 20] is Wall)
                            {
                                task_canv.Children.Add(world[i * 20, j * 20].Uielement);
                                Canvas.SetLeft(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].X);
                                Canvas.SetTop(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].Y);

                            }
                        }
                    }
                    break;
                case "Finish":
                    task_canv.Children.Clear();
                    for (int i = 0; i < world.Rows; i++)
                    {
                        for (int j = 0; j < world.Columns; j++)
                        {
                            if ( world[i * 20, j * 20] is Finish)
                            {
                                task_canv.Children.Add(world[i * 20, j * 20].Uielement);
                                Canvas.SetLeft(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].X);
                                Canvas.SetTop(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].Y);

                            }
                        }
                    }
                    break;
            }
        }
    }
}
