using LavarropasApp.DAL;
using LavarropasApp.Domain;

namespace LavarropasApp.BLL
{
    // La UI pide todo por aca, asi no conoce ni el lavarropas ni el registro.
    public static class FabricaLavado
    {
        public static ServicioLavado Crear(string modelo, string carpetaRegistros)
        {
            Lavarropas lavarropas = new Lavarropas(modelo);
            IRegistroLavado registro = new RegistroLavadoArchivo(carpetaRegistros);
            return new ServicioLavado(lavarropas, registro);
        }
    }
}
