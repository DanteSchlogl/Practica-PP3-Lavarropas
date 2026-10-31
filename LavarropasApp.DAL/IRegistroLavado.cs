using LavarropasApp.Domain;

namespace LavarropasApp.DAL
{
    // Lo que tiene que saber hacer el que guarda el registro del lavado.
    public interface IRegistroLavado
    {
        void Registrar(Lavarropas lavarropas, string mensaje);
    }
}
