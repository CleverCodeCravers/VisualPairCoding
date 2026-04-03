---
id: R0003
titel: "Automatische Fahrer-Rotation mit Fullscreen-Uebergabe"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# Automatische Fahrer-Rotation mit Fullscreen-Uebergabe

## Beschreibung

Als Teilnehmer moechte ich beim Wechsel des Drivers eine auffaellige Fullscreen-Anzeige sehen, die den naechsten Driver ankuendigt und durch einen OK-Button bestaetigt werden muss, damit der Wechsel nicht uebersehen wird.

## Akzeptanzkriterien

- Bei Ablauf der Rundenzeit wird automatisch zum naechsten Teilnehmer gewechselt
- Fullscreen-Fenster zeigt den Namen des naechsten Drivers in grosser Schrift
- OK-Button muss geklickt werden um fortzufahren
- Timer stoppt waehrend der Uebergabe
- Nach der letzten Person in der Liste wird wieder beim ersten Teilnehmer begonnen (Rotation)

## Implementierung

- `NewturnForm.axaml/.cs`: Maximiertes Fenster mit grossem Text und OK-Button
- `RunSessionForm.cs`: `ChooseAnotherPairAndStartNewTurn()` stoppt Timer, zeigt NewTurnForm, wartet auf Close, startet Timer neu
