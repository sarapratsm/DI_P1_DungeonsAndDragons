using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DI_P1_DungeonsAndDragons.Model
{
    public class Alignment
    {
        #region Propietats

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("description")]
        public string Desc { get; set; }
        #endregion

        #region Llista
        public static List<Alignment> GetAlignments()
        {
            return Utils.LoadJsonFromResources<Alignment>("alignments.json");
        }

        #endregion

        #region toSting
        public String toString()
        {
            return Name;
        }
        #endregion

    }


}
