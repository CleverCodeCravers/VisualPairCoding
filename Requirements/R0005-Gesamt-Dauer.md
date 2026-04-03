---
id: R0005
titel: "Optionale Gesamtdauer-Begrenzung"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# Optionale Gesamtdauer-Begrenzung

## Beschreibung

Als Benutzer moechte ich optional eine Gesamtdauer fuer die Session festlegen koennen, damit die Session nach einer definierten Zeit automatisch endet.

## Akzeptanzkriterien

- Checkbox "Total Duration" aktiviert/deaktiviert die Gesamtdauer
- TimePicker zur Eingabe der gewuenschten Gesamtdauer
- Verbleibende Gesamtdauer wird im Session-Fenster angezeigt
- Bei Ablauf der Gesamtdauer erscheint eine Fullscreen-Meldung "Total Duration Exceeded!" und die Session wird beendet

## Implementierung

- `EnterNamesWindow.axaml`: CheckBox + TimePicker
- `RunSessionForm.cs`: `_totalDuration` Countdown parallel zum Runden-Timer, prueft bei jedem Tick ob Gesamtdauer abgelaufen
