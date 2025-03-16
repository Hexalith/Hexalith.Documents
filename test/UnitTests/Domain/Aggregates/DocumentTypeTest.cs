namespace UnitTests.Domain.Aggregates;

using Hexalith.Documents.Domain.DocumentTypes;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Domain.Aggregates;

using Xunit;

public class DocumentTypeTest
{
    [Fact]
    public void AggregateIdShouldReturnId()
    {
        // Arrange
        string id = "doc-type-1";
        DocumentType documentType = new(id, "Test", null, [], [], [], false);

        // Act & Assert
        Assert.Equal(id, documentType.AggregateId);
    }

    [Fact]
    public void AggregateNameShouldReturnDocumentTypeAggregateName()
    {
        // Arrange
        DocumentType documentType = new();

        // Act & Assert
        Assert.Equal("DocumentType", documentType.AggregateName);
    }

    [Fact]
    public void ApplyDocumentTypeAddedWhenAlreadyInitializedShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], [], [], false);
        DocumentTypeAdded added = new("doc-type-2", "Invoice", "Invoice document type", [], []);

        // Act
        ApplyResult result = documentType.Apply(added);

        // Assert
        Assert.True(result.Failed);
        Assert.Equal("The document type already exists.", result.Reason);
    }

    [Fact]
    public void ApplyDocumentTypeAddedWhenNotInitializedShouldSucceed()
    {
        // Arrange
        DocumentType documentType = new();
        DocumentTypeAdded added = new("doc-type-1", "Invoice", "Invoice document type", [], []);

        // Act
        ApplyResult result = documentType.Apply(added);

        // Assert
        Assert.False(result.Failed);
        DocumentType updatedDocumentType = (DocumentType)result.Aggregate;
        Assert.Equal("doc-type-1", updatedDocumentType.Id);
        Assert.Equal("Invoice", updatedDocumentType.Name);
        Assert.Equal("Invoice document type", updatedDocumentType.Comments);
    }

    [Fact]
    public void ApplyDocumentTypeDataExtractionAddedWhenAlreadyExistsShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, ["extraction-1"], [], [], false);
        DocumentTypeDataExtractionAdded addEvent = new("doc-type-1", "extraction-1");

        // Act
        ApplyResult result = documentType.Apply(addEvent);

        // Assert
        Assert.True(result.Failed);
        Assert.Equal("The data extraction instruction already exists.", result.Reason);
    }

    [Fact]
    public void ApplyDocumentTypeDataExtractionAddedWhenNewShouldSucceed()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, ["extraction-1"], [], [], false);
        DocumentTypeDataExtractionAdded addEvent = new("doc-type-1", "extraction-2");

        // Act
        ApplyResult result = documentType.Apply(addEvent);

        // Assert
        Assert.False(result.Failed);
        DocumentType updatedDocumentType = (DocumentType)result.Aggregate;
        Assert.Contains("extraction-1", updatedDocumentType.DataExtractionIds);
        Assert.Contains("extraction-2", updatedDocumentType.DataExtractionIds);
    }

    [Fact]
    public void ApplyDocumentTypeDataExtractionRemovedWhenDoesNotExistShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, ["extraction-1"], [], [], false);
        DocumentTypeDataExtractionRemoved removeEvent = new("doc-type-1", "extraction-2");

        // Act
        ApplyResult result = documentType.Apply(removeEvent);

        // Assert
        Assert.True(result.Failed);
        Assert.Equal("The data extraction instruction does not exist.", result.Reason);
    }

    [Fact]
    public void ApplyDocumentTypeDataExtractionRemovedWhenExistsShouldSucceed()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, ["extraction-1", "extraction-2"], [], [], false);
        DocumentTypeDataExtractionRemoved removeEvent = new("doc-type-1", "extraction-1");

        // Act
        ApplyResult result = documentType.Apply(removeEvent);

        // Assert
        Assert.False(result.Failed);
        DocumentType updatedDocumentType = (DocumentType)result.Aggregate;
        Assert.DoesNotContain("extraction-1", updatedDocumentType.DataExtractionIds);
        Assert.Contains("extraction-2", updatedDocumentType.DataExtractionIds);
    }

    [Fact]
    public void ApplyDocumentTypeDescriptionChangedWhenDifferentShouldSucceed()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", "Old description", [], [], [], false);
        DocumentTypeDescriptionChanged changeEvent = new("doc-type-1", "New Name", "New description");

        // Act
        ApplyResult result = documentType.Apply(changeEvent);

        // Assert
        Assert.False(result.Failed);
        DocumentType updatedDocumentType = (DocumentType)result.Aggregate;
        Assert.Equal("New Name", updatedDocumentType.Name);
        Assert.Equal("New description", updatedDocumentType.Comments);
    }

    [Fact]
    public void ApplyDocumentTypeDescriptionChangedWhenSameShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", "Description", [], [], [], false);
        DocumentTypeDescriptionChanged changeEvent = new("doc-type-1", "Test", "Description");

        // Act
        ApplyResult result = documentType.Apply(changeEvent);

        // Assert
        Assert.True(result.Failed);
        Assert.Equal("The document type name and description is already set to the specified value.", result.Reason);
    }

    [Fact]
    public void ApplyDocumentTypeDisabledWhenAlreadyDisabledShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], [], [], true);
        DocumentTypeDisabled disableEvent = new("doc-type-1");

        // Act
        ApplyResult result = documentType.Apply(disableEvent);

        // Assert
        Assert.True(result.Failed);
        Assert.Equal("The document type is already disabled.", result.Reason);
    }

    [Fact]
    public void ApplyDocumentTypeDisabledWhenEnabledShouldSucceed()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], [], [], false);
        DocumentTypeDisabled disableEvent = new("doc-type-1");

        // Act
        ApplyResult result = documentType.Apply(disableEvent);

        // Assert
        Assert.False(result.Failed);
        DocumentType updatedDocumentType = (DocumentType)result.Aggregate;
        Assert.True(updatedDocumentType.Disabled);
    }

    [Fact]
    public void ApplyDocumentTypeEnabledWhenAlreadyEnabledShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], [], [], false);
        DocumentTypeEnabled enableEvent = new("doc-type-1");

        // Act
        ApplyResult result = documentType.Apply(enableEvent);

        // Assert
        Assert.True(result.Failed);
        Assert.Equal("The document type is already enabled.", result.Reason);
    }

    [Fact]
    public void ApplyDocumentTypeEnabledWhenDisabledShouldSucceed()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], [], [], true);
        DocumentTypeEnabled enableEvent = new("doc-type-1");

        // Act
        ApplyResult result = documentType.Apply(enableEvent);

        // Assert
        Assert.False(result.Failed);
        DocumentType updatedDocumentType = (DocumentType)result.Aggregate;
        Assert.False(updatedDocumentType.Disabled);
    }

    [Fact]
    public void ApplyDocumentTypeFileTypeAddedWhenAlreadyExistsShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], ["pdf"], [], false);
        DocumentTypeFileTypeAdded addEvent = new("doc-type-1", "pdf");

        // Act
        ApplyResult result = documentType.Apply(addEvent);

        // Assert
        Assert.True(result.Failed);
        Assert.Equal("The file type already exists.", result.Reason);
    }

    [Fact]
    public void ApplyDocumentTypeFileTypeAddedWhenNewShouldSucceed()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], ["pdf"], [], false);
        DocumentTypeFileTypeAdded addEvent = new("doc-type-1", "docx");

        // Act
        ApplyResult result = documentType.Apply(addEvent);

        // Assert
        Assert.False(result.Failed);
        DocumentType updatedDocumentType = (DocumentType)result.Aggregate;
        Assert.Contains("docx", updatedDocumentType.FileTypeIds);
        Assert.Contains("pdf", updatedDocumentType.FileTypeIds);
    }

    [Fact]
    public void ApplyDocumentTypeFileTypeRemovedWhenDoesNotExistShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], ["pdf"], [], false);
        DocumentTypeFileTypeRemoved removeEvent = new("doc-type-1", "docx");

        // Act
        ApplyResult result = documentType.Apply(removeEvent);

        // Assert
        Assert.True(result.Failed);
        Assert.Equal("The file type does not exist.", result.Reason);
    }

    [Fact]
    public void ApplyDocumentTypeFileTypeRemovedWhenExistsShouldSucceed()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], ["pdf", "docx"], [], false);
        DocumentTypeFileTypeRemoved removeEvent = new("doc-type-1", "pdf");

        // Act
        ApplyResult result = documentType.Apply(removeEvent);

        // Assert
        Assert.False(result.Failed);
        DocumentType updatedDocumentType = (DocumentType)result.Aggregate;
        Assert.DoesNotContain("pdf", updatedDocumentType.FileTypeIds);
        Assert.Contains("docx", updatedDocumentType.FileTypeIds);
    }

    [Fact]
    public void ApplyDocumentTypeTagAddedWhenAlreadyExistsShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], [], [new DocumentTag("key1", "value1", false)], false);
        DocumentTypeTagAdded addEvent = new("doc-type-1", "key1", "value1", false);

        // Act
        ApplyResult result = documentType.Apply(addEvent);

        // Assert
        Assert.True(result.Failed);
        Assert.Contains("The tag key1=value1 already exists in document type doc-type-1/Test.", result.Reason, StringComparison.InvariantCulture);
    }

    [Fact]
    public void ApplyDocumentTypeTagAddedWhenNewShouldSucceed()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], [], [new DocumentTag("key1", "value1", false)], false);
        DocumentTypeTagAdded addEvent = new("doc-type-1", "key2", "value2", false);

        // Act
        ApplyResult result = documentType.Apply(addEvent);

        // Assert
        Assert.False(result.Failed);
        DocumentType updatedDocumentType = (DocumentType)result.Aggregate;
        Assert.Contains(updatedDocumentType.Tags, tag => tag.Key == "key1" && tag.Value == "value1");
        Assert.Contains(updatedDocumentType.Tags, tag => tag.Key == "key2" && tag.Value == "value2");
    }

    [Fact]
    public void ApplyDocumentTypeTagAddedWhenUniqueKeyExistsShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], [], [new DocumentTag("key1", "value1", true)], false);
        DocumentTypeTagAdded addEvent = new("doc-type-1", "key1", "value2", false);

        // Act
        ApplyResult result = documentType.Apply(addEvent);

        // Assert
        Assert.True(result.Failed);
        Assert.Contains("The unique tag with key=key1 already exists in document type doc-type-1/Test.", result.Reason, StringComparison.InvariantCulture);
    }

    [Fact]
    public void ApplyDocumentTypeTagRemovedWhenDoesNotExistShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], [], [new DocumentTag("key1", "value1", false)], false);
        DocumentTypeTagRemoved removeEvent = new("doc-type-1", "key2", "value2");

        // Act
        ApplyResult result = documentType.Apply(removeEvent);

        // Assert
        Assert.True(result.Failed);
        Assert.Contains("The tag key2 does not exist in document type doc-type-1/Test.", result.Reason, StringComparison.InvariantCulture);
    }

    [Fact]
    public void ApplyDocumentTypeTagRemovedWhenExistsShouldSucceed()
    {
        // Arrange
        DocumentType documentType = new(
            "doc-type-1",
            "Test",
            null,
            [],
            [],
            [new DocumentTag("key1", "value1", false), new DocumentTag("key2", "value2", false)],
            false);
        DocumentTypeTagRemoved removeEvent = new("doc-type-1", "key1", "value1");

        // Act
        ApplyResult result = documentType.Apply(removeEvent);

        // Assert
        Assert.False(result.Failed);
        DocumentType updatedDocumentType = (DocumentType)result.Aggregate;
        Assert.DoesNotContain(updatedDocumentType.Tags, tag => tag.Key == "key1" && tag.Value == "value1");
        Assert.Contains(updatedDocumentType.Tags, tag => tag.Key == "key2" && tag.Value == "value2");
    }

    [Fact]
    public void ApplyInvalidEventShouldReturnInvalidEvent()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], [], [], false);
        object invalidEvent = new();

        // Act
        ApplyResult result = documentType.Apply(invalidEvent);

        // Assert
        Assert.True(result.Failed);
        Assert.Equal(documentType, result.Aggregate);
    }

    [Fact]
    public void ApplyNullDomainEventShouldThrowArgumentNullException()
    {
        // Arrange
        DocumentType documentType = new();

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>(() => documentType.Apply(null!));
    }

    [Fact]
    public void ApplyWhenDisabledShouldAllowEnableEvent()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], [], [], true);
        DocumentTypeEnabled enableEvent = new("doc-type-1");

        // Act
        ApplyResult result = documentType.Apply(enableEvent);

        // Assert
        Assert.False(result.Failed);
        DocumentType updatedDocumentType = (DocumentType)result.Aggregate;
        Assert.False(updatedDocumentType.Disabled);
    }

    [Fact]
    public void ApplyWhenDisabledShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new("doc-type-1", "Test", null, [], [], [], true);
        DocumentTypeFileTypeAdded fileTypeAdded = new("doc-type-1", "pdf");

        // Act
        ApplyResult result = documentType.Apply(fileTypeAdded);

        // Assert
        Assert.True(result.Failed);
        Assert.Equal("Cannot apply changes to a disabled document type.", result.Reason);
    }

    [Fact]
    public void ApplyWhenNotInitializedShouldReturnError()
    {
        // Arrange
        DocumentType documentType = new();
        DocumentTypeFileTypeAdded fileTypeAdded = new("doc-type-1", "pdf");

        // Act
        ApplyResult result = documentType.Apply(fileTypeAdded);

        // Assert
        Assert.True(result.Failed);
        Assert.Equal("Cannot apply changes to an uninitialized document type.", result.Reason);
    }

    [Fact]
    public void ConstructorDefaultShouldCreateEmptyDocumentType()
    {
        // Arrange & Act
        DocumentType documentType = new();

        // Assert
        Assert.Equal(string.Empty, documentType.Id);
        Assert.Equal(string.Empty, documentType.Name);
        Assert.Null(documentType.Comments);
        Assert.Empty(documentType.DataExtractionIds);
        Assert.Empty(documentType.FileTypeIds);
        Assert.Empty(documentType.Tags);
        Assert.False(documentType.Disabled);
    }

    [Fact]
    public void ConstructorWithDocumentTypeAddedShouldCreateDocumentType()
    {
        // Arrange
        string id = "doc-type-1";
        string name = "Invoice";
        string description = "Invoice document type";
        string[] dataExtractionIds = ["extraction-1", "extraction-2"];
        string[] fileTypeIds = ["pdf", "docx"];
        DocumentTypeAdded added = new(id, name, description, dataExtractionIds, fileTypeIds);

        // Act
        DocumentType documentType = new(added);

        // Assert
        Assert.Equal(id, documentType.Id);
        Assert.Equal(name, documentType.Name);
        Assert.Equal(description, documentType.Comments);
        Assert.Equal(dataExtractionIds, documentType.DataExtractionIds);
        Assert.Equal(fileTypeIds, documentType.FileTypeIds);
        Assert.Empty(documentType.Tags);
        Assert.False(documentType.Disabled);
    }

    [Fact]
    public void ConstructorWithNullDocumentTypeAddedShouldThrowArgumentNullException() =>
        Assert.Throws<ArgumentNullException>(() => new DocumentType(null!));
}