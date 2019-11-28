using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programa_NovoSens_5._4_GUI
{
    public class clsCalculoResultado
    {
        private double _SaltoUno;

        public double SaltoUno { get => _SaltoUno; set => _SaltoUno = value; }

        private double _SaltoDos;

        public double SaltoDos { get => _SaltoDos; set => _SaltoDos = value; }




        public clsCalculoResultado()
        {
            SaltoUno = 0;

            SaltoUno = 0;
        }

        public double[] calculosalto(double[] mediav, int elemfilt) // permite calcular cada uno de los saltos y 
                                                                    // devolver un vector con sus valores
        {
            int cantsalt = ((elemfilt * 3) / 4); // variable que contiene la cantidad de los saltos 
           
            int contsaltos = 0; // variable que contiene la cantidad de saltos que se van detectando

            double[] salto = new double[cantsalt]; // vector que va a contener el valor de cada salto

            for (int i = 0; i < elemfilt; i++) // para cada elemento del vector de media se realiza una revisación
            {
                if( i == (elemfilt-1))// si es el último elemento se termina el ciclo
                {

                }
                else // si no es el último
                {
                    if (mediav[(i + 1)] > mediav[i]) // si se cumple que la zona estable actual es menor a la siguiente

                    {
                        
                        salto[contsaltos] = mediav[(i + 1)] - mediav[i];// se carga el vector de los saltos con la 
                                                                        // resta de la zona estable actual y la siguiente

                        contsaltos++; // se aumenta el contador de saltos
                    }

                    else // si no se cumple con la condición se continua con el ciclo
                    {
                    }
                }
             
            }

            return (salto); // se devuelve el vector con los valores de saltos


        }
        ///     ///     ///     ///     ///     ///     ///     ///     ///
        public double[] calcResultado(double[] saltos, int cantsaltos, string estandar) // permite calcular y retornar un vector con los 
                                                                       // resultados de las mediciones del gráfico
        {
            int cantResultados = cantsaltos / 3; // variable que contiene la cantidad de resultados 


            double standard = 0; // constante que permite alamcenar el valor del estandar

            if (estandar == "1.31") // setea el estándar según el valor indicado en el checkBox
            {
                standard = 1.31;
            }

            else if (estandar == "13.06")
            {
                standard = 13.06;
            }

            else if (estandar == "13.21")
            {
                standard = 13.21;
            }

            int j = 0;

            int k = 0;

            double[] resultados = new double[cantResultados]; // vector que va a contener el valor de los resultados

            double[] promSaltos = new double[cantResultados]; // vector que va a contenr el promedio de los primeros
                                                              // y terceros saltos

            for (int i = 0; i < cantResultados; i++) // realiza el calculo del promedio de los 1ros y 3ros saltos
            {

                promSaltos[i] = (saltos[(j + i)] + saltos[(j + i + 2)]) / 2;

                j += 2;

            }

            for (int i = 0; i < cantResultados; i++) // realiza el calculo de los resultados y los almacena en el vector
            {
                resultados[i] = ((standard) * saltos[k + 1]) / promSaltos[i];

                k += 3;
            }

            return (resultados); // retorna el vector con los resultados
    
        }
        ///     ///     ///     ///     ///     ///     ///     ///     ///
        public double[] mediaSaltos (double[] saltos, int cantSaltos) // obtiene la media de los distintos saltos equiva-
                                                                      // lentes
        {
            double[] mediaSaltos = new double[3];

            int Saltosseparados = cantSaltos / 3;

            for (int i = 0; i < 3; i++)
            {
                

                for (int j = 0; j < (cantSaltos); j+=3)
                {
                    mediaSaltos[i] += saltos[(j + i)];
   
                }

                mediaSaltos[i] /= Saltosseparados;
            }

            return (mediaSaltos);
        }
        ///     ///     ///     ///     ///     ///     ///     ///     ///
        public double[] desvestSaltos(double[] saltos, int cantSaltos, double[] mediasaltos) // calcula la desviación estándar del vector
                                                                     // de tipo Double
        {
            double[] diferencias = new double[cantSaltos/3];
            double[] suma = new double[3];

            for (int i = 0; i < 3; i++)
            {
                int h = 0;
                for (int j = 0; j < cantSaltos; j+=3)
                {
                    diferencias[h] = Math.Pow( saltos[j+i] - mediasaltos[i],2);

                    h ++;
                }



                for (int j = 0; j < cantSaltos/3; j++)
                {
                    suma[i] += diferencias[j];
                }

                suma[i] /= cantSaltos;

                suma[i] = Math.Sqrt(suma[i]);
            }

            

            return (suma);
        }



    } 
}

        


    








