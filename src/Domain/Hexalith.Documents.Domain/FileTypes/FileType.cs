namespace Hexalith.Documents.Domain.FileTypes;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Domain.Aggregates;

/// <summary>
/// Represents a file type in the document management system.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="Comments">The description of the file type.</param>
/// <param name="OtherContentTypes">Collection of target identifiers associated with this file type.</param>
/// <param name="Disabled">Indicates whether this file type is disabled.</param>
[DataContract]
public record FileType(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 5)] string ContentType,
    [property: DataMember(Order = 6)] IEnumerable<string> OtherContentTypes,
    [property: DataMember(Order = 3)] string? Comments,
    [property: DataMember(Order = 4)] string? FileToTextConverter,
    [property: DataMember(Order = 7)] bool Disabled) : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileType"/> class.
    /// </summary>
    public FileType()
        : this(
              string.Empty,
              string.Empty,
              string.Empty,
              [],
              null,
              null,
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
              added.ContentType,
              added.OtherContentTypes,
              added.Description,
              added.FileToTextConverter,
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
        if (Disabled && domainEvent is not FileTypeEnabled and not FileTypeDisabled)
        {
            return ApplyResult.Error(this, "Cannot apply changes to a disabled file type.");
        }

        if (!(this as IDomainAggregate).IsInitialized() && domainEvent is not FileTypeAdded)
        {
            return ApplyResult.Error(this, "Cannot apply changes to an uninitialized file type.");
        }

        return domainEvent switch
        {
            FileTypeTargetAdded e => ApplyEvent(e),
            FileTypeTargetRemoved e => ApplyEvent(e),
            FileTypeAdded e => ApplyEvent(e),
            FileTypeDescriptionChanged e => ApplyEvent(e),
            FileTypeDisabled e => ApplyEvent(e),
            FileTypeEnabled e => ApplyEvent(e),
            FileTypeFileToTextConverterChanged e => ApplyEvent(e),
            FileTypeEvent => ApplyResult.NotImplemented(this),
            _ => ApplyResult.InvalidEvent(this, domainEvent),
        };
    }

    /// <summary>
    /// Applies a FileTypeCreated event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeCreated event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeAdded e) => !(this as IDomainAggregate).IsInitialized()
        ? ApplyResult.Success(new FileType(e), [e])
        : ApplyResult.Error(this, "The file type already exists and cannot be added again.");

    /// <summary>
    /// Applies a FileTypeEnabled event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeEnabled event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeEnabled e) => Disabled
            ? ApplyResult.Success(this with { Disabled = false }, [e])
            : ApplyResult.Error(this, "The file type is already enabled.");

    /// <summary>
    /// Applies a FileTypeDisabled event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeDisabled event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeDisabled e) => !Disabled
            ? ApplyResult.Success(this with { Disabled = true }, [e])
            : ApplyResult.Error(this, "The file type is already disabled.");

    /// <summary>
    /// Applies a FileTypeTextExtractionModeChanged event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeTextExtractionModeChanged event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeFileToTextConverterChanged e) => e.FileToTextConverter != FileToTextConverter
        ? ApplyResult.Success(this with { FileToTextConverter = e.FileToTextConverter }, [e])
        : ApplyResult.Error(this, "No changes to apply to file to text converter.");

    /// <summary>
    /// Applies a FileTypeDescriptionChanged event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeDescriptionChanged event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeDescriptionChanged e) => e.Name != Name || e.Description != Comments
        ? ApplyResult.Success(this with { Name = e.Name, Comments = e.Description }, [e])
        : ApplyResult.Error(this, "No changes to apply to the file type name or description.");

    /// <summary>
    /// Applies a FileTypeTargetAdded event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeTargetAdded event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeTargetAdded e)
    {
        List<string> currentTargets = [.. OtherContentTypes];
        return !currentTargets.Contains(e.Target)
            ? ApplyResult.Success(this with { OtherContentTypes = currentTargets.Concat([e.Target]) }, [e])
            : ApplyResult.Error(this, "The target is already added to the file type.");
    }

    /// <summary>
    /// Applies a FileTypeTargetRemoved event to the aggregate.
    /// </summary>
    /// <param name="e">The FileTypeTargetRemoved event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeTargetRemoved e)
    {
        List<string> currentTargets = [.. OtherContentTypes];
        return currentTargets.Contains(e.Target)
            ? ApplyResult.Success(this with { OtherContentTypes = currentTargets.Where(t => t != e.Target) }, [e])
            : ApplyResult.Error(this, "The target is not present in the file type.");
    }
}