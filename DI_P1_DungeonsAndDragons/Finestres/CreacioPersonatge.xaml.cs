using DI_P1_DungeonsAndDragons.Model;
using DI_P1_DungeonsAndDragons.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        Personatge pjEditat; //Si venim d'editar, aquí està el personatge a editar.

        List<Classe> llistaClasses;
        Classe? selectedClass;
        List<Species> llistaSpecies;
        Species? selectedSpecies;
        List<Alignment> llistaAlignments;
        Alignment selectedAlignment;
        Dictionary<string, Label> bonusLabels;

        string characterName;
        string skill1, skill2;
        int strength, dexterity, constitution, intelligence, wisdom, charisma, age;
        float height, weight;

        int totalSpent;

        // Esto crea una "señal" que otra ventana puede escuchar.
        public event EventHandler WindowClosed;

        bool personatgeValid;
        int mode;


        //PjAux serà un personatge auxiliar per fer els bindings a la finestra.
        public Personatge? PjAux
        {
            get { return (Personatge)GetValue(PjAuxProperty); }
            set { SetValue(PjAuxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PjAux.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PjAuxProperty =
            DependencyProperty.Register("PjAux", typeof(Personatge), typeof(CreacioPersonatge), new PropertyMetadata(null));




        public CreacioPersonatge(Personatge? personatgeNavegacio)
        {
            InitializeComponent();
            this.DataContext = this;


            llistaAlignments = Model.Alignment.GetAlignments();
            llistaClasses = Model.Classe.GetClasses();
            llistaSpecies = Model.Species.GetSpecies();

            lsbClasses.ItemsSource = llistaClasses;
            lsbSpecies.ItemsSource = llistaSpecies;
            cmbAlignment.ItemsSource = llistaAlignments;

            if (personatgeNavegacio == null)
            {
                mode = 0; // 0 = Nou personatge
                PjAux = new Personatge(); // Creem un de nou
            }
            else
            {
                mode = 1; // 1 = Editar
                PjAux = personatgeNavegacio; // Assignem el que estem editant
            }

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

            if (mode == 1) // Si estem editant
            {
                selectedClass = PjAux.Classe;
                selectedSpecies = PjAux.Species;
                // Assignar tots els valors
                Classe lst_classe = llistaClasses.FirstOrDefault(c => c.Name == PjAux.Classe.Name);
                if (lst_classe != null)
                {
                    lsbClasses.SelectedItem = lst_classe;
                }

                // Per a la Especie
                Species lst_species = llistaSpecies.FirstOrDefault(s => s.Name == PjAux.Species.Name);
                if (lst_species != null)
                {
                    lsbSpecies.SelectedItem = lst_species;
                }

                // Per al Alignment
                Alignment lst_alignment = llistaAlignments.FirstOrDefault(a => a.Name == PjAux.Alignment.Name);
                if (lst_alignment != null)
                {
                    cmbAlignment.SelectedItem = lst_alignment;
                }

                txtName.Text = PjAux.Nom;
                txtAge.Text = PjAux.Edat + "";
                //TODO: carregar la resta de dades (puntuacions, skills, etc.)
            }
            else // Si estem creant un de nou
            {
                selectedClass = ((List<Classe>)lsbClasses.ItemsSource).FirstOrDefault();
                selectedSpecies = ((List<Species>)lsbSpecies.ItemsSource).FirstOrDefault();
                // Inicialitzar per defecte
                lsbClasses.SelectedIndex = 0;
                lsbSpecies.SelectedIndex = 0;
                cmbAlignment.SelectedIndex = 0;
            }

            ActualitzarBonusRacial();
            ActualitzarBonusTotal();




        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
        }

        private bool PersonatgeEsValid()
        {
            bool esValid = true;
            if (selectedClass == null)
            {
                tbcTab.SelectedIndex = 0; // Cambia a la Pestaña 0
                ShowValidationError(lsbClasses, "Selecciona una classe");
                esValid = false;
            }
            if (selectedSpecies == null)
            {
                tbcTab.SelectedIndex = 0; // Cambia a la Pestaña 0
                ShowValidationError(lsbSpecies, "Selecciona una especie");
                return false;
            }
            if (!ValidPoints())
            {
                tbcTab.SelectedIndex = 1;
                //ShowValidationError(lblSpentPoints, "Has de gastar exactament 27 punts!");
                esValid = false;
            }
            if (seleccioCmbIgual(cmbClassSkill1, cmbClassSkill2))
            {
                tbcTab.SelectedIndex = 1;
                //ShowValidationError(lblSkills, "Les habilitats de classe han de ser diferents");
                return false;
            }
            if (!validName())
            {
                tbcTab.SelectedIndex = 2;
                ShowValidationError(txtName, "El nom ha de tenir almenys 2 caràcters");
                esValid = false;
            }
            if (!validAge())
            {
                tbcTab.SelectedIndex = 2;
                ShowValidationError(txtAge, "L'edat ha de ser un nombre vàlid i positiu");
                esValid = false;
            }
            if (!validHeight())
            {
                tbcTab.SelectedIndex = 2;
                ShowValidationError(txtHeight, "L'alçada ha de ser un nombre vàlid i positiu");
                esValid = false;
            }
            if (!validWeight())
            {
                tbcTab.SelectedIndex = 2;
                ShowValidationError(txtWeight, "El pes ha de ser un nombre vàlid i positiu");
                esValid = false;
            }

            if (cmbAlignment.SelectedItem == null)
            {
                tbcTab.SelectedIndex = 2;
                ShowValidationError(cmbAlignment, "Selecciona una alineació");
                esValid = false;
            }
            return esValid;
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
                return;
            }

            this.totalSpent = (nudStrength.Valor - 8) +
                        (nudDexterity.Valor - 8) +
                        (nudConstitution.Valor - 8) +
                        (nudIntelligence.Valor - 8) +
                        (nudWisdom.Valor - 8) +
                        (nudCharisma.Valor - 8);
            lblSpentPoints.Text = totalSpent.ToString() + "/27";
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

        private void cmbClassSkill1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (seleccioCmbIgual(cmbClassSkill1, cmbClassSkill2))
            {
                ShowValidationError(lblSkills, "Les habilitats de classe han de ser diferents");
            }
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (validName())
            {
                // El nombre es válido, puedes realizar acciones adicionales aquí si es necesario.
                characterName = txtName.Text;
            }
            else
            {
                // El nombre no es válido, puedes mostrar un mensaje de error o realizar otras acciones.
                //ShowValidationError(txtName, "El nom ha de tenir almenys 2 caràcters");
            }
        }

        private void txtHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (validHeight())
            {
                height = float.Parse(txtHeight.Text);
            }
            else
            {
                ShowValidationError(txtHeight, "L'alçada ha de ser un número positiu");
            }
        }

        private void txtWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (validWeight())
            {
                weight = float.Parse(txtWeight.Text);
            }
            else
            {
                ShowValidationError(txtWeight, "El pes ha de ser un número positiu");
            }
        }

        private void txtAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (validAge())
            {
                age = int.Parse(txtAge.Text);
            }
            else
            {
                ShowValidationError(txtAge, "L'edat ha de ser un número positiu");
            }
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
            if (txtAge.Text.Trim() == "")
            {
                return false;
            }
            try
            {
                age = int.Parse(txtAge.Text);
                if (age <= 0) { return false; }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool validHeight()
        {
            if (txtHeight.Text.Trim() == "")
            {
                return false;
            }
            try
            {
                height = float.Parse(txtHeight.Text);
                if (height <= 0) { return false; }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool validWeight()
        {
            if (txtWeight.Text.Trim() == "")
            {
                return false;
            }
            try
            {
                weight = float.Parse(txtWeight.Text);
                if (weight <= 0) { return false; }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool ValidPoints()
        {
            if (totalSpent > 27)
            {
                ShowValidationError(lblSpentPoints, "El màxim de punts és 27!");
                return false;
            }
            else if (totalSpent < 27)
            {
                ShowValidationError(lblSpentPoints, "Has de gastar els 27 punts!");
                return false;
            }
            return true;
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imatges|*.jpg;*.png;*.bmp;*.jpeg"; // Filtre per imatges

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // 1. Obtenim la ruta del fitxer original seleccionat
                    string rutaOrigen = openFileDialog.FileName;
                    string nomFitxer = System.IO.Path.GetFileName(rutaOrigen);

                    // 2. Definim la ruta de destí (la carpeta 'img' on s'executa l'app)
                    string carpetaDesti = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img");

                    // Si la carpeta 'img' no existeix a la carpeta de sortida (bin/Debug...), la creem
                    if (!System.IO.Directory.Exists(carpetaDesti))
                    {
                        System.IO.Directory.CreateDirectory(carpetaDesti);
                    }

                    string rutaDesti = System.IO.Path.Combine(carpetaDesti, nomFitxer);

                    // 3. Copiem el fitxer a la nostra carpeta
                    // El 'true' permet sobreescriure si ja existeix un fitxer amb el mateix nom
                    System.IO.File.Copy(rutaOrigen, rutaDesti, true);

                    // 4. Assignem la nova ruta relativa al personatge
                    // Fem servir '/img/' perquè WPF ho entengui com a ruta relativa
                    PjAux.ImageUrl = $"/img/{nomFitxer}";

                    // (Opcional) Si la imatge no s'actualitza automàticament per algun motiu, 
                    // pots forçar el refresc així:
                    // imgPhotoPreview.Source = new BitmapImage(new Uri(rutaDesti));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al pujar la imatge: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion

        /// <summary>
        /// Muestra un ToolTip (burbuja) de error rojo junto a un control específico.
        /// </summary>
        /// <param name="invalidControl">El control que tiene el error (ej: txtName, cmbAlignment).</param>
        /// <param name="errorMessage">El mensaje que se mostrará en la burbuja.</param>
        private void ShowValidationError(Control invalidControl, string errorMessage)
        {
            // 1. Crea el ToolTip (la burbuja)
            ToolTip errorToolTip = new ToolTip
            {
                Content = errorMessage,
                Background = Brushes.Red,
                Foreground = Brushes.White,
                Placement = PlacementMode.Right,
                PlacementTarget = invalidControl,
                IsOpen = true
            };

            // 2. Asigna el ToolTip al control
            invalidControl.ToolTip = errorToolTip;

            // 3. Cuando el control pierda el foco (el usuario haga clic en otro sitio),
            //    cierra la burbuja para que no se quede ahí para siempre.
            invalidControl.LostFocus += (s, e) => { errorToolTip.IsOpen = false; };

            // 4. (Opcional) Si el control es un TextBox, cierra la burbuja en cuanto el usuario empiece a escribir.
            if (invalidControl is TextBox textBox)
            {
                textBox.TextChanged += (s, e) => { errorToolTip.IsOpen = false; };
            }

            // 5. Pon el foco en el control que tiene el error
            invalidControl.Focus();
        }












        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (mode == 0)// 0 = Nou personatge
            {
                PjAux = null;
            }
            else if (mode == 1) //1=Editat
            {
                PjAux = pjEditat; //Revertim els canvis
            }
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (PersonatgeEsValid())
            {
                // Guardar classe, etc.
                PjAux.Classe = (Classe)lsbClasses.SelectedItem; 
                PjAux.Species = (Species)lsbSpecies.SelectedItem;
                PjAux.Alignment = (Alignment)cmbAlignment.SelectedItem;
          
                //Hbalitats
                PjAux.Skill1 = cmbClassSkill1.SelectedItem?.ToString() ?? "";
                PjAux.Skill2 = cmbClassSkill2.SelectedItem?.ToString() ?? "";

                //Assignar punts
                PjAux.PuntsFor = nudStrength.Valor;
                PjAux.PuntsDes = nudDexterity.Valor;
                PjAux.PuntsCon = nudConstitution.Valor;
                PjAux.PuntsInt = nudIntelligence.Valor;
                PjAux.PuntsSav = nudWisdom.Valor;
                PjAux.PuntsCar = nudCharisma.Valor;

                Close();
            }
        }







    }
}
