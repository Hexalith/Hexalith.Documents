﻿namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request for a list of summaries of document container with essential information.
/// </summary>
/// <param name="Skip">The number of document container summaries to skip.</param>
/// <param name="Take">The number of document container summaries to take.</param>
/// <param name="Result">The list of document container summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentContainerExports(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<DocumentContainerExportViewModel> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentContainerExports"/> class.
    /// </summary>
    public GetDocumentContainerExports()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentContainerExports"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document container summaries to skip.</param>
    /// <param name="take">The number of document container summaries to take.</param>
    public GetDocumentContainerExports(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.DocumentContainerAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentContainerAggregateName;

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => new GetDocumentContainerExports(Skip + Take, Take);
}