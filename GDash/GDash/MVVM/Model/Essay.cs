using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GDash.MVVM.Model
{
    public class Essay
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Image { get; set; }
        public string EssayTitle { get; set; }
        public string EssayText { get; set; }
        public List<int> Likes { get; set; }

        public Essay Clone()
        {
            return (Essay)MemberwiseClone();
        }

        public Essay()
        {
            Id = string.Empty;
            UserId = string.Empty;
            Image = string.Empty;
            EssayTitle = string.Empty;
            EssayText = string.Empty;
            Likes = new List<int>();
        }

        public Essay(string userId, string image, string essayTitle, string essayText)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            Image = image;
            EssayTitle = essayTitle;
            EssayText = essayText;
            Likes = new List<int>();
        }
    }
}
