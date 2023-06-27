using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDash.MVVM.Model
{
    internal class EssayManager
    {
        public static ObservableCollection<Essay> Essays = new ObservableCollection<Essay>();


        public static void CreateEssay(Essay Essay)
        {
            Essays.Add(Essay);
        }

        public static void DeleteEssay(Essay Essay)
        {
            Essays.Remove(Essay);
        }

        public static void EditEssay(Essay Essay, Essay EssayEditado)
        {
            Essay.EssayText = EssayEditado.EssayText;
            Essay.EssayTitle = EssayEditado.EssayTitle;
            Essay.Image = EssayEditado.Image;

        }

        public static void DeleteAllEssays(List<string> essays)
        {
            for (int i = 0; i < essays.Count; i++)
            {
                Essays.Remove(Essays.Where(e => e.Id == essays[i]).Single());
            }
        }
    }
}
