using BedrijfsOpleiding.View.CourseView;
using BedrijfsOpleiding.Models;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class SelectDatesVM : BaseViewModel
    {
        private readonly SelectDatesView _view;
        

        public SelectDatesVM(MainWindowVM vm, SelectDatesView v) : base(vm)
        {
            _view = v;
        }
    }
}
