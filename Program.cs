using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PizzaFutarConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var apiUrl = "http://localhost/Pizza_futarbackend/index.php?futar";
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var futarok = JsonConvert.DeserializeObject<List<Futar>>(content);

                    // 1. A futárok adatainak kiírása a konzolra
                    Console.WriteLine("Futárok adatai:");
                    foreach (var futar in futarok)
                    {
                        Console.WriteLine($"Név: {futar.Fnev}");
                    }

                    // 2. A legkisebb értékben értékesítő futár nevének kiírása
                    string legkisebbFutarNev = "";
                    int legkisebbErtekesites = int.MaxValue;
                    foreach (var futar in futarok)
                    {
                        if (futar.Ertekesites < legkisebbErtekesites)
                        {
                            legkisebbErtekesites = futar.Ertekesites;
                            legkisebbFutarNev = futar.Fnev;
                        }
                    }
                    Console.WriteLine($"Legkisebb értékben értékesítő futár neve: {legkisebbFutarNev}");
                    
                    // 3. A futárok által kézbesített összes érték számítása
                    int osszesErtekesites = 0;
                    foreach (var futar in futarok)
                    {
                        osszesErtekesites += futar.Ertekesites;
                    }
                    Console.WriteLine($"Futárok által kézbesített összes érték: {osszesErtekesites}");
                }
                else
                {
                    Console.WriteLine($"HTTP error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
            Console.ReadLine();
        }
    }
    public class Futar
    {
        public string Fnev { get; set; }
        public int Ertekesites { get; set; }
    }
}
