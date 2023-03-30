using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Socio
    {
       internal string nombre;
       internal string apellido;
       internal int nId;     
       internal List<Ejemplar> listaLibrosDeSocio;

       internal int cantEjemplaresRetirables = 1;


        public string Nombre { get => nombre; }
        public string Apellido { get => apellido; }
        public int NId { get => nId; }
        public int CantEjemplaresRetirables { get => cantEjemplaresRetirables; set => cantEjemplaresRetirables = value; }
        public List<Ejemplar> ListaLibrosDeSocio { get => listaLibrosDeSocio; }

        public Socio (string nombre, string apellido, int nId)
        {
           this.nombre = nombre;
           this.apellido = apellido;
           this.nId = nId;
           listaLibrosDeSocio = new List<Ejemplar>();

        }

    }
}
