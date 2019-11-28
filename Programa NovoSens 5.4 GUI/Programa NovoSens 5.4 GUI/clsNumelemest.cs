using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programa_NovoSens_5._4_GUI
{
   
    public class clsNumelemest
    {
        private int[] _contcantest = new int[4]; //genero vector privado que almacena cantidad de elementos en meseta estable

        public int[] Contcantest { get => _contcantest; set => _contcantest = value; }

        private double[] _lector = new double[5]; // genero vector privado que me permite barrer para analizar estabilidad

        public double[] Lector { get => _lector; set => _lector = value; }


        ArrayList cantidad;


        public clsNumelemest() // inicializo
        {
            for (int i = 0; i < 5; i++)
            {
                Lector[i] = 0;
            }

            for (int i = 0; i < 4; i++)
            {
                Contcantest[i] = 0;
            }

            cantidad = new ArrayList(); 
        }

        clsCalculos mate = new clsCalculos(); // inicializo clsCalculos

        public ArrayList CuentaELemEst(double[] num, int i, string combo) // permite cargar un Array List con los elemntos estables
                                                        // para luego poder obtener la cantidad de zonas estables 
                                                        // que presenta  la curva
        {
            int cont = 0; //contador de elementos en zona estable
            
            double limiteMedia = 0;

            double limitemax = 0;
            if (combo == "Etoh 1")
            {
                limiteMedia = 1010;

                limitemax = 6;
            }
            //limite de estabilidad

            else if (combo == "Teq")
            {
                limiteMedia = 100;

                limitemax = 2;
            }
            double resultado = 0;

            //double limitemin = 0.1;

            int contneg = 0; // contador de elementos en zonas inestables

            int contcantest = 0; // contador de zonas estables

            for (int j = 0; j < i; j++)
            {
                int h = 0;

                for (int g = j; g < (j + 5 ) ; g++) // la renovación de los elementos del vector de barrido
                {

                    if (g < i)
                    {
                        Lector[h] = num[g];

                    }

                    else
                    {
                        Lector[h] = num[g - 5];
                    }
                    h++;
                }


                double media = mate.calcMedia(Lector, h); //calcula la media de los cinco elementos leidos

                double desvest = mate.calcDesvest(Lector, h, media); //calula la desviacion estandar de esos cinco elementos

                resultado = (desvest / media) * 100;
                //Console.WriteLine(desvest);

                if (desvest < limitemax &&  media > limiteMedia) // si la desviacion estandar esta dentro del limite de establildad,
                                         // aumenta el contador de elementos estables en uno
                {
                    contneg = 0;

                    cont++;

                    //Console.WriteLine(cont);

                }

                else // cuando detecta zona inestable
                {
                    if (contneg == 0) // si fue la primera vez que detecto la  inestablildad
                    {
                        //Contcantest[contcantest] = cont; // guarda el valor del contador de zona estables en el vector
                        if (cont > 25)
                        {
                            cantidad.Add(cont);                                // por zona

                            contcantest++;
                        }                                 // que va a contener las distintas cantidades de elementos 
                                         // incrementa el contador de cantidad de zonas estables

                        //Console.WriteLine(cont);

                        //Console.WriteLine(contcantest);

                        cont = 0;                        // reinicia el contador de elementos de zonas estables

                        contneg = 1;
                    }

                    else // siguientes veces que detecta la inestabilidad
                    {
                        contneg++;
                    }
                }

            }

            //Contcantest[3] = cont;


            if (cont > 25)
            {
                cantidad.Add(cont);
            }
            
            
            

            return (cantidad); // retorna el vector con los valores de zonas estables
        }


        ///     ///     ///     ///     ///     ///     ///     ///     ///

        public int[] CargaELemEst(double[] num, int i, int c, string combo) // permite cargar y retornar un vector del tipo Integer
                                                            // con la cantidad de elementos que presenta cada zona estable 
                                                            // de las curvas del archivo.csv
        {
            int[] Contcantest2 = new int[c];

            int cont = 0; //contador de elementos en zona estable

            double limitemax = 0; //limite de estabilidad

            double limiteMedia = 0;

            if (combo == "Etoh 1")
            {
                limiteMedia = 1010;

                limitemax = 6;
            }
            //limite de estabilidad

            else if (combo == "Teq")
            {
                limiteMedia = 100;

                limitemax = 2;
            }
            
            double resultado = 0;


            //double limitemin = 0.1;

            int contneg = 0; // contador de elementos en zonas inestables

            int contcantest = 0; // contador de zonas estables

            for (int j = 0; j < i; j++)
            {
                int h = 0;

                for (int g = j; g < (j + 5); g++) // la renovación de los elementos del vector de barrido
                {

                    if (g < i)
                    {
                        Lector[h] = num[g];

                    }

                    else
                    {
                        Lector[h] = num[g - 5];
                    }
                    h++;
                }


                double media = mate.calcMedia(Lector, h); //calcula la media de los cinco elementos leidos

                double desvest = mate.calcDesvest(Lector, h, media); //calula la desviacion estandar de esos cinco elementos

                resultado = (desvest / media) * 100;
                //Console.WriteLine("Media: " + media);

                //Console.WriteLine("desv: " + desvest);

                //Console.WriteLine("cant: " + c);

                //Console.WriteLine("control: " + contcantest);

                //Console.WriteLine("contador: " + cont);

                if (desvest < limitemax &&  media > limiteMedia) // si la desviacion estandar esta dentro del limite de establildad,
                                         // aumenta el contador de elementos estables en uno
                {
                    contneg = 0;

                    cont++;

                }

                else // cuando detecta zona inestable
                {
                    if (contneg == 0) // si fue la primera vez que detecto la  inestablildad
                    {
                        if (cont > 25)
                        {
                            cont += 3;

                            Contcantest2[contcantest] = cont; // guarda el valor del contador de zona estables en el vector
                                                              // que va a contener las distintas cantidades de elementos 
                                                              // por zona

                            contcantest++;                   // incrementa el contador de cantidad de zonas estables
                        }
                        

                        cont = 0;                        // reinicia el contador de elementos de zonas estables

                        contneg = 1;
                    }

                    else // siguientes veces que detecta la inestabilidad
                    {
                        contneg++;
                    }
                }

            }

            //Contcantest2[3] = cont;
            if (cont > 25)
            {
                Contcantest2[(c-1)] = cont;
            }


            return (Contcantest2); // retorna el vector con los valores de zonas estables
        }

        ///     ///     ///     ///     ///     ///     ///     ///     ///

        public double[,] Elemestables(double[] num, int i,int maxim ,int cantvec, string combo) // carga y retorna un vector del tipo
                                                                                // Double con los elementos presentes
                                                                                // en cada zona estable de las curvas 
                                                                                // del archivo.csv
        {
            double limitemax = 0; //limite de estabilidad

            double limiteMedia = 0;

            if (combo == "Etoh 1")
            {
                limiteMedia = 1010;

                limitemax = 6;
            }
            //limite de estabilidad

            else if (combo == "Teq")
            {
                limiteMedia = 100;

                limitemax = 2;
            }

            double resultado = 0;

            //double limitemin = 0.1;

            int cont = 0; //contador de elementos en zona estable

            double[,] Estables = new double[maxim,cantvec];

           
            int controlvector = 0; //detecta cuando se llenó un vector

            int contneg = 0;

            for (int j = 0; j < i; j++)//realiza el barrido cada 5 elementos para obtener mejor resolución
            {
                int h = 0;

                for (int g = j; g < (j + 5); g++)
                {

                    if (g < i)
                    {
                        Lector[h] = num[g]; 
                    }

                    else
                    {
                        Lector[h] = num[g - 5];
                    }
                    h++;
                }


                double media = mate.calcMedia(Lector, h); //calcula la media de los cinco elementos leidos

                double desvest = mate.calcDesvest(Lector, h, media); //calula la desviacion estandar de esos cinco elementos

                resultado = (desvest / media) * 100;
                //Console.WriteLine(desvest);

                if (desvest < limitemax &&  media > limiteMedia) // si la desviacion estandar esta dentro del limite de establildad
                {

                    if (controlvector < cantvec )
                    {
                       
                        Estables[cont, controlvector] = Lector[0];

                        cont++;
                       
                        contneg = 0;
                    }

                    else
                    {
                        contneg = 0;
                    }
                    
                }

                else // cuando detecta zona inestable cambia de vector para la próxima establildad
                {

                    if (contneg == 0) // si fue la primera vez que detecto la  inestablildad
                    {
                       
                        if (cont > 25)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                //Console.WriteLine("resultado" + resultado);
                                //Console.WriteLine("cont" + cont);
                                //Console.WriteLine("max" + maxim);

                                Estables[(cont + k), controlvector] = Lector[k];
                            }

                           controlvector++;
                        }
                        

                        //Console.WriteLine("control" + controlvector);

                        cont = 0;

                        contneg = 1;
                    }

                    else // siguientes veces que detecta la inestabilidad
                    {
                        contneg++;
                    }
                }

            }


            return (Estables);

            
        }


    }
}
