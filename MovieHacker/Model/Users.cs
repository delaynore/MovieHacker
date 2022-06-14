using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieHacker.Model
{
    public static class Users
    {
        public static UsersRoles Role = UsersRoles.User;
        public static Visibility ToVisibility()
        {
            return Role == UsersRoles.User ? Visibility.Collapsed : Visibility.Visible;
        }
    }
    public enum UsersRoles
    {
        User,
        Admin
    }

}
