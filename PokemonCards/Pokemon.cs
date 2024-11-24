using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EasyHttp.Http;
using Newtonsoft.Json.Linq;

namespace PokemonCards
{
    public class Pokemon
    {
        public static List<Data> allPokemon = new List<Data>();
        
        static Pokemon()
        {
            var http = new HttpClient();
            http.Request.Accept = HttpContentTypes.ApplicationJson;
            var response = http.Get("https://pokeapi.co/api/v2/pokemon?limit=100000&offset=0");
            var body = response.DynamicBody;
            allPokemon.Capacity = body.count;
            foreach (var entry in body.results)
            {
                allPokemon.Add(new Data(entry.name, entry.url));
            }

            allPokemon = allPokemon.OrderBy(entry => entry.Name).ToList();
        }
    }
    
    public class Data
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int HealthPoints { get; set; }
        public int Strength { get; set; }
        public string SpecialPower { get; set; }
        public string ImageUrl { get; set; }
        
        TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;

        public Data(string name, string url)
        {
            this.Name = textInfo.ToTitleCase(name.Replace('-', ' '));
            this.Url = url;
            this.HealthPoints = 0;
            this.Strength = 0;
            this.SpecialPower = "";
        }

        public void populate()
        {
            if (this.HealthPoints != 0) return;
            var http = new HttpClient();
            http.Request.Accept = HttpContentTypes.ApplicationJson;
            var response = http.Get(this.Url);
            var body = response.RawText;
            var json = JObject.Parse(body);
            this.HealthPoints = json["stats"][0]["base_stat"].Value<int>();
            this.Strength = json["stats"][1]["base_stat"].Value<int>();
            this.SpecialPower = textInfo.ToTitleCase(json["abilities"][0]["ability"]["name"].Value<string>().Replace('-', ' '));
            this.ImageUrl = json["sprites"]["other"]["official-artwork"]["front_default"].Value<string>();
        }
    }
}