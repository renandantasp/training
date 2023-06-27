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
        public static ObservableCollection<User> Users = new ObservableCollection<User>{
            new User("Renan", "rndnt", "renan@email.com", "senharenan", "https://picsum.photos/200", "A standard bio for example"),
            new User("Maria", "maria_99", "maria@email.com", "senhamaria", "https://picsum.photos/200", "Another standard bio for example")

        };

        public static ObservableCollection<User> GetUsers()
        {
            return Users;
        }

        public static void CreateUser(User User)
        {
            Users.Add(User);
        }

        public static void DeleteUser(User User)
        {
            Users.Remove(User);
        }

        public static void EditUser(User User, User UserEditado)
        {
            User.Name = UserEditado.Name;
            User.Tag = UserEditado.Tag;
            User.Bio = UserEditado.Bio;
            User.Email = UserEditado.Email;
            User.Password = UserEditado.Password;

        }

        public static void AddEssayToUser(string essayId, string userId)
        {
            for(int i=0; i<Users.Count; i++)
            {
                if (Users[i].Id == userId) {
                    Users[i].Essays.Add(essayId);
                    Users[i].EssayStr = string.Join(", ", Users[i].Essays);
                }
            }
        }

        public static void RemoveEssayToUser(string essayId, string userId)
        {
            for(int i=0; i<Users.Count; i++)
            {
                if (Users[i].Id == userId)
                {
                    Users[i].Essays.Remove(essayId);
                    Users[i].EssayStr = string.Join(", ", Users[i].Essays);
                }
            }
        }

        public static bool Any()
        {
            return Users.Any();
        }
    }
}
