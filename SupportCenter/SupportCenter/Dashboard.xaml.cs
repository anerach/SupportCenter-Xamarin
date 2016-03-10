using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using SC.BL;
using SC.BL.Domain.Views;

using Xamarin.Forms;

namespace SupportCenter
{
    public partial class Dashboard : ContentPage
    {
        private readonly TicketManager manager;

        public Dashboard()
        {
            InitializeComponent();

            manager = new TicketManager();

            ListViewTickets.RefreshCommand = new Command(() =>
            {
                ListViewTickets.IsRefreshing = true;

                LoadTickets();
            });
        }

        private void OnAppearing(object sender, EventArgs args)
        {
            LoadTickets();
        }

        private void LoadTickets()
        {
            ListViewTickets.ItemsSource = null;

            IEnumerable<TicketView> ticketViews = manager.GetTickets().Select(ticket => new TicketView(ticket)).ToList();
            var observable = new ObservableCollection<TicketView>(ticketViews);

            ListViewTickets.ItemsSource = observable;

            if(ListViewTickets.IsRefreshing)
                ListViewTickets.IsRefreshing = false;
        }

        private void ListViewTickets_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var ticket = (TicketView)e.Item;

            Navigation.PushAsync(new TicketDetails(ticket.Ticket.TicketNumber));

            ListViewTickets.SelectedItem = null;
        }

        private void BtnCreate_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TicketCreate());
        }

        public void OnClose(object sender, object e)
        {
            var ticketView = (TicketView)((MenuItem)sender).CommandParameter;

            manager.CloseTicket(ticketView.Ticket);

            LoadTickets();
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var ticketView = (TicketView)((MenuItem)sender).CommandParameter;

            manager.DeleteTicket(ticketView.Ticket);

            LoadTickets();
        }
    }
}
