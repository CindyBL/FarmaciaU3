using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacia.UI.Escritorio.Herramientas;
using Farmacia.UI.Escritorio.Entidades;

namespace Farmacia.UI.Escritorio.Repositorios
{
    class RepositorioDeClientes
    {
        ManejadorDeArchivos archivoClientes;
        List<Clientes> Clientes;
        public RepositorioDeClientes()
        {
            archivoClientes = new ManejadorDeArchivos("Clientes.txt");
            Clientes = new List<Clientes>();
        }

        public bool AgregarCliente(Clientes cliente)
        {
            Clientes.Add(cliente);
            bool resultado = ActualizarArchivo();
            Clientes = LeerClientes();
            return resultado;
        }

        public bool EliminarCliente(Clientes cliente)
        {
            Clientes temporal = new Clientes();
            foreach (var item in Clientes)
            {
                if (item.Nombre == cliente.Nombre && item.Apellido == cliente.Apellido && item.Direccion == cliente.Direccion && item.RFC == cliente.RFC && item.Telefono == cliente.Telefono && item.Correo == cliente.Correo)
                {
                    temporal = item;
                }
            }
            Clientes.Remove(temporal);
            bool resultado = ActualizarArchivo();
            Clientes = LeerClientes();
            return resultado;
        }

        public bool ModificarCliente(Clientes original, Clientes modificado)
        {
            Clientes temporal = new Clientes();
            foreach (var item in Clientes)
            {
                if (original.Nombre == item.Nombre && original.Apellido == item.Apellido && original.Direccion == item.Direccion && original.RFC == item.RFC && original.Telefono == item.Telefono && original.Correo == item.Correo)
                {
                    temporal = item;
                }
            }
            temporal.Nombre = modificado.Nombre;
            temporal.Apellido = modificado.Apellido;
            temporal.Direccion = modificado.Direccion;
            temporal.RFC = modificado.RFC;
            temporal.Telefono = modificado.Telefono;
            temporal.Correo = modificado.Correo;
            bool resultado = ActualizarArchivo();
            Clientes = LeerClientes();
            return resultado;
        }

        private bool ActualizarArchivo()
        {
            string datos = "";
            foreach (Clientes item in Clientes)
            {
                datos += string.Format("{0}|{1}|{2}|{3}|{4}|{5}\n", item.Nombre, item.Apellido, item.Direccion, item.RFC, item.Telefono, item.Correo);
            }
            return archivoClientes.Guardar(datos);
        }
        public List<Clientes> LeerClientes()
        {
            string datos = archivoClientes.Leer();
            if (datos != null)
            {
                List<Clientes> cliente = new List<Clientes>();
                string[] lineas = datos.Split('\n');
                for (int i = 0; i < lineas.Length - 1; i++)
                {
                    string[] campos = lineas[i].Split('|');
                    Clientes a = new Clientes()
                    {
                        Nombre = campos[0],
                        Apellido = campos[1],
                        Direccion = campos[2],
                        RFC = campos[3],
                        Telefono = campos[4],
                        Correo = campos[5],
                    };
                    cliente.Add(a);
                }
                Clientes = cliente;
                return cliente;
            }
            else
            {
                return null;
            }
        }

    }
}
