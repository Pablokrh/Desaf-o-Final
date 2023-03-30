using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class SocioVip : Socio
    {
        internal float cuotaMensual;
        public float CuotaMensual { get => cuotaMensual; }

        public SocioVip(string nombre, string apellido, int nId, float cuotaMensual) : base(nombre, apellido, nId)
        {
            this.cuotaMensual = cuotaMensual;
            this.nombre = nombre;
            this.apellido = apellido;
            this.nId = nId;
            cantEjemplaresRetirables = 3;
            listaLibrosDeSocio = new List<Ejemplar>();
        }
    }

}
