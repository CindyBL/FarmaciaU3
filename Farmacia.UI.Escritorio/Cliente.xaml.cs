using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Farmacia.UI.Escritorio.Repositorios;
using Farmacia.UI.Escritorio.Entidades;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Farmacia.UI.Escritorio
{
    /// <summary>
    /// Lógica de interacción para Cliente.xaml
    /// </summary>
    public partial class Cliente : Window
    {
        RepositorioDeClientes repositorio;
        bool esNuevo;
        public Cliente()
        {
            InitializeComponent();
            repositorio = new RepositorioDeClientes();
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }
        private void HabilitarCajas(bool habilitadas)
        {
            txbNombre.Clear();
            txbApellido.Clear();
            txbDireccion.Clear();
            txbRFC.Clear();
            txbTelefono.Clear();
            txbCorreo.Clear();
            txbNombre.IsEnabled = habilitadas;
            txbApellido.IsEnabled = habilitadas;
            txbDireccion.IsEnabled = habilitadas;
            txbRFC.IsEnabled = habilitadas;
            txbTelefono.IsEnabled = habilitadas;
            txbCorreo.IsEnabled = habilitadas;
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
            MainWindow elegir = new MainWindow();
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
            if (repositorio.LeerClientes().Count == 0)
            {
                MessageBox.Show("NO hay clientes registrados ", "No hay clientes", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgCliente.SelectedItem != null)
                {
                    Clientes Clien = dtgCliente.SelectedItem as Clientes;
                    HabilitarCajas(true);
                    txbNombre.Text = Clien.Nombre;
                    txbApellido.Text = Clien.Apellido;
                    txbDireccion.Text = Clien.Direccion;
                    txbRFC.Text = Clien.RFC;
                    txbTelefono.Text = Clien.Telefono;
                    txbCorreo.Text = Clien.Correo;
                    HabilitarBotones(false);
                    esNuevo = false;
                }
                else
                {
                    MessageBox.Show("Seleccione el Cliente que desea editar", "Clientes", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.LeerClientes().Count == 0)
            {
                MessageBox.Show("Aun no hay clientes registrados", "No existe algun cliente", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgCliente.SelectedItem != null)
                {
                    Clientes Clien = dtgCliente.SelectedItem as Clientes;
                    if (MessageBox.Show("Realmente deseas eliminar a " + Clien.Nombre + "?", "Eliminar..!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (repositorio.EliminarCliente(Clien))
                        {
                            MessageBox.Show("El cliente ha sido eliminado", "Clientes", MessageBoxButton.OK, MessageBoxImage.Information);
                            ActualizarTabla();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar Cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selecciona un Cliente para  eliminarlo", "Eliminar Cliente", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombre.Text) || string.IsNullOrEmpty(txbDireccion.Text) || string.IsNullOrEmpty(txbTelefono.Text) || string.IsNullOrEmpty(txbApellido.Text))
            {
                MessageBox.Show("Faltan datos del Cliente (Asegurese de llenar todos los campos)", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (esNuevo)
            {

                Clientes Clien = new Clientes()
                {
                    Nombre = txbNombre.Text,
                    Apellido = txbApellido.Text,
                    Direccion = txbDireccion.Text,
                    RFC = txbRFC.Text,
                    Telefono = txbTelefono.Text,
                    Correo = txbCorreo.Text,
                };
                if (repositorio.AgregarCliente(Clien))
                {
                    MessageBox.Show("Cliente guardado exitosamente", "Clientes", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("Error al guardar al Cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Clientes original = dtgCliente.SelectedItem as Clientes;
                Clientes Clien = new Clientes();
                Clien.Nombre = txbNombre.Text;
                Clien.Apellido = txbApellido.Text;
                Clien.Direccion = txbDireccion.Text;
                Clien.RFC = txbRFC.Text;
                Clien.Telefono = txbTelefono.Text;
                Clien.Correo = txbCorreo.Text;
                if (repositorio.ModificarCliente(original, Clien))
                {
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                    ActualizarTabla();
                    MessageBox.Show("El Cliente ha sido actualizado correctamente", "Clientes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar el Cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ActualizarTabla()
        {
            dtgCliente.ItemsSource = null;
            dtgCliente.ItemsSource = repositorio.LeerClientes();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }
    }
}
