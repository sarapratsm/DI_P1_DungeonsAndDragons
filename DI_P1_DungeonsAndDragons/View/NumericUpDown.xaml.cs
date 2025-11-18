using DependencyPropertyGenerator; // Importante
using System.Windows;
using System.Windows.Controls;

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
        public event EventHandler ValorChanged;
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

            if (btnMenys != null && btnMes != null)
            {
                btnMenys.IsEnabled = Valor != Min;
                btnMes.IsEnabled = Valor != Max;
            }

            ValorChanged?.Invoke(this, EventArgs.Empty);
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