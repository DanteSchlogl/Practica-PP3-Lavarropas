namespace LavarropasApp.Domain
{
    // Una etapa del lavado: como se llama y cuantos segundos dura.
    public class EtapaLavado
    {
        public string Nombre { get; set; }
        public int Segundos { get; set; }

        public EtapaLavado(string nombre, int segundos)
        {
            Nombre = nombre;
            Segundos = segundos;
        }
    }
}
