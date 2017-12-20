using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Message;


namespace BedrijfsOpleiding.View.MessageView
{
    public partial class MessageView
    {
        private MessageVM _viewModel;
        private string _title;
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
            catch{} 
        }
    }
}
