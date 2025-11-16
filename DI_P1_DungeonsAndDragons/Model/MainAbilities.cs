using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_P1_DungeonsAndDragons.Model
{
    internal class MainAbilities
    {

        #region Propietats

        #endregion

        #region Llista
        public static List<MainAbilities> GetSpecies()
        {
            return Utils.LoadJsonFromResources<MainAbilities>("main_abilities.json");
        }

        #endregion

        #region toSting

        #endregion

    }
}
