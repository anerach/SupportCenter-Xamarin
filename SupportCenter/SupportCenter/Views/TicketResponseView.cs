using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportCenter.Domain.Models;

namespace SupportCenter.Domain.Views
{
    public class TicketResponseView
    {
        private TicketResponse TicketResponse { get; }

        public string Id => $"#{TicketResponse.Id}";
        public string Text => TicketResponse.Text;
        public string Date => TicketResponse.Date.ToString("hh:mm dd/MM/yy");
        public bool IsClientResponse => TicketResponse.IsClientResponse;

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
