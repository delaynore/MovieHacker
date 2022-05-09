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
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model;
using MovieHacker.Model.Tables;
namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataFiller.Fill();
            using (var db = new MHDataBase())
            {
                //var genre = new Genre() { Name = "Фантастика" };
                //db.Genres.Add(genre);
                //var movie = new Movie()
                //{
                //    Name = "Мстители",
                //    DurationInMinutes = 120,
                //    Genre = genre
                //};
                //db.Movies.Add(movie);
                //db.SaveChanges();
                //dataGrid1.ItemsSource = db.Movies.Join(db.Genres, m => m.Genre.Id, g => g.Id, (m, g) =>
                //new
                //{
                //    Name = m.Name,
                //    Genre = g.Name,
                //    Duration = m.DurationInMinutes
                //}).ToArray();
                //db.FilmRooms.Load();
                //dataGrid1.ItemsSource = db.FilmRooms.Local.ToArray();
                
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show(listBox1.SelectedItem.ToString().Split(',')[0].Split()[^1]) ;
            //MessageBox.Show(listBox1.SelectedItem.ToString().ToLower().Split(',').First(x => x.Contains("id")).Split()[^1]);
        }
        private object selectedTag = "null";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button ?? e.Source as Button;
            if (button == null) return;
            var tag = button.Tag;
            if (selectedTag?.ToString()?.CompareTo(tag.ToString()) != 0)
            {
                NavButtonChangeColor(tag);
                mainFrame1.Source = new Uri($"Page{tag}.xaml", UriKind.Relative);
                selectedTag = button.Tag;
            }

        }
        // реализовать возможность настройки цвета
        private static Thickness DefThickness = new Thickness(0);
        private static Brush DefBrush =  Brushes.White;
        private static Thickness SelectedThickness = new Thickness(0, 0, 0, 2);
        private static Brush SelectedBrush = new SolidColorBrush(Color.FromRgb(99, 110, 114)) ?? Brushes.Black;
        private void NavButtonChangeColor(object tag)
        {
            var btn = FindName("navbtn" + tag) as Button;
            if (btn == null) return;
            if(selectedTag.ToString().CompareTo("null") == 0)
            {
                btn.BorderThickness = SelectedThickness;
                btn.BorderBrush = SelectedBrush;
                return;
            }
            var selectedBtn = FindName("navbtn" + selectedTag) as Button;
            btn.BorderThickness = SelectedThickness;
            btn.BorderBrush = SelectedBrush;
            selectedBtn.BorderThickness = DefThickness;
            selectedBtn.BorderBrush = DefBrush;
        }
    }
}
