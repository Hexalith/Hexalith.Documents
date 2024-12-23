namespace Hexalith.Documents.UI.Pages.Documents;

using FluentValidation;

using Labels = Hexalith.Documents.UI.Pages.Resources.Documents;

/// <summary>
/// Validator for the DocumentAdd view model.
/// </summary>
public class DocumentEditValidation : AbstractValidator<DocumentEditViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentEditValidation"/> class.
    /// </summary>
    /// <param name="l">The localizer for retrieving localized validation messages.</param>
    public DocumentEditValidation()
    {
        _ = RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(Labels.NameRequired)
            .MaximumLength(512)
            .WithMessage(string.Format(Labels.MaxNameLengthExceeded, 512));
        _ = RuleFor(x => x.Description)
            .MaximumLength(2048)
            .WithMessage(string.Format(Labels.MaxDescriptionLengthExceeded, 2048));
    }
}