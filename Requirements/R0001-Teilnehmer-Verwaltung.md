---
id: R0001
titel: "Teilnehmer-Verwaltung"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# Teilnehmer-Verwaltung

## Beschreibung

Als Benutzer moechte ich bis zu 10 Teilnehmer fuer eine Pair-/Mob-Coding-Session eingeben koennen, damit alle Beteiligten in die Rotation einbezogen werden.

## Akzeptanzkriterien

- Es koennen 1 bis 10 Teilnehmer-Namen eingegeben werden
- Leere Felder werden ignoriert
- Mindestens ein Teilnehmer muss eingetragen sein (Validierung)
- Die Teilnehmer-Reihenfolge kann per "Randomize Participants"-Button zufaellig gemischt werden

## Implementierung

- `EnterNamesWindow.axaml`: 10 TextBox-Felder (participant1 bis participant10) in 2x5 Grid
- `EnterNamesWindow.axaml.cs`: `GetParticipants()` sammelt nicht-leere Felder, `RandomizeParticipants()` mischt per Fisher-Yates-Shuffle
- `PairCodingSession.Validate()` prueft auf mindestens einen Teilnehmer
