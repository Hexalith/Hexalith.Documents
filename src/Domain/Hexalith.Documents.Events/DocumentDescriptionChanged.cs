namespace Hexalith.Documents.Events;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a change of the name of a document.
/// </summary>
/// <param name="Id">The document identifier.</param>
/// <param name="Name">The document name.</param>
/// <param name="Description">The document description.</param>
[PolymorphicSerialization]
public partial record DocumentDescriptionChanged(string Id, string Name, string Comments) : DocumentEvent(Id)
{
}