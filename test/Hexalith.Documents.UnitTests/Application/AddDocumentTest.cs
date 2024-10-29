namespace Hexalith.Documents.UnitTests.Application;

using System;
using System.Text.Json;

using FluentAssertions;

using Hexalith.Application.Metadatas;
using Hexalith.Documents.Commands;
using Hexalith.Documents.Commands.Extensions;
using Hexalith.Infrastructure.DaprRuntime.Actors;
using Hexalith.PolymorphicSerialization;

public class AddDocumentTest
{
    [Fact]
    public void AddDocumentBaseTypeInEnvelopeShouldBeSameAsOriginal()
    {
        // Arrange
        HexalithDocumentsCommands.RegisterPolymorphicMappers();
        JsonSerializerOptions jsonOptions = PolymorphicHelper.DefaultJsonSerializerOptions;
        AddDocument message = new("1", "Test AddDocumentBaseType", "This is a test AddDocumentBaseType", new Document.Domain.ValueObjects.Person());
        Metadata metadata = Metadata.CreateNew(message, "test", "part1", DateTime.UtcNow);
        ActorMessageEnvelope envelope = ActorMessageEnvelope.Create(message, metadata);

        // Act
        string json = JsonSerializer.Serialize(envelope, jsonOptions);
        ActorMessageEnvelope deserializedEnvelope = JsonSerializer.Deserialize<ActorMessageEnvelope>(json, jsonOptions);
        (object deserializedMessage, Metadata deserializedMetadata) = deserializedEnvelope.Deserialize();

        // Assert
        _ = deserializedMessage.Should().BeEquivalentTo(message);
        _ = deserializedMetadata.Should().BeEquivalentTo(metadata);
    }
}