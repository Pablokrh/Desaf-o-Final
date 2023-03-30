using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vista
{
    public class MenuPrincipal : Presentador.IVista
    {
        private Presentador.Presentador presentador;
        private string opcionMenu;

        private bool salir = false;
        private bool avanzar = false;
        string stringAuxiliar = "";
        private string volver = "\n\n(o presiona la tecla 'enter' para Volver)\n";
        private int numAux = 0;
        private int indice = 0;
        private int opcionSocio = 0;

        public MenuPrincipal()
        {
            presentador = new Presentador.Presentador(this);
            PantallaMenu();

        }

        private void PantallaMenu()
        {

            do
            {
                Console.Clear();
                MostrarTexto("Seleccione la opción deseada a través de su índice numérico:\n");
                MostrarTexto("1- Agregar libro");
                MostrarTexto("2- Agregar un nuevo ejemplar de un libro existente");
                MostrarTexto("3- Consultar si un libro tiene ejemplares disponibles");
                MostrarTexto("4- Agregar socio ");
                MostrarTexto("5- Informar si el socio puede retirar ejemplares");
                MostrarTexto("6- Prestar ejemplar a socio");
                MostrarTexto("7- Devolver ejemplar");
                MostrarTexto("8- Mostrar ejemplares disponibles");
                MostrarTexto("9- Consultar historial de libros prestados/devueltos");               
                MostrarTexto("X- Salir");
                opcionMenu = Console.ReadLine();
                OpcionElegida(opcionMenu, ref salir);
            } while (salir == false);          
        }

        private void OpcionElegida(string opcionElegida, ref bool salir)
        {
            switch (opcionElegida)
            {
                case "1":
                    AgregaLibro();
                    salir = false;
                    break;

                case "2":
                    AgregaEjemplar();
                    salir = false;
                    Console.ReadKey();
                    break;

                case "3":
                    ExisteEjemplarDisponible();
                    Console.ReadKey();
                    salir = false;

                    break;

                case "4":
                    AgregaSocio();
                    salir = false;
                    break;

                case "5":
                    PuedeRetirar();
                    Console.Clear();
                    break;

                case "6":
                    PrestaASocio();
                    Console.Clear();
                    break;

                case "7":
                    DevolverEjemplarDesdeSocio();
                    Console.Clear();
                    break;

                case "8":
                    ImprimeEjemplaresDisponibles();
                    Console.ReadKey();
                    salir = false;
                    break;

                case "9":
                    RegistroPrestamosDevoluciones();
                    Console.Clear();
                    break;

                case "x":
                case "X":
                    salir = true;
                    Environment.Exit(2);
                    break;

                default:
                    MostrarTexto("La opción elegida es invalida");
                    Console.ReadKey();
                    salir = false;
                    break;

            }
        }

        private string validaString(string stringAuxiliar, ref bool avanzar)
        {
            if (stringAuxiliar.Trim() != "")
            {
                avanzar = true;
                return stringAuxiliar;
            }
            else return "";
        }

        private int validaInt(string stringAuxiliar, ref bool avanzar, int numAux)
        {
            if ((int.TryParse(stringAuxiliar, out int intAno) == true) && stringAuxiliar.Trim() != "")
            {
                avanzar = true;
                numAux = int.Parse(stringAuxiliar);
                return numAux;
            }
            else
            {
                avanzar = false;
                return 0;
            }
        }

        private void AgregaLibro()
        {
            string nombreLibro="";
            string autor="";
            string iSBN="";

                avanzar = false;
            do
            {
                avanzar = false;
                Console.Clear();
                MostrarTexto("Introduzca el nombre del libro"+volver);
                stringAuxiliar = Console.ReadLine();
                if (stringAuxiliar == "") PantallaMenu();
                validaString(stringAuxiliar, ref avanzar);
              
                                              
            } while (avanzar == false);
            nombreLibro = stringAuxiliar;

                avanzar = false;
            do
            {
                Console.Clear();
                MostrarTexto("Introduzca el nombre del autor" + volver );
                stringAuxiliar = Console.ReadLine();
                if (stringAuxiliar == "") PantallaMenu();
                validaString(stringAuxiliar, ref avanzar);
                autor = stringAuxiliar;

            } while (avanzar == false);
            
                avanzar = false;
            do
            {
                Console.Clear();
                MostrarTexto("Introduzca ISBN (Código numérico de 13 cifras presente en contratapa)" + volver);
                iSBN = Console.ReadLine();
                if (iSBN == "") PantallaMenu();
                if (presentador.ValidaISBN(iSBN) == false)
                {
                    if ((long.TryParse(iSBN, out long intISBN)) && iSBN.Length == 13)
                    {
                        avanzar = true;
                    }
                    else MostrarTexto("El valor ingresado no es un número de 13 cifras");
                }
                else{
                    MostrarTexto("El número ISBN ingresado ya se encuentra en el catálogo");
                    Console.ReadKey();
                    }

            } while (avanzar == false);

            presentador.AgregaLibro(autor, nombreLibro, iSBN);

        }

        private void AgregaEjemplar()
        {
            int nEd = 0;
            avanzar = false;
            bool irAAgregarEjemplar2 = false;

            do
            {
                Console.Clear();
                MostrarTexto("Escriba el índice correspondiente al libro cuyo ejemplar desea agregar: " + volver);
                presentador.ImprimeListaLibros();
                stringAuxiliar = Console.ReadLine();
                if (stringAuxiliar == "") PantallaMenu();
                numAux= validaInt(stringAuxiliar, ref avanzar, numAux);
                indice = numAux;
                presentador.AgregarEjemplar(indice, ref irAAgregarEjemplar2);

            } while (avanzar == false || irAAgregarEjemplar2==false);

                do
                {
                    avanzar = false;
                    Console.Clear();
                    MostrarTexto("Introduzca el año de edición"+ volver);
                    stringAuxiliar = Console.ReadLine();
                if (stringAuxiliar == "") PantallaMenu();
                    numAux = validaInt(stringAuxiliar, ref avanzar, numAux);

            } while (avanzar == false);
                nEd = numAux;
            
            presentador.AgregarEjemplar2(nEd, indice);
        }

        private void ExisteEjemplarDisponible()
        {
            do
            {
                Console.Clear();
                MostrarTexto("Escriba el índice correspondiente al libro que desea saber si tiene ejemplares \n" + volver + "\n");
                presentador.ImprimeListaLibros();
                stringAuxiliar = Console.ReadLine();
                if (stringAuxiliar == "") PantallaMenu();
                numAux = validaInt(stringAuxiliar, ref avanzar, numAux);
            } while (avanzar == false);
            indice = numAux;
            Console.Clear();
            presentador.ExisteEjemplarDisponible(indice);
        }


        private void ImprimeEjemplaresDisponibles()
       {
            Console.Clear();
            presentador.ImprimeEjemplaresDisponibles();
       }
        
     
        private void AgregaSocio()
        {
            do
            {
                Console.Clear();
                MostrarTexto("Ingresar nombre del socio:" + volver);
                stringAuxiliar = Console.ReadLine();
                if (stringAuxiliar == "") PantallaMenu();
                validaString(stringAuxiliar, ref avanzar);
            } while (avanzar == false);
            string nombre = stringAuxiliar;

            do
            {
                Console.Clear();
                MostrarTexto("Ingresar apellido del socio:" + volver );
                stringAuxiliar = Console.ReadLine();
                if (stringAuxiliar == "") PantallaMenu();
                validaString(stringAuxiliar,ref avanzar);
            } while (avanzar == false);
            string apellido = stringAuxiliar;

            do
            {
                avanzar = false;
                Console.Clear();
                MostrarTexto("Ingresar número de identificación del socio:" + volver);
                stringAuxiliar = Console.ReadLine();
                if (stringAuxiliar == "") PantallaMenu();
                numAux = validaInt(stringAuxiliar, ref avanzar, numAux);
                if (presentador.ControlaId(numAux)==true)
                {
                    Console.Clear();
                    MostrarTexto("El Id ingresado ya existe");
                    Console.ReadKey();
                    avanzar = false;
                }


            } while (avanzar == false);
            int nId = numAux;

            bool socioEsVip = false;
            string socioVip = "";
            do 
            {
                Console.Clear(); 
                MostrarTexto("Se trata de un socio VIP (si-no)" + volver);
                socioVip = Console.ReadLine();
                if (socioVip == "") PantallaMenu();
                if (socioVip == "si" || socioVip == "no") socioEsVip = true;

            } while (socioEsVip == false);

            int cuotaMensual=0;

            if (socioVip == "si")
            {
                do
                {
                    Console.Clear();
                    MostrarTexto("Ingresar valor de cuota mensual:" + volver );
                    stringAuxiliar = Console.ReadLine();
                if (stringAuxiliar == "") PantallaMenu();
                    numAux = validaInt(stringAuxiliar, ref avanzar, numAux);
                } while (avanzar == false);
                cuotaMensual = numAux;

            }

            presentador.AgregaSocio(nombre, apellido, nId, cuotaMensual, socioVip);
            Console.ReadKey();
        }

     

        private void PuedeRetirar()
        {
            if (presentador.HaySocio() == true)
            {

                do
                {
                    Console.Clear();
                    presentador.ImprimeSocios();
                    MostrarTexto("Ingrese el N° de Id del socio:" + volver);
                    stringAuxiliar = Console.ReadLine();
                    if (stringAuxiliar == "") PantallaMenu();
                    numAux = validaInt(stringAuxiliar, ref avanzar, numAux);
                    if (presentador.SocioExiste(numAux) == false)
                    {
                        Console.Clear();
                        MostrarTexto("Por favor, selecciona una Id de socio existente");
                        Console.ReadKey();
                        avanzar = false;
                    }
                    } while (avanzar == false);
                indice = numAux;
                presentador.PuedeRetirar(indice);
                Console.ReadKey();

            }
            else
            {
                Console.Clear();
                MostrarTexto("Aún no se ha ingresado ningún socio");
                Console.ReadKey();
            }
        }

        
        private void PrestaASocio()
        {
            if (presentador.HaySocio() == true)
            {

                do
                {
                    Console.Clear();
                    MostrarTexto("Escribe el número de Id del socio a quien se le hará el prestamo" + volver);
                    presentador.ImprimeSocios();
                    stringAuxiliar = Console.ReadLine();
                    if (stringAuxiliar == "") PantallaMenu();
                    numAux = validaInt(stringAuxiliar, ref avanzar, numAux);
                    if (presentador.SocioExiste(numAux) == false)
                    {
                        Console.Clear();
                        MostrarTexto("Por favor, selecciona una Id de socio existente");
                        Console.ReadKey();
                        avanzar = false;
                    }
                } while (avanzar == false);
                opcionSocio = numAux;
                string hayEjemplar = "";
                do
                {
                    Console.Clear();
                    MostrarTexto("Escriba el índice correspondiente al libro que será prestado" + volver);
                    presentador.ImprimeEjemplaresDisponibles();
                    stringAuxiliar = Console.ReadLine();
                    if (stringAuxiliar == "") PantallaMenu();
                    numAux = validaInt(stringAuxiliar, ref avanzar, numAux);

                } while (avanzar == false);
                int opcionEjemplar = numAux;
                presentador.PrestaASocio(opcionEjemplar, opcionSocio, ref hayEjemplar);
                Console.Clear();
                Console.WriteLine(hayEjemplar);
            }
            else
            {
                Console.Clear();
                MostrarTexto("Aún no se ha ingresado ningún socio");
                Console.ReadKey();
            }

        }

        private void DevolverEjemplarDesdeSocio()
        {
            if (presentador.HaySocio() == true)
            {
                do
                {
                    Console.Clear();
                    MostrarTexto("Escribe el número de Id del socio que realiza la devolución" + volver);
                    presentador.ImprimeSocios();
                    stringAuxiliar = Console.ReadLine();
                    if (stringAuxiliar == "") PantallaMenu();
                    numAux = validaInt(stringAuxiliar, ref avanzar, numAux);
                    Console.ReadKey();
                    if (presentador.SocioExiste(numAux) == false)
                    {
                        Console.Clear();
                        MostrarTexto("Por favor, selecciona una Id de socio existente");
                        Console.ReadKey();
                        avanzar = false;
                        
                    }

                } while (avanzar == false);

                opcionSocio = numAux;

                do
                {
                    Console.Clear();
                    MostrarTexto("Escriba el índice correspondiente al libro, cuyo ejemplar será devuelto" + volver);
                    presentador.ImprimeEjemplaresDeSocio(opcionSocio);
                    stringAuxiliar = Console.ReadLine();
                    if (stringAuxiliar == "") PantallaMenu();
                    numAux = validaInt(stringAuxiliar, ref avanzar, numAux);
                    presentador.SocioTieneEjemplar(numAux, opcionSocio, ref avanzar);

                } while (avanzar == false);
                indice = numAux;
                presentador.DevuelveEjemparDesdeSocio(opcionSocio, indice);
            }
            else
            {
                Console.Clear();
                MostrarTexto("Aún no se ha ingresado ningún socio");
                Console.ReadKey();
            }
        }
        private void RegistroPrestamosDevoluciones()
        {
            Console.Clear();
            MostrarTexto("----------------------------------");
            MostrarTexto("HISTORIAL DE PRÉSTAMOS/DEVOLUCIONES");
            MostrarTexto("----------------------------------\n");
            MostrarTexto("Libros prestados hasta el momento:\n");
            presentador.ImprimeHistorialPrestamos();
            MostrarTexto("\n-------------------------------------\n\n");
            MostrarTexto("Libros devueltos hasta el momento:");
            presentador.ImprimeHistorialDevoluciones();
            Console.ReadLine();
        }


        public void MostrarTexto(string texto)
        {
            Console.WriteLine(texto);
        }
        

        
    }
}
