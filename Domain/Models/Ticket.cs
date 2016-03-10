using System;
using System.Collections.Generic;

namespace SC.BL.Domain
{
    public class Ticket
    {
        public int TicketNumber { get; set; }
        public int AccountId { get; set; }
        public string Text { get; set; }
        public DateTime DateOpened { get; set; }
        public TicketState State { get; set; }

        public List<TicketResponse> Responses { get; set; }

        public override string ToString()
        {
            return $"#{TicketNumber} - {Text}";
        }
    }
}