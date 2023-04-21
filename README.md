# UniverseDB
Baza danych galaktyk oraz ich odkrywców + statki kosmiczne

W systemie będzie występować 3 użytkowników – 

• Odkrywca, który będzie miał możliwość Dodawania(odkrywania) planet/gwiazd/etc.. do bazy danych oraz nabywania statków kosmicznych:

- Task<int> GetAllPlanetsCount(); - liczba znanych planet
- Task<Planet> GetHeaviestPlanet(); - najcięższa planeta
- Task AddRandomStars(int count); - generowanie gwiazd
- void DiscoverStar(Star o); - odkrywanie gwiazdy
- void DiscoverPlanet(Planet o); - odkrywanie planety
- void DiscoverStarSystem(StarSystem o); - odkrywanie systemu gwiezdnego
- void DiscoverGalaxy(Galaxy o); - odkrywanie galaktyki
- public void ReserveShip(Ship o); - rezerwacja statku
- public void ReturnShip(); - zwracanie statku
- public void MarkBroken(); - oznaczenie statku jako uszkodzony
        
• Administracja będzie miał możliwość tworzenia nowych odkrywców oraz przenoszenia planet lub całych układów słonecznych pomiędzy galaktykami:
        
- Task MoveStarSystemToAnotherGalaxy(StarSystem starsystemToMove, Galaxy destinationGalaxy); - przenoszenie systemów gwiezdnych między galaktykami
- Task HireNewDiscoverer(string name, string surname, int age); - zatrudnienie nowego odkrywcy

• Dealer będzie miał możliwość tworzenia nowych statków kosmicznych:
        
- Task MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, Discoverer? discoverer = null); - zbudowanie statku
- Task RewardExplorerByNewShip(Discoverer discovererToAward, Ship newShip); - przydzielenie odkrywcy nowego statku
