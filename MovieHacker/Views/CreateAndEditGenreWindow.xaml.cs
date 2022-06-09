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
        private readonly IGenreWindowMode mode;
        public CreateAndEditGenreWindow(IGenreWindowMode windowMode)
        {
            InitializeComponent();
            mode = windowMode;
            textHint.Text = mode.TextBlockText;
            button.Content = mode.ButtonContent;
            textGenre.Text = mode.Genre.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(mode.GenreController.GetAll().Select(x=>x.Name).IsAlreadyExistInEnumerable(textGenre.Text))
                MessageBox.Show("Такой жанр уже существует! Введите другое имя", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                mode.Genre.Name = textGenre.Text;
                mode.Execute();
                Close();
            }
        }

    }
}
