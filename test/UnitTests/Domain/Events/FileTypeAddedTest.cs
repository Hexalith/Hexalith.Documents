﻿// namespace UnitTests.Domain.Events;

// using System.Text.Json;

// using FluentAssertions;

// using Hexalith.Application.Abstractions.Extensions;
// using Hexalith.Application.Metadatas;
// using Hexalith.Application.States;
// using Hexalith.Documents.Events.Extensions;
// using Hexalith.Documents.Events.FileTypes;

// public class FileTypeAddedTest
// {
//    public FileTypeAddedTest()
//    {
//        HexalithApplicationAbstractions.RegisterPolymorphicMappers();
//        HexalithDocumentsEvents.RegisterPolymorphicMappers();
//    }

// [Fact]
//    public void SerializeAndDeserializeFileTypeAddedShouldBeEquivalentToOriginal()
//    {
//        // Arrange
//        FileTypeAdded added = new(
//            "MD",
//            "Markdown file",
//            "Type for Markdown files",
//            "MarkdownCleaner",
//            ["*.md", "*.markdown"]);
//        Metadata metadata = Metadata.CreateNew(added, "test", "part1", DateTime.UtcNow);
//        MessageState messageState = new(added, metadata);

// // Act
//        string json = JsonSerializer.Serialize(messageState);
//        MessageState deserializedMessageState = JsonSerializer.Deserialize<MessageState>(json);

// // Assert
//        _ = deserializedMessageState.Should().BeEquivalentTo(messageState);
//        _ = deserializedMessageState.MessageObject.Should().BeEquivalentTo(added);
//        _ = deserializedMessageState.Metadata.Should().BeEquivalentTo(metadata);
//    }
// }