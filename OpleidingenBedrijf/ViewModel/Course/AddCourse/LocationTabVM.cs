using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BedrijfsOpleiding.API.GoogleMaps;
using BedrijfsOpleiding.View.CourseView.AddCourse;

namespace BedrijfsOpleiding.ViewModel.Course.AddCourse
{
    public class LocationTabVM : BaseViewModel
    {


        private List<string> _suggestions;
        public List<string> Suggestions
        {
            get { return _suggestions = _suggestions ?? API.GoogleMaps.AutoCompleteLocations.FetchLocations(TbCityValue); }
            set => _suggestions = value;
        }

        //private string _tbCityValue;

        public string TbCityValue
        {
            get => TbCityValue;
            set
            {
                TbCityValue = value;
                Suggestions = _suggestions ?? API.GoogleMaps.AutoCompleteLocations.FetchLocations(TbCityValue);
                OnPropertyChanged(nameof(Suggestions));
            }
        }

        
        public LocationTabVM(MainWindowVM vm) : base(vm)
        {
           
        }
        
    }
}
