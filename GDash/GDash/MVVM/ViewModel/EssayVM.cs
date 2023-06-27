using GDash.Core;
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
            Essay essay = new Essay();
            essay.Id = Guid.NewGuid().ToString();

            EssayForm essayForm = new EssayForm();

            essayForm.Title = "New Essay";
            essayForm.DataContext = essay;
            essayForm.ShowDialog();

            if (essayForm.DialogResult.HasValue && essayForm.DialogResult.Value && essayForm.UserIdComboBox.Text != string.Empty)
            {
                EssayManager.CreateEssay(essay);
                UserManager.AddEssayToUser(essay.Id, essay.UserId);
                SelectedEssay = essay;
            }
        }, canExecute => UserManager.Any());

        public ICommand UpdateCMD => new RelayCommand(exec => {
            Essay editedEssay = SelectedEssay.Clone();
            EssayForm essayForm = new EssayForm();

            essayForm.Title = "Essay Edit";
            essayForm.DataContext = editedEssay;
            essayForm.ShowDialog();
            if (essayForm.DialogResult.HasValue && essayForm.DialogResult.Value)
            {
                EssayManager.EditEssay(SelectedEssay, editedEssay);
            }

        }, canExecute => Essays.Any());
        public ICommand DeleteCMD => new RelayCommand(_ => {

            string userId = SelectedEssay.UserId;
            UserManager.RemoveEssayToUser(SelectedEssay.Id, SelectedEssay.UserId);

            EssayManager.DeleteEssay(SelectedEssay);

            SelectedEssay = Essays.FirstOrDefault();
        }, canExecute => Essays.Any());
        public EssayVM()
        {
            Essays = EssayManager.GetEssays();
        }
    }
}
