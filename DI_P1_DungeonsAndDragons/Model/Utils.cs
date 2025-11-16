using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DI_P1_DungeonsAndDragons.Model
{
    class Utils
    {

        /// <summary>
        /// Llegeix i deserialitza un fitxer JSON des de la carpeta "Resources" del projecte.
        /// </summary>
        /// <typeparam name="T">Tipus de dades a deserialitzar</typeparam>
        /// <param name="fileName">Nom del fitxer JSON (ex: "class_skills.json")</param>
        /// <returns>Llista d'objectes del tipus T</returns>
        public static List<T> LoadJsonFromResources<T>(string fileName)
        {
            string basePath = Path.Combine(AppContext.BaseDirectory, "Resources");
            string filePath = Path.Combine(basePath, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"No s'ha trobat el fitxer JSON a: {filePath}");

            string json = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<T>? items = JsonSerializer.Deserialize<List<T>>(json, options);
            return items ?? new List<T>();
        }
    }
}
