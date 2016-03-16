using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SC.BL;
using SC.BL.Domain;
using Xamarin.Forms;

namespace SupportCenter
{
    public partial class TicketCreate : ContentPage
    {
        private readonly TicketManager manager;

        public TicketCreate()
        {
            InitializeComponent();

            manager = new TicketManager();
        }
        
        private async void BtnCreate_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var ticket = new Ticket()
                {
                    AccountId = Convert.ToInt32(txtAccountId.Text),
                    Text = txtText.Text
                };

                ticket = await Task.Run(() => manager.CreateTicket(ticket));

                if (ticket != null)
                {
                    await DisplayAlert("Ticket created!", ticket.Text, "OK");
                    await Navigation.PopAsync(true);
                }
                else
                    await DisplayAlert("Oy Vey!", "Something went wrong...\nTry again in a minute please...", "OK");
            }
            catch (AggregateException)
            {
                await DisplayAlert("Error", "No internet connection", "Ok");
            }
        }
    }
}
