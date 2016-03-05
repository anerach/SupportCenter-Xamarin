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
using SupportCenter.Domain.Views;
using SupportCenter.Domain.Models;

using Xamarin.Forms;

namespace SupportCenter
{
    public partial class Dashboard : ContentPage
    {
        private const string BaseUrl = "http://dappersupportcenter.azurewebsites.net/api/";

        public Dashboard()
        {
            InitializeComponent();

            LoadTickets();

            ListViewTickets.RefreshCommand = new Command(() =>
            {
                ListViewTickets.IsRefreshing = true;

                ListViewTickets.ItemsSource = null;
                LoadTickets();

                ListViewTickets.IsRefreshing = false;
            });
        }

        private async void LoadTickets()
        {
            var url = BaseUrl + "Ticket/All";
            var uri = new Uri(string.Format(url, string.Empty));
            var client = new HttpClient();

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tickets = JsonConvert.DeserializeObject<List<Ticket>>(content);
                var ticketViews = tickets.Select(ticket => new TicketView(ticket)).ToList();
                var observable = new ObservableCollection<TicketView>(ticketViews);

                ListViewTickets.ItemsSource = observable;
            }
        }

        private void ListViewTickets_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var ticket = (TicketView)e.Item;

            Navigation.PushAsync(new TicketDetails(ticket));
        }

        private void BtnCreate_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TicketCreate());
        }
    }
}
