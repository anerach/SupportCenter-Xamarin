namespace SupportCenter.Domain.Models
{
  public enum TicketState : byte
  {
    Open = 1,
    Answered,
    ClientAnswer,
    Closed
  }
}
