namespace LavarropasApp.Domain
{
    // El lavarropas: sabe en que etapa esta y si esta prendido.
    public class Lavarropas
    {
        public string Modelo { get; set; }
        public string EtapaActual { get; set; }
        public bool Encendido { get; set; }

        public Lavarropas(string modelo)
        {
            Modelo = modelo;
            EtapaActual = "Apagado";
        }
    }
}
