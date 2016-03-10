using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.BL.Domain;
using Xamarin.Forms;

namespace SC.BL.Domain.Views
{
    public class TicketResponseView
    {
        public TicketResponse TicketResponse { get; }

        public string Id => $"#{TicketResponse.Id}";
        public string Text => TicketResponse.Text;
        public string Date => TicketResponse.Date.ToString("hh:mm dd/MM/yy");
        public string IsClientResponse => TicketResponse.IsClientResponse ? "Client" : "Staff";

        public Color IsClientResponseColor => TicketResponse.IsClientResponse ? Color.Fuchsia : Color.Red;

        public TicketResponseView(TicketResponse ticketResponse)
        {
            TicketResponse = ticketResponse;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
