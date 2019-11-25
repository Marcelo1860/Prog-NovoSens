using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programa_NovoSens_5._4_GUI
{
    public class clsContenedor_de_datos
    {
        string[] lineas;    // array del tipo String que va a contener el archivo.csv
       
       


        public clsContenedor_de_datos() // permite cargar el array con el archivo.csv a partir de la ubicacion del mismo
        {
         
        }

        private void ImprimeDatos() // permite imprimir los datos del archivo.csv
        {
            foreach (var linea in lineas)
            {
                var valores = linea.Split(';');
                Console.WriteLine("El elemento " + valores[0] + " vale " + valores[1]);
            }

            Console.ReadKey();
        }

     
       private int ElementoCSV( int i ) // obtiene la cantidad de elementos que contiene el archivo.csv
        {
           foreach (var linea in lineas)
           {
                i++;
            }
            return (i);
        }

       public double[] Cargavector( double[] num, string[] lineass, string combo) // realiza la carga de una vector del tipo Double 
                                                  // con los elementos del archivo.csv y lo devuelve
        {
            if (combo == "Etoh 1")
            {
                int j = 0;
                foreach (var linea in lineass)
                {
                    var valores = linea;
                    double valornum = 0;
                    Double.TryParse(valores, out valornum);
                    num[j] = valornum;
                    j++;
                }
            }

            else if (combo == "Teq")
            {
                int j = 0;
                foreach (var linea in lineass)
                {
                    var valores = linea.Split(null);
                    double valornum = 0;
                    Double.TryParse(valores[1], out valornum);
                    num[j] = valornum/100;
                    j++;

                }
            }

            else
            {
                
            }

            return (num); 
        }

    }
}
