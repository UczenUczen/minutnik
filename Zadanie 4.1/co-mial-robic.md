Co miał robić minutnik
1. Co robi
Program pozwala odmierzać czas pracy, domyślnie ustawiony na 25 minut.
Program pozwala odmierzać czas przerwy, domyślnie ustawiony na 5 minut.
Program pozwala zmieniać długość pracy i przerwy w oknie aplikacji.
Program pozwala uruchomić i wstrzymać odliczanie jednym przyciskiem.
Program pozwala zresetować minutnik drugim przyciskiem.
Program pokazuje aktualną fazę: pracę albo przerwę.
Program pokazuje pozostały czas w formacie `mm:ss`.
Program automatycznie rozpoczyna przerwę po zakończeniu czasu pracy.
Program zwiększa licznik ukończonych cykli po zakończeniu przerwy.
Program pokazuje historię ukończonych cykli wraz z godziną ich zakończenia.
2. Czego nie robi
Program nie zapisuje historii ukończonych cykli do trwałego pliku — historia jest dostępna w bieżącym uruchomieniu aplikacji.
Program nie udostępnia kont użytkowników ani synchronizacji danych między urządzeniami.
Program nie pozwala tworzyć własnych rodzajów faz poza pracą i przerwą.
3. Po czym poznam
Kiedy uruchomię minutnik, to pozostały czas zacznie zmniejszać się co sekundę.
Kiedy wstrzymam minutnik, to odliczanie zatrzyma się do chwili ponownego uruchomienia.
Kiedy skończy się czas pracy, to aplikacja automatycznie przełączy się na przerwę i rozpocznie jej odliczanie.
Kiedy skończy się czas przerwy, to licznik ukończonych cykli zwiększy się o jeden, a aplikacja wróci do fazy pracy.
Kiedy nacisnę Reset, to odliczanie zatrzyma się, minutnik wróci do fazy pracy, a licznik ukończonych cykli zostanie wyzerowany.
4. Do ustalenia
Czy naciśnięcie Reset powinno usuwać również historię ukończonych cykli, czy tylko zerować widoczny licznik?
Czy zmiana długości pracy lub przerwy powinna być możliwa również podczas trwającego odliczania?
Czy historia ukończonych cykli powinna być zachowywana po zamknięciu i ponownym uruchomieniu aplikacji?
5. Porównanie z siedmioma zasadami minutnika
Po porównaniu pierwszej wersji opisu z siedmioma zasadami przekazanymi agentowi można stwierdzić, że w pierwszej wersji udało się odtworzyć 5 z 7 zasad:
Udało się odtworzyć domyślne czasy pracy (25 minut) i przerwy (5 minut) oraz możliwość zmiany obu wartości w oknie.
Udało się odtworzyć obsługę jednym przyciskiem Start/Pauza oraz drugim przyciskiem Reset.
Udało się odtworzyć widoczny licznik ukończonych cykli.
Udało się odtworzyć automatyczne rozpoczęcie przerwy po zakończeniu pracy.
Udało się odtworzyć wyświetlanie czasu w formacie `mm:ss`.
Nie zostało odtworzone wymaganie dotyczące polskiego interfejsu i angielskich nazw w kodzie.
Nie zostało odtworzone wymaganie dotyczące niewykorzystywania dodatkowych bibliotek.
Dwie ostatnie zasady dotyczą odpowiednio języka interfejsu i kodu oraz sposobu budowy projektu, a nie bezpośrednio funkcji minutnika, dlatego nie znalazły się w pierwszym opisie jego działania.
Zachowania, których nie określało siedem zasad
Program prowadzi historię ukończonych cykli i zapisuje przy nich godzinę zakończenia. Przy przejściu między fazami próbuje również odtworzyć sygnał dźwiękowy. Żadna z siedmiu zasad nie wymagała tych funkcji.
Siedem zasad nie określało także dokładnego zachowania przy resecie w trakcie przerwy. Z kodu wynika, że Reset zatrzymuje odliczanie, przełącza minutnik na fazę pracy, ustawia czas pracy ponownie, zeruje licznik ukończonych cykli i nie usuwa historii. Jest to zachowanie zaimplementowane w programie, a nie decyzja opisana w siedmiu zasadach.