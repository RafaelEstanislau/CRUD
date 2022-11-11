﻿

namespace Dominio.RegraDeNegocio
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string mensagem) : base(mensagem){ }
        public NotFoundException(string mensagem, Exception innerException) : base(mensagem, innerException) { }

    }
}
