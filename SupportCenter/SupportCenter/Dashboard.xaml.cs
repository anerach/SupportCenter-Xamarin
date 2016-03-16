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
using SC.BL.Domain;
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

        private async void LoadTickets()
        {
            try
            {
                ListViewTickets.ItemsSource = null;

                var ticketViews = await Task.Run(() => manager.GetTicketViews());
                var observable = new ObservableCollection<TicketView>(ticketViews);

                ListViewTickets.ItemsSource = observable;
            }
            catch (AggregateException)
            {
                await DisplayAlert("Error", "No internet connection", "Ok");
            }
            finally
            {
                if (ListViewTickets.IsRefreshing)
                    ListViewTickets.IsRefreshing = false;
            }
        }

        private async void ListViewTickets_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var ticket = (TicketView)e.Item;

            await Navigation.PushAsync(new TicketDetails(ticket.Ticket.TicketNumber));

            ListViewTickets.SelectedItem = null;
        }

        private async void BtnCreate_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TicketCreate());
        }

        public async void OnClose(object sender, object e)
        {
            try
            {
                var ticketView = (TicketView)((MenuItem)sender).CommandParameter;

                manager.CloseTicket(ticketView.Ticket);

                LoadTickets();
            }
            catch (AggregateException)
            {
                await DisplayAlert("Error", "No internet connection", "Ok");
            }
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            try
            {
                var ticketView = (TicketView)((MenuItem)sender).CommandParameter;

                manager.DeleteTicket(ticketView.Ticket);

                LoadTickets();
            }
            catch (AggregateException)
            {
                await DisplayAlert("Error", "No internet connection", "Ok");
            }
        }
    }
}
