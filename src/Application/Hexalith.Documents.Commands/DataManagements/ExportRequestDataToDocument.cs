namespace Hexalith.Documents.Commands.DataManagements;

using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to export data to a document.
/// </summary>
/// <param name="Id">The identifier of the command.</param>
/// <param name="UserId">The identifier of the user who requested the export.</param>
/// <param name="Request">The query to execute to export the data.</param>
[PolymorphicSerialization]
[method: JsonConstructor]
public partial record ExportRequestDataToDocument(
    string Id,
    [property: DataMember(Order = 2)] string Request)
    : DataManagementCommand(Id)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExportRequestDataToDocument"/> class.
    /// </summary>
    /// <param name="id">The identifier of the command.</param>
    /// <param name="description">The description of the export request.</param>
    /// <param name="userId">The identifier of the user who requested the export.</param>
    /// <param name="requestObject">The query object to execute to export the data.</param>
    public ExportRequestDataToDocument(string id, IChunkableRequest requestObject)
        : this(id, Serialize((PolymorphicRecordBase)requestObject))
    {
    }

    /// <summary>
    /// Gets the query object.
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public IChunkableRequest RequestObject
        => Deserialize();

    /// <summary>
    /// Serializes the query object to a JSON string.
    /// </summary>
    /// <param name="request">The query object to serialize.</param>
    /// <returns>The serialized JSON string.</returns>
    private static string Serialize(PolymorphicRecordBase request)
        => JsonSerializer.Serialize<PolymorphicRecordBase>(request, PolymorphicHelper.DefaultJsonSerializerOptions);

    /// <summary>
    /// Deserializes the query JSON string to a query object.
    /// </summary>
    /// <returns>The deserialized query object.</returns>
    /// <exception cref="InvalidOperationException">Thrown when deserialization fails.</exception>
    private IChunkableRequest Deserialize() => JsonSerializer
            .Deserialize<PolymorphicRecordBase>(Request, PolymorphicHelper.DefaultJsonSerializerOptions) as IChunkableRequest
            ?? throw new InvalidOperationException("Unable to deserialize the query : " + Request);
}