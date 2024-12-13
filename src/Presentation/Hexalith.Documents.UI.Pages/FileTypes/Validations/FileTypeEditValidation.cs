namespace Hexalith.Documents.UI.Pages.FileTypes.Validations;

using FluentValidation;

using Hexalith.Documents.UI.Pages.FileTypes.ViewModels;

using Microsoft.Extensions.Localization;

using Labels = Hexalith.Documents.UI.Pages.Resources.FileTypes.FileTypeEditValidation;

/// <summary>
/// Validator for adding a new document type.
/// </summary>
public class FileTypeEditValidation : AbstractValidator<FileTypeEditViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeEditValidation"/> class.
    /// </summary>
    /// <param name="l">The localizer for retrieving localized validation messages.</param>
    public FileTypeEditValidation(IStringLocalizer<FileTypeEditValidation> l)
    {
        ArgumentNullException.ThrowIfNull(l, paramName: nameof(l));
        _ = RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(l[Labels.IdRequired])
            .MaximumLength(32)
            .WithMessage(l[Labels.MaxIdLengthExceeded, 32]);
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