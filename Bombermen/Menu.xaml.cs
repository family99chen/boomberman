using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bombermen
{
    /// <summary>
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer player = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
            player.Open(new Uri("music.mp3", UriKind.Relative));
            player.Play();
        }

        private void Play_focus(object sender, MouseEventArgs e)
        {
            first.Visibility = Visibility.Visible;
            first.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF0B"));
        }

        private void Exit_focus(object sender, MouseEventArgs e)
        {
            fourth.Visibility = Visibility.Visible;
            fourth.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF0B"));
        }

        private void Play_leave(object sender, MouseEventArgs e)
        {
            first.Visibility = Visibility.Hidden;
        }

        private void Exit_leave(object sender, MouseEventArgs e)
        {
            fourth.Visibility = Visibility.Hidden;
        }

        private void Play_Click(object sender, MouseButtonEventArgs e)
        {
            Main main = new Main();
            player.Stop();
            
            Close();
            main.Show();
        }

        private void Exit_Click(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            Close();
        }
    }
}
