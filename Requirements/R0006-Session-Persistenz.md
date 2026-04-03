---
id: R0006
titel: "Session-Konfiguration laden und speichern"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# Session-Konfiguration laden und speichern

## Beschreibung

Als Benutzer moechte ich Session-Konfigurationen als Dateien speichern und laden koennen, damit ich wiederkehrende Sessions nicht jedes Mal neu eingeben muss.

## Akzeptanzkriterien

- Sessions koennen als `.vpcsession`-Dateien gespeichert werden (JSON-Format)
- Sessions koennen aus `.vpcsession`-Dateien geladen werden
- Dateiname-Vorschlag basiert auf Teilnehmer-Namen (z.B. `Bob_Eik.vpcsession`)
- Zuletzt verwendete Sessions werden automatisch im AppData-Verzeichnis gespeichert
- "Recent"-Menue zeigt zuletzt verwendete Sessions
- Dateidialog mit `.vpcsession`-Filter
- "New"-Menue setzt die Session zurueck

## Implementierung

- `SessionConfigurationFileHandler`: JSON-Serialisierung via `System.Text.Json`
- `SessionConfigurationFolderHandler`: Verwaltet AppData-Verzeichnis (`%APPDATA%/VisualPairCoding/` auf Windows, `~/.local/share/VisualPairCoding/` auf Linux)
- `SessionConfiguration` Record: `string[] Participants`, `int SessionLength`
