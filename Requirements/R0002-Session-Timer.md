---
id: R0002
titel: "Session-Timer mit Always-on-Top-Fenster"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# Session-Timer mit Always-on-Top-Fenster

## Beschreibung

Als Benutzer moechte ich waehrend der Pair-Coding-Session ein kleines Always-on-Top-Fenster sehen, das den aktuellen Driver und die verbleibende Zeit anzeigt, damit ich jederzeit den Status der Session im Blick habe.

## Akzeptanzkriterien

- Kleines Fenster (305x120px) bleibt immer im Vordergrund
- Zeigt den aktuellen Driver-Namen an
- Zeigt die verbleibende Zeit der aktuellen Runde an (Countdown)
- Zeigt einen empfohlenen Navigator an
- Fenster ist frei verschiebbar per Drag (chromeless)
- Pause/Stop/Skip-Buttons vorhanden

## Implementierung

- `RunSessionForm.axaml/.cs`: Chromeless Window mit DispatcherTimer (1-Sekunden-Intervall)
- Timer zaehlt von `MinutesPerTurn` herunter
- Dragging via PointerPressed/PointerMoved/PointerReleased
