﻿namespace Hexalith.Documents.Domain.FileTextExtractionModes;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.Documents.Events.DocumentInformationExtractions;
using Hexalith.Domain.Aggregates;
using Hexalith.Domain.Events;

/// <summary>
/// Represents a file text extraction mode configuration that defines how text should be extracted from files.
/// </summary>
/// <param name="Id">The unique identifier for the extraction mode.</param>
/// <param name="Name">The display name of the extraction mode.</param>
/// <param name="Instructions">The instructions defining how text should be extracted.</param>
/// <param name="Description">Optional description providing additional details about the extraction mode.</param>
/// <param name="Disabled">Flag indicating whether this extraction mode is currently disabled.</param>
[DataContract]
public record DocumentInformationExtraction(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string Model,
    [property: DataMember(Order = 3)] string Instructions,
    [property: DataMember(Order = 4)] string? Description,
    [property: DataMember(Order = 5)] bool Disabled) : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentInformationExtraction"/> class with default values.
    /// </summary>
    public DocumentInformationExtraction()
        : this(
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              null,
              false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentInformationExtraction"/> class from a creation event.
    /// </summary>
    /// <param name="added">The event containing the initial state of the extraction mode.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="added"/> is null.</exception>
    public DocumentInformationExtraction(DocumentInformationExtractionAdded added)
        : this(
              (added ?? throw new ArgumentNullException(nameof(added))).Id,
              added.Name,
              added.Model,
              added.Instructions,
              added.Description,
              false)
    {
    }

    /// <inheritdoc/>
    public string AggregateId => Id;

    /// <inheritdoc/>
    public string AggregateName => DocumentDomainHelper.DocumentInformationExtractionAggregateName;

    /// <inheritdoc/>
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        if (domainEvent is DocumentInformationExtractionEvent ev && domainEvent is not DocumentInformationExtractionEnabled && Disabled)
        {
            return new ApplyResult(
                this,
                [new DocumentInformationExtractionEventCancelled(ev, "File text extraction mode is disabled.")],
                true);
        }

        return domainEvent switch
        {
            DocumentInformationExtractionAdded e => ApplyEvent(e),
            DocumentInformationExtractionDescriptionChanged e => ApplyEvent(e),
            DocumentInformationExtractionDisabled e => ApplyEvent(e),
            DocumentInformationExtractionEnabled e => ApplyEvent(e),
            DocumentInformationInstructionsChanged e => ApplyEvent(e),
            DocumentInformationExtractionEvent e => new ApplyResult(
                this,
                [new DocumentInformationExtractionEventCancelled(e, "Event not implemented")],
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
    private ApplyResult ApplyEvent(DocumentInformationExtractionAdded e) => !IsInitialized()
        ? new ApplyResult(
            new DocumentInformationExtraction(e),
            [e],
            false)
        : new ApplyResult(this, [new DocumentInformationExtractionEventCancelled(e, "The text extraction mode already exists.")], true);

    /// <summary>
    /// Applies an enable event to the extraction mode.
    /// </summary>
    /// <param name="e">The enable event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(DocumentInformationExtractionEnabled e) => Disabled
            ? new ApplyResult(
            this with { Disabled = false },
            [e],
            false)
            : new ApplyResult(this, [new DocumentInformationExtractionEventCancelled(e, "The text extraction mode is already enabled.")], true);

    /// <summary>
    /// Applies a disable event to the extraction mode.
    /// </summary>
    /// <param name="e">The disable event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(DocumentInformationExtractionDisabled e) => !Disabled
            ? new ApplyResult(
            this with { Disabled = true },
            [e],
            false)
            : new ApplyResult(this, [new DocumentInformationExtractionEventCancelled(e, "The text extraction mode is already disabled.")], true);

    /// <summary>
    /// Applies an instructions change event to the extraction mode.
    /// </summary>
    /// <param name="e">The instructions change event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(DocumentInformationInstructionsChanged e) => e.Instructions != Instructions
        ? new ApplyResult(
            this with { Instructions = e.Instructions },
            [e],
            false)
        : new ApplyResult(this, [], false);

    /// <summary>
    /// Applies a description change event to the extraction mode.
    /// </summary>
    /// <param name="e">The description change event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(DocumentInformationExtractionDescriptionChanged e) => e.Name != Name || e.Description != Description
        ? new ApplyResult(
            this with { Name = e.Name, Description = e.Description },
            [e],
            false)
        : new ApplyResult(this, [], false);
}