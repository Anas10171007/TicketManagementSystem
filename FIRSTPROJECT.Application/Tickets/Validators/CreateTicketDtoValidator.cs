using FIRSTPROJECT.Application.Tickets.DTOs;
using FIRSTPROJECT.Domain.Constants;
using FluentValidation;

namespace FIRSTPROJECT.Application.Tickets.Validators;

public class CreateTicketDtoValidator : AbstractValidator<CreateTicketDto>
{
    public CreateTicketDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(DomainConstants.MaxTicketTitleLength);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(DomainConstants.MaxTicketDescriptionLength);

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Invalid priority value.");
    }
}