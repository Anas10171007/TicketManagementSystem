using FIRSTPROJECT.Application.Common;
using FIRSTPROJECT.Application.Tickets.DTOs;

namespace FIRSTPROJECT.Application.Tickets.Interfaces;

public interface ITicketService
{
    Task<IReadOnlyList<TicketDto>> GetAllAsync();
    Task<TicketDto?> GetByIdAsync(Guid id);
    Task<ServiceResult<TicketDto>> CreateAsync(CreateTicketDto dto, string createdByUserId);
    Task<ServiceResult> UpdateAsync(Guid id, UpdateTicketDto dto);
    Task<ServiceResult> DeleteAsync(Guid id);
}