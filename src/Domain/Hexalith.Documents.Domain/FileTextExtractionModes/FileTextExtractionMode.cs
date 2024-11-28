namespace Hexalith.Documents.Domain.FileTextExtractionModes;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.Documents.Events.FileTextExtractionModes;
using Hexalith.Domain.Aggregates;
using Hexalith.Domain.Events;

/// <summary>
/// Represents a file text extraction mode configuration that defines how text should be extracted from files.
/// </summary>
/// <param name="Id">The unique identifier for the extraction mode.</param>
/// <param name="Name">The display name of the extraction mode.</param>
/// <param name="ExtractionInstructions">The instructions defining how text should be extracted.</param>
/// <param name="Description">Optional description providing additional details about the extraction mode.</param>
/// <param name="Disabled">Flag indicating whether this extraction mode is currently disabled.</param>
[DataContract]
public record FileTextExtractionMode(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string ExtractionInstructions,
    [property: DataMember(Order = 4)] string? Description,
    [property: DataMember(Order = 5)] bool Disabled) : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileTextExtractionMode"/> class with default values.
    /// </summary>
    public FileTextExtractionMode()
        : this(
              string.Empty,
              string.Empty,
              string.Empty,
              null,
              false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileTextExtractionMode"/> class from a creation event.
    /// </summary>
    /// <param name="added">The event containing the initial state of the extraction mode.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="added"/> is null.</exception>
    public FileTextExtractionMode(FileTextExtractionModeCreated added)
        : this(
              (added ?? throw new ArgumentNullException(nameof(added))).Id,
              added.Name,
              added.ExtractionInstructions,
              added.Description,
              false)
    {
    }

    /// <inheritdoc/>
    public string AggregateId => Id;

    /// <inheritdoc/>
    public string AggregateName => DocumentDomainHelper.FileTextExtractionModeAggregateName;

    /// <inheritdoc/>
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        if (domainEvent is FileTextExtractionModeEvent ev && domainEvent is not FileTextExtractionModeEnabled && Disabled)
        {
            return new ApplyResult(
                this,
                [new FileTextExtractionModeEventCancelled(ev, "File text extraction mode is disabled.")],
                true);
        }

        return domainEvent switch
        {
            FileTextExtractionModeCreated e => ApplyEvent(e),
            FileTextExtractionModeDescriptionChanged e => ApplyEvent(e),
            FileTextExtractionModeDisabled e => ApplyEvent(e),
            FileTextExtractionModeEnabled e => ApplyEvent(e),
            FileTextExtractionInstructionsChanged e => ApplyEvent(e),
            FileTextExtractionModeEvent e => new ApplyResult(
                this,
                [new FileTextExtractionModeEventCancelled(e, "Event not implemented")],
                true),
            _ => new ApplyResult(
                this,
                [InvalidEventApplied.CreateNotSupportedAppliedEvent(
                    AggregateName,
                    AggregateId,
                    domainEvent)],
                true),
        };
    }

    /// <inheritdoc/>
    public bool IsInitialized() => !string.IsNullOrWhiteSpace(Id);

    /// <summary>
    /// Applies a creation event to the extraction mode.
    /// </summary>
    /// <param name="e">The creation event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTextExtractionModeCreated e) => !IsInitialized()
        ? new ApplyResult(
            new FileTextExtractionMode(e),
            [e],
            false)
        : new ApplyResult(this, [new FileTextExtractionModeEventCancelled(e, "The text extraction mode already exists.")], true);

    /// <summary>
    /// Applies an enable event to the extraction mode.
    /// </summary>
    /// <param name="e">The enable event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTextExtractionModeEnabled e) => Disabled
            ? new ApplyResult(
            this with { Disabled = false },
            [e],
            false)
            : new ApplyResult(this, [new FileTextExtractionModeEventCancelled(e, "The text extraction mode is already enabled.")], true);

    /// <summary>
    /// Applies a disable event to the extraction mode.
    /// </summary>
    /// <param name="e">The disable event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTextExtractionModeDisabled e) => !Disabled
            ? new ApplyResult(
            this with { Disabled = true },
            [e],
            false)
            : new ApplyResult(this, [new FileTextExtractionModeEventCancelled(e, "The text extraction mode is already disabled.")], true);

    /// <summary>
    /// Applies an instructions change event to the extraction mode.
    /// </summary>
    /// <param name="e">The instructions change event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTextExtractionInstructionsChanged e) => e.ExtractionInstructions != ExtractionInstructions
        ? new ApplyResult(
            this with { ExtractionInstructions = e.ExtractionInstructions },
            [e],
            false)
        : new ApplyResult(this, [], false);

    /// <summary>
    /// Applies a description change event to the extraction mode.
    /// </summary>
    /// <param name="e">The description change event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTextExtractionModeDescriptionChanged e) => e.Name != Name || e.Description != Description
        ? new ApplyResult(
            this with { Name = e.Name, Description = e.Description },
            [e],
            false)
        : new ApplyResult(this, [], false);
}
