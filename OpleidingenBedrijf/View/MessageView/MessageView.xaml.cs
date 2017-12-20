using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Message;
using Message = BedrijfsOpleiding.Models.Message;


namespace BedrijfsOpleiding.View.MessageView
{
    public partial class MessageView
    {
        private MessageVM _viewModel;
        private string _title;
        private bool _read;
        public MessageVM ViewModel
        {
            get => _viewModel = _viewModel ?? new MessageVM(MainVM, this);
            set => _viewModel = value;
        }
        public MessageView(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainVM.CurrentView = new DashBoardView(MainVM);
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _title = ((Message)Messages.SelectedItem).Title;
                Title.Content = _title;
                MessageText.Text = ((Message)Messages.SelectedItem).MessageText;
                using (CustomDbContext context = new CustomDbContext())
                {
                    IQueryable<Message> read = (from message in context.Messages
                                                where message.MessageID == ((Message)Messages.SelectedItem).MessageID
                                                select message);
                    foreach (var m in read)
                    {
                        m.Read = true;
                    }
                    context.SaveChanges();
                }
            }
            catch
            {

            }


        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CustomDbContext context = new CustomDbContext())
                {
                    IQueryable<Message> deleteMessage = (from message in context.Messages
                                                         where message.MessageID == ((Message)Messages.SelectedItem).MessageID
                                                         select message);
                    context.Messages.Remove(deleteMessage.First());
                    context.SaveChanges();
                    MainVM.CurrentView = new MessageView(MainVM);

                }
            }
            catch { }
        }
    }
}
