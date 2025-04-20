// <copyright file="DocumentType.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.DocumentTypes;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.ValueObjects;
using Hexalith.Domains;
using Hexalith.Domains.Results;

/// <summary>
/// Represents a document type in the system, defining metadata and processing rules for documents.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Name">The name of the document type.</param>
/// <param name="Comments">A detailed description of the document type.</param>
/// <param name="DataExtractionIds">A collection of instructions for extracting data from documents of this type.</param>
/// <param name="FileTypeIds">A collection of supported file type identifiers.</param>
/// <param name="Tags">A collection of tags associated with this document type.</param>
/// <param name="Disabled">A value indicating whether this document type is disabled.</param>
[DataContract]
public record DocumentType(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Comments,
    [property: DataMember(Order = 7)] IEnumerable<string> DataExtractionIds,
    [property: DataMember(Order = 8)] IEnumerable<string> FileTypeIds,
    [property: DataMember(Order = 9)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 10)] bool Disabled) : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentType"/> class.
    /// Creates a default document type with empty values.
    /// </summary>
    public DocumentType()
        : this(
              string.Empty,
              string.Empty,
              null,
              [],
              [],
              [],
              false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentType"/> class based on a creation event.
    /// </summary>
    /// <param name="added">The event containing the initial document type data.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="added"/> is null.</exception>
    public DocumentType(DocumentTypeAdded added)
        : this(
              (added ?? throw new ArgumentNullException(nameof(added))).Id,
              added.Name,
              added.Description,
              added.DataExtractionIds,
              added.FileTypeIds,
              [],
              false)
    {
        // Ensure that collections are initialized.
        _ = DataExtractionIds ??= [];
        _ = FileTypeIds ??= [];
        _ = Tags ??= [];
    }

    /// <summary>
    /// Gets the unique identifier of the aggregate.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the name of the aggregate type.
    /// </summary>
    public string AggregateName => DocumentDomainHelper.DocumentTypeAggregateName;

    /// <summary>
    /// Applies a domain event to the document type.
    /// </summary>
    /// <param name="domainEvent">The domain event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvent"/> is null.</exception>
    [SuppressMessage("Critical Code Smell", "S1541:Methods and properties should not be too complex", Justification = "This method is not complex")]
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        if (Disabled && domainEvent is not DocumentTypeEnabled and not DocumentTypeDisabled)
        {
            return ApplyResult.Error(this, "Cannot apply changes to a disabled document type.");
        }

        if (!(this as IDomainAggregate).IsInitialized() && domainEvent is not DocumentTypeAdded)
        {
            return ApplyResult.Error(this, "Cannot apply changes to an uninitialized document type.");
        }

        return domainEvent switch
        {
            DocumentTypeAdded e => ApplyEvent(e),
            DocumentTypeDataExtractionAdded e => ApplyEvent(e),
            DocumentTypeDataExtractionRemoved e => ApplyEvent(e),
            DocumentTypeDescriptionChanged e => ApplyEvent(e),
            DocumentTypeDisabled e => ApplyEvent(e),
            DocumentTypeEnabled e => ApplyEvent(e),
            DocumentTypeFileTypeAdded e => ApplyEvent(e),
            DocumentTypeFileTypeRemoved e => ApplyEvent(e),
            DocumentTypeTagAdded e => ApplyEvent(e),
            DocumentTypeTagRemoved e => ApplyEvent(e),
            DocumentTypeEvent => ApplyResult.NotImplemented(this),
            _ => ApplyResult.InvalidEvent(this, domainEvent),
        };
    }

    /// <summary>
    /// Applies a document type creation event.
    /// </summary>
    /// <param name="e">The creation event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeAdded e) => !(this as IDomainAggregate).IsInitialized()
        ? ApplyResult.Success(new DocumentType(e), [e])
        : ApplyResult.Error(this, "The document type already exists.");

    /// <summary>
    /// Applies a document type enable event.
    /// </summary>
    /// <param name="e">The enable event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeEnabled e) => Disabled
            ? ApplyResult.Success(this with { Disabled = false }, [e])
            : ApplyResult.Error(this, "The document type is already enabled.");

    /// <summary>
    /// Applies a document type disable event.
    /// </summary>
    /// <param name="e">The disable event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeDisabled e) => !Disabled
            ? ApplyResult.Success(this with { Disabled = true }, [e])
            : ApplyResult.Error(this, "The document type is already disabled.");

    /// <summary>
    /// Applies a document type description change event.
    /// </summary>
    /// <param name="e">The description change event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeDescriptionChanged e) => e.Name != Name || e.Description != Comments
        ? ApplyResult.Success(this with { Name = e.Name, Comments = e.Description }, [e])
            : ApplyResult.Error(this, "The document type name and description is already set to the specified value.");

    /// <summary>
    /// Applies an event to add a file type.
    /// </summary>
    /// <param name="e">The file type addition event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeFileTypeAdded e)
    {
        List<string> fileTypes = [.. FileTypeIds];
        if (fileTypes.Contains(e.FileTypeId))
        {
            return ApplyResult.Error(this, "The file type already exists.");
        }

        fileTypes.Add(e.FileTypeId);
        return ApplyResult.Success(this with { FileTypeIds = fileTypes }, [e]);
    }

    /// <summary>
    /// Applies an event to remove a file type.
    /// </summary>
    /// <param name="e">The file type removal event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeFileTypeRemoved e)
    {
        List<string> fileTypes = [.. FileTypeIds];
        if (!fileTypes.Remove(e.FileTypeId))
        {
            return ApplyResult.Error(this, "The file type does not exist.");
        }

        return new ApplyResult(
            this with { FileTypeIds = fileTypes },
            [e],
            false);
    }

    /// <summary>
    /// Applies an event to add a data extraction instruction.
    /// </summary>
    /// <param name="e">The data extraction addition event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeDataExtractionAdded e)
    {
        if (DataExtractionIds.Any(p => p == e.DataInformationExtractionId))
        {
            return ApplyResult.Error(this, "The data extraction instruction already exists.");
        }

        return ApplyResult.Success(
            this with
            {
                DataExtractionIds = [..DataExtractionIds
                .Append(e.DataInformationExtractionId)
                .Order()],
            },
            [e]);
    }

    /// <summary>
    /// Applies an event to remove a data extraction instruction.
    /// </summary>
    /// <param name="e">The data extraction removal event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeDataExtractionRemoved e)
    {
        if (!DataExtractionIds.Any(p => p == e.DataInformationExtractionId))
        {
            return ApplyResult.Error(this, "The data extraction instruction does not exist.");
        }

        return ApplyResult.Success(this with { DataExtractionIds = [.. DataExtractionIds.Where(p => p != e.DataInformationExtractionId)] }, [e]);
    }

    /// <summary>
    /// Applies an event to add a tag.
    /// </summary>
    /// <param name="e">The tag addition event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeTagAdded e)
    {
        if (Tags.Any(p => p.Key == e.Key && p.Value == e.Value))
        {
            return ApplyResult.Error(this, $"The tag {e.Key}={e.Value} already exists in document type {Id}/{Name}.");
        }

        if (Tags.Any(p => p.Key == e.Key && (e.Unique || p.Unique)))
        {
            return ApplyResult.Error(this, $"The unique tag with key={e.Key} already exists in document type {Id}/{Name}.");
        }

        return ApplyResult.Success(
            this with
            {
                Tags = [..Tags
                    .Append(new DocumentTag(e.Key, e.Value, e.Unique))
                    .Distinct()
                    .OrderBy(p => p.Key)
                    .ThenBy(p => p.Value)],
            },
            [e]);
    }

    /// <summary>
    /// Applies an event to remove a tag.
    /// </summary>
    /// <param name="e">The tag removal event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeTagRemoved e)
    {
        if (!Tags.Any(p => p.Key == e.Key && p.Value == e.Value))
        {
            return ApplyResult.Error(this, $"The tag {e.Key} does not exist in document type {Id}/{Name}.");
        }

        return ApplyResult.Success(
            this with { Tags = [.. Tags.Where(p => p.Key != e.Key || p.Value != e.Value)] },
            [e]);
    }
}