using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BedrijfsOpleiding.API.GoogleMaps;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.CourseView.AddCourse;

namespace BedrijfsOpleiding.ViewModel.Course.AddCourse
{
    public class LocationTabVM : BaseViewModel
    {
        private LocationTab _view;
        private AddCourseView _addCourseView;
        private List<string> _suggestions;
        private string _errorMessage;
        private Visibility _errorVisible = Visibility.Hidden;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public Visibility ErrorVisible
        {
            get => _errorVisible;
            set
            {
                _errorVisible = value;
                OnPropertyChanged(nameof(ErrorVisible));
            }
        }
        public List<string> Suggestions
        {
            get
            {
                return _suggestions ?? API.GoogleMaps.AutoCompleteLocations.FetchLocations(TbCityValue);
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

        public int SelectedLocationID { get; set; }

        public int AddLocation(string mapsLoc)
        {
            string[] loc = new string[3];
            int i = 0;
            foreach (string l in mapsLoc.Split(','))
            {
                loc[i] = l;
                i++;
            }

            if (String.IsNullOrWhiteSpace(loc[0]) || String.IsNullOrWhiteSpace(loc[1]) || String.IsNullOrWhiteSpace(loc[2]))
            {
                ErrorVisible = Visibility.Visible;
                ErrorMessage = "De opgegeven locatie is niet correct";
                return 0;
            }
            int locID;
            using (CustomDbContext context = new CustomDbContext())
            {
                Models.Location location = new Location()
                {
                    Street = loc[0],
                    City = loc[1],
                    Country = loc[2]
                };
                context.Locations.AddOrUpdate(location);
                context.SaveChanges();
                locID = location.LocationID;
            }
            return locID;
        }

        public int GetLocation(string mapsLoc)
        {
            string[] loc = mapsLoc.Split(',');   // classroom, street, city, Country
            try
            {
                string street = loc[0];
                string city = loc[1];
                string country = loc[2];
                using (CustomDbContext context = new CustomDbContext())
                {
                    IQueryable<Location> loclist = from l in context.Locations
                                                   where ((l.Street == street) && (l.City == city) && (l.Country == country))
                                                   select l;
                    Location location = loclist.First();
                    return location.LocationID;
                }
            }
            catch
            {
                return 0;
            }
        }

        /*Provides values for combobox*/
        //private string _oneTeacher;
        public CollectionView LocationList { get; }

        public LocationTabVM(LocationTab view, MainWindowVM vm, AddCourseView addCourseView) : base(vm)
        {
            _addCourseView = addCourseView;
            _view = view;
            List<Location> locations;
            using (CustomDbContext context = new CustomDbContext())
            {
                locations = (from l in context.Locations select l).ToList();
            }
            List<string> loclist = locations.Select(loc => $"{loc.Street},{loc.City},{loc.Country}").ToList();
            loclist.Insert(0, "Nieuwe locatie toevoegen");
            LocationList = new CollectionView(loclist);
        }

        public void CheckData()
        {
            if (_view.tbCity.Text.IsEmpty() == false || (string)_view.cboChooseLocation.SelectedValue != "Nieuwe locatie toevoegen")
                _addCourseView.tabControl.SelectedIndex += 1;
        }

        public string[] GetLocationArray() =>
            (string)_view.cboChooseLocation.SelectedValue != "Nieuwe locatie toevoegen" ? _view.cboChooseLocation.SelectedValue.ToString().Split(',') : _view.tbCity.Text.Split(',');
    }
}
