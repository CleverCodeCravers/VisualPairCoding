---
id: R0004
titel: "Zufaellige Navigator-Empfehlung"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# Zufaellige Navigator-Empfehlung

## Beschreibung

Als Teilnehmer moechte ich eine Empfehlung fuer den Navigator sehen, damit die Rollen klar verteilt sind und verschiedene Paarungen entstehen.

## Akzeptanzkriterien

- Ein zufaelliger Teilnehmer (ausser dem aktuellen Driver) wird als Navigator empfohlen
- Die Empfehlung wird im Session-Fenster angezeigt (grau, unter dem Driver)
- Bei nur einem Teilnehmer wird kein Navigator angezeigt

## Implementierung

- `RunSessionForm.cs`: `GetRandomNavigatorFromListWithout()` waehlt zufaellig aus den verbleibenden Teilnehmern
