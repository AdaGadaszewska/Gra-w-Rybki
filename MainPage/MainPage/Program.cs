using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

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
            { "Wielki Karp", 50 },
            { "Orki z Majorki", 1000},
            { "Delfin", 500 },
            { "Megalodon", 700},
            { "Rekin", 250 }
        };

        // Ekwipunek gracza (lista złowionych ryb)
        List<string> ekwipunek = new List<string>();

        // Zmienna do losowania ryby
        Random random = new Random();

        // Zmienna do przechowywania punktów
        int punkty = 0;

        Console.WriteLine("Witaj w grze 'Łowienie Ryb'!");
        Console.WriteLine("Aby złowić rybę, wpisz 'łowienie'. Aby sprzedać ryby, wpisz 'sprzedaj'. Aby zakończyć grę, wpisz 'koniec'.");

        string email = PromptForEmail();

        while (true)
        {
            Console.Write("Co chcesz zrobić? ");
            string komenda = Console.ReadLine().ToLower();

            if (komenda == "łowienie")
            {
                string ryba = RandomRyba(ryby, random);
                ekwipunek.Add(ryba);
                punkty += ryby[ryba];
                Console.WriteLine($"Złowiłeś/aś: {ryba}! Zdobywasz {ryby[ryba]} punktów. Łącznie masz {punkty} punktów.");
            }
            else if (komenda == "sprzedaj")
            {
                if (ekwipunek.Count > 0)
                {
                    Console.WriteLine("Masz następujące ryby do sprzedaży:");
                    for (int i = 0; i < ekwipunek.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {ekwipunek[i]}");
                    }

                    Console.Write("Wybierz numer ryby, którą chcesz sprzedać (lub 0, aby anulować): ");
                    if (int.TryParse(Console.ReadLine(), out int numerRyby) && numerRyby >= 1 && numerRyby <= ekwipunek.Count)
                    {
                        string sprzedanaRyba = ekwipunek[numerRyby - 1];
                        ekwipunek.RemoveAt(numerRyby - 1);
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
                Console.WriteLine($"Dziękujemy za grę! Zdobyłeś/aś {punkty} punktów.");
                SendEmail(email, punkty);
                break;
            }
            else
            {
                Console.WriteLine("Nie rozumiem tej komendy. Spróbuj 'łowienie', 'sprzedaj', 'ekwipunek' lub 'koniec'.");
            }
        }
    }

    static string RandomRyba(Dictionary<string, int> ryby, Random random)
    {
        List<string> rybyList = new List<string>(ryby.Keys);
        return rybyList[random.Next(rybyList.Count)];
    }

    static string PromptForEmail()
    {
        Console.Write("Podaj swój adres e-mail, aby otrzymać wynik gry: ");
        return Console.ReadLine();
    }

    static void SendEmail(string email, int punkty)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("adixadixowska@gmail.com"); // Podaj tutaj swój adres e-mail
            mail.To.Add(email);
            mail.Subject = "Wynik gry 'Łowienie Ryb'";
            mail.Body = $"Dziękujemy za grę! Zdobyłeś/aś {punkty} punktów. Ryby które zabierasz do domu: {ekwipunek}";

            smtpServer.Port = 587;
            smtpServer.Credentials = new NetworkCredential("adixadixowska@gmail.com", "vuas odqg thnu gpuj"); // Podaj swoje dane logowania
            smtpServer.EnableSsl = true;

            smtpServer.UseDefaultCredentials = false;
            smtpServer.Send(mail);
            Console.WriteLine("Wynik gry został wysłany na podany adres e-mail."); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas wysyłania e-maila: {ex.Message}");
        }
    }
}