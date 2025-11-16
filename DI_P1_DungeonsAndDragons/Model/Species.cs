using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DI_P1_DungeonsAndDragons.Model
{
    public class Species
    {
        #region Properties
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("traits")]
        public string Traits { get; set; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("ability_bonuses")]
        public List<AbilityBonus> AbilityBonuses { get; set; }

        [JsonIgnore]
        #endregion

        public string AbilityBonusesDisplay
        {
            get
            {
                if (AbilityBonuses == null || AbilityBonuses.Count == 0)
                    return string.Empty;

                return string.Join(", ", AbilityBonuses.Select(ab => $"{ab.Ability} +{ab.Bonus}"));
            }
        }

        public static List<Species> GetSpecies()
        {
            string basePath = Path.Combine(AppContext.BaseDirectory, "Resources");
            string filePath = Path.Combine(basePath, "species_merged.json");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"No s'ha trobat el fitxer JSON a: {filePath}");

            string json = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Species>? speciesList = JsonSerializer.Deserialize<List<Species>>(json, options);

            return speciesList ?? new List<Species>();
        }

        public override string ToString()
        {
            return $"{Name} - {AbilityBonusesDisplay}";
        }
    }

    public class AbilityBonus
    {
        [JsonPropertyName("ability")]
        public string Ability { get; set; }

        [JsonPropertyName("bonus")]
        public int Bonus { get; set; }
    }
}
