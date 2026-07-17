using FIRSTPROJECT.Application.Common;
using FIRSTPROJECT.Application.Common.Interfaces;
using FIRSTPROJECT.Application.Tickets.DTOs;
using FIRSTPROJECT.Application.Tickets.Interfaces;
using FIRSTPROJECT.Domain.Constants;
using System.Net;

namespace FIRSTPROJECT.Application.Tickets.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ICategoryRepository _categoryRepository;

    public TicketService(ITicketRepository ticketRepository, ICategoryRepository categoryRepository)
    {
        _ticketRepository = ticketRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IReadOnlyList<TicketDto>> GetAllAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();
        return tickets.Select(MapToDto).ToList();
    }

    public async Task<TicketDto?> GetByIdAsync(Guid id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
        return ticket is null ? null : MapToDto(ticket);
    }

    public async Task<ServiceResult<TicketDto>> CreateAsync(CreateTicketDto dto, string createdByUserId)
    {
        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

        if (category is null)
        {
            return ServiceResult<TicketDto>.Fail(HttpStatusCode.BadRequest, ResponseMessages.CategoryNotFound);
        }

        var ticket = new Ticket
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            Status = TicketStatus.Open,
            CategoryId = dto.CategoryId,
            CreatedByUserId = createdByUserId,
            CreatedAt = DateTime.UtcNow
        };

        await _ticketRepository.AddAsync(ticket);

        return ServiceResult<TicketDto>.Ok(MapToDto(ticket));
    }

    public async Task<ServiceResult> UpdateAsync(Guid id, UpdateTicketDto dto)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            return ServiceResult.Fail(HttpStatusCode.NotFound, ResponseMessages.TicketNotFound);
        }

        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

        if (category is null)
        {
            return ServiceResult.Fail(HttpStatusCode.BadRequest, ResponseMessages.CategoryNotFound);
        }

        if (dto.Status == TicketStatus.Resolved && ticket.Status != TicketStatus.Resolved)
        {
            ticket.ResolvedAt = DateTime.UtcNow;
        }

        if (dto.Status == TicketStatus.Closed && ticket.Status != TicketStatus.Closed)
        {
            ticket.ClosedAt = DateTime.UtcNow;
        }

        ticket.Title = dto.Title;
        ticket.Description = dto.Description;
        ticket.Priority = dto.Priority;
        ticket.Status = dto.Status;
        ticket.CategoryId = dto.CategoryId;
        ticket.AssignedToUserId = dto.AssignedToUserId;
        ticket.UpdatedAt = DateTime.UtcNow;

        await _ticketRepository.UpdateAsync(ticket);

        return ServiceResult.Ok(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            return ServiceResult.Fail(HttpStatusCode.NotFound, ResponseMessages.TicketNotFound);
        }

        await _ticketRepository.DeleteAsync(ticket);

        return ServiceResult.Ok(HttpStatusCode.NoContent);
    }

    private static TicketDto MapToDto(Ticket ticket) => new()
    {
        Id = ticket.Id,
        Title = ticket.Title,
        Description = ticket.Description,
        Status = ticket.Status,
        Priority = ticket.Priority,
        CategoryId = ticket.CategoryId,
        CreatedByUserId = ticket.CreatedByUserId,
        AssignedToUserId = ticket.AssignedToUserId,
        CreatedAt = ticket.CreatedAt,
        UpdatedAt = ticket.UpdatedAt,
        ResolvedAt = ticket.ResolvedAt,
        ClosedAt = ticket.ClosedAt
    };
}