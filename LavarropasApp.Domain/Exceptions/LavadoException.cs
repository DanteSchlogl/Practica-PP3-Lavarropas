using System;

namespace LavarropasApp.Domain.Exceptions
{
    // Error propio del lavado, asi la UI no ve las excepciones de .NET.
    public class LavadoException : Exception
    {
        public LavadoException(string mensaje) : base(mensaje)
        {
        }

        public LavadoException(string mensaje, Exception interna) : base(mensaje, interna)
        {
        }
    }
}
