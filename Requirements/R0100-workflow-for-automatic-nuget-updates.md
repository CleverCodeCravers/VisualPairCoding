# Automatische Aktualisierungen der Nuget-Abhängigkeiten der enthaltenen Projekte

## Zusammenfassung

Wir möchten gerne einen zusätzlichen github-Workflow haben, der 
- automatisch täglich nach nuget-Aktualisierungen für die enthalten dotnet-Projekte sucht
- Aktualisierungen installiert
- Prüft, ob der Build und die Tests funktionieren
- Wenn ja, dann soll automatisch ein neuer Commit / Push erzeugt werden, der den normalen Build/Release Workflow indirekt startet

