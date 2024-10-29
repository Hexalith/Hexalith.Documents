namespace Hexalith.Document.Domain.Helpers;

using System.Collections.Generic;
using System.Linq;

using Hexalith.Document.Domain.ValueObjects;

/// <summary>
/// Provides helper methods for working with DocumentPoint collections.
/// </summary>
public static class DocumentPointHelper
{
    /// <summary>
    /// Gets the value of a document point of a specific type from a collection of document points.
    /// </summary>
    /// <param name="documentPoints">The collection of document points to search.</param>
    /// <param name="type">The type of document point to find.</param>
    /// <returns>The value of the first document point matching the specified type, or null if not found.</returns>
    public static string? GetDocumentPointValue(this IEnumerable<DocumentPoint> documentPoints, DocumentPointType type)
        => documentPoints.FirstOrDefault(p => p.PointType == type)?.Value;

    /// <summary>
    /// Gets the email address from a collection of document points.
    /// </summary>
    /// <param name="documentPoints">The collection of document points to search.</param>
    /// <returns>The email address, or null if not found.</returns>
    public static string? GetEmail(this IEnumerable<DocumentPoint> documentPoints)
        => documentPoints.GetDocumentPointValue(DocumentPointType.Email);

    /// <summary>
    /// Gets the mobile number from a collection of document points.
    /// </summary>
    /// <param name="documentPoints">The collection of document points to search.</param>
    /// <returns>The mobile number, or null if not found.</returns>
    public static string? GetMobile(this IEnumerable<DocumentPoint> documentPoints)
        => documentPoints.GetDocumentPointValue(DocumentPointType.Mobile);

    /// <summary>
    /// Gets the phone number from a collection of document points.
    /// </summary>
    /// <param name="documentPoints">The collection of document points to search.</param>
    /// <returns>The phone number, or null if not found.</returns>
    public static string? GetPhone(this IEnumerable<DocumentPoint> documentPoints)
        => documentPoints.GetDocumentPointValue(DocumentPointType.Phone);
}
