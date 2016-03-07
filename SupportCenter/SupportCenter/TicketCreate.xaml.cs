using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SupportCenter.DAL;
using SupportCenter.Domain.Models;
using Xamarin.Forms;

namespace SupportCenter
{
    public partial class TicketCreate : ContentPage
    {
        private readonly RESTRepository repo;

        public TicketCreate()
        {
            InitializeComponent();

            repo = new RESTRepository();
        }
        
        private async void BtCreate_OnClicked(object sender, EventArgs e)
        {
            var accId = Convert.ToInt32(AccountId.Text);
            var problem = Problem.Text;

            var ticket = repo.CreateTicket(accId, problem);

            if (ticket != null)
            {
                DisplayAlert("Ticket created!", ticket.Text, "OK");
            }
            else
            {
                DisplayAlert("Oy Vey!", "Something went wrong...\nTry again in a minute please...", "OK");
            }

        }
    }
}
