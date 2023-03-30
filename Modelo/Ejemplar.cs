using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
   public class Ejemplar:Libro
    {
        private int nEd;
        private string ubicacion;
      
        public Ejemplar( string nombre, string autor, string iSBN, int nEd): base (autor, nombre, iSBN)
        {
           this.nombre = nombre;
           this.autor = autor;
           this.iSBN = iSBN;
           this.nEd = nEd;
           ubicacion = UbicacionLibro(Nombre);
        }

        public int NEd { get => nEd;}
        public string Ubicacion { get => ubicacion; }
       
        public string UbicacionLibro(string nombre)
        {
            char primera = nombre.First();
            
            switch (primera)
            {

                case 'A':
                case 'B':
                case 'C':

                    return ubicacion = "baja-ABC";


                case 'D':
                case 'E':
                case 'F':
                    return ubicacion = "baja-DEF";

                case 'G':
                case 'H':
                case 'I':
                    return ubicacion = "baja-GHI";

                case 'J':
                case 'K':
                case 'L':
                    return ubicacion = "media-JKL";

                case 'M':
                case 'N':
                case 'Ñ':
                    return ubicacion = "media-MNÑ";

                case 'O':
                case 'P':
                case 'Q':

                    return ubicacion = "media-OPQ";

                case 'R':
                case 'S':
                case 'T':
                    return ubicacion = "alta-RST";

                case 'U':
                case 'V':
                case 'W':
                    return ubicacion = "alta-UVW";

                case 'X':
                case 'Y':
                case 'Z':

                    return ubicacion = "alta-XYZ";

                default:
                    return ubicacion = "Extras";

            }

        }




    }
    
    
    

}
