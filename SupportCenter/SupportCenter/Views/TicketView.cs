﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportCenter.Domain.Models;

namespace SupportCenter.Domain.Views
{
    public class TicketView
    {
        private Ticket Ticket { get; set; }

        public string TicketNumber => $"#{Ticket.TicketNumber}";
        public string AccountId => Ticket.AccountId.ToString();
        public string Text => Ticket.Text;
        public string DateOpened => Ticket.DateOpened.ToString("hh:mm dd/MM/yy");
        public string State => Ticket.State.ToString();

        public ICollection<TicketResponseView> Responses => Ticket.Responses.Select(response => new TicketResponseView(response)).ToList();

        public TicketView(Ticket ticket)
        {
            Ticket = ticket;
        }
    }
}
