using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.BL.Domain;
using Xamarin.Forms;

namespace SC.BL.Domain.Views
{
    public class TicketView
    {
        public Ticket Ticket { get; set; }

        public string TicketNumber => $"#{Ticket.TicketNumber}";
        public string AccountId => $"Account ID: {Ticket.AccountId}";
        public string Text => Ticket.Text;
        public string DateOpened => Ticket.DateOpened.ToString("hh:mm dd/MM/yy");
        public string State => Ticket.State.ToString();

        public Color StateColor {
            get
            {
                switch (Ticket.State)
                {
                    case TicketState.Open:
                        return Color.Green;
                    case TicketState.Closed:
                        return Color.Red;
                    default:
                        return Color.FromRgb(255,153,0);
                }
            }
        }

        public List<TicketResponseView> Responses => (Ticket.Responses[0] == null) ? null : Ticket.Responses.Select(response => new TicketResponseView(response)).ToList();

        public TicketView(Ticket ticket)
        {
            Ticket = ticket;
        }
    }
}
