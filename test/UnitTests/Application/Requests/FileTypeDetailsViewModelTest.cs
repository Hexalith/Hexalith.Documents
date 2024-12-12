namespace UnitTests.Application.Requests;

using System.Text.Json;

using FluentAssertions;

using Hexalith.Documents.Requests.FileTypes;

public class FileTypeDetailsViewModelTest
{
    [Fact]
    public void SerializeAndDeserializeShouldBeEquivalentToOriginal()
    {
        // Arrange
        FileTypeDetailsViewModel model = new(
            "MD",
            "Markdown file",
            "Markdown format text file",
            "MarkdownCleaner",
            [],
            true);

        // Act
        string json = JsonSerializer.Serialize(model);
        FileTypeDetailsViewModel deserialized = JsonSerializer.Deserialize<FileTypeDetailsViewModel>(json);

        // Assert
        _ = deserialized.Should().NotBeNull();
        _ = deserialized.Should().BeEquivalentTo(model);
    }
}