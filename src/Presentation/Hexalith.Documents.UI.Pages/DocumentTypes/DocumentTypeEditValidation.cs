namespace Hexalith.Documents.UI.Pages.DocumentTypes;

using FluentValidation;

using Labels = Hexalith.Documents.UI.Pages.Resources.DocumentTypes;

/// <summary>
/// Validator for adding a new document type.
/// </summary>
public class DocumentTypeEditValidation : AbstractValidator<DocumentTypeEditViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentTypeEditValidation"/> class.
    /// </summary>
    /// <param name="l">The localizer for retrieving localized validation messages.</param>
    public DocumentTypeEditValidation()
    {
        _ = RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(Labels.IdRequired)
            .MaximumLength(32)
            .WithMessage(string.Format(Labels.MaxIdLengthExceeded, 32));
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