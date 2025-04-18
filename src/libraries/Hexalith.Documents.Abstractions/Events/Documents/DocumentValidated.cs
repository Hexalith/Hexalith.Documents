namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a document is validated.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="ByContactId">The identifier of the contact who validated the document.</param>
/// <param name="Date">The date and time when the document was validated.</param>
[PolymorphicSerialization]
public partial record DocumentValidated(string Id, string ByContactId, DateTimeOffset Date) : DocumentEvent(Id)
{
}