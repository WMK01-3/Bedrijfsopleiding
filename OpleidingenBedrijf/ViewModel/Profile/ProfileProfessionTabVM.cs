using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.Profile;

namespace BedrijfsOpleiding.ViewModel.Profile
{
    public class ProfileProfessionTabVM : BaseViewModel
    {
        private List<ProfileProfessionsItem> _professions;
        public List<ProfileProfessionsItem> Professions
        {
            get => _professions;
            set
            {
                _professions = value;
                OnPropertyChanged(nameof(Professions));
            }
        }

        public ProfileProfessionTabVM(MainWindowVM vm) : base(vm)
        {
            UpdateProfessionsList();
        }

        public void UpdateList()
        {
            OnPropertyChanged(nameof(Professions));
        }

        public void AddProfession(string text)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                List<string> profs = (from p in context.Professions
                                      where p.UserID == MainVM.CurUser.UserID
                                      select p.ProfessionName).ToList();

                if (profs.Contains(text) == false)
                    context.Professions.Add(new Profession { ProfessionName = text, UserID = MainVM.CurUser.UserID});

                context.SaveChanges();
            }

            UpdateProfessionsList();
        }

        public void UpdateProfessionsList()
        {
            var professions = new List<ProfileProfessionsItem>();

            using (CustomDbContext context = new CustomDbContext())
            {
                List<Profession> profs = (from p in context.Professions
                                      where p.UserID == MainVM.CurUser.UserID
                                      select p).ToList();

                professions.AddRange(profs.Select(prof => new ProfileProfessionsItem(prof.ProfessionName, prof.ProfessionID)));
            }

            Professions = professions;
        }
    }

    public class ProfileProfessionsItem
    {
        public string ProfessionName { get; set; }
        public int ProfessionID { get; set; }

        public ProfileProfessionsItem(string profession, int professionID)
        {
            ProfessionID = professionID;
            ProfessionName = profession;
        }
    }
}
