using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using BedrijfsOpleiding.View.CourseView.AddCourse;

namespace BedrijfsOpleiding.ViewModel.Course.AddCourse
{
    public class MainTabVM : BaseViewModel
    {
        private MainTab _view;

        private string _errorMessage = "";

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public MainTabVM(MainWindowVM vm, MainTab view) : base(vm)
        {
            _view = view;
        }

        public void CheckData()
        {
            var textBlocks = new TextBox[] { _view.CourseName, _view.Price, _view.Duration };
            int errorAmount = textBlocks.Sum(item => item.Text.IsEmpty() ? 1 : 0);

            errorAmount += new TextRange(_view.Description.Document.ContentStart, _view.Description.Document.ContentEnd).Text.IsEmpty() ? 1 : 0;

            ErrorMessage = errorAmount > 0 ? "Een of meerdere velden zijn leeg" : "";
            
            if (errorAmount == 0)
                _view.View.tabControl.SelectedIndex += 1;
        }
    }
}
