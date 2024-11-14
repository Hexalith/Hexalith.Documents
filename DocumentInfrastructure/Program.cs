// <copyright file="Program.cs" company="Hexalith SAS Paris France">
//     Copyright (c) Hexalith SAS Paris France. All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>

using Hexalith.Infrastructure.AspireService.Hosting.Helpers;

using Microsoft.Extensions.Configuration;

HexalithDistributedApplication app = new(args);

app.Builder.AddForwardedHeaders();

if (app.Builder.ExecutionContext.IsRunMode)
{
    Console.WriteLine($"Starting environment {app.Builder.Environment.EnvironmentName}");
    _ = app
        .Builder
        .AddExecutable("dapr-dashboard", "dapr", ".", "dashboard")
        .WithHttpEndpoint(port: 8080, targetPort: 8080, name: "dashboard-http", isProxied: false);
}

app.Builder.Configuration.AddUserSecrets<Program>();

if (app.IsProjectEnabled<Projects.HexalithApp_WebServer>())
{
    _ = app
        .AddProject<Projects.HexalithApp_WebServer>("documents-web")
        .WithEnvironmentFromConfiguration("Hexalith__EasyAuthentication__UseMsal")
        .WithEnvironmentFromConfiguration("Hexalith__EasyAuthentication__Enabled")
        .WithEnvironmentFromConfiguration("AzureAd__Instance")
        .WithEnvironmentFromConfiguration("AzureAd__Domain")
        .WithEnvironmentFromConfiguration("AzureAd__TenantId")
        .WithEnvironmentFromConfiguration("AzureAd__ClientId");
}

if (app.IsProjectEnabled<Projects.HexalithApp_WebServer>())
{
    _ = app
        .AddProject<Projects.HexalithApp_ApiServer>("documents-api");
}

await app
    .Builder
    .Build()
    .RunAsync()
    .ConfigureAwait(false);