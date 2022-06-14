using MovieHacker.Model;
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

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        public PageAdmin()
        {
            InitializeComponent();
            if (Users.Role == UsersRoles.Admin)
                ChangeRoleTo(UsersRoles.Admin);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text != "admin" || password.Password != "admin")
                MessageBox.Show("Неправильный пароль или логин!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                ChangeRoleTo(UsersRoles.Admin);
                MessageBox.Show("Вы успешно вошли!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeRoleTo(UsersRoles.User);
        }
        private void ChangeRoleTo(UsersRoles roles)
        {
            Users.Role = roles;
            if (Users.Role == UsersRoles.Admin)
            {
                stackPanelLogin.Visibility = Visibility.Collapsed;
                stackPanelPassword.Visibility = Visibility.Collapsed;
                signin.Visibility = Visibility.Collapsed;
                signout.Visibility = Visibility.Visible;
            }
            else
            {
                stackPanelLogin.Visibility = Visibility.Visible;
                stackPanelPassword.Visibility = Visibility.Visible;
                signin.Visibility = Visibility.Visible;
                signout.Visibility = Visibility.Collapsed;
            }
        }
    }
}
