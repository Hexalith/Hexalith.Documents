# Document Commands

This project contains the application commands for the Document application. These commands are used to manage documents in the system, including creation, modification, and status changes.

## Available Commands

- **AddDocument**: Creates a new document with basic information and file details
- **AddDocumentActor**: Adds an actor (participant) to a document
- **ChangeDocumentDescription**: Updates the name and description of a document
- **DisableDocument**: Marks a document as disabled
- **EnableDocument**: Marks a document as enabled
- **RemoveDocumentActor**: Removes an actor from a document
- **RemoveDocumentPoint**: Removes a point/section from a document
- **SumarizeDocument**: Adds or updates a document's summary

## Command Structure

All document commands inherit from the base `DocumentCommand` class, which provides:
- A unique document identifier (`Id`)
- Aggregate ID and name functionality for event sourcing
- Polymorphic serialization support

## Usage

Commands can be dispatched through the application's command handling system. Each command is designed to be immutable and contains all necessary information to perform its specific operation.

