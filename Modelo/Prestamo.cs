using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Prestamo
    {
       private Ejemplar ejemplarPrestado;
       private SocioVip socio;
       private DateTime fecha;
       private readonly float cuotaMensual;

        public Prestamo(Ejemplar ejemplarPrestado, string nombre, string apellido, int nId, float cuotaMensual = 0)
        {
            this.ejemplarPrestado = ejemplarPrestado;
            fecha = DateTime.Now;
            socio = new SocioVip(nombre, apellido, nId, cuotaMensual);
            cuotaMensual = socio.cuotaMensual;
        }

        public Ejemplar EjemplarPrestado { get => ejemplarPrestado; }
        public Socio Socio { get => socio; }
        public DateTime Fecha { get => fecha; }
        public float CuotaMensual {get=> cuotaMensual; }

}
}
