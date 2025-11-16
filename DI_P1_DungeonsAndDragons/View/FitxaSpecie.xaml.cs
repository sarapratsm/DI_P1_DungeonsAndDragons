using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DI_P1_DungeonsAndDragons.Model;

namespace DI_P1_DungeonsAndDragons.View
{
    /// <summary>
    /// Lógica de interacción para FitxaSpecie.xaml
    /// </summary>
    public partial class FitxaSpecie : UserControl
    {
        public FitxaSpecie()
        {
            InitializeComponent();
        }
        public Species TheSpecies
        {
            get { return (Species)GetValue(TheSpeciesProperty); }
            set { SetValue(TheSpeciesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TheSpecies.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TheSpeciesProperty =
            DependencyProperty.Register("TheSpecies", typeof(Species), typeof(FitxaSpecie), new PropertyMetadata(null));

    }
}
