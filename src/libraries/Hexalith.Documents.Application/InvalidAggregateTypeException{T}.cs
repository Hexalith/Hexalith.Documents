// <copyright file="InvalidAggregateTypeException{T}.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Application;

using System;
using System.Text.Json;

using Hexalith.Domains;

/// <summary>
/// Exception thrown when an invalid aggregate type is encountered.
/// </summary>
/// <typeparam name="T">The expected aggregate type.</typeparam>
public class InvalidAggregateTypeException<T> : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidAggregateTypeException{T}"/> class.
    /// </summary>
    public InvalidAggregateTypeException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidAggregateTypeException{T}"/> class.
    /// </summary>
    /// <param name="aggregate">The aggregate.</param>
    public InvalidAggregateTypeException(IDomainAggregate aggregate)
        : base($"The exected type is {typeof(T).Name} but aggregate is {aggregate?.GetType().Name ?? "null"}: {JsonSerializer.Serialize(aggregate)}")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidAggregateTypeException{T}"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public InvalidAggregateTypeException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidAggregateTypeException{T}"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="innerException">The inner exception.</param>
    public InvalidAggregateTypeException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}