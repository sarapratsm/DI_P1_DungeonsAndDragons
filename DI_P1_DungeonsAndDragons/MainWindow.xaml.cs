using DI_P1_DungeonsAndDragons.Model;
using System.Collections.ObjectModel;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Personatge? personatgeSeleccionat;
        List<Personatge> llistaEquip = new List<Personatge>();
        ObservableCollection<Personatge>? llistaPersonatges = new ObservableCollection<Personatge>();
        private CreacioPersonatge? creacioPersonatgeWindow = null;

        public MainWindow()
        {
            InitializeComponent();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Carregar dades inicials
            llistaPersonatges = Model.Personatge.GetLlistaPersonatges;
            lsvPersonatges.ItemsSource = llistaPersonatges;
            lsbTeam.ItemsSource = llistaEquip;
            actualitzaBotons();
        }

        private void lsvPersonatges_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            personatgeSeleccionat = (Personatge)lsvPersonatges.SelectedItem;
            actualitzaBotons();
        }

        private void actualitzaBotons()
        {
            //el boto dret s'activa si hi ha un personatge seleccionat i l'equip té menys de 6 personatges i el personatge seleccionat no està a l'equip
            btnRight.IsEnabled = personatgeSeleccionat != null && llistaEquip.Count < 5 && !llistaEquip.Contains(personatgeSeleccionat);

            //el boto esquerre s'activa si hi ha un personatge seleccionat i el personatge seleccionat està a l'equip
            btnLeft.IsEnabled = personatgeSeleccionat != null && llistaEquip.Contains(personatgeSeleccionat);

            //El boto eliminar s'activa si hi ha un personatge seleccionat de la llista de personatges ListView i no es troba a la llista d'equip ListBox
            btnDeleteCharacter.IsEnabled = personatgeSeleccionat != null && !llistaEquip.Contains(personatgeSeleccionat);

            //El botó crear personatge ha d'estar actiu si no hi ha una finestra de creacio activa
            btnNewCharacter.IsEnabled = creacioPersonatgeWindow == null;
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            //eliminar personatge de l'equip
            if (personatgeSeleccionat != null && llistaEquip.Contains(personatgeSeleccionat))
            {
                llistaEquip.Remove(personatgeSeleccionat);
                lsbTeam.Items.Refresh();
            }
            personatgeSeleccionat = null;
            actualitzaBotons();
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            // Afegir personatge a l'equip
            if (personatgeSeleccionat != null && llistaEquip.Count < 6 && !llistaEquip.Contains(personatgeSeleccionat))
            {
                llistaEquip.Add(personatgeSeleccionat);
                lsbTeam.Items.Refresh();
            }
            personatgeSeleccionat = null;
            actualitzaBotons();
        }

        private void lsbTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            personatgeSeleccionat = (Personatge)lsbTeam.SelectedItem;
            actualitzaBotons();

        }

        private void btnDeleteCharacter_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Personatge> p = (ObservableCollection<Personatge>)lsvPersonatges.ItemsSource;
            if (personatgeSeleccionat != null && !llistaEquip.Contains(personatgeSeleccionat))
            {
                p.Remove(personatgeSeleccionat);
                lsvPersonatges.Items.Refresh();
            }
        }

        private void btnNewCharacter_Click(object sender, RoutedEventArgs e)
        {
            creacioPersonatgeWindow = new CreacioPersonatge(null);

            creacioPersonatgeWindow.Closed += CreacioPersonatge_Closed;

            //creacioPersonatgeWindow.Show(); // Mostra la finestra de manera no modal
            creacioPersonatgeWindow.ShowDialog(); // Mostra la finestra de manera modal

            Personatge nou = creacioPersonatgeWindow.PjAux;

            llistaPersonatges.Add(nou);

            actualitzaBotons();
        }

        private void CreacioPersonatge_Closed(object? sender, EventArgs e)
        {
            btnNewCharacter.IsEnabled = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            creacioPersonatgeWindow?.Close();
            creacioPersonatgeWindow = null;
        }

        private void lsvPersonatges_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Obrir la finestra passant el PersonatgeSeleccionat

            creacioPersonatgeWindow = new CreacioPersonatge(personatgeSeleccionat);

            creacioPersonatgeWindow.Closed += CreacioPersonatge_Closed;

            creacioPersonatgeWindow.ShowDialog(); // Mostra la finestra de manera modal

            Personatge? editat = creacioPersonatgeWindow.PjAux;
            if (editat != null)
            {
                llistaPersonatges.Remove(item: personatgeSeleccionat);
                llistaPersonatges.Add(editat);

            }

            actualitzaBotons();

        }
    }
}