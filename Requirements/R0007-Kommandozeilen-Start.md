---
id: R0007
titel: "Kommandozeilen-Start mit Config-Datei"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# Kommandozeilen-Start mit Config-Datei

## Beschreibung

Als Benutzer moechte ich die Anwendung mit einem Dateipfad als Kommandozeilen-Parameter starten koennen, damit die Session automatisch mit der gespeicherten Konfiguration beginnt.

## Akzeptanzkriterien

- Wenn ein Dateipfad als erstes Argument uebergeben wird, wird die Session-Konfiguration geladen
- Die Session startet automatisch (ohne manuellen Klick auf "Start")
- Nach Ende der Session kehrt die Anwendung zum normalen Eingabefenster zurueck

## Implementierung

- `Program.cs`: `IsStartupWithSessionFile()` prueft auf Argument, uebergibt `autostart=true` und `configPath` an `EnterNamesForm`
- `EnterNamesForm`: Bei `_autostart=true` wird `StartForm()` automatisch im `OnActivated`-Event aufgerufen
