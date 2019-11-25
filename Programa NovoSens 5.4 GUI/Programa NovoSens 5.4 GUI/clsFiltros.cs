using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programa_NovoSens_5._4_GUI
{
    public class clsFiltros
    {

        private int contelementos;

        public int Contelementos { get => contelementos; set => contelementos = value; }
        
        public clsFiltros()
        {
            Contelementos = 0;
        }

        public int dimensionFiltrado(double[] medianofil, int cantidad) // permite obtener la cantidad de zonas estables 
                                                                       // que cumplen con las condiciones para ser con-
                                                                       // sideradas saltos (siempre múltiplo de 4)
        {
            int contsaltos = 0; // contador que almacena la cantidad de zonas estables que cumplen con los requisitos

            for (int m = 0; m < cantidad; m++) // realiza un barrido por la cantidad total de zonas estables que hay
            {
                if (contsaltos < 3) // si todavía no se alcanzó la cantidad de zonas estables para ser 
                                    // consideradas correctas
                {
                    if (m == (cantidad-1))  //si estamos en la última zona estable y no hay condicion deseada, 
                                            //se termina el ciclo sin aumentar el contador
                    {
                        
                    }

                    else // si no es el ultimo elemento
                    {
                       // Console.WriteLine(medianofil[(m + 1)] + " " + medianofil[m]);

                        if (medianofil[(m+1)] > (medianofil[m]+100)) // se compara el primer elemento de una zona estable con
                                                               // el primero de la siguiente y se verifica que el último    
                                                               // sea el mayor 
                        {
                            contsaltos++;                      // si se cumple la condicion se aumenta el contador de condi-
                                                               // ciones correctas
                            //Console.WriteLine(contsaltos);
                        }

                        else                                   // en caso contrario se reinicia el contador
                        {
                            contsaltos = 0;

                            
                        }
                    }
                }

                else // cuando se puedo verificar que por un tiempo se mantienen las condiciones correctas, podemos
                     // considerar que hay un conjunto de zonas estables que cumplen con los requisitos
                {
                    contsaltos = 0; // se reinicia el contador de zonas estables consecutivas 

                    if (m == (cantidad-1)) // si es la última zona estable, se aumenta directamente el contador de zonas 
                                           // estables que cumplen la condición
                    {
                        

                        contelementos += 4;

                        
                    }

                    else  // si no es el último, se compara la zona estable actual con la siguiente.
                    {
                        if (medianofil[(m + 1)] < medianofil[m])// si la actual es mayor a la siguiente significa que 
                                                                  // se cumple la condición y se aumenta el contador
                        {
                            

                            contelementos += 4;
                            
                        }

                        
                    }
                }

                //Console.WriteLine(contsaltos);
            }

            return (contelementos);// se devuelve el contador de zonas estables que cumplen con la condción
        }

        /// /// /// /// /// /// /// ///


        public int[] cargaVectorFiltrado(double[] medianofil, int[] zonasest, int cantidadutil ,int cantidad) // realiza la carga 
                                                                                        // de un vector que va a 
                                                                                        // contener las dimensiones
                                                                                        // de cada zona estable fil-   
                                                                                        // trada
        {
            int[] vector = new int[cantidadutil];   // se crea un vector de dimension equivalente a la cantidad de zonas
                                                    // estables útiles 

            int contHaySalto = 0;                   // se crea el contador de veces que hay una conjunto de zonas útiles
                                                    // que cumplen con la condición (siempre multiplo de 4)

            int contzonaest = 0;                    // un contador que permite tener registro sobre la zona útil que 
                                                    // está siendo analizada

            int contsaltos = 0; // contador que almacena la cantidad de zonas estables que cumplen con los requisitos

            for (int m = 0; m < cantidad; m++)// realiza un barrido por la cantidad total de zonas estables que hay
            {
                if (contsaltos < 3) // si todavía no se alcanzó la cantidad de zonas estables para ser
                                    // consideradas correctas
                {
                    if (m == (cantidad-1))//si estamos en la última zona estable y no hay condicion deseada, 
                                          //se aumenta el contador de zonas estables
                    {
                        contzonaest++;
                    }

                    else // si no es el ultimo elemento
                    {
                        if (medianofil[(m+1)] > (medianofil[m]+100)) // se compara el primer elemento de una zona estable con
                                                               // el primero de la siguiente y se verifica que el último    
                                                               // sea el mayor 
                        {
                            contsaltos++;                      // en condición correcta se aumentan los contadores de
                                                               // los saltos y de las zonas estables

                            contzonaest++;
                        }

                        else // si no se cumple la condición se reinicia el contador de saltos pero se incrementa el
                             // zonas estables
                        {
                            contsaltos = 0;

                            contzonaest++;
                        }
                    }
                }

                else // cuando se puedo verificar que por un tiempo se mantienen las condiciones correctas, podemos
                     // considerar que hay un conjunto de zonas estables que cumplen con los requisitos
                {
                    contsaltos = 0; // se reinicia el contador de saltos

                    if (m == (cantidad-1))// si es la última zona estable, se aumenta directamente el contador de zonas 
                                          // estables que cumplen la condición y, primero, se carga el vector de ele-
                                          // mentos estables útiles con los elementos del vector de zonas estables y 
                                          // se aumenta el contador de zonas estables
                    {

                        //Console.WriteLine("hola");

                        for (int i = 0; i < 4; i++)
                        {
                            //Console.WriteLine(zonasest[(contzonaest + i - 3)]);  

                            vector[(contHaySalto + i)] = zonasest[(contzonaest + i - 3)];
                        }

                        contzonaest++;

                        contHaySalto += 4;
                    }

                    else // si no es la última zona estables, se compara la zona estable actual con la siguiente. 
                    {
                        if (medianofil[(m+1)] < medianofil[m])// si la actual es mayor a la siguiente significa que 
                                                              // se cumple la condición y se realiza la carga del vec-
                                                              // tor y se aumenta los respectivos contadores
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Console.WriteLine(zonasest[(contzonaest + i - 3)]);

                                vector[(contHaySalto + i)] = zonasest[(contzonaest + i - 3)];
                            }

                            contzonaest++;

                            contHaySalto += 4;
                        }

                        else // si no se cumple, únicamente se aumenta el contador de zonas estables 
                        {
                            contzonaest++;
                        }
                    }
                }
            }

            return (vector);
        }

        /// /// /// /// /// /// /// ///

        public double[,] cargaMatrizFiltrada(double[,] estables, double[] medianofil, int cantidadutil, int cantidad, int maxelem) // realiza la carga 
                                                                                                             // de una matriz que va a 
                                                                                                             // contener los elementos
                                                                                                             // de cada zona estable fil-   
                                                                                                             // trada
        {
            double[,] matriz = new double[maxelem,cantidadutil];   // se crea una matriz de dimensiones equivalentes a la cantidad de zonas
                                                    // estables útiles y la maxima cantidad de elementos que preseta el vector
                                                    // de zonas estables filtradas

            int contHaySalto = 0;                   // se crea el contador de veces que hay una conjunto de zonas útiles
                                                    // que cumplen con la condición (siempre multiplo de 4)

            int contzonaest = 0;                    // un contador que permite tener registro sobre la zona útil que 
                                                    // está siendo analizada

            int contsaltos = 0; // contador que almacena la cantidad de zonas estables que cumplen con los requisitos

            for (int m = 0; m < cantidad; m++)// realiza un barrido por la cantidad total de zonas estables que hay
            {
                if (contsaltos < 3) // si todavía no se alcanzó la cantidad de zonas estables para ser
                                    // consideradas correctas
                {
                    if (m == (cantidad - 1))//si estamos en la última zona estable y no hay condicion deseada, 
                                            //se aumenta el contador de zonas estables
                    {
                        contzonaest++;
                    }

                    else // si no es el ultimo elemento
                    {
                        if (medianofil[(m + 1)] > (medianofil[m] + 100)) // se compara el primer elemento de una zona estable con
                                                                   // el primero de la siguiente y se verifica que el último    
                                                                   // sea el mayor 
                        {
                            contsaltos++;                      // en condición correcta se aumentan los contadores de
                                                               // los saltos y de las zonas estables

                            contzonaest++;
                        }

                        else // si no se cumple la condición se reinicia el contador de saltos pero se incrementa el
                             // zonas estables
                        {
                            contsaltos = 0;

                            contzonaest++;
                        }
                    }
                }

                else // cuando se puedo verificar que por un tiempo se mantienen las condiciones correctas, podemos
                     // considerar que hay un conjunto de zonas estables que cumplen con los requisitos
                {
                    contsaltos = 0; // se reinicia el contador de saltos

                    if (m == (cantidad - 1))// si es la última zona estable, se aumenta directamente el contador de zonas 
                                            // estables que cumplen la condición y, primero, se carga la matriz de ele-
                                            // mentos estables útiles con los elementos de la matriz de zonas estables y 
                                            // se aumenta el contador de zonas estables
                    {
                        for (int j = 0; j < maxelem; j++)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                matriz[j,(contHaySalto + i)] = estables[j,(contzonaest + i - 3)];
                            }
                        }
                      

                        contzonaest++;

                        contHaySalto += 4;
                    }

                    else // si no es la última zona estables, se compara la zona estable actual con la siguiente. 
                    {
                        if (medianofil[(m + 1)] < medianofil[m])// si la actual es mayor a la siguiente significa que 
                                                                  // se cumple la condición y se realiza la carga de la
                                                                  // matriz y se aumenta los respectivos contadores
                        {
                            for (int j = 0; j < maxelem; j++)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    matriz[j, (contHaySalto + i)] = estables[j, (contzonaest + i - 3)];
                                }
                            }

                            contzonaest++;

                            contHaySalto += 4;
                        }

                        else // si no se cumple, únicamente se aumenta el contador de zonas estables 
                        {
                            contzonaest++;
                        }
                    }
                }
            }

            return (matriz);
        }

    }
}
