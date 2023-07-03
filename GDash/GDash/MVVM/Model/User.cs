using GDash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace GDash.MVVM.Model
{
    public class User : ObservableObject, IModel
    {

        private string _id;
        private string _name;
        private string _tag;
        private string _email;
        private string _password;
        private string _profileImage;
        private string _bio;
        private string _essayStr;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public string Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                RaisePropertyChanged(nameof(Tag));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }
        public string ProfileImage
        {
            get => _profileImage;
            set
            {
                _profileImage = value;
                RaisePropertyChanged(nameof(ProfileImage));
            }
        }
        public string Bio
        {
            get => _bio;
            set
            {
                _bio = value;
                RaisePropertyChanged(nameof(Bio));
            }
        }

        public string EssayStr
        {
            get => _essayStr;
            set
            {
                _essayStr = value;
                RaisePropertyChanged(nameof(EssayStr));
            }
        }

        public User Clone()
        {
            return (User)MemberwiseClone();
        }

        public IModel GetObject()
        {
            return this;
        }

        public User()
        {
            Id = string.Empty;
            Name = string.Empty;
            Tag = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ProfileImage = string.Empty;
            Bio = string.Empty;

        }

        public User(string id, string name, string tag, string email, string password, string profileImage, string bio)
        {
            Id = id;
            Name = name;
            Tag = tag;
            Email = email;
            Password = password;
            ProfileImage = profileImage;
            Bio = bio;

        }


    }
}
