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
     
        private readonly IVista _Vista;
        private List<Socio> ListadoSocios = new List<Socio>();
        private List<SocioVip> ListadoSociosVip = new List<SocioVip>();
        private List<Libro> librosExistentes = new List<Libro>();
        private List<Ejemplar> librosPrestados = new List<Ejemplar>();

        public List<Libro> LibrosExistentes { get => librosExistentes; }
        public List<Ejemplar> LibrosPrestados { get => librosPrestados; }

        public Presentador(IVista vista)
        {
            _Vista = vista;
        }

        public void AgregaLibro(string autor, string nombre, string iSBN)
        {
            LibrosExistentes.Add(new Modelo.Libro(autor, nombre, iSBN));
        }

        public bool ValidaISBN(string iSBN)
        {
            bool existeISBN = false;
            for (int i = 0; i < librosExistentes.Count; i++)
            {
                if (librosExistentes[i].ISBN == iSBN) existeISBN = true;

            }
            if (existeISBN) return true;
            else return false;
                

        }


        public void ImprimeListaLibros()
        {

            for (int i = 0; i < LibrosExistentes.Count; i++)
            {
                _Vista.MostrarTexto(($"{i}- Libro: {LibrosExistentes[i].Nombre}. Autor: {LibrosExistentes[i].Autor}. Código ISBN: { LibrosExistentes[i].ISBN}"));
            }

        }

        public void ImprimeEjemplaresPrestados()
        {
            _Vista.MostrarTexto("Ejemplares prestados:\n");
            for (int i = 0; i < librosPrestados.Count; i++)
            {
                _Vista.MostrarTexto($"\n-{i}_ Libro: {librosPrestados[i].Nombre}. Autor: {librosPrestados[i].Autor}. Código ISBN: { librosPrestados[i].ISBN}. Año de edición: {librosPrestados[i].NEd}. Sección: {librosPrestados[i].Ubicacion}"+"\n");
            }
        }

        public void DevolverEjemplar(int opcion)
        {
            string encuentraIndice;
            int indiceLibro = 0;

            if (librosPrestados.Count > 0)
            {
                if (opcion < librosPrestados.Count && opcion >= 0)
                {
                    encuentraIndice = librosPrestados[opcion].ISBN;

                    for (int i = 0; i < librosExistentes.Count; i++)
                    {
                        if (librosExistentes[i].ISBN == encuentraIndice)
                        {
                            indiceLibro = i;
                        }
                    }                    

                    for (int i = 0; i < librosPrestados.Count; i++)
                    {
                        if (i == opcion)
                        {
                            _Vista.MostrarTexto("Se ha realizado la siguiente devolución: \nLibro: " + librosPrestados[i].Nombre + ". Autor: " + librosPrestados[i].Autor + " . Código ISBN: " + librosPrestados[i].ISBN + ". Año de edición: " + librosPrestados[i].NEd);
                            librosExistentes[indiceLibro].ListEjemplaresDisponibles.Add(librosPrestados[i]);
                            librosPrestados.RemoveAt(i);
                          
                        }
                    }
                }
                else _Vista.MostrarTexto("No se han encontrado ejemplares prestados con el índice señalado");

            }

            else _Vista.MostrarTexto("Aún no se ha prestado ningún libro");
        }


        public void AgregarEjemplar(int indice, ref bool irAAgregarEjemplar2)
        {

            if (indice > (librosExistentes.Count - 1) || indice < 0) _Vista.MostrarTexto("No hay libros que coincidan con el índice señalado");
            else irAAgregarEjemplar2 = true;
        }

        public void AgregarEjemplar2(int nEd, int indice)
        {
            librosExistentes[indice].ListEjemplaresDisponibles.Add(new Ejemplar(librosExistentes[indice].Nombre, librosExistentes[indice].Autor, librosExistentes[indice].ISBN, nEd));

        }

        public void ImprimeEjemplaresDisponibles()
        {
            _Vista.MostrarTexto("Ejemplares disponibles:\n");
            int indice = 0;
            for (int i = 0; i < LibrosExistentes.Count; i++)
            {
                for (int x = 0; x < librosExistentes[i].ListEjemplaresDisponibles.Count; x++)
                {

                    _Vista.MostrarTexto(indice + "- Libro: " + librosExistentes[i].ListEjemplaresDisponibles[x].Nombre + ". Autor: " + librosExistentes[i].ListEjemplaresDisponibles[x].Autor + ". Año de edición: " + librosExistentes[i].ListEjemplaresDisponibles[x].NEd +
                    "\nCódigo ISBN: " + librosExistentes[i].ListEjemplaresDisponibles[x].ISBN + ". Sector: " + librosExistentes[i].ListEjemplaresDisponibles[x].Ubicacion);
                    indice++;
                }

            }
        }



        public void ExisteEjemplarDisponible(int opcion)
        {
            bool haylibro = false;
            bool hayejemplar = false;
            if (opcion < librosExistentes.Count && opcion >= 0)
            {
                for (int i = 0; i < librosExistentes.Count; i++)
                {
                    if (i == opcion)
                    {
                        haylibro = true;

                        if (librosExistentes[i].ListEjemplaresDisponibles.Count >0)
                        {                           
                            hayejemplar = true;
                            _Vista.MostrarTexto("Actualmente hay ejemplares disponibles del libro");
                        }

                    }

                }
            }


            else _Vista.MostrarTexto("El índice seleccionado no corresponde con ningún libro existente.");

            if (haylibro == true && hayejemplar == false) _Vista.MostrarTexto("Actualmente no hay ejemplares disponibles del libro catalogado");



        }


    }

   

}
