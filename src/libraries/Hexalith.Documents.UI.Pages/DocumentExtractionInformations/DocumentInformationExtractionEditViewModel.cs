namespace Hexalith.Documents.UI.Pages.DocumentExtractionInformations;

using Hexalith.Documents.Requests.DocumentInformationExtractions;
using Hexalith.Domain.ValueObjects;
using Hexalith.Extensions.Helpers;

/// <summary>
/// ViewModel for editing document information extraction details.
/// </summary>
public sealed class DocumentInformationExtractionEditViewModel : IIdDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentInformationExtractionEditViewModel"/> class.
    /// </summary>
    /// <param name="details">The details of the document information extraction.</param>
    /// <exception cref="ArgumentNullException">Thrown when details is null.</exception>
    public DocumentInformationExtractionEditViewModel(DocumentInformationExtractionDetailsViewModel details)
    {
        ArgumentNullException.ThrowIfNull(details);
        Id = details.Id;
        Name = details.Name;
        Comments = details.Comments;
        Model = details.Model;
        Instructions = details.Instructions;
        ValidationModel = details.ValidationModel;
        ValidationInstructions = details.ValidationInstructions;
        OutputFormat = details.OutputFormat;
        OutputSample = details.OutputSample;
        Disabled = details.Disabled;
        Original = details;
        SystemMessage = details.SystemMessage;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentInformationExtractionEditViewModel"/> class.
    /// </summary>
    public DocumentInformationExtractionEditViewModel()
    : this(new DocumentInformationExtractionDetailsViewModel(
    UniqueIdHelper.GenerateUniqueStringId(),
    string.Empty,
    string.Empty,
    string.Empty,
    string.Empty,
    string.Empty,
    string.Empty,
    string.Empty,
    string.Empty,
    null,
    false))
    {
    }

    /// <summary>
    /// Gets or sets the comments.
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Gets a value indicating whether the description has changed.
    /// </summary>
    public bool DescriptionChanged => Comments != Original.Comments || Name != Original.Name;

    /// <summary>
    /// Gets or sets a value indicating whether the item is disabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets a value indicating whether there are any changes.
    /// </summary>
    public bool HasChanges =>
        Id != Original.Id ||
        DescriptionChanged ||
        InstructionsChanged ||
        ValidationInstructionsChanged ||
        OutpuChanged ||
        Disabled != Original.Disabled;

    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the instructions.
    /// </summary>
    public string Instructions { get; set; }

    /// <summary>
    /// Gets a value indicating whether the instructions have changed.
    /// </summary>
    public bool InstructionsChanged => Model != Original.Model || Instructions != Original.Instructions;

    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the original details.
    /// </summary>
    public DocumentInformationExtractionDetailsViewModel Original { get; }

    /// <summary>
    /// Gets a value indicating whether the output has changed.
    /// </summary>
    public bool OutpuChanged => OutputFormat != Original.OutputFormat || OutputSample != Original.OutputSample;

    /// <summary>
    /// Gets or sets the output format.
    /// </summary>
    public string OutputFormat { get; set; }

    /// <summary>
    /// Gets or sets the output sample.
    /// </summary>
    public string OutputSample { get; set; }

    /// <summary>
    /// Gets or sets the system message.
    /// </summary>
    public string SystemMessage { get; set; }

    /// <summary>
    /// Gets or sets the validation instructions.
    /// </summary>
    public string ValidationInstructions { get; set; }

    /// <summary>
    /// Gets a value indicating whether the validation instructions have changed.
    /// </summary>
    public bool ValidationInstructionsChanged => ValidationModel != Original.ValidationModel || ValidationInstructions != Original.ValidationInstructions;

    /// <summary>
    /// Gets or sets the validation model.
    /// </summary>
    public string ValidationModel { get; set; }

    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}