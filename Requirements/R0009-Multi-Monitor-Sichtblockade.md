---
id: R0009
titel: "Multi-Monitor-Sichtblockade auf korrektem Bildschirm"
typ: Feature
status: Offen
erstellt: 2026-04-03
---

# R0009: Multi-Monitor-Sichtblockade auf korrektem Bildschirm

## Beschreibung

Auf Rechnern mit mehreren Bildschirmen (physisch oder virtuell, z.B. via TeamViewer) muss das Fullscreen-Blockade-Fenster (`NewTurnForm`) auf dem Bildschirm erscheinen, auf dem sich die Maus gerade befindet. Alternativ soll der Benutzer waehlen koennen, auf welchem Bildschirm die Blockade angezeigt wird. Die Monitor-Konfiguration kann sich waehrend einer Session aendern (virtuelle Monitore koennen hinzukommen oder entfernt werden).

## Akzeptanzkriterien

### Grundfunktion
- [ ] Das `NewTurnForm` (Sichtblockade beim Fahrerwechsel) oeffnet sich auf dem Bildschirm, auf dem sich der Mauszeiger befindet
- [ ] Das "Total Duration Exceeded"-Fenster oeffnet sich ebenfalls auf dem korrekten Bildschirm
- [ ] Die Blockade deckt den gesamten Bildschirm ab (nicht nur den primaeren Monitor)

### Monitor-Erkennung
- [ ] Beim Oeffnen des Blockade-Fensters wird die aktuelle Mausposition ermittelt
- [ ] Der Monitor, der die Mausposition enthaelt, wird als Ziel-Monitor bestimmt
- [ ] Das Fenster wird auf dem Ziel-Monitor maximiert/positioniert

### Dynamische Monitor-Konfiguration
- [ ] Wenn waehrend einer Session Monitore hinzukommen (z.B. TeamViewer-Verbindung), funktioniert die Zuordnung weiterhin
- [ ] Wenn Monitore entfernt werden, faellt das Fenster auf den primaeren Monitor zurueck
- [ ] Kein Absturz bei Monitor-Konfigurationsaenderungen waehrend die Blockade angezeigt wird

### Optionale Bildschirmwahl
- [ ] In den Session-Einstellungen (EnterNamesWindow) kann optional ein fester Bildschirm gewaehlt werden
- [ ] Die Auswahl zeigt alle aktuell verfuegbaren Monitore an
- [ ] Standard-Einstellung: "Mausposition folgen" (automatisch)

## Status

- [ ] Offen

## Technische Details

### Zielverzeichnisse

| Verzeichnis | Zweck |
|------------|-------|
| `Source/VisualPairCoding/VisualPairCoding.AvaloniaUI/` | UI-Aenderungen an NewTurnForm und RunSessionForm |
| `Source/VisualPairCoding/VisualPairCoding.BL/` | Ggf. Monitor-Praeferenz im Session-Modell |
| `Source/VisualPairCoding/VisualPairCoding.BL.Tests/` | Tests fuer erweiterte Session-Logik |

### Zu aendernde Dateien

| Datei | Aenderung |
|-------|----------|
| `VisualPairCoding.AvaloniaUI/NewturnForm.axaml.cs` | Statt `WindowState.Maximized` den Ziel-Monitor ermitteln und das Fenster manuell auf dessen Bounds positionieren/skalieren |
| `VisualPairCoding.AvaloniaUI/RunSessionForm.axaml.cs` | Beim Erstellen des `NewTurnForm` den Ziel-Monitor (via Mausposition oder Praeferenz) ermitteln und uebergeben |
| `VisualPairCoding.AvaloniaUI/EnterNamesWindow.axaml` | Optional: Dropdown/ComboBox fuer Monitor-Auswahl |
| `VisualPairCoding.AvaloniaUI/EnterNamesWindow.axaml.cs` | Optional: Monitor-Auswahl-Logik |

### Vermutete Komponenten

| Komponente | Verantwortung |
|------------|---------------|
| `NewTurnForm` | Muss statt `WindowState.Maximized` den korrekten Monitor nutzen: Position auf Monitor-Bounds setzen, Groesse auf Monitor-Aufloesung setzen |
| `RunSessionForm` | Muss beim Erzeugen des NewTurnForm den aktuellen Maus-Monitor ermitteln |
| Avalonia `Screens` API | `Window.Screens.All` liefert alle Monitore, `Screens.ScreenFromPoint()` oder `Screens.ScreenFromWindow()` fuer Zuordnung |

### Avalonia-API-Hinweise

Relevante Avalonia-APIs fuer Multi-Monitor:
- `Window.Screens.All` — Liste aller Monitore (aktualisiert sich dynamisch)
- `Window.Screens.Primary` — Primaerer Monitor
- `Screen.Bounds` — Position und Groesse eines Monitors (in Pixeln)
- `Screen.WorkingArea` — Nutzbare Flaeche (ohne Taskbar)
- Mausposition: Avalonia-interne Pointer-Position oder `System.Windows.Forms.Cursor.Position` (nur Windows) oder plattformuebergreifend via `PointerEventArgs`

### Implementierungsansatz

1. Im `RunSessionForm` vor dem Oeffnen des `NewTurnForm`:
   - Aktuelle Mausposition ermitteln (oder Position des RunSessionForm-Fensters)
   - Monitor finden, der diese Position enthaelt
   - Monitor-Bounds an `NewTurnForm` uebergeben

2. Im `NewTurnForm`:
   - Statt `WindowState = WindowState.Maximized`:
     - `WindowStartupLocation = WindowStartupLocation.Manual`
     - `Position` auf die linke obere Ecke des Ziel-Monitors setzen
     - `Width`/`Height` auf die Monitor-Aufloesung setzen
   - Alternativ: Fenster auf dem Ziel-Monitor positionieren und dann maximieren

### Tests

| Testdatei | Prueft |
|-----------|-------|
| `VisualPairCoding.BL.Tests/PairCodingSessionTests.cs` | Ggf. erweiterte Session-Konfiguration mit Monitor-Praeferenz |

## Abhaengigkeiten

- Abhaengig von: R0003 (Fahrer-Rotation — das ist das Feature, das die Blockade nutzt)
- Blockiert: keine

## Notizen

- **TeamViewer-Kontext**: Bei Remote-Sessions ueber TeamViewer aendert sich die Monitor-Konfiguration dynamisch. Der virtuelle Monitor des Remote-Rechners kann sich von der lokalen Konfiguration unterscheiden. Die Loesung muss robust mit wechselnden Monitor-Setups umgehen.
- **Fallback**: Wenn die Monitor-Erkennung fehlschlaegt (z.B. auf einer Plattform ohne Multi-Monitor-Support), soll das bisherige Verhalten beibehalten werden (`WindowState.Maximized` auf dem Standardmonitor).
- **Cross-Platform**: Die Avalonia `Screens`-API funktioniert auf Windows, Linux und macOS — plattformspezifischer Code sollte vermeidbar sein.
