# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/).

## [Unreleased]

### Added
- Unit-Test-Projekte fuer BL und Infrastructure (40 Tests)
- 8 Kern-Feature-Anforderungen dokumentiert (R0001-R0008)
- Wartungsbericht R0101 mit Verbesserungsvorschlaegen
- `.editorconfig` fuer einheitlichen Code-Stil

### Changed
- SessionConfigurationFileHandler und SessionConfigurationFolderHandler von static auf Instanz-Klassen refactored (Testbarkeit)
- Teilnehmer-Code in EnterNamesWindow durch Schleifen ersetzt (war 10x Copy-Paste)
- GitHub Actions auf aktuelle Versionen aktualisiert (checkout@v4, setup-dotnet@v4, action-gh-release@v2)
- Avalonia UI auf 11.3.13 aktualisiert (via NuGet-Update PR #27)

### Fixed
- GetParticipants() Bug: participant2 wurde vor participant1 hinzugefuegt
- Leerer catch-Block in StartForm durch Fehleranzeige ersetzt
- Typo `activeParticipnat` -> `activeParticipant` korrigiert

### Removed
- Ungenutztes VisualPairCoding.Interfaces-Projekt (IGithubAPIResponse — Ueberrest der entfernten Auto-Update-Funktion)
- Ungenutzte Newtonsoft.Json-Abhaengigkeit (Code nutzt System.Text.Json)
- Unused using `BitVector32` in EnterNamesWindow

## [v1.78] - 2023-09-17

### Fixed
- Fix for recent sessions

## [v1.77] - 2023-09-10

### Fixed
- NumericUpDown control appended `,0` — fixed formatting

## [v1.76] - 2023-05-28

### Added
- Recent sessions menu
- Total duration option for sessions

## [v1.75] - 2023-02-25

### Changed
- Migrated from WinForms to Avalonia UI (cross-platform)
- Build workflow creates Windows, Linux, and macOS releases

### Added
- About dialog with version info
- Session load/save functionality (.vpcsession files)
- Command-line startup with config file
- Skip current driver button
- Randomize participants button

## [v1.74] - 2023-02-05

### Added
- Full-screen animation when driver changes
- Draggable chromeless session window
- Application icon

## [0.0.1] - 2022-07-29

### Added
- Initial release
- Pair coding session timer with participant rotation
- Up to 10 participants
- Configurable turn duration
- Always-on-top timer window
- Recommended navigator display
