using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacia.UI.Escritorio.Herramientas;
using Farmacia.UI.Escritorio.Entidades;

namespace Farmacia.UI.Escritorio.Repositorios
{
    class RepositorioDeProducto
    {
        ManejadorDeArchivos archivoProducto;
        List<Producto> Producto;
        public RepositorioDeProducto()
        {
            archivoProducto = new ManejadorDeArchivos("Producto.txt");
            Producto = new List<Producto>();
        }

        public bool AgregarProducto(Producto producto)
        {
            Producto.Add(producto);
            bool resultado = ActualizarArchivo();
            Producto = LeerProducto();
            return resultado;
        }

        public bool EliminarProducto(Producto producto)
        {
            Producto temporal = new Producto();
            foreach (var item in Producto)
            {
                if (item.NombreProducto == producto.NombreProducto && item.Categoria == producto.Categoria && item.PrecioCompra == producto.PrecioCompra && item.PrecioVenta == producto.PrecioVenta && item.Descripcion == producto.Descripcion && item.Presentacion == producto.Presentacion)
                {
                    temporal = item;
                }
            }
            Producto.Remove(temporal);
            bool resultado = ActualizarArchivo();
            Producto= LeerProducto();
            return resultado;
        }

        public bool ModificarProducto(Producto original, Producto modificado)
        {
            Producto temporal = new Producto();
            foreach (var item in Producto)
            {
                if (original.NombreProducto == item.NombreProducto && original.Categoria == item.Categoria && original.PrecioCompra == item.PrecioCompra && original.PrecioVenta == item.PrecioVenta && original.Descripcion == item.Descripcion && original.Presentacion == item.Presentacion)
                {
                    temporal = item;
                }
            }
            temporal.NombreProducto = modificado.NombreProducto;
            temporal.Categoria = modificado.Categoria;
            temporal.PrecioCompra = modificado.PrecioCompra;
            temporal.PrecioVenta = modificado.PrecioVenta;
            temporal.Descripcion = modificado.Descripcion;
            temporal.Presentacion = modificado.Presentacion;
            bool resultado = ActualizarArchivo();
            Producto = LeerProducto();
            return resultado;
        }

        private bool ActualizarArchivo()
        {
            string datos = "";
            foreach (Producto item in Producto)
            {
                datos += string.Format("{0}|{1}|{2}|{3}|{4}|{5}\n", item.NombreProducto, item.PrecioVenta, item.PrecioCompra, item.Descripcion, item.Presentacion, item.Categoria);
            }
            return archivoProducto.Guardar(datos);
        }
        public List<Producto> LeerProducto()
        {
            string datos = archivoProducto.Leer();
            if (datos != null)
            {
                List<Producto> producto = new List<Producto>();
                string[] lineas = datos.Split('\n');
                for (int i = 0; i < lineas.Length - 1; i++)
                {
                    string[] campos = lineas[i].Split('|');
                    Producto pro = new Producto()
                    {
                        NombreProducto = campos[0],
                        PrecioVenta = campos[1],
                        PrecioCompra = campos[2],
                        Descripcion = campos[3],
                        Presentacion = campos[4],
                        Categoria = campos[5],
                    };
                    producto.Add(pro);
                }
                Producto = producto;
                return producto;
            }
            else
            {
                return null;
            }
        }

    }
}