namespace Hexalith.Documents.UI.Pages.FileTypes;

using System.Globalization;
using System.Text;

using FluentValidation;

using Labels = Hexalith.Documents.UI.Pages.Resources.FileTypes;

/// <summary>
/// Validator for adding a new document type.
/// </summary>
public class FileTypeEditValidation : AbstractValidator<FileTypeEditViewModel>
{
    private static readonly CompositeFormat _maxDescriptionLengthFormat = CompositeFormat.Parse(Labels.MaxDescriptionLengthExceeded);
    private static readonly CompositeFormat _maxIdLengthFormat = CompositeFormat.Parse(Labels.MaxIdLengthExceeded);
    private static readonly CompositeFormat _maxNameLengthFormat = CompositeFormat.Parse(Labels.MaxNameLengthExceeded);

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
            .WithMessage(string.Format(CultureInfo.InvariantCulture, _maxIdLengthFormat, 32));

        _ = RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(Labels.NameRequired)
            .MaximumLength(512)
            .WithMessage(string.Format(CultureInfo.InvariantCulture, _maxNameLengthFormat, 512));

        _ = RuleFor(x => x.Description)
            .MaximumLength(2048)
            .WithMessage(string.Format(CultureInfo.InvariantCulture, _maxDescriptionLengthFormat, 2048));
    }
}