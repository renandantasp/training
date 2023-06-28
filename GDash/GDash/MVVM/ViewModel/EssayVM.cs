using GDash.Core;
using GDash.DB;
using GDash.MVVM.Model;
using GDash.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GDash.MVVM.ViewModel
{
    public class EssayVM : ObservableObject
    {
        public ObservableCollection<Essay> Essays { get; set; }
        private EssayConn conn;

        private Essay _selectedEssasy;
        public Essay SelectedEssay
        {
            get { return _selectedEssasy; }
            set
            {
                _selectedEssasy = value;
            }
        }

        public ICommand CreateCMD => new RelayCommand(_ =>
        {
            Essay newEssay = new Essay();
            newEssay.Id = Guid.NewGuid().ToString();

            EssayForm essayForm = new EssayForm();

            essayForm.Title = "New Essay";
            essayForm.DataContext = newEssay;
            essayForm.ShowDialog();

            if (essayForm.DialogResult.HasValue && essayForm.DialogResult.Value && essayForm.UserIdComboBox.Text != string.Empty)
            {
                conn.InsertEssayDB(newEssay);
                SelectedEssay = newEssay;
            }
        }, canExecute => conn.HasUser());

        public ICommand UpdateCMD => new RelayCommand(exec => {
            Essay editedEssay = SelectedEssay.Clone();
            EssayForm essayForm = new EssayForm();

            essayForm.Title = "Essay Edit";
            essayForm.DataContext = editedEssay;
            essayForm.ShowDialog();

            if (essayForm.DialogResult.HasValue && essayForm.DialogResult.Value)
            {
                conn.AlterEssayDB(editedEssay);
            }

        }, canExecute => Essays.Any() && SelectedEssay != null);
        public ICommand DeleteCMD => new RelayCommand(_ => {

            string userId = SelectedEssay.UserId;
            conn.DeleteEssayDB(SelectedEssay.Id);

            SelectedEssay = Essays.FirstOrDefault();
        }, canExecute => Essays.Any());
        
        
        ObservableCollection<Essay> GetEssays()
        {
            return new ObservableCollection<Essay>(conn.GetEssaysDB());
        }

        public EssayVM()
        {
            conn = new EssayConn();
            Essays = GetEssays();
        }
    }
}
