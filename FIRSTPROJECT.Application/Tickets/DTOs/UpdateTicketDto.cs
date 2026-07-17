namespace FIRSTPROJECT.Application.Tickets.DTOs;

public class UpdateTicketDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketPriority Priority { get; set; }
    public TicketStatus Status { get; set; }
    public Guid CategoryId { get; set; }
    public string? AssignedToUserId { get; set; }
}