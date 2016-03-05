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
        RESTRepository rep = new RESTRepository();
        public TicketCreate()
        {
            InitializeComponent();
        }


        private async void BtCreate_OnClicked(object sender, EventArgs e)
        {
            int accId = Convert.ToInt32(AccountId.Text);
            string problem = Problem.Text;
            Ticket createdTicket = rep.CreateTicket(accId, problem);
            if (createdTicket != null)
            {
                DisplayAlert("Ticket created!", createdTicket.Text, "OK");
            }
            else
            {
                DisplayAlert("Oy Vey!", "Something went wrong...\nTry again in a minute please...", "OK");
            }

        }
    }
}
