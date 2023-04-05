using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Bombermen
{
    /// <summary>
    ///
    /// </summary>
    public partial class Construct_Window : Window
    {
        int col = 0;
        int row = 0;
        int CurX = 0;
        int CurY = 0;
        int step = 20;
        string name;
        List<World> levels;
        int i = 0;
        int h = 0;
        Element[,] map;

        public Construct_Window()
        {
            InitializeComponent();
            Prepare();
        }

        private void Button(object sender, RoutedEventArgs e)
        {
            try
            {
                col = int.Parse(Col.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Check column inputs");
                Col.Text = "";
            }
            try
            {
                row = int.Parse(Row.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Check row inputs");
                Row.Text = "";
            }
            map = new Element[row, col];
        }

        private void Wall_Click(object sender, RoutedEventArgs e)
        {
            if (col == 0)
                MessageBox.Show("Input column");
            if (row == 0)
                MessageBox.Show("Input row");
            if(i < col)
            {
                Wall w = new Wall(CurX, CurY);
                cons_canv.Children.Add(w.Uielement);
                Canvas.SetLeft(w.Uielement, w.X);
                Canvas.SetTop(w.Uielement, w.Y);
                CurX += step;              
                map[h, i] = w;
              
                i++;
            }
            else
            {
                h++;
                if (h >= row)
                    return;
                
                i = 0;
                CurX =0;
                CurY +=step;
            }
        }

        private void Empty(object sender, RoutedEventArgs e)
        {
            if (col == 0)
                MessageBox.Show("Check column inputs");
            if (row == 0)
                MessageBox.Show("Check row inputs");
            if (i < col)
            {
                Empty_Cell w = new Empty_Cell(CurX, CurY);
                cons_canv.Children.Add(w.Uielement);
                Canvas.SetLeft(w.Uielement, w.X);
                Canvas.SetTop(w.Uielement, w.Y);
                CurX += step;
                map[h, i] = w;
                i++;
            }
            else
            {
                h++;
                if (h >= row)
                    return;

                i = 0;
                CurX = 0;
                CurY += step;
            }
        }

        private void Brick(object sender, RoutedEventArgs e)
        {
            if (col == 0)
                MessageBox.Show("Input column");
            if (row == 0)
                MessageBox.Show("Input row");

            if (i < col)
            {
                Brick_Wall w = new Brick_Wall(CurX, CurY);
                cons_canv.Children.Add(w.Uielement);
                Canvas.SetLeft(w.Uielement, w.X);
                Canvas.SetTop(w.Uielement, w.Y);
                CurX += step;  
                map[h, i] = w;
                i++;
            }
            else
            {
                h++;
                if (h >= row)
                    return;

                i = 0;
                CurX = 0;
                CurY += step;
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            if (col == 0)
                MessageBox.Show("Input column");
            if (row == 0)
                MessageBox.Show("Input row");
            if (i < col)
            {
                Finish w = new Finish(CurX, CurY);
                cons_canv.Children.Add(w.Uielement);
                Canvas.SetLeft(w.Uielement, w.X);
                Canvas.SetTop(w.Uielement, w.Y);
                CurX += step;
                map[h, i] = w;
                i++;
            }
            else
            {
                h++;
                if (h >= row)
                    return;

                i = 0;
                CurX = 0;
                CurY += step;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(map == null)
            {
                MessageBox.Show("Make a map first");
                return;
            }
            name = Name.Text;
            if (levels.Where(x => x.Name == name).FirstOrDefault() == null)
            {
                World stage = new World(map, name);
                levels.Add(stage);

                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter writer = new StreamWriter("Levels.json"))
                {
                    using (JsonWriter jw = new JsonTextWriter(writer))
                    {
                        serializer.Serialize(jw, levels);

                    }
                }
                MessageBox.Show("Saved");
            }
            else
            {
                MessageBox.Show("A level with this name already exists");
                Name.Text = "";
            }
        }

        private void Prepare()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sr = new StreamReader("Levels.json"))
            {
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    levels= (List<World>)serializer.Deserialize(reader, typeof(List<World>));
                }
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            map = new Element[row, col];
            cons_canv.Children.Clear();
            CurX = 0;
            CurY = 0;
        }

        private void ToMenu(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            Close();
            menu.Show();
        }
    }
}
