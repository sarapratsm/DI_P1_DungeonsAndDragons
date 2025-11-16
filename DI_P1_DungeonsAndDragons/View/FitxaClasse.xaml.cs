using DI_P1_DungeonsAndDragons.Model;
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

namespace DI_P1_DungeonsAndDragons.View
{
    /// <summary>
    /// Lógica de interacción para FitxaClasse.xaml
    /// </summary>
    public partial class FitxaClasse : UserControl
    {
        public FitxaClasse()
        {
            InitializeComponent();
        }





        public Classe TheClasse
        {
            get { return (Classe)GetValue(TheClasseProperty); }
            set { SetValue(TheClasseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TheClasse.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TheClasseProperty =
            DependencyProperty.Register("TheClasse", typeof(Classe), typeof(FitxaClasse), new PropertyMetadata(null));



    }
}
