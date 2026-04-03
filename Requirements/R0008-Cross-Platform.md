---
id: R0008
titel: "Cross-Platform-Unterstuetzung (Windows, Linux, macOS)"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# Cross-Platform-Unterstuetzung

## Beschreibung

Als Benutzer moechte ich die Anwendung auf Windows, Linux und macOS verwenden koennen, damit alle Team-Mitglieder unabhaengig vom Betriebssystem teilnehmen koennen.

## Akzeptanzkriterien

- Anwendung laeuft auf Windows (x64), Linux (x64), macOS (x64)
- Self-Contained Deployment (kein .NET-Runtime-Install noetig)
- Single-File-Executable fuer jede Plattform
- CI/CD baut fuer alle 3 Plattformen
- AppData-Pfad wird plattformspezifisch aufgeloest

## Implementierung

- Avalonia UI als Cross-Platform-UI-Framework
- `SessionConfigurationFolderHandler`: Plattform-Erkennung via `RuntimeInformation.IsOSPlatform()`
- GitHub Actions Workflow mit Matrix-Build (windows-latest, ubuntu-latest, macos-latest)
- Publish mit `--self-contained true -p:PublishSingleFile=true`
