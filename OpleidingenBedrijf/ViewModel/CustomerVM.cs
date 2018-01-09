using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View;

namespace BedrijfsOpleiding.ViewModel
{
    public class CustomerVM : BaseViewModel
    {
        private CustomerView _view;
        private string _nameFilter = "";
        private string _roleFilter = "";

        #region GridItems : List<CustomerDataGridItem>

        private List<CustomerDataGridItem> _gridItems;
        public List<CustomerDataGridItem> GridItems
        {
            get => _gridItems;
            set
            {
                _gridItems = value;
                OnPropertyChanged(nameof(GridItems));
            }
        }

        private bool _infoChanged;
        public bool InfoChanged
        {
            get => _infoChanged;
            set
            {
                _infoChanged = value;
                OnPropertyChanged(nameof(InfoChanged));
            }
        }

        #endregion

        public CustomerVM(MainWindowVM vm, CustomerView view) : base(vm)
        {
            _view = view;
        }

        public void UpdateDataGrid()
        {
            GridItems = GetGridItems();
        }

        public List<CustomerDataGridItem> GetGridItems()
        {
            List<User> users = GetUsers();
            return users.Select(user => new CustomerDataGridItem(user.UserID) { FullName = user.FullName, Blocked = user.Blocked.ToString(), OriginalRole = user.Role, CurrentRole = user.Role }).ToList();
        }

        /// <summary>
        /// Get a list of users from the database based on the filter
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            var users = new List<User>();

            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<User> userList = from u in context.Users
                                            select u;
                foreach (User user in userList)
                {
                    if ($"{user.FirstName} {user.LastName}".Contains(_nameFilter) == false) continue;
                    if (user.Role.ToString().Contains(_roleFilter) == false) continue;

                    users.Add(user);
                }
            }
            return users;
        }

        public void SaveEdits()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                foreach (CustomerDataGridItem gridItem in GridItems)
                {
                    IQueryable<User> userQueryable = from u in context.Users
                                                     where u.UserID == gridItem.UserID
                                                     select u;

                    if (!userQueryable.Any()) continue;

                    User user = userQueryable.First();
                    user.Role = gridItem.CurrentRole;
                    context.Users.Attach(user);
                    DbEntityEntry<User> entry = context.Entry(user);
                    entry.Property(e => e.Role).IsModified = true;
                    context.SaveChanges();
                }
            }
            UpdateDataGrid();
        }

        public bool IsInfoDifferent() =>
            GridItems.Any(gridItem => gridItem.CurrentRole != gridItem.OriginalRole);

        public void FilterText()
        {
            _nameFilter = _view.TxtUserFullName.Text;

            if (_view.CbxRole.SelectedValue != null)
                _roleFilter = _view.CbxRole.SelectedValue.ToString();
            UpdateDataGrid();
        }
        public void Block(int userID)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<User> userResults = from u in context.Users
                                               where userID == u.UserID
                                               select u;

                userResults.First().Blocked = !userResults.First().Blocked;
                context.SaveChanges();
            }
        }
    }
}
