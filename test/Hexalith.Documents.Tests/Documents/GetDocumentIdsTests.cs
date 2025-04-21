// <copyright file="GetDocumentIdsTests.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Tests.Documents;

using Hexalith.Documents.Requests.Documents;

using Shouldly;

/// <summary>
/// Tests for the <see cref="GetDocumentIds"/> class.
/// </summary>
public class GetDocumentIdsTests
{
    /// <summary>
    /// Tests that CreateNextChunkRequest creates a request with the correct skip and take values.
    /// </summary>
    [Fact]
    public void CreateNextChunkRequestShouldCreateRequestWithCorrectSkipAndTake()
    {
        // Arrange
        var request = new GetDocumentIds(10, 20);

        // Act
        Application.Requests.IChunkableRequest nextRequest = request.CreateNextChunkRequest();

        // Assert
        _ = nextRequest.ShouldBeOfType<GetDocumentIds>();
        var typedNextRequest = (GetDocumentIds)nextRequest;
        typedNextRequest.Skip.ShouldBe(30); // 10 + 20
        typedNextRequest.Take.ShouldBe(20);
        typedNextRequest.Results.ShouldBeEmpty();
    }

    /// <summary>
    /// Tests that CreateResults creates a request with the correct results.
    /// </summary>
    [Fact]
    public void CreateResultsShouldCreateRequestWithCorrectResults()
    {
        // Arrange
        var request = new GetDocumentIds(10, 20);
        object[] results = ["id1", "id2", "id3"];

        // Act
        Application.Requests.ICollectionRequest resultsRequest = request.CreateResults(results);

        // Assert
        _ = resultsRequest.ShouldBeOfType<GetDocumentIds>();
        var typedResultsRequest = (GetDocumentIds)resultsRequest;
        typedResultsRequest.Skip.ShouldBe(10);
        typedResultsRequest.Take.ShouldBe(20);
        typedResultsRequest.Results.ShouldBe(results.Cast<string>());
    }

    /// <summary>
    /// Tests that the default constructor initializes properties correctly.
    /// </summary>
    [Fact]
    public void DefaultConstructorShouldInitializePropertiesCorrectly()
    {
        // Arrange & Act
        var request = new GetDocumentIds();

        // Assert
        request.Skip.ShouldBe(0);
        request.Take.ShouldBe(0);
        _ = request.Results.ShouldNotBeNull();
        request.Results.ShouldBeEmpty();
    }

    /// <summary>
    /// Tests that the constructor with all parameters initializes properties correctly.
    /// </summary>
    [Fact]
    public void FullConstructorShouldInitializePropertiesCorrectly()
    {
        // Arrange
        string[] results = ["id1", "id2", "id3"];

        // Act
        var request = new GetDocumentIds(10, 20, results);

        // Assert
        request.Skip.ShouldBe(10);
        request.Take.ShouldBe(20);
        request.Results.ShouldBe(results);
    }

    /// <summary>
    /// Tests that the constructor with skip and take parameters initializes properties correctly.
    /// </summary>
    [Fact]
    public void SkipTakeConstructorShouldInitializePropertiesCorrectly()
    {
        // Arrange & Act
        var request = new GetDocumentIds(10, 20);

        // Assert
        request.Skip.ShouldBe(10);
        request.Take.ShouldBe(20);
        _ = request.Results.ShouldNotBeNull();
        request.Results.ShouldBeEmpty();
    }
}