using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacia.UI.Escritorio.Herramientas;
using Farmacia.UI.Escritorio.Entidades;

namespace Farmacia.UI.Escritorio.Repositorios
{
    class RepositorioDeEmpleado
    {
        ManejadorDeArchivos archivoEmpleado;
        List<Empleado> Empleado;
        public RepositorioDeEmpleado()
        {
            archivoEmpleado = new ManejadorDeArchivos("Empleados.txt");
            Empleado = new List<Empleado>();
        }

        public bool AgregarEmpleado(Empleado empleado)
        {
            Empleado.Add(empleado);
            bool resultado = ActualizarArchivo();
            Empleado = LeerEmpleado();
            return resultado;
        }

        public bool EliminarEmpleado(Empleado empleado)
        {
            Empleado temporal = new Empleado();
            foreach (var item in Empleado)
            {
                if (item.Nombre == empleado.Nombre && item.Apellido == empleado.Apellido && item.NoEmpleado == empleado.NoEmpleado)
                {
                    temporal = item;
                }
            }
            Empleado.Remove(temporal);
            bool resultado = ActualizarArchivo();
            Empleado = LeerEmpleado();
            return resultado;
        }

        public bool ModificarEmpleado(Empleado original, Empleado modificado)
        {
            Empleado temporal = new Empleado();
            foreach (var item in Empleado)
            {
                if (original.Nombre == item.Nombre && original.Apellido == item.Apellido && original.NoEmpleado == item.NoEmpleado)
                {
                    temporal = item;
                }
            }
            temporal.Nombre = modificado.Nombre;
            temporal.Apellido = modificado.Apellido;
            temporal.NoEmpleado = modificado.NoEmpleado;
            bool resultado = ActualizarArchivo();
            Empleado = LeerEmpleado();
            return resultado;
        }

        private bool ActualizarArchivo()
        {
            string datos = "";
            foreach (Empleado item in Empleado)
            {
                datos += string.Format("{0}|{1}|{2}\n", item.Nombre, item.Apellido, item.NoEmpleado);
            }
            return archivoEmpleado.Guardar(datos);
        }
        public List<Empleado> LeerEmpleado()
        {
            string datos = archivoEmpleado.Leer();
            if (datos != null)
            {
                List<Empleado> empleado = new List<Empleado>();
                string[] lineas = datos.Split('\n');
                for (int i = 0; i < lineas.Length - 1; i++)
                {
                    string[] campos = lineas[i].Split('|');
                    Empleado a = new Empleado()
                    {
                        Nombre = campos[0],
                        Apellido = campos[1],
                        NoEmpleado = campos[2],
                    };
                    empleado.Add(a);
                }
                Empleado = empleado;
                return empleado;
            }
            else
            {
                return null;
            }
        }

    }
}
