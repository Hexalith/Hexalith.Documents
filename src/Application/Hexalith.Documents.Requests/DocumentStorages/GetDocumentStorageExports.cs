﻿namespace Hexalith.Documents.Requests.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request for a list of summaries of document storage with essential information.
/// </summary>
/// <param name="Skip">The number of document storage summaries to skip.</param>
/// <param name="Take">The number of document storage summaries to take.</param>
/// <param name="Result">The list of document storage summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentStorageExports(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<DocumentStorageImportExportViewModel> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentStorageExports"/> class.
    /// </summary>
    public GetDocumentStorageExports()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentStorageExports"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document storage summaries to skip.</param>
    /// <param name="take">The number of document storage summaries to take.</param>
    public GetDocumentStorageExports(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.DocumentStorageAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentStorageAggregateName;

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results) => this with { Results = (IEnumerable<DocumentStorageImportExportViewModel>)results };

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => new GetDocumentStorageExports(Skip + Take, Take);
}