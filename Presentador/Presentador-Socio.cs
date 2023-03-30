using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentador
{
    public partial class Presentador
    {
      
        List<Prestamo> HistorialPrestados = new List<Prestamo>();
        List<Prestamo> HistorialDevueltos = new List<Prestamo>();

        public void AgregaSocio(string nombre, string apellido, int nId, float cuotaMensual, string socioVip)
        {
            if (socioVip == "si")
            {
                ListadoSociosVip.Add(new SocioVip(nombre, apellido, nId, cuotaMensual));
            }

            else
            {
                ListadoSocios.Add(new Socio(nombre, apellido, nId));
            }

            ImprimeSocios();
        }


        public void ImprimeSocios()
        {
            if (ListadoSociosVip.Count > 0)
            {
                _Vista.MostrarTexto("\nListado de socios VIP:");

                for (int i = 0; i < ListadoSociosVip.Count; i++)
                {

                    _Vista.MostrarTexto("\nId: " + ListadoSociosVip[i].NId + ". Nombre: " + ListadoSociosVip[i].Nombre + ". Apellido: " + ListadoSociosVip[i].Apellido + ". Cuota Mensual: " + ListadoSociosVip[i].CuotaMensual);
                }
            }

            if (ListadoSocios.Count > 0)
            {
                _Vista.MostrarTexto("\n\nListado de socios comunes:\n");

                for (int i = 0; i < ListadoSocios.Count; i++)
                {

                    _Vista.MostrarTexto("Id: " + ListadoSocios[i].NId + ". Nombre: " + ListadoSocios[i].Nombre + ". Apellido: " + ListadoSocios[i].Apellido);
                }
            }
        }


        public void PuedeRetirar(int codigo)
        {
            bool socioEncontrado = false;

            do
            {
                if (ListadoSocios.Count > 0)
                {

                    for (int i = 0; i < ListadoSocios.Count; i++)
                    {
                        if (ListadoSocios[i].NId == codigo)
                        {
                            if (ListadoSocios[i].CantEjemplaresRetirables > 0)
                            {
                                _Vista.MostrarTexto("El socio puede retirar " + ListadoSocios[i].CantEjemplaresRetirables + " ejemplares");
                            }
                            else _Vista.MostrarTexto("Ya no puede retirar ejemplares");
                            socioEncontrado = true;
                        }
                    }
                }

                if (ListadoSociosVip.Count > 0)
                {
                    for (int i = 0; i < ListadoSociosVip.Count; i++)
                    {
                        if (ListadoSociosVip[i].NId == codigo)
                        {
                            if (ListadoSociosVip[i].CantEjemplaresRetirables > 0)
                            {
                                _Vista.MostrarTexto("El socio puede retirar " + ListadoSociosVip[i].CantEjemplaresRetirables + " ejemplares");
                            }
                            else _Vista.MostrarTexto("Ya no puede retirar ejemplares");
                            socioEncontrado = true;

                        }
                    }
                }
            } while (socioEncontrado == false);

        }

        public bool HaySocio()
        {
            if (ListadoSocios.Count == 0 && ListadoSociosVip.Count == 0)
            {
                return false;
            }
            return true;
        }
        

        public bool SocioExiste(int numAux)
        {
            bool existeElSocio = false;
            for (int i = 0; i < ListadoSocios.Count; i++)
            {
                if (ListadoSocios[i].NId == numAux) existeElSocio = true;
            }
            for (int l = 0; l < ListadoSociosVip.Count; l++)
            {
                if (ListadoSociosVip[l].NId == numAux) existeElSocio = true;

            }

            if (existeElSocio == true) return true;
            else return false;

        }
     
        public void PrestaASocio(int opcionEjemplar, int opcionSocio, ref string hayEjemplar)
        {
            bool HayEjemplar = false;
            int indiceEjemplar = 0;
            for (int i = 0; i < LibrosExistentes.Count; i++)
            {
                for (int x = 0; x < librosExistentes[i].ListEjemplaresDisponibles.Count; x++)
                {
                    if (indiceEjemplar == opcionEjemplar)
                    {
                        int indiceLibEx = i;
                        int indiceListEjDisp = x;
                        Ejemplar ejemplarAPrestar = librosExistentes[i].ListEjemplaresDisponibles[x];
                        PrestaSocioII(opcionSocio, ejemplarAPrestar, indiceLibEx, indiceListEjDisp);
                        HayEjemplar = true;
                    }
                    indiceEjemplar++;

                }
            }
            if (HayEjemplar == false) hayEjemplar = "No se encontraron ejemplares con el valor ingresado";

        }

        private void PrestaSocioII(int opcionSocio, Ejemplar ejemplarAPrestar, int indiceLibEx, int indiceListEjDisp)
        {
            for (int y = 0; y < ListadoSociosVip.Count; y++)
            {
                if (opcionSocio == ListadoSociosVip[y].NId)
                {
                    if (ListadoSociosVip[y].CantEjemplaresRetirables > 0)
                    {
                        ListadoSociosVip[y].ListaLibrosDeSocio.Add(ejemplarAPrestar);
                        ListadoSociosVip[y].CantEjemplaresRetirables--;
                        librosExistentes[indiceLibEx].ListEjemplaresDisponibles.Remove(librosExistentes[indiceLibEx].ListEjemplaresDisponibles[indiceListEjDisp]);
                        HistorialPrestados.Add(new Prestamo(ejemplarAPrestar, ListadoSociosVip[y].Apellido, ListadoSociosVip[y].Nombre, ListadoSociosVip[y].NId, ListadoSociosVip[y].CuotaMensual));
                    }
                    else
                    {
                        _Vista.MostrarTexto("El socio seleccionado no puede recibir mas libros prestados");
                        Console.ReadKey();
                    }

                }
            }

            for (int z = 0; z < ListadoSocios.Count; z++)
            {
                if (opcionSocio == ListadoSocios[z].NId)
                {
                    if (ListadoSocios[z].CantEjemplaresRetirables > 0)
                    {
                        ListadoSocios[z].ListaLibrosDeSocio.Add(ejemplarAPrestar);
                        ListadoSocios[z].CantEjemplaresRetirables--;
                        librosExistentes[indiceLibEx].ListEjemplaresDisponibles.Remove(librosExistentes[indiceLibEx].ListEjemplaresDisponibles[indiceListEjDisp]);
                        HistorialPrestados.Add(new Prestamo(ejemplarAPrestar, ListadoSocios[z].Apellido, ListadoSocios[z].Nombre, ListadoSocios[z].NId));
                    }
                   
                    else
                    {
                        _Vista.MostrarTexto("El socio no puede recibir mas préstamos por el momento");
                        Console.ReadKey();
                    }
                    

                }

            }

        }



        public void ImprimeEjemplaresDeSocio(int opcionSocio)
        {
            bool avanzar = true;
            int indiceEjemplar = 0;
            for (int i = 0; i < ListadoSocios.Count; i++)
            {
                if (opcionSocio == ListadoSocios[i].NId)
                {
                    for (int x = 0; x < ListadoSocios[i].ListaLibrosDeSocio.Count; x++)
                    {
                        indiceEjemplar = x;
                        _Vista.MostrarTexto(indiceEjemplar + "- Libro: " + ListadoSocios[i].ListaLibrosDeSocio[x].Nombre + ". Autor: " + ListadoSocios[i].ListaLibrosDeSocio[x].Autor + ". Año de edición: " + ListadoSocios[i].ListaLibrosDeSocio[x].NEd + ". Código ISBN: " + ListadoSocios[i].ListaLibrosDeSocio[x].ISBN);
                        avanzar = false;
                    }
                }
            }
            if (avanzar == true)
            {
                for (int i = 0; i < ListadoSociosVip.Count; i++)
                {
                    if (opcionSocio == ListadoSociosVip[i].NId)
                    {
                        for (int x = 0; x < ListadoSociosVip[i].ListaLibrosDeSocio.Count; x++)
                        {
                            _Vista.MostrarTexto(x + "- Libro: " + ListadoSociosVip[i].ListaLibrosDeSocio[x].Nombre + ". Autor: " + ListadoSociosVip[i].ListaLibrosDeSocio[x].Autor + ". Año de edición" + ListadoSociosVip[i].ListaLibrosDeSocio[x].NEd + ". Código ISBN: " + ListadoSociosVip[i].ListaLibrosDeSocio[x].ISBN);
                        }
                    }
                }
            }

        }

        public bool ControlaId(int numAux)
        {
            bool idPrevio = false;
            for (int i = 0; i < ListadoSocios.Count; i++)
            {
                if (ListadoSocios[i].NId == numAux) idPrevio = true;

            }

            for (int li = 0; li < ListadoSociosVip.Count; li++)
            {
                if (ListadoSociosVip[li].NId == numAux) idPrevio = true;
            }

            if (idPrevio == true) return true;
            else return false;
        }

        public void SocioTieneEjemplar(int indiceEjemplar, int iDSocio, ref bool avanzar)
        {
            bool hayEjemplar = false;
            bool hayAlgo = false;

            for (int i = 0; i < ListadoSocios.Count; i++)
            {
                if (iDSocio == ListadoSocios[i].NId)
                {
                    if (ListadoSocios[i].ListaLibrosDeSocio.Count > 0)
                    {
                        for (int x = 0; x < ListadoSocios[i].ListaLibrosDeSocio.Count; x++)
                        {

                            if (indiceEjemplar == x)
                            {
                                hayEjemplar = true;
                            }
                        }
                        hayAlgo = true;

                    }


                }

            }

            for (int i = 0; i < ListadoSociosVip.Count; i++)
            {
                if (iDSocio == ListadoSociosVip[i].NId)
                {
                    if (ListadoSociosVip[i].ListaLibrosDeSocio.Count > 0)
                    {
                        for (int x = 0; x < ListadoSociosVip[i].ListaLibrosDeSocio.Count; x++)
                        {

                            if (indiceEjemplar == x)
                            {
                                hayEjemplar = true;
                            }
                        }
                        hayAlgo = true;


                    }
                    
                }

            }

            if (hayAlgo == false)
            {
                _Vista.MostrarTexto("El socio no tiene libros para devolver");
                Console.ReadKey();
            }
            else
            {
                if (hayEjemplar == false)
                {

                    _Vista.MostrarTexto("No has seleccionado ningún libro");
                    Console.ReadKey();
                    avanzar = true;
                }

            }
           

            


        }

        public void DevuelveEjemparDesdeSocio(int opcionSocio, int indice)
        {
            bool esSocioComun = false;
            for (int i = 0; i < ListadoSocios.Count; i++)
            {
                if (ListadoSocios[i].NId == opcionSocio)

                {
                    esSocioComun = true;
                }
            }
            if (esSocioComun == true) DevuelveEjemparDesdeSocioComun(opcionSocio, indice);
            else DevuelveEjemparDesdeSocioVip(opcionSocio, indice);


        }
        public void DevuelveEjemparDesdeSocioComun(int opcionSocio, int indice)
        {

            for (int i = 0; i < ListadoSocios.Count; i++)
            {
                if (opcionSocio == ListadoSocios[i].NId)
                {
                    bool avanza = false;

                    for (int x = 0; x < ListadoSocios[i].ListaLibrosDeSocio.Count; x++)
                    {
                        if (indice == x)
                        {
                            do
                            {
                                for (int h = 0; h < librosExistentes.Count; h++)
                                {
                                    if (librosExistentes[h].ISBN == ListadoSocios[i].ListaLibrosDeSocio[x].ISBN) librosExistentes[h].ListEjemplaresDisponibles.Add(ListadoSocios[i].ListaLibrosDeSocio[x]);
                                }
                                Ejemplar ejemplarADevolver = ListadoSocios[i].ListaLibrosDeSocio[x];
                                CreaHistorialDevoluciones(ejemplarADevolver);
                                ListadoSocios[i].ListaLibrosDeSocio.Remove(ListadoSocios[i].ListaLibrosDeSocio[x]);
                                ListadoSocios[i].CantEjemplaresRetirables++;
                               
                                avanza = true;
                            } while (avanza == false);
                        }
                    }
                }

            }


        }
        public void DevuelveEjemparDesdeSocioVip(int opcionSocio, int indice)
        {

            for (int i = 0; i < ListadoSociosVip.Count; i++)
            {
                if (opcionSocio == ListadoSociosVip[i].NId)
                {
                    bool avanza = false;

                    for (int x = 0; x < ListadoSociosVip[i].ListaLibrosDeSocio.Count; x++)
                    {
                        if (indice == x)
                        {
                            do
                            {
                                for (int h = 0; h < librosExistentes.Count; h++)
                                {
                                    if (librosExistentes[h].ISBN == ListadoSociosVip[i].ListaLibrosDeSocio[x].ISBN) librosExistentes[h].ListEjemplaresDisponibles.Add(ListadoSociosVip[i].ListaLibrosDeSocio[x]);
                                }
                                Ejemplar ejemplarADevolver = ListadoSociosVip[i].ListaLibrosDeSocio[x];
                                CreaHistorialDevoluciones(ejemplarADevolver);
                                ListadoSociosVip[i].ListaLibrosDeSocio.Remove(ListadoSociosVip[i].ListaLibrosDeSocio[x]);
                                ListadoSociosVip[i].CantEjemplaresRetirables++;
                                avanza = true;

                            } while (avanza == false);
                        }
                    }
                }
            }

        }

        private void CreaHistorialDevoluciones( Ejemplar ejemplarADevolver)
        {
            int aux = 0;
            for (int i = 0; i < HistorialPrestados.Count; i++)
            {
                if (ejemplarADevolver.Autor == HistorialPrestados[i].EjemplarPrestado.Autor && ejemplarADevolver.Nombre == HistorialPrestados[i].EjemplarPrestado.Nombre && ejemplarADevolver.ISBN== HistorialPrestados[i].EjemplarPrestado.ISBN && ejemplarADevolver.NEd== HistorialPrestados[i].EjemplarPrestado.NEd)
                {
                    aux = i;
                }
            }
            HistorialDevueltos.Add(new Prestamo(HistorialPrestados[aux].EjemplarPrestado, HistorialPrestados[aux].Socio.Nombre, HistorialPrestados[aux].Socio.Apellido, HistorialPrestados[aux].Socio.NId));

        }

        public void ImprimeHistorialPrestamos()
        {
            for (int i = 0; i < HistorialPrestados.Count; i++)
            {
                _Vista.MostrarTexto(" - Libro: " + HistorialPrestados[i].EjemplarPrestado.Nombre + ". Autor:" + HistorialPrestados[i].EjemplarPrestado.Autor + "." + " Año de edición: " + HistorialPrestados[i].EjemplarPrestado.NEd +
                ". Código ISBN: " + HistorialPrestados[i].EjemplarPrestado.ISBN + ". Prestado a: " + HistorialPrestados[i].Socio.Apellido + ", " + HistorialPrestados[i].Socio.Nombre + ". N° Id: " + HistorialPrestados[i].Socio.NId + "//" + HistorialPrestados[i].Fecha);
            }
        
        }

        public void ImprimeHistorialDevoluciones()
        {
            for (int i = 0; i < HistorialDevueltos.Count; i++)
            {
                _Vista.MostrarTexto(" - Libro: " + HistorialDevueltos[i].EjemplarPrestado.Nombre + ". Autor:" + HistorialDevueltos[i].EjemplarPrestado.Autor + ". " + "Año de edición: " + HistorialDevueltos[i].EjemplarPrestado.NEd +
                ". Código ISBN: " + HistorialDevueltos[i].EjemplarPrestado.ISBN + ". Devuelto por: " + HistorialDevueltos[i].Socio.Apellido + ", " + HistorialDevueltos[i].Socio.Nombre + "\n. N° Id: " + HistorialDevueltos[i].Socio.NId + "  // " + HistorialDevueltos[i].Fecha);
            }
          
        }



    }
}
