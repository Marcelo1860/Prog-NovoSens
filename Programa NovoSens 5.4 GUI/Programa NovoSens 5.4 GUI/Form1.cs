using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programa_NovoSens_5._4_GUI
{
    public partial class Form1 : Form
    {
        
    
        public Form1()
        {
   
            InitializeComponent();
        }

        string[] lineas;
        
        int cont = 0;
        
        int i = 0;

        private void Examinar_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = open.FileName;
            }
            open.Dispose();
        }

       

        private void button2_Click_1(object sender, EventArgs e)
        {
            // SE CARGAN LOS DATOS EN UN VECTOR DEL TIPO DOUBLE

            listBox1.Items.Clear();

            string cadena = textBox1.Text;

            lineas = File.ReadAllLines(@cadena);

            string combo = comboBox1.Text;

            string estandar = comboBox2.Text;

            foreach (var linea in lineas)
            {
                i++;
            }

            double[] number = new double[i];

            clsContenedor_de_datos file = new clsContenedor_de_datos();

            number = file.Cargavector(number, lineas, combo, i);

            for (int k = 0; k < i; k++)
            {
                listBox1.Items.Add(number[k]);
            }

            ///////////////////////////////////////////////////////////////////////////////

            //-----REALIZA LA OBTENCION DEL NUMERO DE ZONAS ESTABLES, LA DIMENSION DE CADA UNA Y GUARDA SUS VALORES----
            if ((combo == "Etoh 1" || combo == "Teq convertido" || combo == "Teq directo") && (estandar == "1.31" || estandar == "13.06" || estandar == "13.21"))
            {

                clsNumelemest zona = new clsNumelemest(); // inicia clase que permite distinguir zonas y elementos estables

                ArrayList Zonasest; // genera ArrayList para obtener cantidad de zonas estables

                Zonasest = new ArrayList(); // inicializa ArrayList

                Zonasest = zona.CuentaELemEst(number, i, combo); // calcula la cantidad de zona estables

                int c = Zonasest.Count; // obtiene cantidad de zonas estables de la curva

                int[] zonaest = new int[(c)]; // genera vector con dimension 

                zonaest = zona.CargaELemEst(number, i, (c), combo); // carga el vector con las cantidades de elementos estables
                                                                    // por zona

                int maxelem = zonaest.Max(); // obtiene el tamaño maximo de las zonas de elem. estables

                double[,] estables = new double[maxelem, (c)]; // genera matriz que va a contener los valores de las 
                                                               // zonas estables

                estables = zona.Elemestables(number, i, maxelem, (c), combo); // carga la matriz con los valores de los 
                                                                              // elementos estables

                clsCalculos mate = new clsCalculos(); // inicializa clase para realizar calculos de media y 
                                                      // desviación estándar
                double[] medianofil = new double[c];  // se crea el vector que almacena la media de cada zona estable

                medianofil = mate.calcVectorMedia(estables, zonaest, maxelem, c);

                //----REALIZA UN FILTRADO CON LOS DATOS PREVIAMENTE OBTENIDOS PARA OBTENER EL NUMERO, LA DIMENSION Y
                //----UNA MATRIZ CON LOS DATOS DE CADA ZONA ESTABLES CONSIDERADA COMO ÚTIL SEGÚN EL CRITERIO ESTABLECIDO

                clsFiltros filt = new clsFiltros(); // se inicializa la clase que permite el filtrado de las señales 
                                                    // estables para obtener las zonas estables que son útiles

                int elemfilt = filt.dimensionFiltrado(medianofil, c);// calcula la cantidad de zonas estables útiles

                //Console.WriteLine(elemfilt);
                // SI HUBO ELEMENTOS UTILES OBTIENE LOS DATOS PREVIAMENTE MENCIONADOS Y TAMBIEN GENERA UN VECTOR CON LA
                // MEDIA DE CADA UNA DE ESAS ZONAS ESTABLES UTILES

                if (elemfilt > 0)
                {
                    int[] vectorZonafiltadas = new int[elemfilt]; // se crea el vector que almacena la cantidad 
                                                                  // de elementos para zona estable filtrada

                    vectorZonafiltadas = filt.cargaVectorFiltrado(medianofil, zonaest, elemfilt, c); // carga el vector

                    //Console.WriteLine("vector filtrado");

                    //for (int k = 0; k < elemfilt; k++)
                    //{
                    //    Console.WriteLine(vectorZonafiltadas[k]);
                    //}

                    int maxelemfilt = vectorZonafiltadas.Max(); // maxima cantidad de elementos estables que posee una zona estable

                    double[,] establesfiltrados = new double[maxelemfilt, elemfilt]; // crea la matriz que almacena los datos 
                                                                                     // de las zonas estables

                    establesfiltrados = filt.cargaMatrizFiltrada(estables, medianofil, elemfilt, c, maxelemfilt); // carga la matriz


                    double[] ultimos = new double[elemfilt]; // crea un vector que almacena los últimos elementos de cada zona estable

                    double[] mediav = new double[elemfilt]; // crea un vector que contiene la media de cada zona estable filtrada

                    double[] media20 = new double[elemfilt]; // crea vector que permite contener la media de los últimos 20 elementos 
                                                             // de cada zona estable

                    double[,] mediaprom = new double[4, elemfilt];

                    mediav = mate.calcVectorMedia(establesfiltrados, vectorZonafiltadas, maxelemfilt, elemfilt);

                    media20 = mate.media20ult(establesfiltrados, vectorZonafiltadas, maxelemfilt, elemfilt);

                    clsCalculoResultado res = new clsCalculoResultado(); // inicializa la clase que permite obtener resul-
                                                                         // tados de los saltos obtenidos

                    int cantSaltos = ((elemfilt * 3) / 4); // variable que almacena la cantidad de saltos que se van a detectar

                    double[] Saltos = new double[cantSaltos]; // vector que va a contener los valores de cada salto

                    Saltos = res.calculosalto(media20, elemfilt); // se carga el vector que contiene los saltos

                    int canResult = cantSaltos / 3; // variable que va a contener la cantidad de mediciones en el gráfico

                    double[] Resultados = new double[canResult]; // vector que va a contener el resultado de las medidas

                    Resultados = res.calcResultado(Saltos, cantSaltos, estandar); // se carga el vector de resultados

                    double mediares = mate.calcMedia(Resultados, canResult); // variable que contiene la media 
                                                                             // de todos los resultados 

                    double desvres = mate.calcDesvest(Resultados, canResult, mediares); // variable que contiene la 
                                                                                        // desvición estándar de cada
                                                                                        // resultado

                    double[] mediaSaltos = new double[3]; // vector que contiene la media de cada conjunto de saltos 
                                                          // equivalentes

                    mediaSaltos = res.mediaSaltos(Saltos, cantSaltos); // carga el vector de media de los saltos

                    double[] desvestSaltos = new double[3]; // vector que contiene la desviación estándar de cada
                                                            // conjunto de saltos equivalentes

                    desvestSaltos = res.desvestSaltos(Saltos, cantSaltos, mediaSaltos);// carga el vector de desviación
                                                                                       // de los saltos

                    if (Resultadosx.Checked == true || MediaResultx.Checked == true || DesvResultx.Checked == true || Saltosx.Checked == true || MediaSaltosx.Checked == true || DesvSaltosx.Checked == true || VectDatosx.Checked == true || CantSaltosx.Checked == true || ZonestX.Checked == true || CVResultados.Checked == true || CVSaltos.Checked == true)
                    {

                        string sensor = textBox2.Text;

                        string fecha = textBox3.Text;

                        listBox1.Items.Add("Sensor: " + sensor + " Fecha: " + fecha);

                        if (Resultadosx.Checked == true)
                        {
                            listBox1.Items.Add("Los resultados son: ");

                            

                            for (int k = 0; k < canResult; k++)

                            {
                                Resultados[k] = Math.Truncate(Resultados[k] * 100) / 100;

                                listBox1.Items.Add(Resultados[k]);

                               
                            }
                        }

                        if (MediaResultx.Checked == true)
                        {
                            listBox1.Items.Add("La media de los resultados es: ");

                            mediares = Math.Truncate(mediares * 100) / 100;

                            listBox1.Items.Add(mediares);
                        }

                        if (DesvResultx.Checked == true)
                        {
                            listBox1.Items.Add("La desviación estándar de los resultados es: ");

                            desvres = Math.Truncate(desvres * 100) / 100;

                            listBox1.Items.Add(desvres);
                        }

                        if (CVResultados.Checked == true)
                        {
                            double cvSaltos = (desvres / mediares) *100;

                            listBox1.Items.Add(" El coeficiente de variación porcentual de los resultados es: ");

                            cvSaltos = Math.Truncate(cvSaltos * 100) / 100;

                            listBox1.Items.Add(cvSaltos + "%");
                        }

                        if (Saltosx.Checked == true)
                        {
                            listBox1.Items.Add("Los saltos medidos son: ");

                            for (int k = 0; k < cantSaltos; k++)
                            {
                               Saltos[k] = Math.Truncate(Saltos[k] * 100) / 100;

                               listBox1.Items.Add(Saltos[k]);
                            }
                        }

                        if (MediaSaltosx.Checked == true)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                listBox1.Items.Add("Media de los saltos número " + (k + 1) + " : ");

                                mediaSaltos[k] = Math.Truncate(mediaSaltos[k] * 100) / 100;

                                listBox1.Items.Add(mediaSaltos[k]);
                            }
                        }

                        if (DesvSaltosx.Checked == true)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                listBox1.Items.Add("Desviación estándar de los saltos número " + (k + 1) + " : ");

                                desvestSaltos[k] = Math.Truncate(desvestSaltos[k] * 100) / 100;

                                listBox1.Items.Add(desvestSaltos[k]);
                            }
                        }

                        if (CVSaltos.Checked == true)
                        {
                            double[] cvSaltos = new double[3];

                            for (int k = 0; k < 3; k++)
                            {
                                cvSaltos[k] = (desvestSaltos[k] / mediaSaltos[k]) * 100;

                                cvSaltos[k] = Math.Truncate(cvSaltos[k] * 100) / 100;

                                listBox1.Items.Add("EL coeficiente de variación porcentual de los saltos número " + (k + 1) + "es:");

                                listBox1.Items.Add(cvSaltos[k] + "%");
                            }
                        }

                        if (VectDatosx.Checked == true)
                        {
                            listBox1.Items.Add("El vector con todos los datos es: ");

                            for (int k = 0; k < i; k++)
                            {
                                number[k] = Math.Truncate(number[k] * 100) / 100;

                                listBox1.Items.Add(number[k]);
                            }
                        }

                        if (CantSaltosx.Checked == true)
                        {
                            listBox1.Items.Add("La cantidad de saltos es: ");

                            listBox1.Items.Add(cantSaltos);
                        }

                        if (ZonestX.Checked == true)
                        {
                            listBox1.Items.Add("Las zonas estables son: ");

                            for (int k = 0; k < elemfilt; k++)
                            {
                                media20[k] = Math.Truncate(media20[k] * 100) / 100;

                                listBox1.Items.Add(media20[k]);
                            }
                        }
                    }

                    else
                    {
                        listBox1.Items.Add("Seleccione alguna de las casillas");
                    }


                }

                else
                {
                    listBox1.Items.Add("El gráfico analizado no presenta elemntos estables útiles");
                }


            }

            else
            {
                for (int k = 0; k < i; k++)
                {

                    if (number[k] == 0)
                    {
                        cont++;
                    }


                }

                if (cont > 10)
                {
                    listBox1.Items.Add("Seleccione un equipo y valor de estándar");
                    cont = 0;
                }

                else
                {

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //StreamWriter sw = new StreamWriter(@"C:\Users\Baders\Desktop\Libroscsv\Archivo.txt");
            //foreach (object lista in listBox1.Items)
            //{
            //    sw.WriteLine(lista.ToString());
            //}
            //sw.Close();

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
             
                    myStream.Close();
                }

                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                foreach (object lista in listBox1.Items)
                {
                    sw.WriteLine(lista.ToString());
                }

                sw.Close();
            }
        }


        ///////////////////////////////////////////////////////////////////////

    }
}
