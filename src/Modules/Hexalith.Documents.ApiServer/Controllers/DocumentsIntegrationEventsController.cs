namespace Hexalith.Documents.ApiServer.Controllers;

using Dapr;

using Hexalith.Application.Events;
using Hexalith.Application.Projections;
using Hexalith.Application.States;
using Hexalith.Documents.Domain;
using Hexalith.Infrastructure.WebApis.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// Class PartiesPubSubController.
/// Implements the <see cref="EventIntegrationController" />.
/// </summary>
/// <seealso cref="EventIntegrationController" />
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentsIntegrationEventsController"/> class.
/// </remarks>
/// <param name="eventProcessor">The event processor.</param>
/// <param name="projectionProcessor">The projection processor.</param>
/// <param name="hostEnvironment">The host environment.</param>
/// <param name="logger">The logger.</param>
[ApiController]
public abstract class DocumentsIntegrationEventsController(
    IIntegrationEventProcessor eventProcessor,
    IProjectionUpdateProcessor projectionProcessor,
    IHostEnvironment hostEnvironment,
    ILogger logger) : EventIntegrationController(eventProcessor, projectionProcessor, hostEnvironment, logger)
{
    /// <summary>
    /// Handle aggregate external reference events as an asynchronous operation.
    /// </summary>
    /// <param name="eventState">State of the event.</param>
    /// <returns>A Task&lt;ActionResult&gt; representing the asynchronous operation.</returns>
    [FileTypeEventsBusTopic]
    [TopicMetadata("requireSessions", "true")]
    [TopicMetadata("sessionIdleTimeoutInSec ", "15")]
    [TopicMetadata("maxConcurrentSessions", "32")]
    [HttpPost("api/events/documents/filetype")]
    [SwaggerOperation(Summary = "Handles file type events", Description = "Processes file type events and updates projections accordingly.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Event processed successfully.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid event data.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "An error occurred while processing the event.")]
    public async Task<ActionResult> HandleFileTypeEventsAsync(MessageState eventState)
         => await HandleEventAsync(
                eventState,
                DocumentDomainHelper.FileTypeAggregateName,
                CancellationToken.None)
             .ConfigureAwait(false);
}