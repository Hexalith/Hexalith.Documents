namespace UnitTests.Application.Requests;

using System.Text.Json;

using FluentAssertions;

using Hexalith.Application.Metadatas;
using Hexalith.Application.States;
using Hexalith.Documents.Events.Extensions;
using Hexalith.Documents.Events.FileTypes;

public class FileTypeEventCancelledTest
{
    [Fact]
    public void SerializeAndDeserializeFileTypeAddedShouldBeEquivalentToOriginal()
    {
        // Arrange
        HexalithDocumentsEvents.RegisterPolymorphicMappers();
        FileTypeAdded added = new(
            "MD",
            "Markdown file",
            "Type for Markdown files",
            "MarkdownCleaner",
            ["*.md", "*.markdown"]);
        Metadata metadataAdded = Metadata.CreateNew(added, "test", "part1", DateTime.UtcNow);
        MessageState addedState = new(added, metadataAdded);
        FileTypeEventCancelled cancelled = new(addedState, "testing");
        Metadata metadataCancelled = Metadata.CreateNew(cancelled, "test", "part1", DateTime.UtcNow);
        MessageState messageState = new(cancelled, metadataCancelled);

        // Act
        string json = JsonSerializer.Serialize(messageState);
        MessageState deserializedMessageState = JsonSerializer.Deserialize<MessageState>(json);

        // Assert
        _ = deserializedMessageState.Should().BeEquivalentTo(messageState);
        _ = deserializedMessageState.MessageObject.Should().BeOfType<FileTypeEventCancelled>();
        FileTypeEventCancelled cancelledEvent = (FileTypeEventCancelled)deserializedMessageState.MessageObject;
        _ = cancelledEvent.Should().BeEquivalentTo(cancelled);
        _ = cancelledEvent.Event.Should().BeEquivalentTo(addedState);
        _ = deserializedMessageState.Metadata.Should().BeEquivalentTo(metadataCancelled);
    }
}