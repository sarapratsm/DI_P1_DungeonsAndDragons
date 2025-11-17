/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DI_P1_DungeonsAndDragons.Model
{
    internal class ClassSkills
    {
        private string name;
        private List<string> skills;

        #region Propietats

        #endregion
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("skills")]
        public List<string> Skills { get; set; }

        #region llista

        #endregion
        public static List<ClassSkills> GetClassSkills()
        {
            //string resourceName = "Resources.classes.json";

            //var assembly = Assembly.GetExecutingAssembly();
            //string fullResourceName = $"{assembly.GetName().Name}.{resourceName.Replace('/', '.')}";

            //using Stream? stream = assembly.GetManifestResourceStream(fullResourceName);
            //if (stream == null)
            //    throw new FileNotFoundException($"No s'ha trobat el recurs incrustat: {fullResourceName}");

            //using var reader = new StreamReader(stream);
            //string json = reader.ReadToEnd();

            // Obtenim la ruta de la carpeta Resources al costat de l'executable
            string basePath = Path.Combine(AppContext.BaseDirectory, "Resources");
            string filePath = Path.Combine(basePath, "class_skills.json");


            if (!File.Exists(filePath))
                throw new FileNotFoundException($"No s'ha trobat el fitxer JSON a: {filePath}");

            string json = File.ReadAllText(filePath);

            // Parsing del JSON
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<ClassSkills>? classSkills = JsonSerializer.Deserialize<List<ClassSkills>>(json, options);

            return classSkills ?? new List<ClassSkills>();
        }

        #region toSting

        #endregion
    }
}
*/