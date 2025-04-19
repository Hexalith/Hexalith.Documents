// <copyright file="AddDocumentFile.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.Documents;

using System.Runtime.Serialization;
using System.Text;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to create a new document.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="Name">The name of the document.</param>
/// <param name="ContentType">The content type of the document.</param>
/// <param name="Base64Content">The content of the document in base64.</param>
[PolymorphicSerialization]
public partial record AddDocumentFile(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] FileContentType ContentType,
    [property: DataMember(Order = 4)] string Base64Content)
    : DocumentCommand(Id)
{
    /// <summary>
    /// Creates a new AddDocumentFile instance for a JSON file.
    /// </summary>
    /// <param name="id">The unique identifier of the document.</param>
    /// <param name="name">The name of the document.</param>
    /// <param name="content">The content of the Json file.</param>
    /// <returns>A new AddDocumentFile instance.</returns>
    public static AddDocumentFile AddJsonFile(string id, string name, string content)
    => new(
        id,
        name,
        FileContentType.Json,
        Convert.ToBase64String(Encoding.UTF8.GetBytes(content)));

    /// <summary>
    /// Creates a new AddDocumentFile instance for a text file.
    /// </summary>
    /// <param name="id">The unique identifier of the document.</param>
    /// <param name="name">The name of the document.</param>
    /// <param name="content">The content of the text file.</param>
    /// <returns>A new AddDocumentFile instance.</returns>
    public static AddDocumentFile AddTextFile(string id, string name, string content)
    => new(
        id,
        name,
        FileContentType.Text,
        Convert.ToBase64String(Encoding.UTF8.GetBytes(content)));
}