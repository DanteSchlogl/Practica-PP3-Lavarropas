using System;
using System.IO;
using LavarropasApp.Domain;
using LavarropasApp.Domain.Exceptions;

namespace LavarropasApp.DAL
{
    // Guarda cada paso en un archivo de texto, uno por dia.
    public class RegistroLavadoArchivo : IRegistroLavado
    {
        private readonly string carpeta;

        public RegistroLavadoArchivo(string carpeta)
        {
            this.carpeta = carpeta;
        }

        public void Registrar(Lavarropas lavarropas, string mensaje)
        {
            try
            {
                Directory.CreateDirectory(carpeta);

                string nombre = "lavado_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string ruta = Path.Combine(carpeta, nombre);
                string linea = DateTime.Now.ToString("HH:mm:ss") + " | " + lavarropas.Modelo + " | " + mensaje;

                File.AppendAllText(ruta, linea + Environment.NewLine);
            }
            catch (Exception ex)
            {
                throw new LavadoException("No se pudo escribir el registro del lavado.", ex);
            }
        }
    }
}
