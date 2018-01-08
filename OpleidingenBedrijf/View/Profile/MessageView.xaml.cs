using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Message;
using Message = BedrijfsOpleiding.Models.Message;


namespace BedrijfsOpleiding.View.Profile
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vm"></param>
        public MessageView(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();
            ViewModel.IsNotNewMessage = true;
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.IsNotNewMessage = true;

            if (Messages.SelectedItem == null) return;

            _title = ((Message)Messages.SelectedItem).Title;
            Title.Text = _title;
            MessageText.Text = ((Message)Messages.SelectedItem).MessageText;
            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<Message> read = from message in context.Messages
                                           where message.MessageID == ((Message)Messages.SelectedItem).MessageID
                                           select message;

                foreach (Message m in read)
                    m.Read = true;
                context.SaveChanges();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
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

        private void Messages_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            Message test = (Message)e.Row.DataContext;
            if (test.Read)
                e.Row.Foreground = new SolidColorBrush(Colors.LightGray);
        }

        private void Btn_SendMessage_OnClick(object sender, RoutedEventArgs e)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<User> user = from u in context.Users
                                        where u.UserName == NameTextBox.Text
                                        select u;

                if (user.Any())
                {
                    context.Messages.Add(new Message { MessageText = MessageText.Text, Read = false, Title = Title.Text, UserID = user.First().UserID });
                    context.SaveChanges();
                    MainVM.CurrentView = new MessageView(MainVM);
                }
                else
                {
                    NameTextBox.BorderBrush = new SolidColorBrush(Colors.Orange);
                    NameTextBox.Text = "Gebruiker Bestaat niet";
                }
            }
        }

        private void Btn_NewMessage_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.IsNotNewMessage = false;
        }
    }
}
