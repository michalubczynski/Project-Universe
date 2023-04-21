# UniverseDB
Universe database + mvc

W systemie będzie występować 3 użytkowników – 

*Odkrywca, który będzie miał możliwość Dodawania(odkrywania) planet/gwiazd/etc.. do bazy danych oraz nabywania statków kosmicznych:

- Task<int> GetAllPlanetsCount();
- Task<Planet> GetHeaviestPlanet();
- Task AddRandomStars(int count);
- void DiscoverStar(Star o);
- void DiscoverPlanet(Planet o);
- void DiscoverStarSystem(StarSystem o);
- void DiscoverGalaxy(Galaxy o);
- public void ReserveShip(Ship o);
- public void ReturnShip();
- public void MarkBroken();
        
*Administracja będzie miał możliwość tworzenia nowych odkrywców oraz przenoszenia planet lub całych układów słonecznych pomiędzy galaktykami:
        
- Task MoveStarSystemToAnotherGalaxy(StarSystem starsystemToMove, Galaxy destinationGalaxy);
- Task HireNewDiscoverer(string name, string surname, int age);

*Dealer będzie miał możliwość tworzenia nowych statków kosmicznych:
        
- Task MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, Discoverer? discoverer = null);
- Task RewardExplorerByNewShip(Discoverer discovererToAward, Ship newShip);
