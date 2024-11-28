namespace Hexalith.Documents.UI.Pages.Documents.Validations;

using FluentValidation;

using Microsoft.Extensions.Localization;

using Labels = Hexalith.Documents.UI.Pages.Resources.Documents.Pages.DocumentAdd;

/// <summary>
/// Validator for the DocumentAdd view model.
/// </summary>
public class DocumentAddValidation : AbstractValidator<ViewModels.DocumentAddViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentAddValidation"/> class.
    /// </summary>
    /// <param name="l">The localizer for retrieving localized validation messages.</param>
    public DocumentAddValidation(IStringLocalizer<DocumentAdd> l)
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