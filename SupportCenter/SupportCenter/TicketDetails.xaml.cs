using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportCenter.DAL;
using SupportCenter.Domain.Views;
using SupportCenter.Domain.Models;
using Xamarin.Forms;

namespace SupportCenter
{
    public partial class TicketDetails : ContentPage
    {
        RESTRepository rep = new RESTRepository();

        private TicketView currentTicket;

        public TicketDetails(TicketView ticket)
        {
            InitializeComponent();
            currentTicket = ticket;
            this.BindingContext = ticket;
        }

        private void Close_OnClicked(object sender, EventArgs e)
        {
            if (rep.CloseTicket(currentTicket.TicketNumberInt))
            {
                DisplayAlert("You did good!", "The ticket has been succesfully closed!", "OK");
            }
            else
            {
                DisplayAlert("Oh no!", "Something went wrong... Please try again in a minute...", "OK");
            }
        }

        private void Answer_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AnswerTicket(currentTicket));
        }
    }
}
