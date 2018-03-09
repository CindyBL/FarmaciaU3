using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacia.UI.Escritorio.Herramientas;
using Farmacia.UI.Escritorio.Entidades;

namespace Farmacia.UI.Escritorio.Repositorios
{
    class RepositorioDeCategoria
    {
        ManejadorDeArchivos archivoCategorias;
        List<Categorias> Categorias;
        public RepositorioDeCategoria()
        {
            archivoCategorias = new ManejadorDeArchivos("Categoria.txt");
            Categorias = new List<Categorias>();
        }

        public bool AgregarCategorias(Categorias categorias)
        {
            Categorias.Add(categorias);
            bool resultado = ActualizarArchivo();
            Categorias = LeerCategorias();
            return resultado;
        }

        public bool EliminarCategorias(Categorias categorias)
        {
            Categorias temporal = new Categorias();
            foreach (var item in Categorias)
            {
                if (item.nombreCategoria== categorias.nombreCategoria)
                {
                    temporal = item;
                }
            }
            Categorias.Remove(temporal);
            bool resultado = ActualizarArchivo();
            Categorias = LeerCategorias();
            return resultado;
        }

        public bool ModificarCategorias(Categorias original, Categorias modificado)
        {
            Categorias temporal = new Categorias();
            foreach (var item in Categorias)
            {
                if (original.nombreCategoria == item.nombreCategoria)
                {
                    temporal = item;
                }
            }
            temporal.nombreCategoria = modificado.nombreCategoria;
            bool resultado = ActualizarArchivo();
            Categorias = LeerCategorias();
            return resultado;
        }

        private bool ActualizarArchivo()
        {
            string datos = "";
            foreach (Categorias item in Categorias)
            {
                datos += string.Format("{0}|\n", item.nombreCategoria);
            }
            return archivoCategorias.Guardar(datos);
        }
        public List<Categorias> LeerCategorias()
        {
            string datos = archivoCategorias.Leer();
            if (datos != null)
            {
                List<Categorias> categorias = new List<Categorias>();
                string[] lineas = datos.Split('\n');
                for (int i = 0; i < lineas.Length - 1; i++)
                {
                    string[] campos = lineas[i].Split('|');
                    Categorias a = new Categorias()
                    {
                        nombreCategoria = campos[0],
                    };
                    categorias.Add(a);
                }
                Categorias = categorias;
                return categorias;
            }
            else
            {
                return null;
            }
        }

    }
}
