namespace UnitTests.Domain.Events;

using System.Text.Json;

using FluentAssertions;

using Hexalith.Application.Abstractions.Extensions;
using Hexalith.Application.Events;
using Hexalith.Application.Metadatas;
using Hexalith.Application.States;
using Hexalith.Documents.Events.Extensions;
using Hexalith.Documents.Events.FileTypes;

public class FileTypeEventCancelledTest
{
    [Fact]
    public void SerializeAndDeserializeShouldBeEquivalentToOriginal()
    {
        // Arrange
        HexalithApplicationAbstractions.RegisterPolymorphicMappers();
        HexalithDocumentsEvents.RegisterPolymorphicMappers();
        FileTypeAdded added = new(
            "MD",
            "Markdown file",
            "Type for Markdown files",
            "MarkdownCleaner",
            ["*.md", "*.markdown"]);
        Metadata metadataAdded = Metadata.CreateNew(added, "test", "part1", DateTime.UtcNow);
        MessageState addedState = new(added, metadataAdded);
        DomainEventCancelled cancelled = new("testing", addedState);
        Metadata metadataCancelled = Metadata.CreateNew(cancelled, "test", "part1", DateTime.UtcNow);
        MessageState messageState = new(cancelled, metadataCancelled);

        // Act
        string json = JsonSerializer.Serialize(messageState);
        MessageState deserializedMessageState = JsonSerializer.Deserialize<MessageState>(json);

        // Assert
        _ = deserializedMessageState.Should().BeEquivalentTo(messageState);
        _ = deserializedMessageState.MessageObject.Should().BeOfType<DomainEventCancelled>();
        DomainEventCancelled cancelledEvent = (DomainEventCancelled)deserializedMessageState.MessageObject;
        _ = cancelledEvent.Should().BeEquivalentTo(cancelled);
        _ = cancelledEvent.Event.Should().BeEquivalentTo(addedState);
        _ = deserializedMessageState.Metadata.Should().BeEquivalentTo(metadataCancelled);
    }
}