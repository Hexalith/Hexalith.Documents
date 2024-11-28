namespace Hexalith.Documents.Domain.FileTypes;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Document.Domain;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Domain.Aggregates;
using Hexalith.Domain.Events;

/// <summary>
/// Represents a file type in the document management system.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="Description">The description of the file type.</param>
/// <param name="TextExtractionModeId">The identifier of the text extraction mode to be used for this file type.</param>
/// <param name="SummarizationEnabled">Indicates whether text summarization is enabled for this file type.</param>
/// <param name="SummarizationInstructions">Optional instructions for text summarization.</param>
/// <param name="Targets">Collection of target identifiers associated with this file type.</param>
/// <param name="Disabled">Indicates whether this file type is disabled.</param>
[DataContract]
public record FileType(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string Description,
    [property: DataMember(Order = 4)] string? TextExtractionModeId,
    [property: DataMember(Order = 5)] bool SummarizationEnabled,
    [property: DataMember(Order = 6)] string? SummarizationInstructions,
    [property: DataMember(Order = 7)] IEnumerable<string> Targets,
    [property: DataMember(Order = 8)] bool Disabled) : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileType"/> class.
    /// </summary>
    public FileType()
        : this(
              string.Empty,
              string.Empty,
              string.Empty,
              null,
              false,
              null,
              [],
              false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileType"/> class based on the <see cref="FileTypeAdded"/> event.
    /// </summary>
    /// <param name="added">The <see cref="FileTypeAdded"/> event containing the initialization data.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="added"/> is null.</exception>
    public FileType(FileTypeAdded added)
        : this(
              (added ?? throw new ArgumentNullException(nameof(added))).Id,
              added.Name,
              added.Description,
              added.TextExtractionModeId,
              added.SummarizationEnabled,
              added.SummarizationInstructions,
              added.Targets,
              false)
    {
    }

    /// <inheritdoc/>
    public string AggregateId => Id;

    /// <inheritdoc/>
    public string AggregateName => DocumentDomainHelper.FileTypeAggregateName;

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvent"/> is null.</exception>
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        if (domainEvent is FileTypeEvent ev && domainEvent is not FileTypeEnabled && Disabled)
        {
            return new ApplyResult(
                this,
                [new FileTypeEventCancelled(ev, "File type is disabled.")],
                true);
        }

        return domainEvent switch
        {
            FileTypeTargetAdded e => ApplyEvent(e),
            FileTypeTargetRemoved e => ApplyEvent(e),
            FileTypeAdded e => ApplyEvent(e),
            FileTypeDescriptionChanged e => ApplyEvent(e),
            FileTypeDisabled e => ApplyEvent(e),
            FileTypeEnabled e => ApplyEvent(e),
            FileTypeTextSummarizationDisabled e => ApplyEvent(e),
            FileTypeTextSummarizationEnabled e => ApplyEvent(e),
            FileTypeTextExtractionModeChanged e => ApplyEvent(e),
            FileTypeEvent e => new ApplyResult(
                this,
                [new FileTypeEventCancelled(e, "Event not implemented")],
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
    /// Applies a FileTypeCreated event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeCreated event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeAdded e) => !IsInitialized()
        ? new ApplyResult(
            new FileType(e),
            [e],
            false)
        : new ApplyResult(this, [new FileTypeEventCancelled(e, "The document already exists.")], true);

    /// <summary>
    /// Applies a FileTypeEnabled event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeEnabled event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeEnabled e) => Disabled
            ? new ApplyResult(
            this with { Disabled = false },
            [e],
            false)
            : new ApplyResult(this, [new FileTypeEventCancelled(e, "The document is already enabled.")], true);

    /// <summary>
    /// Applies a FileTypeDisabled event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeDisabled event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeDisabled e) => !Disabled
            ? new ApplyResult(
            this with { Disabled = true },
            [e],
            false)
            : new ApplyResult(this, [new FileTypeEventCancelled(e, "The document is already disabled.")], true);

    /// <summary>
    /// Applies a FileTypeTextSummarizationEnabled event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeTextSummarizationEnabled event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeTextSummarizationEnabled e) => !SummarizationEnabled || SummarizationInstructions != e.SummarizationInstructions
            ? new ApplyResult(
            this with { SummarizationEnabled = true, SummarizationInstructions = e.SummarizationInstructions },
            [e],
            false)
            : new ApplyResult(this, [new FileTypeEventCancelled(e, "The document is already enabled.")], true);

    /// <summary>
    /// Applies a FileTypeTextSummarizationDisabled event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeTextSummarizationDisabled event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeTextSummarizationDisabled e) => SummarizationEnabled || SummarizationInstructions != null
            ? new ApplyResult(
            this with { SummarizationEnabled = false, SummarizationInstructions = null },
            [e],
            false)
            : new ApplyResult(this, [new FileTypeEventCancelled(e, "The document is already disabled.")], true);

    /// <summary>
    /// Applies a FileTypeTextExtractionModeChanged event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeTextExtractionModeChanged event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeTextExtractionModeChanged e) => e.TextExtractionModeId != TextExtractionModeId
        ? new ApplyResult(
            this with { TextExtractionModeId = e.TextExtractionModeId },
            [e],
            false)
        : new ApplyResult(this, [], false);

    /// <summary>
    /// Applies a FileTypeDescriptionChanged event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeDescriptionChanged event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeDescriptionChanged e) => e.Name != Name || e.Description != Description
        ? new ApplyResult(
            this with { Name = e.Name, Description = e.Description },
            [e],
            false)
        : new ApplyResult(this, [], false);

    /// <summary>
    /// Applies a FileTypeTargetAdded event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeTargetAdded event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeTargetAdded e)
    {
        List<string> currentTargets = Targets.ToList();
        return !currentTargets.Contains(e.Target)
            ? new ApplyResult(
                this with { Targets = currentTargets.Concat([e.Target]) },
                [e],
                false)
            : new ApplyResult(this, [new FileTypeEventCancelled(e, "The target is already added.")], true);
    }

    /// <summary>
    /// Applies a FileTypeTargetRemoved event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeTargetRemoved event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeTargetRemoved e)
    {
        List<string> currentTargets = Targets.ToList();
        return currentTargets.Contains(e.Target)
            ? new ApplyResult(
                this with { Targets = currentTargets.Where(t => t != e.Target) },
                [e],
                false)
            : new ApplyResult(this, [new FileTypeEventCancelled(e, "The target is not present.")], true);
    }
}
