using FIRSTPROJECT.Domain.Common;

namespace FIRSTPROJECT.Domain.Entities;

public class TicketComment : BaseEntity
{
    public Guid TicketId { get; set; }
    public string AuthorUserId { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;//actual comment content

    public bool IsInternalNote { get; set; }//indicates whether the comment is an internal note or visible to the ticket submitter
    public DateTime CreatedAt { get; set; }
}

