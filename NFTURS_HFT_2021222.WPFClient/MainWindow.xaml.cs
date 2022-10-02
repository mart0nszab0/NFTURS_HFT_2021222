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

namespace NFTURS_HFT_2021222.WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Game_Button_Click(object sender, RoutedEventArgs e)
        {
            GameEditor gameEditor = new GameEditor();
            gameEditor.Show();
        }

        private void Genre_Button_Click(object sender, RoutedEventArgs e)
        {
            GenreEditor genreEditor = new GenreEditor();
            genreEditor.Show();
        }

        private void Publisher_Button_Click(object sender, RoutedEventArgs e)
        {
            PublisherEditor publisherEditor = new PublisherEditor();
            publisherEditor.Show();
        }
    }
}
