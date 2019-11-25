using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programa_NovoSens_5._4_GUI
{
    public class clsCalculos
    {
        private double _media;
        public double media { get => _media; set => _media = value; }
        
        private double _desvest;
        public double desvest { get => _desvest; set => _desvest = value; }
       
        private double _cv;

        public double cv { get => _cv; set => _cv = value; }


        public clsCalculos()
        {
            media = 0;

            desvest = 0;

            cv = 0;
        }

        public double calcMedia(double[] num, int i) // calcula la media aritmética de un vector del tipo double
        {
            media = 0;

            for (int j = 0; j < i; j++)
            {
                media += num[j];
            }

            media /= i;

            return (media);
        }

        public double calcDesvest(double[] num, int i, double media) // calcula la desviación estándar del vector
                                                                     // de tipo Double
        {
            double[] diferencias = new double[i];
            double suma = 0;

            for (int j = 0; j < i; j++)
            {
                diferencias[j] = num[j] - media;
                diferencias[j] = Math.Pow(diferencias[j], 2);
            }



            for (int j = 0; j < i; j++)
            {
                suma += diferencias[j];
            }

            suma /= i;

            suma = Math.Sqrt(suma);

            return (suma);
        }

        // Permite calcular la media de los elementos de una matriz  
        public double[] calcVectorMedia (double[,] establesfiltrados, int[] elemfiltrados, int maxelemfilt, int cantidadutil)
        {
            double[] mediav = new double[cantidadutil];

            for (int i = 0; i < cantidadutil; i++)
            {
                for (int j = 0; j < maxelemfilt; j++)
                {
                    mediav[i] += establesfiltrados[j, i];
                }

                mediav[i] /= elemfiltrados[i];
            }

            return (mediav);
        }

        public double[] calcVectorUltimos(double[,] establesfiltrados, int[] elemfiltrados, int cantutil)
        {
            double[] ultiVect = new double[cantutil];

            for (int i = 0; i < cantutil; i++)
            {
                ultiVect[i] = establesfiltrados[(elemfiltrados[i] - 1), i];
            }

            return (ultiVect);
        }

        public double[] media20ult (double[,] establesfiltrados, int[] elemfiltrados, int maxelemfilt, int cantutil)
            // calcula la media de los últimos 20 elementos de cada zona estable ya filtrada
        {
            double[] media20 = new double[cantutil];

            double[,] mediaprom = new double[4, cantutil];

            for (int i = 0; i < cantutil; i++)
            {
                for (int k = 1; k < 5; k++)
                {
                    for (int j = 0; j < (20 + k); j++)

                    {
                        media20[i] += establesfiltrados[(elemfiltrados[i] - j - k), i];
                    }

                    media20[i] /= 20;

                    mediaprom[(k - 1), i] = media20[i]; 
                }
              

            }

            for (int i = 0; i < cantutil; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    media20[i] += mediaprom[j, i];
                }

                media20[i] /= 4;
            }

            return (media20);
        }
    }
}
