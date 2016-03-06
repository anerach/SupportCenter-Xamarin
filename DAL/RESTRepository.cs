using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SupportCenter.Domain.Models;
using SupportCenter.Domain.Views;

namespace SupportCenter.DAL
{
    public class RESTRepository
    {
        private const string BaseUrl = "http://dappersupportcenter.azurewebsites.net/api/";

        public HttpClient GetClient()
        {
            return new HttpClient();
        }

        public IEnumerable<Ticket> GetTickets()
        {
            var url = BaseUrl + "Ticket/All";
            var uri = new Uri(string.Format(url, string.Empty));
            var client = new HttpClient();

            IEnumerable<Ticket> returnTickets = null;

            //var response = await client.GetAsync(uri);
            HttpResponseMessage response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                //var content = await response.Content.ReadAsStringAsync();
                string content = response.Content.ReadAsStringAsync().Result;
                returnTickets = JsonConvert.DeserializeObject<List<Ticket>>(content);
                //var ticketViews = tickets.Select(ticket => new TicketView(ticket)).ToList();
               // var observable = new ObservableCollection<TicketView>(ticketViews);

                //ListViewTickets.ItemsSource = observable;
            }

            return returnTickets;
        }

        public Ticket CreateTicket(int accountId, string problem)
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
                    AccountId = accountId,
                    Text = problem
                };

                string dataAsJsonString = JsonConvert.SerializeObject(data);
                httpRequest.Content = new StringContent(dataAsJsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = client.SendAsync(httpRequest).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    string responseContentAsString = httpResponse.Content.ReadAsStringAsync().Result;
                    returnTicket = JsonConvert.DeserializeObject<Ticket>(responseContentAsString);
                }
            }

            return returnTicket;
        }

        public bool CloseTicket(int ticketNumber)
        {
            bool success;
            using (var client = GetClient())
            {
                string uri = BaseUrl + "Ticket/" + ticketNumber + "/State/Closed";
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Put, uri);

                HttpResponseMessage httpResponse = client.SendAsync(httpRequest).Result;

                if (!httpResponse.IsSuccessStatusCode)
                {
                    success = false;
                }
                else
                {
                    success = true;
                }
            }
            return true;
        }

        public TicketResponse CreateTicketResponse(TicketResponse response)
        {
            string uri = BaseUrl + "TicketResponse";
            TicketResponse returnResponse = null;

            using (var client = GetClient())
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, uri);
                object data = new
                {
                    TicketNumber = response.Ticket.TicketNumber,
                    ResponseText = response.Text,
                    IsClientResponse = response.IsClientResponse
                };

                string dataAsJsonString = JsonConvert.SerializeObject(data);
                httpRequest.Content = new StringContent(dataAsJsonString, Encoding.UTF8, "application/json");
                httpRequest.Headers.Add("Accept", "application/json");

                HttpResponseMessage httpResponse = client.SendAsync(httpRequest).Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    string responseContentAsString = httpResponse.Content.ReadAsStringAsync().Result;
                    returnResponse = JsonConvert.DeserializeObject<TicketResponse>(responseContentAsString);
                }
                else
                {
                    throw new Exception(httpResponse.StatusCode + " " + httpResponse.ReasonPhrase);
                }
            }

            return returnResponse;
        }
    }
}
