using System;
using LavarropasApp.BLL;
using LavarropasApp.Domain.Exceptions;

namespace LavarropasApp.UI
{
    class Program
    {
        static void Main()
        {
            try
            {
                ServicioLavado servicio = FabricaLavado.Crear("Whirlpool", "Registros");
                servicio.Aviso += Mostrar;

                // Cada etapa se demora lo que dice el segundo numero.
                servicio.AgregarEtapa("Llenar", 1);
                servicio.AgregarEtapa("Lavar", 3);
                servicio.AgregarEtapa("Enjuagar", 2);
                servicio.AgregarEtapa("Centrifugar", 2);

                Console.WriteLine("Arranca el lavado");
                servicio.Lavar();
            }
            catch (LavadoException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void Mostrar(string mensaje)
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "  " + mensaje);
        }
    }
}
