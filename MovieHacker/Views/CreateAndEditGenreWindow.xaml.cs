using MovieHacker.Model;
using MovieHacker.Model.Tables;
using MovieHacker.Model.Extensions;
using MovieHacker.Model.WindowsModes;
using System.Windows;
using System.Linq;
using System;
namespace MovieHacker.Views
{
    public partial class CreateAndEditGenreWindow : Window
    {
        private readonly IWindowMode<Genre> mode;
        public CreateAndEditGenreWindow(IWindowMode<Genre> windowMode)
        {
            InitializeComponent();
            mode = windowMode;
            button.Content = mode.ButtonContent;
            textGenre.Text = mode.Entity.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(mode.Db!.Genres.Select(x=>x.Name).IsAlreadyExistInEnumerable(textGenre.Text))
                MessageBox.Show("Такой жанр уже существует! Введите другое имя", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                mode.Entity.Name = textGenre.Text;
                mode.Execute();
                mode.Db.SaveChanges();
                Close();
            }
        }

    }
}
