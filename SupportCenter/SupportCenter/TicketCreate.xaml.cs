using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SupportCenter.Domain.Models;
using Xamarin.Forms;

namespace SupportCenter
{
    public partial class TicketCreate : ContentPage
    {
        private const string BaseUrl = "http://dappersupportcenter.azurewebsites.net/api/";

        public TicketCreate()
        {
            InitializeComponent();
        }


        private async void BtCreate_OnClicked(object sender, EventArgs e)
        {
            Ticket createdTicket = CreateTicket();
            DisplayAlert("Ticket created!", createdTicket.Text, "OK");

        }

        private Ticket CreateTicket()
        {
            string apiUrl = "Ticket";
            string url = String.Format(BaseUrl + apiUrl, string.Empty);
            Uri uri = new Uri(url);

            Ticket returnTicket = null;

            using (var client = new HttpClient())
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, uri);
                httpRequest.Headers.Add("Accept", "application/json");

                object data = new
                {
                    AccountId = Convert.ToInt32(AccountId.Text),
                    Text = Problem.Text
                };

                string dataAsJsonString = JsonConvert.SerializeObject(data);
                httpRequest.Content = new StringContent(dataAsJsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = client.SendAsync(httpRequest).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    string responseContentAsString = httpResponse.Content.ReadAsStringAsync().Result;
                    returnTicket = JsonConvert.DeserializeObject<Ticket>(responseContentAsString);
                }
                else
                {
                    throw new Exception(httpResponse.StatusCode.ToString());
                }
            }
            return returnTicket;
        }
    }
}
