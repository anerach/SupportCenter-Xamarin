﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
                var observable = new ObservableCollection<Ticket>(tickets);

                ListViewTickets.ItemsSource = observable;
            }
        }
    }
}