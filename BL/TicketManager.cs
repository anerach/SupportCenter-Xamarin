using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.BL.Domain;
using SC.BL.Domain.Views;
using SC.DAL;

namespace SC.BL
{
    public class TicketManager
    {
        private readonly RESTProxy repo;

        public TicketManager()
        {
            repo = new RESTProxy();
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return repo.GetTickets();
        }

        public IEnumerable<TicketView> GetTicketViews()
        {
            return repo.GetTickets().Select(ticket => new TicketView(ticket));
        } 

        public Ticket GetTicket(int ticketNumber)
        {
            return repo.GetTicket(ticketNumber);
        }

        public TicketView GetTicketView(int ticketNumber)
        {
            return new TicketView(GetTicket(ticketNumber));
        }

        public Ticket CreateTicket(Ticket ticket)
        {
            return repo.CreateTicket(ticket.AccountId, ticket.Text);
        }

        public bool CloseTicket(Ticket ticket)
        {
            var success = repo.CloseTicket(ticket.TicketNumber);

            if (success)
                ticket.State = TicketState.Closed;

            return success;
        }

        public void DeleteTicket(Ticket ticket)
        {
            repo.DeleteTicket(ticket.TicketNumber);
        }

        public TicketResponse CreateTicketResponse(TicketResponse response)
        {
            if (response.Text == string.Empty)
                return null;

            return repo.CreateTicketResponse(response);
        }
    }
}
