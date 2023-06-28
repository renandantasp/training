using GDash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GDash.MVVM.Model
{
    public class Essay : ObservableObject, IModel
    {
        private string _id;
        private string _userId;
        private string _image;
        private string _essayTitle;
        private string _essayText;
        private List<string> _likes;

        
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
        public string UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                RaisePropertyChanged(nameof(_userId));
            }
        }
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged(nameof(_image));
            }
        }
        public string EssayTitle
        {
            get => _essayTitle;
            set
            {
                _essayTitle = value;
                RaisePropertyChanged(nameof(_essayTitle));
            }
        }
        public string EssayText
        {
            get => _essayText;
            set
            {
                _essayText = value;
                RaisePropertyChanged(nameof(_essayText));
            }
        }
        public List<string> Likes
        {
            get => _likes;
            set
            {
                _likes = value;
                RaisePropertyChanged(nameof(_likes));
            }
        }

        public Essay Clone()
        {
            return (Essay)MemberwiseClone();
        }
        
        public IModel GetObject()
        {
            return this;
        }

        public Essay()
        {
            Id = string.Empty;
            UserId = string.Empty;
            Image = string.Empty;
            EssayTitle = string.Empty;
            EssayText = string.Empty;
            Likes = new List<string>();
        }

        public Essay(string userId, string image, string essayTitle, string essayText)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            Image = image;
            EssayTitle = essayTitle;
            EssayText = essayText;
            Likes = new List<string>();
        }

        public Essay(string id, string userId, string image, string essayTitle, string essayText)
        {
            Id = id;
            UserId = userId;
            Image = image;
            EssayTitle = essayTitle;
            EssayText = essayText;
            Likes = new List<string>();
        }
    }
}
