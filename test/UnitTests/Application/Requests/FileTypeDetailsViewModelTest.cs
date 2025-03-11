namespace UnitTests.Application.Requests;

using System.Text.Json;

using FluentAssertions;

using Hexalith.Documents.Requests.FileTypes;

public class FileTypeDetailsViewModelTest
{
    private static readonly string[] _otherContentTypes = ["text/plain"];
    private static readonly string[] _otherFileExtensions = [".txt"];

    [Fact]
    public void SerializeAndDeserializeShouldBeEquivalentToOriginal()
    {
        // Arrange
        FileTypeDetailsViewModel model = new(
            "MD",
            "Markdown file",
            "text/markdown",
            _otherContentTypes,
            ".md",
            _otherFileExtensions,
            "Markdown format text file",
            "MarkdownCleaner",
            true);

        // Act
        string json = JsonSerializer.Serialize(model);
        FileTypeDetailsViewModel deserialized = JsonSerializer.Deserialize<FileTypeDetailsViewModel>(json);

        // Assert
        _ = deserialized.Should().NotBeNull();
        _ = deserialized.Should().BeEquivalentTo(model);
    }
}