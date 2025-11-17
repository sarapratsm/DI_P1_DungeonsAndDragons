using DI_P1_DungeonsAndDragons.Model;
using DI_P1_DungeonsAndDragons.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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
        List<Alignment> llistaAlignments;
        Alignment selectedAlignment;
        Dictionary<string, Label> bonusLabels;

        int strength, dexterity, constitution, intelligence, wisdom, charisma, age;
        string characterName;
        float height, weight;
        string skill1, skill2;

        int totalSpent;

        // Esto crea una "señal" que otra ventana puede escuchar.
        public event EventHandler WindowClosed;

        bool personatgeValid;


        public CreacioPersonatge()
        {
            InitializeComponent();
            

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
            llistaAlignments = Model.Alignment.GetAlignments();
            llistaClasses = Model.Classe.GetClasses();
            llistaSpecies = Model.Species.GetSpecies();

            lsbClasses.ItemsSource = llistaClasses; lsbClasses.SelectedIndex = 0;
            lsbSpecies.ItemsSource = llistaSpecies; lsbSpecies.SelectedIndex = 0;
            cmbAlignment.ItemsSource = llistaAlignments; cmbAlignment.SelectedIndex = 0;

            selectedClass = ((List<Classe>)lsbClasses.ItemsSource).FirstOrDefault();
            selectedSpecies = ((List<Species>)lsbSpecies.ItemsSource).FirstOrDefault();
            ActualitzarBonusRacial();
            ActualitzarBonusTotal();


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
        }

        private bool PersonatgeEsValid()
        {
            if (selectedClass == null || selectedSpecies == null) { return false; }
            if (totalSpent < 27) { return false; }
            if (!(validName() && validAge() && validHeight() && validWeight())) { return false; }
            if (selectedAlignment == null) { return false; }
            if (seleccioCmbIgual(cmbClassSkill1, cmbClassSkill2)) { return false; }
            return true;
        }



        #region Classe i Especie

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
        #endregion

        #region Habilitats
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

            this.totalSpent = (nudStrength.Valor - 8) +
                        (nudDexterity.Valor - 8) +
                        (nudConstitution.Valor - 8) +
                        (nudIntelligence.Valor - 8) +
                        (nudWisdom.Valor - 8) +
                        (nudCharisma.Valor - 8);
            lblSpentPoints.Content = totalSpent.ToString() + "/27";
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
        private bool seleccioCmbIgual(ComboBox cmb1, ComboBox cmb2)
        {
            if (cmb1.SelectedItem == cmb2.SelectedItem)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #region Nud Events
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

        #endregion

        #endregion

        #region Personal Info
        private bool validName()
        {
            characterName = txtName.Text;
            if (characterName.Length < 2)
            {
                return false;
            }
            return true;
        }

        private bool validAge()
        {
            try
            {
                age = int.Parse(txtAge.Text);
                if (age < 0) { return false; }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool validHeight()
        {
            try
            {
                height = float.Parse(txtHeight.Text);
                if (height < 0) { return false; }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool validWeight()
        {
            try
            {
                weight = float.Parse(txtWeight.Text);
                if (weight < 0) { return false; }
            }
            catch
            {
                return false;
            }
            return true;
        }


        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion







        


        



        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (PersonatgeEsValid()) { 
            //Crear nou personatge, afagir-lo a la llista de personatges i tancar la finestra


            
            }
        }

    }
}
