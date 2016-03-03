using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportCenter.Domain.Views;
using Xamarin.Forms;

namespace SupportCenter
{
    public partial class TicketDetails : ContentPage
    {
        public TicketDetails(TicketView ticket)
        {
            InitializeComponent();

            this.BindingContext = ticket;
        }
    }
}
