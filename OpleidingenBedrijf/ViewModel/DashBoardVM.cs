using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Windows.Controls;
using System.Xml;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View;
using Newtonsoft.Json;

namespace BedrijfsOpleiding.ViewModel
{
    public class DashBoardVM : BaseViewModel
    {
        public bool IsCursist => MainVM.IsEmployee == false;

        public bool IsEmployee => MainVM.IsEmployee;

        // private double[] _longlat = new double[2];
        private double[] _longlat = new double[2];



        public DashBoardVM(MainWindowVM vm, DashBoardView v) : base(vm)
        {
        }

        private double[] _getLongLat(string street, string city)
        {
            // to Read the Stream
            StreamReader sr = null;

            //The Google Maps API Either return JSON or XML. We are using XML Here
            //Saving the url of the Google API 
            string url =
                String.Format(
                    $"http://maps.googleapis.com/maps/api/geocode/xml?address={street}+{city}&sensor=false");

            //to Send the request to Web Client 
            WebClient wc = new WebClient();
            try
            {
                sr = new StreamReader(wc.OpenRead(url));
            }
            catch (Exception ex)
            {
                throw new Exception("The Error Occured" + ex.Message);
            }

            try
            {
                XmlTextReader xmlReader = new XmlTextReader(sr);
                bool latread = false;
                bool longread = false;

                while (xmlReader.Read())
                {
                    xmlReader.MoveToElement();
                    switch (xmlReader.Name)
                    {
                        case "lat":

                            if (!latread)
                            {
                                xmlReader.Read();
                                _longlat[1] = Convert.ToDouble(xmlReader.Value, CultureInfo.InvariantCulture);
                                Console.WriteLine(_longlat[1]);
                                latread = true;

                            }
                            break;

                        case "lng":
                            if (!longread)
                            {
                                xmlReader.Read();
                                _longlat[0] = Convert.ToDouble(xmlReader.Value, CultureInfo.InvariantCulture);
                                Console.WriteLine(_longlat[0]);
                                longread = true;
                            }

                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An Error Occured" + ex.Message);
            }

            Console.WriteLine($"Return _longlat:{_longlat[0]}, {_longlat[1]}");

            return _longlat;

        }


        public void LoadMarkers(WebBrowser wbMaps)
        {

            using (var context = new CustomDbContext())
            {
                var allLocationIdFromCourses = (from cl in context.Courses
                    select cl.LocationID).Distinct().ToList();

                foreach (var location_id in allLocationIdFromCourses)
                {
                    var location = (from l in context.Locations
                        where l.LocationID == location_id
                        select l).FirstOrDefault();

                    double Long = _getLongLat(location.Street, location.City)[0];
                    double Lat = _getLongLat(location.Street, location.City)[1];
                    string title = location.Classroom.ToString();

                    Console.WriteLine($"{location.City}: {Long}, {Lat}");

                    wbMaps.InvokeScript("addMarker", JsonConvert.SerializeObject(new
                    {
                        Lat = new double[] {Lat},
                        Long = new double[] {Long}
                    }), title, location.LocationID);
                }
            }

        }

        public void LoadStandardDataBoxes(Label label1, Label label2, Label label3)
        {
            using (var context = new CustomDbContext())
            {
                var users = (from u in context.Users
                    where u.Role == User.RoleEnum.Customer
                    select u).ToList();

                var course = (from c in context.Courses
                    select c).ToList();

                var teachers = (from u in context.Users
                    where u.Role == User.RoleEnum.Teacher
                    select u).ToList();

                label1.Content = users.Count.ToString();
                label2.Content = course.Count.ToString();
                label3.Content = teachers.Count.ToString();
            }

        }

        public void LoadCourseBox(Label labelTitle, TextBlock tbxDesc, int course_id)
        {
            using (var context = new CustomDbContext())
            {
                var course = (from c in context.Courses
                    where c.CourseID == course_id
                    select c).First();

                labelTitle.Content = course.Title;
                tbxDesc.Text = course.Description;


            }
        }
    }

}
