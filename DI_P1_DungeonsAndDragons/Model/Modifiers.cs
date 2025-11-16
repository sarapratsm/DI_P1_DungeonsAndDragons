using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_P1_DungeonsAndDragons.Model
{
    internal class Modifiers
    {
        #region Propietats

        #endregion

        #region Llista
        public static List<Modifiers> GetSpecies()
        {
            return Utils.LoadJsonFromResources<Modifiers>("modifiers.json");
        }

        #endregion

        #region toSting

        #endregion
    }
}
