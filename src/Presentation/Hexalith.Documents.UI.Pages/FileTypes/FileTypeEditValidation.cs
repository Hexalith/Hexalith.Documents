namespace Hexalith.Documents.UI.Pages.FileTypes;

using FluentValidation;

using Labels = Hexalith.Documents.UI.Pages.Resources.FileTypes;

/// <summary>
/// Validator for adding a new document type.
/// </summary>
public class FileTypeEditValidation : AbstractValidator<FileTypeEditViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeEditValidation"/> class.
    /// </summary>
    /// <param name="l">The localizer for retrieving localized validation messages.</param>
    public FileTypeEditValidation()
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