// <copyright file="DocumentEditValidation.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.UI.Pages.Documents;

using FluentValidation;

using Hexalith.UI.Components.Validations;

using Microsoft.Extensions.Localization;

using Labels = Hexalith.Documents.UI.Pages.Resources.Documents;

/// <summary>
/// Validator for adding a new document type.
/// </summary>
public class DocumentEditValidation : EntityValidation<DocumentEditViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentEditValidation"/> class.
    /// </summary>
    /// <param name="l">The localizer for retrieving localized strings.</param>
    public DocumentEditValidation(IStringLocalizer<Labels> l)
    {
        ArgumentNullException.ThrowIfNull(l);
        _ = RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(l[nameof(Labels.IdRequired)]);
        _ = RuleFor(x => x.DocumentContainerId)
            .NotEmpty()
            .WithMessage(l[nameof(Labels.ContainerRequired)]);
        _ = RuleFor(x => x.DocumentTypeId)
            .NotEmpty()
            .WithMessage(l[nameof(Labels.TypeRequired)]);
        _ = RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(l[nameof(Labels.NameRequired)]);
    }
}