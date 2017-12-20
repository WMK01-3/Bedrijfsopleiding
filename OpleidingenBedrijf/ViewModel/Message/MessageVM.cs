using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BedrijfsOpleiding.Models;

namespace BedrijfsOpleiding.ViewModel.Message
{
    public class MessageVM : BaseViewModel
    {
        private readonly User _user;
        public List<Models.Message> MessageList => GetMessageList();

        private string _title;
        public string Title { get => _title; set => OnPropertyChanged(); }

        public MessageVM(MainWindowVM vm, UserControl boundView) : base(vm)
        {
            _user = vm.CurUser;
        }

        public List<Models.Message> GetMessageList()
        {
            var MessageList = new List<Models.Message>();
            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<Models.Message> result = from m in context.Messages
                    where m.UserID == _user.UserID
                    select m;

                foreach (Models.Message message in result)
                {
                    _title = message.Title;
                    MessageList.Add(message);
                }

                return MessageList;
            }
        }
    }
}
