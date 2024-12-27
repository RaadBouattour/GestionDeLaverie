using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace gestionLaverie.Simulateur
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Lancement du Simulation";

            Console.WriteLine("Chargement de la configuration...");

            try
            {
                using (var client = new HttpClient { BaseAddress = new Uri("http://localhost:5223/api/configuration/") })
                {
                    var response = await client.GetAsync("");

                    if (response.IsSuccessStatusCode)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Configuration chargée avec succès!\n");

                        var config = await response.Content.ReadAsStringAsync();
                        Console.ResetColor();
                        Console.WriteLine(config);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Erreur: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erreur: {ex.Message}");
            }

            Console.ResetColor();
            Console.WriteLine("\nAppuyez sur une touche pour quitter...");
            Console.ReadKey();
        }
    }
}
