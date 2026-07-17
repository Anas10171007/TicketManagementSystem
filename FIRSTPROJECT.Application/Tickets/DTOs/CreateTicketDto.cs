namespace FIRSTPROJECT.Application.Tickets.DTOs;

public class CreateTicketDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketPriority Priority { get; set; } = TicketPriority.Low;
    public Guid CategoryId { get; set; }
}