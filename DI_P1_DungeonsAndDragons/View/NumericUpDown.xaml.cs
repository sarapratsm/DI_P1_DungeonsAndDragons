using DependencyPropertyGenerator; // Importante
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Lógica de interacción para UserControl1.xaml
    /// </summary>
    [DependencyProperty<int>("Valor", DefaultValue = 0, Description = "Valor", OnChanged = "OnValorChanged")]
    [DependencyProperty<int>("Max", DefaultValue = 15, Description = "Max value", OnChanged = "OnMaxChanged")]
    [DependencyProperty<int>("Min", DefaultValue = 8, Description = "Min value", OnChanged = "OnMinChanged")]
    public partial class NumericUpDown : UserControl
    {
        public NumericUpDown()
        {
            InitializeComponent();
        }

        private void btnMenys_Click(object sender, RoutedEventArgs e)
        {
            // El generador crea la propiedad "Valor", así que puedes usarla directamente.
            Valor--;
        }

        private void btnMes_Click(object sender, RoutedEventArgs e)
        {
            // El 'set' de Valor llamará automáticamente a OnValorChanged.
            Valor++;
        }

        public void OnValorChanged()
        {
            Valor = Math.Clamp(Valor, Min, Max);
            btnMenys.IsEnabled = Valor != Min;
            btnMes.IsEnabled = Valor != Max;
        }

        public void OnMinChanged(int old, int newValue)
        {
            if (Min >= Max)
            {
                Min = Max - 1;
            }
            OnValorChanged();
        }

        public void OnMaxChanged(int old, int newValue)
        {
            if (Max <= Min)
            {
                Max = Min + 1;
            }
            OnValorChanged();
        }

    }
}