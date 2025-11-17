using DI_P1_DungeonsAndDragons.Model;
using DI_P1_DungeonsAndDragons.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DI_P1_DungeonsAndDragons
{
    /// <summary>
    /// Lógica de interacción para CreacioPersonatge.xaml
    /// </summary>
    public partial class CreacioPersonatge : Window
    {
        List<Classe> llistaClasses;
        Classe? selectedClass;
        List<Species> llistaSpecies;
        Species? selectedSpecies;
        Dictionary<string, Label> bonusLabels;

        int strength, dexterity, constitution, intelligence, wisdom, charisma, age;
        string characterName;
        float height, weight;
        string skill1, skill2;
        Alignment selectedAlignment;

        // Esto crea una "señal" que otra ventana puede escuchar.
        public event EventHandler WindowClosed;


        public CreacioPersonatge()
        {
            InitializeComponent();

            lsbClasses.ItemsSource = Model.Classe.GetClasses();
            lsbSpecies.ItemsSource = Model.Species.GetSpecies();

            selectedClass = ((List<Classe>)lsbClasses.ItemsSource).FirstOrDefault();
            selectedSpecies = ((List<Species>)lsbSpecies.ItemsSource).FirstOrDefault();

            bonusLabels = new Dictionary<string, Label>
            {
                { "Strength", lblStrengthBonus },
                { "Dexterity", lblDexterityBonus },
                { "Constitution", lblConstitutionBonus },
                { "Intelligence", lblIntelligenceBonus },
                { "Wisdom", lblWisdomBonus },
                { "Charisma", lblCharismaBonus }
            };


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ActualitzarBonusRacial();
            ActualitzarBonusTotal();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
        }

        private void CalculaSpentBonus()
        {
            // --- AÑADE ESTA COMPROBACIÓN ---
            // Si alguno de los controles no se ha cargado, salimos para evitar el crash.
            if (nudStrength == null || nudDexterity == null || nudConstitution == null ||
                nudIntelligence == null || nudWisdom == null || nudCharisma == null ||
                lblSpentPoints == null)
            {
                return; // Salimos de la función
            }
            // ---------------------------------

            int spent = (nudStrength.Valor - 8) +
                        (nudDexterity.Valor - 8) +
                        (nudConstitution.Valor - 8) +
                        (nudIntelligence.Valor - 8) +
                        (nudWisdom.Valor - 8) +
                        (nudCharisma.Valor - 8);
            lblSpentPoints.Content = spent.ToString() + "/27";
        }

        private void nudStrength_ValorChanged(object sender, EventArgs e)
        {
            CalculaBonus(nudStrength, lblStrengthBonus, lblStrengthFinal);
            CalculaSpentBonus();
        }

        private void nudDexterity_ValorChanged(object sender, EventArgs e)
        {
            CalculaBonus(nudDexterity, lblDexterityBonus, lblDexterityFinal);
            CalculaSpentBonus();
        }

        private void nudConstitution_ValorChanged(object sender, EventArgs e)
        {
            CalculaBonus(nudConstitution, lblConstitutionBonus, lblConstitutionFinal);
            CalculaSpentBonus();

        }

        private void nudIntelligence_ValorChanged(object sender, EventArgs e)
        {
            CalculaBonus(nudIntelligence, lblIntelligenceBonus, lblIntelligenceFinal);
            CalculaSpentBonus();
        }

        private void nudWisdom_ValorChanged(object sender, EventArgs e)
        {
            CalculaBonus(nudWisdom, lblWisdomBonus, lblWisdomFinal);
            CalculaSpentBonus();
        }

        private void nudCharisma_ValorChanged(object sender, EventArgs e)
        {
            CalculaBonus(nudCharisma, lblCharismaBonus, lblCharismaFinal);
            CalculaSpentBonus();
        }

       

        private void ActualitzarBonusRacial()
        {
            foreach (var label in bonusLabels.Values)
            {
                label.Content = "+0";
            }

            if (selectedSpecies == null || selectedSpecies.AbilityBonuses == null)
            {
                return; // No hay nada que actualizar
            }

            foreach (var bonus in selectedSpecies.AbilityBonuses) //
            {
                if (bonusLabels.ContainsKey(bonus.Ability))
                {
                    bonusLabels[bonus.Ability].Content = $"+{bonus.Bonus}";
                }
            }
            ActualitzarBonusTotal();
        }

        

        private void ActualitzarBonusTotal()
        {
            CalculaBonus(nudCharisma, lblCharismaBonus, lblCharismaFinal);
            CalculaBonus(nudConstitution, lblConstitutionBonus, lblConstitutionFinal);
            CalculaBonus(nudDexterity, lblDexterityBonus, lblDexterityFinal);
            CalculaBonus(nudIntelligence, lblIntelligenceBonus, lblIntelligenceFinal);
            CalculaBonus(nudStrength, lblStrengthBonus, lblStrengthFinal);
            CalculaBonus(nudWisdom, lblWisdomBonus, lblWisdomFinal);
        }

        private void CalculaBonus(NumericUpDown nud, Label lbl_racial, Label lbl_total)
        {
            // 1. Comprobamos si los controles existen (medida de seguridad)
            if (nud == null || lbl_racial == null || lbl_total == null)
            {
                return; // No podemos continuar, algo falla en el XAML
            }

            int baseScore = nud.Valor;
            int racialBonus = 0;

            // 2. FORMA SEGURA de leer el bonus racial, sin que pete
            if (lbl_racial.Content != null)
            {
                // Usamos TryParse, que NUNCA explota. Si falla, bonus es 0.
                int.TryParse(lbl_racial.Content.ToString().TrimStart('+'), out racialBonus);
            }

            int total = baseScore + racialBonus;

            // 3. Fórmula oficial de D&D para el modificador
            //    (total - 10) / 2, usando división de enteros
            int modifier = (total - 10) / 2;

            // 4. Formateamos el string final
            if (modifier >= 0)
            {
                lbl_total.Content = $"{total} (+{modifier})";
            }
            else
            {
                lbl_total.Content = $"{total} ({modifier})";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lsbClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedClass = (Classe)lsbClasses.SelectedItem;

            if (selectedClass != null)
            {
                //ClassSkills:
                List<string> skillsDeLaClasse = selectedClass.Skills;
                cmbClassSkill1.ItemsSource = skillsDeLaClasse;
                cmbClassSkill2.ItemsSource = skillsDeLaClasse;
                cmbClassSkill1.SelectedIndex = 0;
                cmbClassSkill2.SelectedIndex = 1;

                


            }
        }

        private void lsbSpecies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedSpecies = (Species)lsbSpecies.SelectedItem;
            ActualitzarBonusRacial();
        }
    }
}
