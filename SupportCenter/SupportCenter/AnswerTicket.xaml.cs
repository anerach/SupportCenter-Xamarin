using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportCenter.DAL;
using SupportCenter.Domain.Models;
using SupportCenter.Domain.Views;
using Xamarin.Forms;

namespace SupportCenter
{
    public partial class AnswerTicket : ContentPage
    {
        private RESTRepository repo = new RESTRepository();

        private TicketView Ticket;

        public AnswerTicket(TicketView ticket)
        {
            InitializeComponent();
            Ticket = ticket;
        }


        private void Button_OnClicked(object sender, EventArgs e)
        {
            TicketResponse tr = new TicketResponse()
            {
                IsClientResponse= Client.IsToggled,
                Text = Response.Text,
                Ticket = new Ticket() { TicketNumber = Ticket.TicketNumberInt}
            };

            TicketResponse returned = repo.CreateTicketResponse(tr);
            Navigation.PopAsync(true);
        }
    }
}
