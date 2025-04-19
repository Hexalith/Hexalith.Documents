// <copyright file="DocumentCommandValidator.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.Documents;

using FluentValidation;

using Microsoft.Extensions.Localization;

using Labels = Hexalith.Documents.Localizations.Documents;

/// <summary>
/// Validator for DocumentCommand.
/// </summary>
/// <typeparam name="TCommand">The type of the document command.</typeparam>
public class DocumentCommandValidator<TCommand> : AbstractValidator<TCommand>
    where TCommand : DocumentCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentCommandValidator{TCommand}"/> class.
    /// </summary>
    /// <param name="l">The string localizer for labels.</param>
    public DocumentCommandValidator(IStringLocalizer<Labels> l)
    {
        ArgumentNullException.ThrowIfNull(l);
        _ = RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage(l[Labels.IdRequired]);
    }
}