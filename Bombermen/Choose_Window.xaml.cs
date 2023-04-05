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
    public partial class Choose_Window : Window
    {
        List<World> levels;
        public Choose_Window()
        {
            InitializeComponent();
            Prepare();
            list.ItemsSource = levels;
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
        }

        private void Click(object sender, MouseButtonEventArgs e)
        {
            int col = list.SelectedIndex;
            Main main = new Main(false, col);
            Close();
            main.Show();
        }
    }
}
