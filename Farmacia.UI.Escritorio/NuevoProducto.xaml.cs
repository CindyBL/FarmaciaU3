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
using Farmacia.UI.Escritorio.Repositorios;
using Farmacia.UI.Escritorio.Entidades;

namespace Farmacia.UI.Escritorio
{
    /// <summary>
    /// Lógica de interacción para NuevoProducto.xaml
    /// </summary>
    public partial class NuevoProducto : Window
    {
        RepositorioDeProducto repositorio;
        bool esNuevo;
        public NuevoProducto()
        {
            InitializeComponent();
            repositorio = new RepositorioDeProducto();
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }
        private void HabilitarCajas(bool habilitadas)
        {
            txbNombre.Clear();
            txbDescripcion.Clear();
            txbPrecioCompra.Clear();
            txbPrecioVenta.Clear();
            txbPresentacion.Clear();
            txbCategoria.Clear();
            txbNombre.IsEnabled = habilitadas;
            txbDescripcion.IsEnabled = habilitadas;
            txbPrecioCompra.IsEnabled = habilitadas;
            txbPrecioVenta.IsEnabled = habilitadas;
            txbPresentacion.IsEnabled = habilitadas;
            txbCategoria.IsEnabled = habilitadas;
        }

        private void HabilitarBotones(bool habilitados)
        {
            btnNuevo.IsEnabled = habilitados;
            btnEditar.IsEnabled = habilitados;
            btnEliminar.IsEnabled = habilitados;
            btnGuardar.IsEnabled = !habilitados;
            btnCancelar.IsEnabled = !habilitados;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Productos elegir = new Productos();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(true);
            HabilitarBotones(false);
            esNuevo = true;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.LeerProducto().Count == 0)
            {
                MessageBox.Show("NO hay productos registrados ", "No hay productos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgProductos.SelectedItem != null)
                {
                    Producto Produc = dtgProductos.SelectedItem as Producto;
                    HabilitarCajas(true);
                    txbNombre.Text = Produc.NombreProducto;
                    txbPrecioVenta.Text = Produc.PrecioVenta;
                    txbPrecioCompra.Text = Produc.PrecioCompra;
                    txbDescripcion.Text = Produc.Descripcion;
                    txbPresentacion.Text = Produc.Presentacion;
                    txbCategoria.Text = Produc.Categoria;
                    HabilitarBotones(false);
                    esNuevo = false;
                }
                else
                {
                    MessageBox.Show("Seleccione el producto que desea editar", "producto", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.LeerProducto().Count == 0)
            {
                MessageBox.Show("Aun no hay productos registrados", "No existe algun producto", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgProductos.SelectedItem != null)
                {
                    Producto Produc = dtgProductos.SelectedItem as Producto;
                    if (MessageBox.Show("Realmente deseas eliminar a " + Produc.NombreProducto + "?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (repositorio.EliminarProducto(Produc))
                        {
                            MessageBox.Show("El producto ha sido eliminado", "Productos", MessageBoxButton.OK, MessageBoxImage.Information);
                            ActualizarTabla();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selecciona el producto para  eliminarlo", "Eliminar Cliente", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombre.Text) || string.IsNullOrEmpty(txbCategoria.Text) || string.IsNullOrEmpty(txbPrecioCompra.Text) || string.IsNullOrEmpty(txbPrecioVenta.Text))
            {
                MessageBox.Show("Faltan datos del Producto (Asegurese de llenar todos los campos)", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (esNuevo)
            {

                Producto produc = new Producto()
                {
                    NombreProducto = txbNombre.Text,
                    PrecioVenta = txbPrecioVenta.Text,
                    PrecioCompra = txbPrecioCompra.Text,
                    Descripcion = txbDescripcion.Text,
                    Presentacion= txbPresentacion.Text,
                    Categoria = txbCategoria.Text,
                };
                if (repositorio.AgregarProducto(produc))
                {
                    MessageBox.Show("Producto guardado exitosamente", "Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("Error al guardar el producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Producto original = dtgProductos.SelectedItem as Producto;
                Producto produc = new Producto();
                produc.NombreProducto = txbNombre.Text;
                produc.PrecioVenta = txbPrecioVenta.Text;
                produc.PrecioCompra = txbPrecioCompra.Text;
                produc.Descripcion= txbDescripcion.Text;
                produc.Presentacion = txbPresentacion.Text;
                produc.Categoria = txbCategoria.Text;
                if (repositorio.ModificarProducto(original, produc))
                {
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                    ActualizarTabla();
                    MessageBox.Show("El producto ha sido actualizado correctamente", "Productos", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar el Producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ActualizarTabla()
        {
            dtgProductos.ItemsSource = null;
            dtgProductos.ItemsSource = repositorio.LeerProducto();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void dtgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
