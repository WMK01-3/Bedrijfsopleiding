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
            get
            {
                return _suggestions ?? API.GoogleMaps.AutoCompleteLocations.FetchLocations(TbCityValue);
                return null;
            }
            set
            {
                _suggestions = value;
                OnPropertyChanged(nameof(Suggestions));

            }
        }

        private string _tbCityValue;

        public string TbCityValue
        {
            get => _tbCityValue;
            set
            {
                _tbCityValue = value;
                OnPropertyChanged(nameof(Suggestions));
            }
        }

        
        public LocationTabVM(MainWindowVM vm) : base(vm)
        {
           
        }
        
    }
}
