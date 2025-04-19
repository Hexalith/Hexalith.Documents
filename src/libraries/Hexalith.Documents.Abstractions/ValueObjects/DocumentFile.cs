// <copyright file="DocumentFile.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.ValueObjects;

using System.Runtime.Serialization;
using System.Text;

/// <summary>
/// Represents a document file with a name, content type, and content in base64 format.
/// </summary>
/// <param name="Name">The name of the document file.</param>
/// <param name="ContentType">The MIME content type of the document file.</param>
/// <param name="Content">The content of the document file in base64 format.</param>
[DataContract]
public record DocumentFile(
    [property: DataMember(Order = 1)] string Name,
    [property: DataMember(Order = 2)] string ContentType,
    [property: DataMember(Order = 3)] string Content)
{
    /// <summary>
    /// Gets an empty document file.
    /// </summary>
    public static DocumentFile Empty => new(string.Empty, string.Empty, string.Empty);

    /// <summary>
    /// Creates a new instance of the <see cref="DocumentFile"/> class with the specified name, content type, and plain text content.
    /// </summary>
    /// <param name="name">The name of the document file.</param>
    /// <param name="text">The text content of the document file. It will be converted to base64.</param>
    /// <returns>A new instance of the <see cref="DocumentFile"/> class. with. </returns>
    public static DocumentFile TextDocument(string name, string text)
        => new(name, "text/plain", Convert.ToBase64String(Encoding.UTF8.GetBytes(text)));
}