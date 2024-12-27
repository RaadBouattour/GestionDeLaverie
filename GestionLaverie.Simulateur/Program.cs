using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestionLaverie.Simulateur
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Lancement de la Simulation";


            Console.WriteLine("Lancement de la Simulation");
            Console.WriteLine("");


            Console.Write("Chargement de la configuration");
            for (int i = 0; i < 3; i++)
            {
                await Task.Delay(500);
                Console.Write(".");
            }

            try
            {
                Console.Clear();
                Console.WriteLine("Lancement de la Simulation");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Configuration chargée avec succès!");
                Console.ResetColor();

                var config = await GetConfiguration();

                while (true)
                {
                    Console.WriteLine("\n1 - Lister les infos");
                    Console.WriteLine("2 - Démarrer un cycle");
                    Console.WriteLine("3 - Quitter");

                    Console.Write("\nChoisissez une option: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            DisplayConfiguration(config);
                            break;
                        case "2":
                            Console.WriteLine("\nCette fonctionnalité sera disponible plus tard!");
                            break;
                        case "3":
                            Console.WriteLine("\nMerci d'avoir utilisé le simulateur!");
                            return;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nOption invalide. Veuillez réessayer.");
                            Console.ResetColor();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nErreur: {ex.Message}");
                Console.ResetColor();
            }
        }

        static async Task<Dictionary<string, object>> GetConfiguration()
        {
            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5223/api/configuration/") };

            var response = await client.GetStringAsync("");
            try
            {
                return JsonSerializer.Deserialize<Dictionary<string, object>>(response) ?? throw new Exception("La réponse JSON est nulle.");
            }
            catch (JsonException ex)
            {
                throw new Exception("Erreur de désérialisation du JSON.", ex);
            }
        }

        static void DisplayConfiguration(Dictionary<string, object> config)
        {
            try
            {
                Console.WriteLine("\nNom de l'utilisateur: " + config["username"]);

                var laveries = (JsonElement)config["laveries"];
                Console.WriteLine("├── Laveries:");
                foreach (var laverie in laveries.EnumerateArray())
                {
                    Console.WriteLine($"│   ├── {laverie.GetProperty("localisation").GetString()}");

                    var machines = laverie.GetProperty("machines");
                    Console.WriteLine($"│   │   ├── Machines:");
                    foreach (var machine in machines.EnumerateArray())
                    {
                        Console.WriteLine($"│   │   │   ├── Machine {machine.GetProperty("idMachine").GetInt32()}");

                        var cycles = machine.GetProperty("cycles");
                        Console.WriteLine($"│   │   │   │   ├── Cycles:");
                        foreach (var cycle in cycles.EnumerateArray())
                        {
                            Console.WriteLine($"│   │   │   │   │   ├── Cycle {cycle.GetProperty("duration").GetInt32()} mn - {cycle.GetProperty("cost").GetDecimal()} dt");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erreur lors de l'affichage: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
