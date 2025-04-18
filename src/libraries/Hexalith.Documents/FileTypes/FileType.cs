namespace Hexalith.Documents.FileTypes;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Domain.Aggregates;

/// <summary>
/// Represents a file type in the document management system.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="ContentType">The content type of the file.</param>
/// <param name="OtherContentTypes">Collection of other content types associated with this file type.</param>
/// <param name="FileExtension">The file extension of the file type.</param>
/// <param name="OtherFileExtensions">Collection of other file extensions associated with this file type.</param>
/// <param name="Comments">The description of the file type.</param>
/// <param name="FileToTextConverter">The file to text converter associated with this file type.</param>
/// <param name="Disabled">Indicates whether this file type is disabled.</param>
[DataContract]
public record FileType(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string ContentType,
    [property: DataMember(Order = 4)] IEnumerable<string> OtherContentTypes,
    [property: DataMember(Order = 5)] string FileExtension,
    [property: DataMember(Order = 6)] string? Comments,
    [property: DataMember(Order = 7)] string? FileToTextConverter,
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
              [],
              string.Empty,
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
              added.FileExtension,
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
        if (domainEvent is FileTypeEvent && domainEvent is not FileTypeEnabled or FileTypeDisabled && Disabled)
        {
            return ApplyResult.Error(this, "Cannot change a disabled file type.");
        }

        if (!(this as IDomainAggregate).IsInitialized() && domainEvent is not FileTypeAdded)
        {
            return ApplyResult.Error(this, "Cannot apply changes to an uninitialized file type.");
        }

        return domainEvent switch
        {
            FileTypeOtherContentTypeAdded e => ApplyEvent(e),
            FileTypeOtherContentTypeRemoved e => ApplyEvent(e),
            FileTypeAdded e => ApplyEvent(e),
            FileTypeDescriptionChanged e => ApplyEvent(e),
            FileTypeContentTypeChanged e => ApplyEvent(e),
            FileTypeFileExtensionChanged e => ApplyEvent(e),
            FileTypeDisabled e => ApplyEvent(e),
            FileTypeEnabled e => ApplyEvent(e),
            FileTypeFileToTextConverterChanged e => ApplyEvent(e),
            FileTypeEvent => ApplyResult.NotImplemented(this),
            _ => ApplyResult.InvalidEvent(this, domainEvent),
        };
    }

    /// <summary>
    /// Applies a <see cref="FileTypeAdded"/> event to the aggregate.
    /// </summary>
    /// <param name="e">The <see cref="FileTypeAdded"/> event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeAdded e) => !(this as IDomainAggregate).IsInitialized()
        ? ApplyResult.Success(new FileType(e), [e])
        : ApplyResult.Error(this, "The file type already exists and cannot be added again.");

    /// <summary>
    /// Applies a <see cref="FileTypeEnabled"/> event to the aggregate.
    /// </summary>
    /// <param name="e">The <see cref="FileTypeEnabled"/> event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeEnabled e) => Disabled
            ? ApplyResult.Success(this with { Disabled = false }, [e])
            : ApplyResult.Error(this, "The file type is already enabled.");

    /// <summary>
    /// Applies a <see cref="FileTypeDisabled"/> event to the aggregate.
    /// </summary>
    /// <param name="e">The <see cref="FileTypeDisabled"/> event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeDisabled e) => !Disabled
            ? ApplyResult.Success(this with { Disabled = true }, [e])
            : ApplyResult.Error(this, "The file type is already disabled.");

    /// <summary>
    /// Applies a <see cref="FileTypeFileToTextConverterChanged"/> event to the aggregate.
    /// </summary>
    /// <param name="e">The <see cref="FileTypeFileToTextConverterChanged"/> event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeFileToTextConverterChanged e) => e.FileToTextConverter != FileToTextConverter
        ? ApplyResult.Success(this with { FileToTextConverter = e.FileToTextConverter }, [e])
        : ApplyResult.Error(this, "No changes to apply to file to text converter.");

    /// <summary>
    /// Applies a <see cref="FileTypeDescriptionChanged"/> event to the aggregate.
    /// </summary>
    /// <param name="e">The <see cref="FileTypeDescriptionChanged"/> event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeDescriptionChanged e) => e.Name != Name || e.Comments != Comments
        ? ApplyResult.Success(this with { Name = e.Name, Comments = e.Comments }, [e])
        : ApplyResult.Error(this, "No changes to apply to the file type name or description.");

    /// <summary>
    /// Applies a <see cref="FileTypeContentTypeChanged"/> event to the aggregate.
    /// </summary>
    /// <param name="e">The <see cref="FileTypeContentTypeChanged"/> event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeContentTypeChanged e) => e.ContentType != ContentType
        ? ApplyResult.Success(this with { ContentType = e.ContentType }, [e])
        : ApplyResult.Error(this, "No changes to apply to the file type content type.");

    /// <summary>
    /// Applies a <see cref="FileTypeFileExtensionChanged"/> event to the aggregate.
    /// </summary>
    /// <param name="e">The <see cref="FileTypeFileExtensionChanged"/> event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeFileExtensionChanged e) => e.FileExtension != FileExtension
        ? ApplyResult.Success(this with { FileExtension = e.FileExtension }, [e])
        : ApplyResult.Error(this, "No changes to apply to the file type file extension.");

    /// <summary>
    /// Applies a <see cref="FileTypeOtherContentTypeAdded"/> event to the aggregate.
    /// </summary>
    /// <param name="e">The <see cref="FileTypeOtherContentTypeAdded"/> event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeOtherContentTypeAdded e)
    {
        List<string> currentTargets = [.. OtherContentTypes];
        return !currentTargets.Contains(e.OtherContentType)
            ? ApplyResult.Success(this with { OtherContentTypes = currentTargets.Concat([e.OtherContentType]) }, [e])
            : ApplyResult.Error(this, "The other content type is already added to the file type.");
    }

    /// <summary>
    /// Applies a <see cref="FileTypeOtherContentTypeRemoved"/> event to the aggregate.
    /// </summary>
    /// <param name="e">The <see cref="FileTypeOtherContentTypeRemoved"/> event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(FileTypeOtherContentTypeRemoved e)
    {
        List<string> currentTargets = [.. OtherContentTypes];
        return currentTargets.Contains(e.OtherContentType)
            ? ApplyResult.Success(this with { OtherContentTypes = currentTargets.Where(t => t != e.OtherContentType) }, [e])
            : ApplyResult.Error(this, "The other content type is not present in the file type.");
    }
}