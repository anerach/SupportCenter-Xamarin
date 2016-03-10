using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.BL;
using SC.BL.Domain;
using SC.BL.Domain.Views;

using Xamarin.Forms;

namespace SupportCenter
{
    public partial class TicketDetails : ContentPage
    {
        private readonly TicketManager manager;
        private TicketView ticket;
        private int ticketNumber;

        public TicketDetails(int ticketNumber)
        {
            InitializeComponent();

            manager = new TicketManager();

            this.ticketNumber = ticketNumber;
        }

        private void TicketDetails_OnAppearing(object sender, EventArgs e)
        {
            ticket = new TicketView(manager.GetTicket(ticketNumber));

            BindingContext = ticket;

            if (ticket.Ticket.State == TicketState.Closed)
            {
                btnClose.IsVisible = false;
                btnClose.IsEnabled = false;

                btnAnswer.IsVisible = false;
                btnAnswer.IsEnabled = false;
            }
        }

        private void btnClose_OnClicked(object sender, EventArgs e)
        {
            if (manager.CloseTicket(ticket.Ticket))
            {
                DisplayAlert("You did good!", "The ticket has been succesfully closed!", "OK");
                Navigation.PopAsync(true);
            }
            else
                DisplayAlert("Oh no!", "Something went wrong... Please try again in a minute...", "OK");
        }

        private void btnAnswer_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TicketResponseCreate(ticket));
        }

        private void ListViewResponses_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListViewResponses.SelectedItem = null;
        }
    }
}
