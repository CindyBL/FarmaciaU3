using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Farmacia.UI.Escritorio.Repositorios;
using Farmacia.UI.Escritorio.Entidades;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Farmacia.UI.Escritorio
{
    /// <summary>
    /// Lógica de interacción para Categoria.xaml
    /// </summary>
    public partial class Categoria : Window
    {
        RepositorioDeCategoria repositorio;
        bool esNuevo;
        public Categoria()
        {
            InitializeComponent();
            repositorio = new RepositorioDeCategoria();
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }
        private void HabilitarCajas(bool habilitadas)
        {
            txbNombreCategoria.Clear();
            txbNombreCategoria.IsEnabled = habilitadas;
        }

        private void HabilitarBotones(bool habilitados)
        {
            btnNuevo.IsEnabled = habilitados;
            btnEliminar.IsEnabled = habilitados;
            btnEditar.IsEnabled = habilitados;
            btnGuardar.IsEnabled = !habilitados;
            btnCancelar.IsEnabled = !habilitados;
        }

        private void dtgCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            if (repositorio.LeerCategorias().Count == 0)
            {
                MessageBox.Show("Aun no hay Categorias registradas ", "No hay Categorias", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgCategoria.SelectedItem != null)
                {
                    Categorias Cat = dtgCategoria.SelectedItem as Categorias;
                    HabilitarCajas(true);
                    txbNombreCategoria.Text = Cat.nombreCategoria;
                    HabilitarBotones(false);
                    esNuevo = false;
                }
                else
                {
                    MessageBox.Show("Seleccione la Categoria que desea editar", "Editar Categorias", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.LeerCategorias().Count == 0)
            {
                MessageBox.Show("Aun no hay Categorias registradas", "No existen Categorias", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgCategoria.SelectedItem != null)
                {
                    Categorias Cat = dtgCategoria.SelectedItem as Categorias;
                    if (MessageBox.Show("Realmente deseas eliminar esta categoria " + Cat.nombreCategoria + "?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (repositorio.EliminarCategorias(Cat))
                        {
                            MessageBox.Show("La Categoria ha eliminada", "Categorias", MessageBoxButton.OK, MessageBoxImage.Information);
                            ActualizarTabla();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar Categoria", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selecciona una Categoria para ser eliminada", "Categorias", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombreCategoria.Text))
            {
                MessageBox.Show("Faltan datos para la categoria", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (esNuevo)
            {

                Categorias Cat = new Categorias()
                {
                    nombreCategoria = txbNombreCategoria.Text,
                };
                if (repositorio.AgregarCategorias(Cat))
                {
                    MessageBox.Show("La categoria fue guardada exitosamente", "Categorias", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("Error al guardar la categoria", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Categorias original = dtgCategoria.SelectedItem as Categorias;
                Categorias Cat = new Categorias();
                Cat.nombreCategoria = txbNombreCategoria.Text;
                if (repositorio.ModificarCategorias(original, Cat))
                {
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                    ActualizarTabla();
                    MessageBox.Show("La Categoria ha sido actualizada correctamente", "Categorias", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar la Categoria", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ActualizarTabla()
        {
            dtgCategoria.ItemsSource = null;
            dtgCategoria.ItemsSource = repositorio.LeerCategorias();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void btnCancelar_Click_1(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }
    }
}
