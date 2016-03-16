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
    public partial class TicketResponseCreate : ContentPage
    {
        private readonly TicketManager manager;
        private readonly TicketView ticket;

        public TicketResponseCreate(TicketView ticket)
        {
            InitializeComponent();

            this.ticket = ticket;
            manager = new TicketManager();
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var ticketResponse = new TicketResponse()
                {
                    IsClientResponse = tglClient.IsToggled,
                    Text = txtResponse.Text,
                    Ticket = ticket.Ticket
                };

                ticketResponse = await Task.Run(() => manager.CreateTicketResponse(ticketResponse));

                if (ticketResponse == null)
                {
                    await DisplayAlert("Error", "Make sure every field is filled in", "Cancel");
                    return;
                }

                await Navigation.PopAsync(true);
            }
            catch (AggregateException)
            {
                await DisplayAlert("Error", "No internet connection", "Ok");
            }
        }
    }
}
