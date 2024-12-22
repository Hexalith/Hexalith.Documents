namespace Hexalith.Documents.Projections.DataExports.Services;

using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Provides demo data export data for testing and demonstration purposes.
/// This static class contains sample data exports that can be used during development and testing.
/// </summary>
public static class DataExportQuickStartData
{
    /// <summary>
    /// Gets a collection of sample data export details.
    /// </summary>
    /// <value>
    /// An enumerable collection of <see cref="DataExportDetailsViewModel"/> containing predefined data exports.
    /// </value>
    public static IEnumerable<DataExportDetailsViewModel> Data => [Export1, Export2];

    /// <summary>
    /// Gets the details for the Excel data export.
    /// </summary>
    internal static DataExportDetailsViewModel Export1 => new(
        "Export1",
        556893,
        DateTimeOffset.Now.AddDays(-10),
        DateTimeOffset.Now.AddDays(-10).AddSeconds(10));

    /// <summary>
    /// Gets the details for the Excel data export.
    /// </summary>
    internal static DataExportDetailsViewModel Export2 => new(
        "Export2",
        1556893,
        DateTimeOffset.Now.AddDays(-10),
        DateTimeOffset.Now.AddDays(-10).AddSeconds(1000));
}