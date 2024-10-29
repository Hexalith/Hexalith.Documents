namespace Hexalith.Document.Domain.ValueObjects;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the type of document point in the document management system.
/// </summary>
/// <remarks>
/// This enum is used to categorize different types of document information.
/// It allows for consistent classification of document points throughout the application,
/// enabling proper handling and validation of different document information types.
/// The [JsonConverter(typeof(JsonStringEnumConverter))] attribute is applied to this enum to ensure that
/// when serialized to JSON, the enum values are represented as strings rather than integer values.
/// This improves readability and maintainability of the JSON output, especially when working with APIs
/// or storing data in JSON format.
/// 
/// Example JSON representation:
/// {
///     "documentPointType": "Email"
/// }
/// Instead of:
/// {
///     "documentPointType": 2
/// }
/// 
/// When using this enum, consider implementing appropriate validation for each document point type
/// to ensure the integrity of the stored data.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentPointType
{
    /// <summary>
    /// Represents a phone number document point.
    /// </summary>
    /// <remarks>
    /// This can include various types of phone numbers such as landline or work numbers.
    /// Example: +1 (555) 123-4567.
    /// </remarks>
    Phone = 1,

    /// <summary>
    /// Represents a mobile phone number document point.
    /// </summary>
    /// <remarks>
    /// This is specifically used for mobile or cellular phone numbers.
    /// Example: +1 (555) 987-6543.
    /// </remarks>
    Mobile = 2,

    /// <summary>
    /// Represents an email address document point.
    /// </summary>
    /// <remarks>
    /// This is used for electronic mail addresses.
    /// Example: "example@email.com"
    /// </remarks>
    Email = 3,

    /// <summary>
    /// Represents a postal address document point.
    /// </summary>
    /// <remarks>
    /// This is used for physical mailing addresses.
    /// Example: "123 Main St, Anytown, ST 12345, Country".
    /// </remarks>
    PostalAddress = 4,

    /// <summary>
    /// Represents a social media document point.
    /// </summary>
    /// <remarks>
    /// This can include various social media platforms such as Twitter, Facebook, LinkedIn, etc.
    /// Example: "@username" or "https://www.linkedin.com/in/username".
    /// </remarks>
    SocialMedia = 5,

    /// <summary>
    /// Represents any other type of document point not covered by the specific categories.
    /// </summary>
    /// <remarks>
    /// This can be used for custom or less common types of document information.
    /// Example: "Skype: username" or "Fax: +1 (555) 987-6543".
    /// </remarks>
    Other = 0,
}
