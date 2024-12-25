namespace UnitTests.Domain;

using FluentAssertions;

using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Domain.Aggregates;

/// <summary>
/// Tests for the FileType aggregate.
/// </summary>
public class FileTypeTest
{
    private static readonly string[] _expected = new[] { "Target1", "Target2" };
    private static readonly string[] _targets = new[] { "Target1" };
    private static readonly string[] _targetsArray = new[] { "Target2" };

    /// <summary>
    /// Tests that a FileTypeAdded event can be applied to an uninitialized FileType.
    /// </summary>
    [Fact]
    public void ShouldApplyFileTypeAddedEventToUninitializedFileType()
    {
        FileType fileType = new();
        FileTypeAdded addedEvent = new("1", "PDF", "PDF File", "PDFConverter", _targets);
        ApplyResult result = fileType.Apply(addedEvent);
        _ = result.Failed.Should().BeFalse();
        FileType updatedFileType = result.Aggregate as FileType;
        _ = updatedFileType.Id.Should().Be("1");
        _ = updatedFileType.Name.Should().Be("PDF");
    }

    /// <summary>
    /// Tests that a FileTypeDescriptionChanged event can be applied when the description is different.
    /// </summary>
    [Fact]
    public void ShouldApplyFileTypeDescriptionChangedEventWithDifferentDescription()
    {
        FileType fileType = new("1", "PDF", "Old Description", "PDFConverter", _targets, false);
        FileTypeDescriptionChanged descriptionChangedEvent = new("1", "PDF", "New Description");
        ApplyResult result = fileType.Apply(descriptionChangedEvent);
        _ = result.Failed.Should().BeFalse();
        FileType updatedFileType = result.Aggregate as FileType;
        _ = updatedFileType.Description.Should().Be("New Description");
    }

    /// <summary>
    /// Tests that a FileTypeDisabled event can be applied to an enabled FileType.
    /// </summary>
    [Fact]
    public void ShouldApplyFileTypeDisabledEventToEnabledFileType()
    {
        FileType fileType = new("1", "PDF", "PDF File", "PDFConverter", _targets, false);
        FileTypeDisabled disabledEvent = new("1");
        ApplyResult result = fileType.Apply(disabledEvent);
        _ = result.Failed.Should().BeFalse();
        FileType updatedFileType = result.Aggregate as FileType;
        _ = updatedFileType.Disabled.Should().BeTrue();
    }

    /// <summary>
    /// Tests that a FileTypeEnabled event can be applied to a disabled FileType.
    /// </summary>
    [Fact]
    public void ShouldApplyFileTypeEnabledEventToDisabledFileType()
    {
        FileType fileType = new("1", "PDF", "PDF File", "PDFConverter", _targets, true);
        FileTypeEnabled enabledEvent = new("1");
        ApplyResult result = fileType.Apply(enabledEvent);
        _ = result.Failed.Should().BeFalse();
        FileType updatedFileType = result.Aggregate as FileType;
        _ = updatedFileType.Disabled.Should().BeFalse();
    }

    /// <summary>
    /// Tests that a FileTypeFileToTextConverterChanged event can be applied when the converter is different.
    /// </summary>
    [Fact]
    public void ShouldApplyFileTypeFileToTextConverterChangedEventWithDifferentConverter()
    {
        FileType fileType = new("1", "PDF", "PDF File", "OldConverter", _targets, false);
        FileTypeFileToTextConverterChanged converterChangedEvent = new("1", "NewConverter");
        ApplyResult result = fileType.Apply(converterChangedEvent);
        _ = result.Failed.Should().BeFalse();
        FileType updatedFileType = result.Aggregate as FileType;
        _ = updatedFileType.FileToTextConverter.Should().Be("NewConverter");
    }

    /// <summary>
    /// Tests that a FileTypeTargetAdded event can be applied with a new target.
    /// </summary>
    [Fact]
    public void ShouldApplyFileTypeTargetAddedEventWithNewTarget()
    {
        FileType fileType = new("1", "PDF", "PDF File", "PDFConverter", _targets, false);
        FileTypeTargetAdded targetAddedEvent = new("1", "Target2");
        ApplyResult result = fileType.Apply(targetAddedEvent);
        _ = result.Failed.Should().BeFalse();
        FileType updatedFileType = result.Aggregate as FileType;
        _ = updatedFileType.Targets.Should().Contain(_expected);
    }

    /// <summary>
    /// Tests that a FileTypeTargetRemoved event can be applied with an existing target.
    /// </summary>
    [Fact]
    public void ShouldApplyFileTypeTargetRemovedEventWithExistingTarget()
    {
        FileType fileType = new("1", "PDF", "PDF File", "PDFConverter", _targets, false);
        FileTypeTargetRemoved targetRemovedEvent = new("1", "Target1");
        ApplyResult result = fileType.Apply(targetRemovedEvent);
        _ = result.Failed.Should().BeFalse();
        FileType updatedFileType = result.Aggregate as FileType;
        _ = updatedFileType.Targets.Should().NotContain("Target1");
    }

