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


namespace Farmacia.UI.Escritorio
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnProductos_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Productos elegir = new Productos();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnEmpleados_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Empleados elegir = new Empleados();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Cliente elegir = new Cliente();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnVentas_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Venta elegir = new Venta();
            elegir.Owner = this;
            elegir.Show();
        }
    }
}
