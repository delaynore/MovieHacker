using System;
using System.Windows;
using System.Windows.Controls;

namespace MovieHacker.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DataFiller.Fill();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button ?? e.Source as Button;
            if (button == null) return;
            var tag = button.Tag;
            mainFrame1.Source = new Uri($"Page{tag}.xaml", UriKind.Relative);
        }
    }
}
