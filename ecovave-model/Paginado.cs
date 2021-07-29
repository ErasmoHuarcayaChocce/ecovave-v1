using System;
using System.Collections.Generic;
using System.Text;

namespace ecovave.model
{
    public class Paginado
    {
        public int paginaActual { get; set; } = 1;
        public int tamanioPagina { get; set; } = 10;

        public Paginado()
        {

        }

        public Paginado(int paginaActual, int tamanioPagina)
        {
            this.paginaActual = paginaActual;
            this.tamanioPagina = tamanioPagina;
        }
    }
}
