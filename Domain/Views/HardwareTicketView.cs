using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.BL.Domain;

namespace SC.BL.Domain.Views
{
    public class HardwareTicketView : TicketView
    {
        public string DeviceName => ((HardwareTicket)Ticket).DeviceName;

        public HardwareTicketView(HardwareTicket ticket) : base(ticket)
        {

        }
    }
}
