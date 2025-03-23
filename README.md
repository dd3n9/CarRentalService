# System Rezerwacji Pojazdów

## Funkcjonalności:
- Rezerwacja pojazdu na określony czas
- Zwrot pojazdu
- Sprawdzenie dostępnych pojazdów w danym terminie

## Dodatkowe atuty:
- Utworzenie testów jednostkowych
- Zastosowanie autoryzacji użytkownika (JWT)
- Zastosowanie wzorca Mediator / architektury CQRS i Event Sourcing
- Dostarczenie rozwiązania w środowisku dockerowym
- Asynchroniczne powiadomienia o zbliżającym się terminie zwrotu pojazdu (SignalR)
- Frontend SPA w Angularze:
    - Wyszukiwanie i rezerwacja pojazdów
    - Zarządzanie swoimi rezerwacjami
    - Wyświetlanie szczegółów pojazdu
    - Strona administracyjna do zarządzania pojazdami, rezerwacjami.

## Dodatkowe wymagania:
- Obsługa różnych typów pojazdów (samochody osobowe, ciężarowe, motocykle)
- Możliwość pobierania raportów o stanie floty pojazdów
- Zastosowanie IdentityRole dla zarządzania rolami użytkowników
