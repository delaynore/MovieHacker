using MovieHacker.Model;
using MovieHacker.Model.Tables;
using MovieHacker.Model.WindowsModes;
using System.Windows;
using System.Linq;
using System;
namespace MovieHacker.Views
{
    public partial class AddNewGenre : Window
    {
        private IGenreWindowMode _windowMode;
        public AddNewGenre(IGenreWindowMode mode)
        {
            InitializeComponent();
            _windowMode = mode;
            Title = _windowMode.TitleText;
            textHint.Text = _windowMode.TextBlockText;
            button.Content = _windowMode.ButtonContent;
            textGenre.Text = _windowMode.GenreName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _windowMode.GenreName = textGenre.Text;
            _windowMode.Execute();
            Close();
        }

    }
}
