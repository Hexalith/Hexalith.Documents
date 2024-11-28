# Document Type Commands

This project contains the application commands for Document Type management in the Hexalith framework. Document Types are used to categorize and manage different kinds of documents in the system.

## Available Commands

### AddDocumentType
Creates a new document type with the following properties:
- `Id`: Unique identifier for the document type
- `Name`: Display name of the document type
- `Description`: Detailed description of the document type's purpose

### ChangeDocumentTypeDescription
Updates an existing document type's name and description:
- `Id`: Identifier of the document type to update
- `Name`: New name for the document type
- `Description`: New description for the document type

### EnableDocumentType
Activates a document type in the system:
- `Id`: Identifier of the document type to enable

### DisableDocumentType
Deactivates a document type in the system:
- `Id`: Identifier of the document type to disable

## Base Command

All document type commands inherit from `DocumentTypeCommand` which provides:
- Common `Id` property for aggregate identification
- Static `AggregateName` property linking to the document domain

## Usage

These commands are used in conjunction with the document management system to maintain document type definitions. They support the polymorphic serialization pattern for proper serialization/deserialization in the system.


