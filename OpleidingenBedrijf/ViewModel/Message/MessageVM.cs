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
        public string Title { get; set; }

        public MessageVM(MainWindowVM vm, UserControl boundView) : base(vm)
        {
            _user = vm.CurUser;
        }

        public List<Models.Message> GetMessageList()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<Models.Message> result = from m in context.Messages
                    where m.UserID == _user.UserID
                    select m;

                foreach (Models.Message message in result)
                {       

                    MessageList.Add(message);
                }

                return MessageList;
            }
        }
    }
}
