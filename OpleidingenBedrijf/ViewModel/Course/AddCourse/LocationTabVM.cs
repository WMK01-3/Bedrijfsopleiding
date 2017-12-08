using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BedrijfsOpleiding.API.GoogleMaps;
using BedrijfsOpleiding.View.CourseView.AddCourse;

namespace BedrijfsOpleiding.ViewModel.Course.AddCourse
{
    public class LocationTabVM : BaseViewModel
    {


        public List<string> SuggestionList { get; set; }
        
        public LocationTabVM(MainWindowVM vm) : base(vm)
        {
           
        }
        
    }
}
