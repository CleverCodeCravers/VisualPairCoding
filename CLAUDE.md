# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

### Windows
```bash
./build.ps1
```

### Linux/Cross-platform
```bash
./build-linux.ps1
```

### Core Build Commands
```bash
# Build
dotnet build --configuration Release

# Run tests (40 tests: 12 BL, 28 Infrastructure)
dotnet test

# Publish executable
dotnet publish Source/VisualPairCoding/VisualPairCoding.AvaloniaUI/VisualPairCoding.AvaloniaUI.csproj \
  --configuration Release \
  --output ./artifacts \
  --runtime win-x64 \
  --self-contained true \
  -p:PublishSingleFile=true
```

## Architecture Overview

This is a Visual Pair Coding timer application built with Clean Architecture:

```
VisualPairCoding.BL/                  → Business logic and domain models
VisualPairCoding.Infrastructure/      → External concerns (file I/O, JSON serialization)
VisualPairCoding.AvaloniaUI/          → Cross-platform UI using Avalonia
VisualPairCoding.BL.Tests/            → Unit tests for business logic (xUnit)
VisualPairCoding.Infrastructure.Tests/ → Unit tests for infrastructure (xUnit)
```

### Key Components

- **PairCodingSession** (BL/PairCodingSession.cs): Core session logic, timer management, participant rotation
- **SessionConfiguration** (Infrastructure/): Handles saving/loading session configuration as JSON
- **UI Forms**: 
  - EnterNamesWindow: Initial participant setup
  - RunSessionForm: Main timer display (always-on-top)
  - NewturnForm: Full-screen transition between drivers

### Technology Stack
- .NET 9.0
- Avalonia UI 11.3.13 (cross-platform desktop UI)
- System.Text.Json for serialization
- xUnit for testing

## Development Notes

- Solution file: `Source/VisualPairCoding/VisualPairCoding.sln`
- Version management: `Scripts/Set-Version-Number.ps1` replaces `$$VERSION$$` placeholder during CI builds
- 40 unit tests (BL + Infrastructure), run with `dotnet test`
- Requirements documented in `Requirements/` directory (R0001-R0008 features, R0101 maintenance)
- Application supports command-line startup with config file path