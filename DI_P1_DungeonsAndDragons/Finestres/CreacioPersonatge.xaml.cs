using DI_P1_DungeonsAndDragons.Model;
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

        int strength, dexterity, constitution, intelligence, wisdom, charisma, age;
        string characterName;
        float height, weight;

        // Esto crea una "señal" que otra ventana puede escuchar.
        public event EventHandler WindowClosed;

        private void Window_Closed(object sender, EventArgs e)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
        }

        ClassSkills skill1, skill2;
        Alignment selectedAlignment;

        public CreacioPersonatge()
        {
            InitializeComponent();

            lsbClasses.ItemsSource = Model.Classe.GetClasses();
            lsbSpecies.ItemsSource = Model.Species.GetSpecies();

            selectedClass = ((List<Classe>)lsbClasses.ItemsSource).FirstOrDefault();
            selectedSpecies = ((List<Species>)lsbSpecies.ItemsSource).FirstOrDefault();



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
        }

        private void lsbSpecies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedSpecies = (Species)lsbSpecies.SelectedItem;
        }
    }
}
