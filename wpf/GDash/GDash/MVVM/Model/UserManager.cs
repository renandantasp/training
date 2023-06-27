using GDash.Core;
using GDash.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDash.MVVM.Model
{
    public class UserManager
    {
        public static ObservableCollection<User> Users = new ObservableCollection<User>();
        private Connection conn = new Connection();
   

        public static void CreateUser(User user)
        {
            Users.Add(user);
        }

        public static void DeleteUser(User user)
        {
            Users.Remove(user);
        }

        public static void EditUser(User user, User userEditado)
        {
            user.Name       = userEditado.Name;
            user.Tag        = userEditado.Tag;
            user.Bio        = userEditado.Bio;
            user.Email      = userEditado.Email;
            user.Password   = userEditado.Password;

        }

        public static void AddEssayToUser(string essayId, string userId)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Id == userId)
                {
                    Users[i].Essays.Add(essayId);
                    Users[i].EssayStr = string.Join(", ", Users[i].Essays);
                }
            }
        }

        public static void RemoveEssayToUser(string essayId, string userId)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Id == userId)
                {
                    Users[i].Essays.Remove(essayId);
                    Users[i].EssayStr = string.Join(", ", Users[i].Essays);
                }
            }
        }

    }
}