    /// <summary>
    /// Tests that a FileType can be initialized from a FileTypeAdded event.
    /// </summary>
    [Fact]
    public void ShouldInitializeFileTypeFromFileTypeAddedEvent()
    {
        FileTypeAdded addedEvent = new("1", "PDF", "PDF File", "PDFConverter", _targets);
        FileType fileType = new(addedEvent);
        _ = fileType.Id.Should().Be("1");
        _ = fileType.Name.Should().Be("PDF");
        _ = fileType.Description.Should().Be("PDF File");
        _ = fileType.FileToTextConverter.Should().Be("PDFConverter");
        _ = fileType.Targets.Should().ContainSingle().Which.Should().Be("Target1");
        _ = fileType.Disabled.Should().BeFalse();
    }

    /// <summary>
    /// Tests that a FileType is initialized with default values when created with the parameterless constructor.
    /// </summary>
    [Fact]
    public void ShouldInitializeFileTypeWithDefaultValues()
    {
        FileType fileType = new();
        _ = fileType.Id.Should().BeEmpty();
        _ = fileType.Name.Should().BeEmpty();
        _ = fileType.Description.Should().BeNull();
        _ = fileType.FileToTextConverter.Should().BeNull();
        _ = fileType.Targets.Should().BeEmpty();
        _ = fileType.Disabled.Should().BeFalse();
    }

    /// <summary>
    /// Tests that a FileTypeAdded event cannot be applied to an already initialized FileType.
    /// </summary>
    [Fact]
    public void ShouldNotApplyFileTypeAddedEventToInitializedFileType()
    {
        FileType fileType = new("1", "PDF", "PDF File", "PDFConverter", _targets, false);
        FileTypeAdded addedEvent = new("2", "DOC", "DOC File", "DOCConverter", _targetsArray);
        ApplyResult result = fileType.Apply(addedEvent);
        _ = result.Failed.Should().BeTrue();
    }

    /// <summary>
    /// Tests that a FileTypeDescriptionChanged event cannot be applied when the description is the same.
    /// </summary>
    [Fact]
    public void ShouldNotApplyFileTypeDescriptionChangedEventWithSameDescription()
    {
        FileType fileType = new("1", "PDF", "Same Description", "PDFConverter", _targets, false);
        FileTypeDescriptionChanged descriptionChangedEvent = new("1", "PDF", "Same Description");
        ApplyResult result = fileType.Apply(descriptionChangedEvent);
        _ = result.Failed.Should().BeTrue();
    }

    /// <summary>
    /// Tests that a FileTypeDisabled event cannot be applied to an already disabled FileType.
    /// </summary>
    [Fact]
    public void ShouldNotApplyFileTypeDisabledEventToDisabledFileType()
    {
        FileType fileType = new("1", "PDF", "PDF File", "PDFConverter", _targets, true);
        FileTypeDisabled disabledEvent = new("1");
        ApplyResult result = fileType.Apply(disabledEvent);
        _ = result.Failed.Should().BeTrue();
    }

    /// <summary>
    /// Tests that a FileTypeEnabled event cannot be applied to an already enabled FileType.
    /// </summary>
    [Fact]
    public void ShouldNotApplyFileTypeEnabledEventToEnabledFileType()
    {
        FileType fileType = new("1", "PDF", "PDF File", "PDFConverter", _targets, false);
        FileTypeEnabled enabledEvent = new("1");
        ApplyResult result = fileType.Apply(enabledEvent);
        _ = result.Failed.Should().BeTrue();
    }

    /// <summary>
    /// Tests that a FileTypeFileToTextConverterChanged event cannot be applied when the converter is the same.
    /// </summary>
    [Fact]
    public void ShouldNotApplyFileTypeFileToTextConverterChangedEventWithSameConverter()
    {
        FileType fileType = new("1", "PDF", "PDF File", "SameConverter", _targets, false);
        FileTypeFileToTextConverterChanged converterChangedEvent = new("1", "SameConverter");
        ApplyResult result = fileType.Apply(converterChangedEvent);
        _ = result.Failed.Should().BeTrue();
    }

    /// <summary>
    /// Tests that a FileTypeTargetAdded event cannot be applied with an existing target.
    /// </summary>
    [Fact]
    public void ShouldNotApplyFileTypeTargetAddedEventWithExistingTarget()
    {
        FileType fileType = new("1", "PDF", "PDF File", "PDFConverter", _targets, false);
        FileTypeTargetAdded targetAddedEvent = new("1", "Target1");
        ApplyResult result = fileType.Apply(targetAddedEvent);
        _ = result.Failed.Should().BeTrue();
    }

    /// <summary>
    /// Tests that a FileTypeTargetRemoved event cannot be applied with a non-existing target.
    /// </summary>
    [Fact]
    public void ShouldNotApplyFileTypeTargetRemovedEventWithNonExistingTarget()
    {
        FileType fileType = new("1", "PDF", "PDF File", "PDFConverter", _targets, false);
        FileTypeTargetRemoved targetRemovedEvent = new("1", "NonExistingTarget");
        ApplyResult result = fileType.Apply(targetRemovedEvent);
        _ = result.Failed.Should().BeTrue();
    }
}