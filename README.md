# Hexalith.Documents

This is a template repository for creating new Hexalith packages. The repository provides a structured starting point for developing new packages within the Hexalith ecosystem.

## Build Status

[![License: MIT](https://img.shields.io/github/license/hexalith/hexalith.Documents)](https://github.com/hexalith/hexalith/blob/main/LICENSE)
[![Discord](https://img.shields.io/discord/1063152441819942922?label=Discord&logo=discord&logoColor=white&color=d82679)](https://discordapp.com/channels/1102166958918610994/1102166958918610997)

[![Coverity Scan Build Status](https://scan.coverity.com/projects/31529/badge.svg)](https://scan.coverity.com/projects/hexalith-hexalith-Documents)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/d48f6d9ab9fb4776b6b4711fc556d1c4)](https://app.codacy.com/gh/Hexalith/Hexalith.Documents/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Hexalith_Hexalith.Documents&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Hexalith_Hexalith.Documents)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=Hexalith_Hexalith.Documents&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=Hexalith_Hexalith.Documents)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=Hexalith_Hexalith.Documents&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=Hexalith_Hexalith.Documents)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=Hexalith_Hexalith.Documents&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=Hexalith_Hexalith.Documents)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=Hexalith_Hexalith.Documents&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=Hexalith_Hexalith.Documents)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=Hexalith_Hexalith.Documents&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=Hexalith_Hexalith.Documents)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=Hexalith_Hexalith.Documents&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=Hexalith_Hexalith.Documents)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=Hexalith_Hexalith.Documents&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=Hexalith_Hexalith.Documents)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=Hexalith_Hexalith.Documents&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=Hexalith_Hexalith.Documents)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Hexalith_Hexalith.Documents&metric=bugs)](https://sonarcloud.io/summary/new_code?id=Hexalith_Hexalith.Documents)

[![Build status](https://github.com/Hexalith/Hexalith.Documents/actions/workflows/build-release.yml/badge.svg)](https://github.com/Hexalith/Hexalith.Documents/actions)
[![NuGet](https://img.shields.io/nuget/v/Hexalith.Documents.svg)](https://www.nuget.org/packages/Hexalith.Documents)
[![Latest](https://img.shields.io/github/v/release/Hexalith/Hexalith.Documents?include_prereleases&label=preview)](https://github.com/Hexalith/Hexalith.Documents/pkgs/nuget/Hexalith.Documents)

## Overview

This repository provides a template for creating new Hexalith packages. It includes all the necessary configuration files, directory structure, and GitHub workflow configurations to ensure consistency across Hexalith packages.

## Repository Structure

The repository is organized as follows:

- [src](./src/README.md) Is the source code directory of your project.
- [src/libraries](./src/libraries/README.md) Is the source code directory where you will add your Nuget package projects.
- [src/examples](./src/examples/README.md) Contains example implementations of your projects.
- [src/servers](./src/servers/README.md) Is the source code directory where you will add your Docker container projects.
- [src/aspire](./src/aspire/README.md) Is the source code directory where you will add your Aspire project.
- [test](./test/README.md) Contains test projects for your packages.
- [Hexalith.Builds](./Hexalith.Builds/README.md) Contains shared build configurations and tools.

## Getting Started

### Prerequisites

- [Hexalith.Builds](https://github.com/Hexalith/Hexalith.Builds)
- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later
- [PowerShell 7](https://github.com/PowerShell/PowerShell) or later
- [Git](https://git-scm.com/)

### Initializing the Package

To use this template to create a new Hexalith package:

1. Clone this repository or use it as a template when creating a new repository on GitHub.
2. Run the initialization script with your desired package name:

```powershell
./initialize.ps1 -PackageName "YourPackageName"
```

This script will:

- Replace all occurrences of "Documents" with your package name
- Replace all occurrences of "Documents" with the lowercase version of your package name
- Rename directories and files that contain "Documents" in their name
- Initialize and update Git submodules
- Set up the project structure for your new package

### Git Submodules

This template uses the Hexalith.Builds repository as a Git submodule. For information about the build system and configuration, refer to the README files in the Hexalith.Builds directory.

## Development

After initializing your package, you can start developing by:

1. Opening the solution file in your preferred IDE
2. Adding your implementation to the src/ directory
3. Writing tests in the test/ directory
4. Building and testing your package

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
