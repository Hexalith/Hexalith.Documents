// <copyright file="AddDocumentValidator.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.Documents;

using FluentValidation;

using Microsoft.Extensions.Localization;

using Labels = Hexalith.Documents.Localizations.Documents;

/// <summary>
/// Validator for the AddDocument command.
/// </summary>
public class AddDocumentValidator : DocumentCommandValidator<AddDocument>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddDocumentValidator"/> class.
    /// </summary>
    /// <param name="l">The string localizer for labels.</param>
    public AddDocumentValidator(IStringLocalizer<Labels> l)
        : base(l)
    {
        _ = RuleFor(command => command.DocumentContainerId)
                .NotEmpty()
                .WithMessage(l[nameof(Labels.ContainerRequired)]);
        _ = RuleFor(command => command.DocumentTypeId)
                .NotEmpty()
                .WithMessage(l[nameof(Labels.TypeRequired)]);
        _ = RuleFor(command => command.Name)
                .NotEmpty()
                .WithMessage(l[nameof(Labels.NameRequired)]);
    }
}