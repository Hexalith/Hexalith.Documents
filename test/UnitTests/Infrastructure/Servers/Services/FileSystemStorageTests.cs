namespace UnitTests.Infrastructure.Servers.Services;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using Hexalith.Documents.Application.Services;
using Hexalith.Documents.Servers.Configurations;
using Hexalith.Documents.Servers.Services;

using Microsoft.Extensions.Options;

using Xunit;

/// <summary>
/// Tests for the <see cref="FileSystemStorage"/> class.
/// </summary>
public sealed class FileSystemStorageTests : IDisposable
{
    private readonly FileSystemStorage _storage;
    private readonly string _testPath;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileSystemStorageTests"/> class.
    /// </summary>
    public FileSystemStorageTests()
    {
        _testPath = Path.Combine(Path.GetTempPath(), "FileSystemStorageTests", Guid.NewGuid().ToString());
        _ = Directory.CreateDirectory(_testPath);
        IOptions<LocalStorageSettings> options = Options.Create(new LocalStorageSettings { Path = _testPath });
        _storage = new FileSystemStorage(options);
    }

    [Fact]
    public async Task CreateFileAsyncShouldCreateFileWhenValidPathAndFileName()
    {
        // Arrange
        string path = "test/folder";
        string fileName = "testfile.txt";

        // Act
        await using IWritableFile file = await _storage.CreateFileAsync(path, fileName, CancellationToken.None);
        file.Stream.Write(new ReadOnlySpan<byte>(System.Text.Encoding.UTF8.GetBytes("hello")));

        // Assert
        string expectedPath = Path.Combine(_testPath, path, fileName);
        _ = File.Exists(expectedPath).Should().BeTrue();
        _ = file.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateFileAsyncShouldRespectCancellation()
    {
        // Arrange
        using CancellationTokenSource cts = new();
        await cts.CancelAsync();

        // Act & Assert
        _ = await FluentActions
            .Awaiting(async () =>
            {
                await using IWritableFile file = await _storage.CreateFileAsync("test", "file.txt", cts.Token);
            })
            .Should()
            .ThrowAsync<OperationCanceledException>();
    }

    // [Fact]
    // public async Task CreateFileAsyncShouldThrowArgumentExceptionWhenFileNameContainsInvalidCharacters()
    // {
    //    // Arrange
    //    string path = "test";
    //    string fileName = "file<>:.txt";

    // // Act & Assert
    //    _ = await FluentActions
    //        .Awaiting(async () =>
    //        {
    //            await using IWritableFile file = await _storage.CreateFileAsync(path, fileName, CancellationToken.None);
    //        })
    //        .Should()
    //        .ThrowAsync<ArgumentException>()
    //        .WithMessage("The file name contains invalid characters.*");
    // }
    [Fact]
    public async Task CreateFileAsyncShouldThrowArgumentExceptionWhenPathIsRooted()
    {
        // Arrange
        string path = Path.GetFullPath("C:/test");
        string fileName = "file.txt";

        // Act & Assert
        _ = await FluentActions
            .Awaiting(async () =>
            {
                await using IWritableFile file = await _storage.CreateFileAsync(path, fileName, CancellationToken.None);
            })
            .Should()
            .ThrowAsync<ArgumentException>()
            .WithMessage("The path must be relative.*");
    }

    [Theory]
    [InlineData(null, "file.txt")]
    [InlineData("", "file.txt")]
    [InlineData("  ", "file.txt")]
    [InlineData("path", null)]
    [InlineData("path", "")]
    [InlineData("path", "  ")]
    public async Task CreateFileAsyncShouldThrowArgumentExceptionWhenPathOrFileNameIsInvalid(string path, string fileName) =>

            // Act & Assert
            await FluentActions
                .Awaiting(async () =>
                {
                    await using IWritableFile file = await _storage.CreateFileAsync(path, fileName, CancellationToken.None);
                })
                .Should()
                .ThrowAsync<ArgumentException>();

    [Fact]
    public async Task CreateFileAsyncShouldThrowInvalidOperationExceptionWhenFileAlreadyExists()
    {
        // Arrange
        string path = "test";
        string fileName = "existing.txt";
        string fullPath = Path.Combine(_testPath, path);
        _ = Directory.CreateDirectory(fullPath);
        await File.WriteAllTextAsync(Path.Combine(fullPath, fileName), string.Empty);

        // Act & Assert
        _ = await FluentActions
            .Awaiting(async () =>
            {
                await using IWritableFile file = await _storage.CreateFileAsync(path, fileName, CancellationToken.None);
            })
            .Should()
            .ThrowAsync<InvalidOperationException>()
            .WithMessage($"The file '*{fileName}' already exists on the file system.");
    }

    /// <summary>
    /// Cleanup test resources.
    /// </summary>
    public void Dispose()
    {
        if (Directory.Exists(_testPath))
        {
            Directory.Delete(_testPath, recursive: true);
        }

        GC.SuppressFinalize(this);
    }
}