namespace Hexalith.Documents.UI.Pages.Documents.Validations;

using Labels = Hexalith.Documents.UI.Pages.Resources.Documents.Pages.DcumentAdd;

public class DocumentAddValidation : AbstractValidator<ViewModels.DocumentAdd>
{
    private static readonly string MaxNameLengthExceededMessage = string.Format(CultureInfo.CurrentCulture, Labels.MaxNameLengthExceeded, 512);
    private static readonly string MaxDescriptionLengthExceededMessage = string.Format(CultureInfo.CurrentCulture, Labels.MaxDescriptionLengthExceeded, 2048);

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentAddValidation"/> class.
    /// </summary>
    public DocumentAddValidation()
    {
        _ = RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(Labels.NameRequired)
            .MaximumLength(512)
            .WithMessage(MaxNameLengthExceededMessage);
        _ = RuleFor(x => x.Description)
            .MaximumLength(2048)
            .WithMessage(MaxDescriptionLengthExceededMessage);
    }
}