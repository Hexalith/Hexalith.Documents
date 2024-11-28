namespace Hexalith.Documents.UI.Pages.DocumentTypes.Validations;

using FluentValidation;

using Hexalith.Documents.UI.Pages.DocumentTypes.ViewModels;

using Microsoft.Extensions.Localization;

using Labels = Hexalith.Documents.UI.Pages.Resources.DocumentTypes.DocumentTypeAddValidation;

/// <summary>
/// Validator for adding a new document type.
/// </summary>
public class DocumentTypeAddValidation : AbstractValidator<DocumentTypeAddViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentTypeAddValidation"/> class.
    /// </summary>
    /// <param name="l">The localizer for retrieving localized validation messages.</param>
    public DocumentTypeAddValidation(IStringLocalizer<DocumentTypeAddValidation> l)
    {
        ArgumentNullException.ThrowIfNull(l, nameof(l));
        _ = RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(l[Labels.NameRequired])
            .MaximumLength(512)
            .WithMessage(l[Labels.MaxNameLengthExceeded, 512]);
        _ = RuleFor(x => x.Description)
            .MaximumLength(2048)
            .WithMessage(l[Labels.MaxDescriptionLengthExceeded, 2048]);
    }
}