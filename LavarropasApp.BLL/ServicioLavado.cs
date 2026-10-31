using System;
using System.Collections.Generic;
using System.Threading;
using LavarropasApp.DAL;
using LavarropasApp.Domain;
using LavarropasApp.Domain.Exceptions;

namespace LavarropasApp.BLL
{
    // Lleva adelante el programa de lavado, etapa por etapa.
    public class ServicioLavado
    {
        private readonly Lavarropas lavarropas;
        private readonly IRegistroLavado registro;
        private readonly List<EtapaLavado> programa = new List<EtapaLavado>();

        // La UI se suscribe aca para ir mostrando el progreso.
        public event Action<string> Aviso;

        public ServicioLavado(Lavarropas lavarropas, IRegistroLavado registro)
        {
            this.lavarropas = lavarropas;
            this.registro = registro;
        }

        public void AgregarEtapa(string nombre, int segundos)
        {
            if (segundos <= 0)
            {
                throw new LavadoException("La etapa '" + nombre + "' tiene que durar mas de 0 segundos.");
            }
            programa.Add(new EtapaLavado(nombre, segundos));
        }

        public void Lavar()
        {
            if (programa.Count == 0)
            {
                throw new LavadoException("El lavarropas no tiene ninguna etapa cargada.");
            }

            lavarropas.Encendido = true;
            registro.Registrar(lavarropas, "Empieza el lavado");

            int numero = 1;
            foreach (EtapaLavado etapa in programa)
            {
                lavarropas.EtapaActual = etapa.Nombre;
                Avisar("Etapa " + numero + " de " + programa.Count + ": " + etapa.Nombre);

                // Aca esta el delay: el programa se queda esperando lo que dura la etapa.
                Thread.Sleep(etapa.Segundos * 1000);

                registro.Registrar(lavarropas, etapa.Nombre + " listo (" + etapa.Segundos + " s)");
                Avisar(etapa.Nombre + " listo");
                numero = numero + 1;
            }

            lavarropas.EtapaActual = "Terminado";
            lavarropas.Encendido = false;
            registro.Registrar(lavarropas, "Fin del lavado");
            Avisar("Fin del lavado");
        }

        private void Avisar(string mensaje)
        {
            if (Aviso != null)
            {
                Aviso(mensaje);
            }
        }
    }
}
