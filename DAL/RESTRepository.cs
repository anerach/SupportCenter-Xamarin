using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SupportCenter.Domain.Models;

namespace SupportCenter.DAL
{
    public class RESTRepository
    {
        private const string BaseUrl = "http://dappersupportcenter.azurewebsites.net/api/";

        public IEnumerable<Ticket> GetTickets()
        {
            var uri = BaseUrl + "Ticket/All";

            IEnumerable<Ticket> returnTickets;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(uri).Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = response.Content.ReadAsStringAsync().Result;

                returnTickets = JsonConvert.DeserializeObject<List<Ticket>>(content);
            }

            return returnTickets;
        }

        public Ticket CreateTicket(int accountId, string problem)
        {
            var uri = BaseUrl + "Ticket";

            Ticket returnTicket;

            using (var client = new HttpClient())
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, uri);

                httpRequest.Headers.Add("Accept", "application/json");

                object data = new
                {
                    AccountId = accountId,
                    Text = problem
                };

                var dataAsJsonString = JsonConvert.SerializeObject(data);

                httpRequest.Content = new StringContent(dataAsJsonString, Encoding.UTF8, "application/json");

                var httpResponse = client.SendAsync(httpRequest).Result;

                if (!httpResponse.IsSuccessStatusCode)
                    return null;

                var responseContentAsString = httpResponse.Content.ReadAsStringAsync().Result;
                returnTicket = JsonConvert.DeserializeObject<Ticket>(responseContentAsString);
            }

            return returnTicket;
        }

        public bool CloseTicket(int ticketNumber)
        {
            var uri = BaseUrl + "Ticket/" + ticketNumber + "/State/Closed";

            bool success;

            using (var client = new HttpClient())
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Put, uri);
                var httpResponse = client.SendAsync(httpRequest).Result;

                success = httpResponse.IsSuccessStatusCode;
            }

            return success;
        }
        
        public void DeleteTicket(int ticketNumber)
        {
            var url = BaseUrl + "Ticket/" + ticketNumber;

            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);

                HttpResponseMessage response = client.SendAsync(request).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
        }

        public TicketResponse CreateTicketResponse(TicketResponse response)
        {
            var uri = BaseUrl + "TicketResponse";

            TicketResponse returnResponse;

            using (var client = new HttpClient())
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, uri);

                object data = new
                {
                    TicketNumber = response.Ticket.TicketNumber,
                    ResponseText = response.Text,
                    IsClientResponse = response.IsClientResponse
                };

                var dataAsJsonString = JsonConvert.SerializeObject(data);

                httpRequest.Content = new StringContent(dataAsJsonString, Encoding.UTF8, "application/json");
                httpRequest.Headers.Add("Accept", "application/json");

                var httpResponse = client.SendAsync(httpRequest).Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseContentAsString = httpResponse.Content.ReadAsStringAsync().Result;
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
