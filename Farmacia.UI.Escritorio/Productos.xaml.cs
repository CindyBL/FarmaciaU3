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
using System.Windows.Shapes;
namespace Farmacia.UI.Escritorio
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : Window
    {
        public Productos()
        {
            InitializeComponent();
        }

        private void btnCategoria_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Categoria elegir = new Categoria();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnProducto_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            NuevoProducto elegir = new NuevoProducto();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow elegir = new MainWindow();
            elegir.Owner = this;
            elegir.Show();
        }
    }
}
