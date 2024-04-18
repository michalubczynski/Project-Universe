# UniverseDB
Baza danych galaktyk oraz ich odkrywców + statki kosmiczne

W systemie występuje 3 użytkowników – 

• Odkrywca, który ma możliwość Dodawania(odkrywania) planet/gwiazd/etc.. do bazy danych oraz nabywania statków kosmicznych:

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
        
• Administracja ma możliwość tworzenia nowych odkrywców oraz przenoszenia planet lub całych układów słonecznych pomiędzy galaktykami:
        
- Task MoveStarSystemToAnotherGalaxy(StarSystem starsystemToMove, Galaxy destinationGalaxy); - przenoszenie systemów gwiezdnych między galaktykami
- Task HireNewDiscoverer(string name, string surname, int age); - zatrudnienie nowego odkrywcy

• Dealer będzie ma tworzenia nowych statków kosmicznych:
        
- Task MakeNewShip(int MaxRange, int MaxSpeed, string? model = null, Discoverer? discoverer = null); - zbudowanie statku
- Task RewardExplorerByNewShip(Discoverer discovererToAward, Ship newShip); - przydzielenie odkrywcy nowego statku

 • TESTY
        W ramach testów  Xunit cztery metody testowe, z których każda używa innej metody testowania:

1. Metoda "BLLTest":
-Tworzy listę obiektów typu Planet i fake repozytorium dla nich.
-Dodaje planety do repozytorium za pomocą pętli foreach.
-Tworzy obiekt TestUnitOfWork, dodaje fake repozytorium do niego i tworzy nowy obiekt Service, używając unitOfWork.
-Wywołuje metodę "GetHeaviestPlanet" z serwisu i sprawdza, czy zwrócona planeta ma poprawne ID.
                
2. Metoda "MockTest":
-Tworzy mockowe repozytorium dla obiektów typu Planet przy użyciu biblioteki Moq.
-Tworzy dwa obiekty typu Planet i ustawia mockowe repozytorium tak, aby zwracały je, gdy wywoływana jest metoda "GetListAsync".
-Tworzy nowy obiekt UnitOfWork, dodaje mockowe repozytorium do niego i tworzy nowy obiekt Service, używając unitOfWork.
-Wywołuje metodę "GetAllPlanetsCount" z serwisu i sprawdza, czy zwrócona liczba jest poprawna.
          
3. Metoda "AddRandomStarsTest":
-Tworzy nowy kontekst bazy danych w pamięci za pomocą metody GetTestDbContext.
-Tworzy repozytorium dla obiektów typu Star przy użyciu kontekstu i dodaje je do nowego obiektu UnitOfWork.
-Tworzy nowy obiekt Service, używając unitOfWork.
-Wywołuje metodę "AddRandomStars" z serwisu z liczbą 2 i sprawdza, czy liczba gwiazd w repozytorium wynosi 2.
        
4.Metoda "DummyPlanetRepoTest":
-Tworzy akcję, która tworzy repozytorium dla obiektów typu Planet z wartością null.
-Sprawdza, czy akcja wywołuje wyjątek typu NullReferenceException.
        
        
https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
