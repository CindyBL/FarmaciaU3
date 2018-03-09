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
using Farmacia.UI.Escritorio.Repositorios;
using Farmacia.UI.Escritorio.Entidades;
using Farmacia.UI.Escritorio;
using System.Windows.Shapes;

namespace Farmacia.UI.Escritorio
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class Empleados : Window
    {
        RepositorioDeEmpleado repositorio;
        bool esNuevo;
        public Empleados()
        {
            InitializeComponent();
            repositorio = new RepositorioDeEmpleado();
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }
        private void HabilitarCajas(bool habilitadas)
        {
            txbNombre.Clear();
            txbApellido.Clear();
            txbNEmpleado.Clear();
            txbNombre.IsEnabled = habilitadas;
            txbApellido.IsEnabled = habilitadas;
            txbNEmpleado.IsEnabled = habilitadas;
        }

        private void HabilitarBotones(bool habilitados)
        {
            btnNuevo.IsEnabled = habilitados;
            btnEditar.IsEnabled = habilitados;
            btnEliminar.IsEnabled = habilitados;
            btnGuardar.IsEnabled = !habilitados;
            btnCancelar.IsEnabled = !habilitados;
        }

        private void dtgEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            if (repositorio.LeerEmpleado().Count == 0)
            {
                MessageBox.Show("Todavia no hay empleados registrados ", "No hay empleados", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgEmpleados.SelectedItem != null)
                {
                    Empleado Empl = dtgEmpleados.SelectedItem as Empleado;
                    HabilitarCajas(true);
                    txbNombre.Text = Empl.Nombre;
                    txbApellido.Text = Empl.Apellido;
                    txbNEmpleado.Text = Empl.NoEmpleado;
                    HabilitarBotones(false);
                    esNuevo = false;
                }
                else
                {
                    MessageBox.Show("Seleccione el empleado que desea editar", "Empleados", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.LeerEmpleado().Count == 0)
            {
                MessageBox.Show("Aun no hay empleados registrados", "No existe ningun empleado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgEmpleados.SelectedItem != null)
                {
                    Empleado Empl = dtgEmpleados.SelectedItem as Empleado;
                    if (MessageBox.Show("Realmente deseas eliminar a " + Empl.Nombre + "?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (repositorio.EliminarEmpleado(Empl))
                        {
                            MessageBox.Show("El empleado ha sido eliminado", "Empleados", MessageBoxButton.OK, MessageBoxImage.Information);
                            ActualizarTabla();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar empleado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selecciona un empleado para ser eliminado", "Empleado", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombre.Text) || string.IsNullOrEmpty(txbApellido.Text) || string.IsNullOrEmpty(txbNEmpleado.Text))
            {
                MessageBox.Show("Faltan datos del empleado", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (esNuevo)
            {

                Empleado Empl = new Empleado()
                {
                    Nombre = txbNombre.Text,
                    Apellido = txbApellido.Text,
                    NoEmpleado = txbNEmpleado.Text,
                };
                if (repositorio.AgregarEmpleado(Empl))
                {
                    MessageBox.Show("Empleado guardado exitosamente", "Empleados", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("Error al guardar Empleado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Empleado Empl = new Empleado();
                Empleado  original = dtgEmpleados.SelectedItem as Empleado;
                Empl = new Empleado();
                Empl.Nombre = txbNombre.Text;
                Empl.Apellido = txbApellido.Text;
                Empl.NoEmpleado = txbNEmpleado.Text;
                if (repositorio.ModificarEmpleado(original, Empl))
                {
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                    ActualizarTabla();
                    MessageBox.Show("El Empleado ha sido actualizado", "Empleados", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar el empleado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ActualizarTabla()
        {
            dtgEmpleados.ItemsSource = null;
            dtgEmpleados.ItemsSource = repositorio.LeerEmpleado();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }
    }
}
