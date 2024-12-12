namespace UnitTests.Application.Requests;

using System.Text.Json;

using FluentAssertions;

using Hexalith.Documents.Requests.FileTypes;

public class FileTypeSummaryViewModelTest
{
    [Fact]
    public void SerializeAndDeserializeShouldBeEquivalentToOriginal()
    {
        // Arrange
        FileTypeSummaryViewModel model = new(
            "MD",
            "Markdown file",
            true);

        // Act
        string json = JsonSerializer.Serialize(model);
        FileTypeSummaryViewModel deserialized = JsonSerializer.Deserialize<FileTypeSummaryViewModel>(json);

        // Assert
        _ = deserialized.Should().NotBeNull();
        _ = deserialized.Should().BeEquivalentTo(model);
    }
}