using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Lista dostępnych ryb i ich punktów
        Dictionary<string, int> ryby = new Dictionary<string, int>
        {
            { "Pstrąg", 10 },
            { "Sielawa", 15 },
            { "Sum", 20 },
            { "Troć wędrowna", 25 },
            { "Węgorz", 30 },
            { "Sandacz", 35 },
            { "Wielki Karp", 50 }
        };

        // Ekwipunek gracza (lista złowionych ryb)
        List<string> ekwipunek = new List<string>();

        // Zmienna do losowania ryby
        Random random = new Random();

        // Zmienna do przechowywania punktów
        int punkty = 0;

        Console.WriteLine("Witaj w grze 'Łowienie Ryb'!");
        Console.WriteLine("Aby złowić rybę, wpisz 'łowienie'. Aby sprzedać ryby, wpisz 'sprzedaj'. Aby zakończyć grę, wpisz 'koniec'.");

        while (true)
        {
            Console.Write("Co chcesz zrobić? ");
            string komenda = Console.ReadLine().ToLower();

            if (komenda == "łowienie")
            {
                // Losowanie ryby z listy
                string ryba = RandomRyba(ryby, random);

                // Dodanie ryby do ekwipunku
                ekwipunek.Add(ryba);

                // Wyświetlenie komunikatu o złowieniu ryby
                Console.WriteLine($"Złowiłeś/aś: {ryba}!");

                // Dodanie punktów
                punkty += ryby[ryba];
                Console.WriteLine($"Zdobywasz {ryby[ryba]} punktów! Łącznie masz {punkty} punktów.");
            }
            else if (komenda == "sprzedaj")
            {
                // Sprawdzanie, czy gracz ma jakieś ryby do sprzedaży
                if (ekwipunek.Count > 0)
                {
                    Console.WriteLine("Masz następujące ryby do sprzedaży:");
                    for (int i = 0; i < ekwipunek.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {ekwipunek[i]}");
                    }

                    Console.Write("Wybierz numer ryby, którą chcesz sprzedać (lub 0, aby anulować): ");
                    int numerRyby;
                    bool validInput = int.TryParse(Console.ReadLine(), out numerRyby);

                    if (validInput && numerRyby >= 1 && numerRyby <= ekwipunek.Count)
                    {
                        string sprzedanaRyba = ekwipunek[numerRyby - 1];
                        ekwipunek.RemoveAt(numerRyby - 1); // Usuwanie sprzedanej ryby z ekwipunku

                        // Dodanie punktów za sprzedaż
                        punkty += ryby[sprzedanaRyba];
                        Console.WriteLine($"Sprzedałeś {sprzedanaRyba} za {ryby[sprzedanaRyba]} punktów! Łącznie masz {punkty} punktów.");
                    }
                    else if (numerRyby == 0)
                    {
                        Console.WriteLine("Anulowano sprzedaż.");
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowy wybór.");
                    }
                }
                else
                {
                    Console.WriteLine("Nie masz żadnych ryb do sprzedaży.");
                }
            }
            else if (komenda == "ekwipunek")
            {
                // Wyświetlanie zawartości ekwipunku
                Console.WriteLine("\nTwój ekwipunek:");
                if (ekwipunek.Count > 0)
                {
                    foreach (var ryba in ekwipunek)
                    {
                        Console.WriteLine(ryba);
                    }
                }
                else
                {
                    Console.WriteLine("Ekwipunek jest pusty.");
                }
            }
            else if (komenda == "koniec")
            {
                // Zakończenie gry
                Console.WriteLine($"Dziękujemy za grę! Zdobyłeś/aś {punkty} punktów.");
                break;
            }
            else
            {
                // Obsługa nieznanej komendy
                Console.WriteLine("Nie rozumiem tej komendy. Spróbuj 'łowienie', 'sprzedaj', 'ekwipunek' lub 'koniec'.");
            }
        }
    }

    // Funkcja losująca rybę z listy
    static string RandomRyba(Dictionary<string, int> ryby, Random random)
    {
        List<string> rybyList = new List<string>(ryby.Keys);
        return rybyList[random.Next(rybyList.Count)];
    }
}