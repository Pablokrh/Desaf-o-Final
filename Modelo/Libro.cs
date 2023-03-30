using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Libro
    {
        internal string nombre;
        internal string autor;
        internal string iSBN;
        public string Nombre { get => nombre; }
        public string Autor { get => autor; }
        public string ISBN { get => iSBN; }
        private List<Ejemplar> listEjemplaresDisponibles;
        public List<Ejemplar> ListEjemplaresDisponibles { get => listEjemplaresDisponibles; }


        public Libro(string autor, string nombre, string iSBN)
        {
            this.nombre = nombre;
            this.autor = autor;
            this.iSBN = iSBN;
            listEjemplaresDisponibles = new List<Ejemplar>();
        }



    }
}
