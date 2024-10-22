# FocusPlanner

**FocusPlanner** is een persoonlijke productiviteitsapplicatie die helpt bij het beheren van taken, deadlines en prioriteiten. Met een overzichtelijke interface, diverse filters en een eenvoudige maar krachtige taakbeheerervaring, biedt FocusPlanner de gebruiker een gebruiksvriendelijke manier om dagelijkse taken te organiseren en op tijd te voltooien.

## Features

### Huidige functionaliteiten

1. **CRUD-functionaliteiten** (Create, Read, Update, Delete):
   - Voeg nieuwe taken toe, werk bestaande taken bij, verwijder ze of markeer ze als voltooid.
   - Dynamische updates zorgen ervoor dat je altijd een actueel overzicht van je taken hebt.
   
2. **Prioriteitssysteem**:
   - Categoriseer je taken op basis van prioriteit (Laag, Medium, Hoog).
   - De prioriteiten zijn visueel gemarkeerd in de lijst, waardoor je snel inzicht hebt in welke taken prioriteit hebben.

3. **Categoriefilter**:
   - Filter taken op basis van verschillende categorieën om gemakkelijk je focus te behouden.
   - Ondersteunt meerdere selecties, zodat je kunt filteren op verschillende categorieën tegelijk.

4. **Datumfiltering**:
   - Filter taken op basis van hun deadline om je te concentreren op wat er op korte termijn moet worden gedaan.
   - Mogelijkheid om alleen de taken weer te geven die voor een specifieke datum zijn gepland.

5. **Takenlijst met Syncfusion DataGrid**:
   - Overzichtelijke lijst met taken met mogelijkheid tot sorteren, filteren en bewerken.
   - Visueel aantrekkelijke lay-out met kleurcodering op basis van prioriteiten.

6. **Taken bewerken**:
   - Mogelijkheid om bestaande taken te bewerken met vooraf ingevulde gegevens.
   - Alle wijzigingen worden dynamisch opgeslagen in het systeem.

7. **Verwijderen van taken**:
   - Verwijder ongewenste taken eenvoudig uit de lijst.

8. **Pictogram voor bewerken**:
   - In plaats van een knop voor "bewerken" worden potloodpictogrammen weergegeven, wat zorgt voor een moderne, strakke UI.

### Toekomstige functionaliteiten

1. **Kalenderweergave**:
   - Mogelijkheid om alle taken in een kalenderweergave te zien. Zo kun je je taken en deadlines visueel volgen.
   - Integreer Syncfusion of een ander visueel kalendersysteem voor overzichtelijke maand-, week- en dagweergaven.

2. **Automatisch opstarten met je PC**:
   - Voeg de optie toe om FocusPlanner automatisch te laten starten wanneer de computer wordt opgestart, zodat je direct toegang hebt tot je taken.

3. **Herinneringen en meldingen**:
   - Ontvang herinneringen over deadlines of naderende taken via meldingen op je PC.
   - Notificaties kunnen worden ingesteld op basis van de vervaldatum of prioriteit.

4. **Geavanceerde rapporten en statistieken**:
   - In de toekomst kan FocusPlanner statistieken bieden over je productiviteit, zoals voltooiingstijden, aantal voltooide taken per week, enz.

## Installatie

### Vereisten

- .NET Framework / .NET Core
- Syncfusion WPF Libraries
- FontAwesome.WPF voor pictogrammen
- Microsoft SQL Server voor databaseondersteuning
- Visual Studio 2022

### Stappen om te installeren

1. **Clone de repository**:
   ```bash
   git clone https://github.com/Naamari1/FocusPlanner.git

2. **Syncfusion installeren**:
   Zorg ervoor dat je de juiste Syncfusion WPF NuGet-pakketten hebt geïnstalleerd via Visual Studio:
   ```bash
   Install-Package Syncfusion.WPF

3. **Database configureren**:
   - De applicatie maakt gebruik van SQL Server voor taakopslag. Zorg ervoor dat je de juiste connectiestrings hebt ingesteld in appsettings.json.
    
4. Applicatie starten:
   - Open het project in Visual Studio en druk op "Start" om de applicatie uit te voeren.

### Gebruik

1. **Taken toevoegen**:
  - Klik op de '+'-knop om een nieuwe taak toe te voegen. Vul de details in en sla de taak op.

2. **Taken bewerken**:
  - Klik op het potloodpictogram naast een taak om deze te bewerken. Wijzig de informatie en sla de taak opnieuw op.

3. **Filteren en zoeken**:
   - Gebruik de zoekbalk om snel taken te filteren op titel.
   - Gebruik de categorie- en datumfilters om je focus te beperken tot specifieke taken.
    

### Stappen om te installeren

Naast de genoemde toekomstige functies zijn er plannen om integraties met andere productiviteitssoftware mogelijk te maken, zoals:
   - Google Calendar-integratie
   - Trello- of Jira-synchronisatie
   - Meer aanpassingsmogelijkheden voor meldingen en deadlines

