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
        private TicketView Ticket { get; set; }
        private int ticketNumber;

        public TicketDetails(int ticketNumber)
        {
            InitializeComponent();

            manager = new TicketManager();

            this.ticketNumber = ticketNumber;
        }

        private async void TicketDetails_OnAppearing(object sender, EventArgs e)
        {
            try
            {
                Ticket = await Task.Run(() => manager.GetTicketView(ticketNumber));

                BindingContext = Ticket;

                if (Ticket.Ticket.State != TicketState.Closed)
                    return;

                btnClose.IsVisible = false;
                btnClose.IsEnabled = false;

                btnAnswer.IsVisible = false;
                btnAnswer.IsEnabled = false;
            }
            catch (AggregateException)
            {
                await DisplayAlert("Error", "No internet connection", "Ok");
            }
        }

        private async void btnClose_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (manager.CloseTicket(Ticket.Ticket))
                {
                    await DisplayAlert("You did good!", "The Ticket has been succesfully closed!", "OK");
                    await Navigation.PopAsync(true);
                }
                else
                    await DisplayAlert("Oh no!", "Something went wrong... Please try again in a minute...", "OK");
            }
            catch (AggregateException)
            {
                await DisplayAlert("Error", "No internet connection", "Ok");
            }
        }

        private async void btnAnswer_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TicketResponseCreate(Ticket));
        }

        private void ListViewResponses_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListViewResponses.SelectedItem = null;
        }
    }
}
