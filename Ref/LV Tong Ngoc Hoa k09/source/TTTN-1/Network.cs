using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MathNet.Numerics.LinearAlgebra.Single;
namespace TTTN_1
{
    public class Network
    {
        /**
         * the number of input nodes of the network
         */
        public int m_iNumInputNodes;
        /*
         * the number of hidden nodes of the network 
         */
        public int m_iNumHiddenNodes;
        /*
         * the number of output nodes of the network
         */
        public int m_iNumOutputNodes;

        public Perceptron[] m_arInputNodes; //array store all input nodes
        public Perceptron[] m_arHiddenNodes; //array store all hidden nodes
        public Perceptron[] m_arOutputNodes; //array store all output nodes

        public double[,] m_arInputHiddenConn; //2-dimensions array store all weight connected between input and hidden nodes
        public double[,] m_arHiddenOutputConn; //2-dimensions array store all weight connected between hidden and output nodes

        public double[] m_arHiddenBias; //array store all bias of hidden nodes
        public double[] m_arOutputBias; //array store all bias of output nodes       


        public double[,] Backup_m_arInputHiddenConn; //2-dimensions array backup all weight connected between input and hidden nodes
        public double[,] Backup_m_arHiddenOutputConn; //2-dimensions array backup all weight connected between hidden and output nodes

        public double[] Backup_m_arHiddenBias; //array backup all bias of hidden nodes
        public double[] Backup_m_arOutputBias; //array backup all bias of output nodes

        //Create new network
        public Network(int nInputNodes, int nHiddenNodes, int nOutputNodes)
        {
            //default parameter
            int i, j;
            m_iNumInputNodes = nInputNodes;
            m_iNumHiddenNodes = nHiddenNodes;
            m_iNumOutputNodes = nOutputNodes;
            m_arInputNodes = new Perceptron[m_iNumInputNodes];

            for (i = 0; i < m_iNumInputNodes; i++)
            {
                m_arInputNodes[i] = new Perceptron(ActionvationFunction.SIGMOID_FUNCTION);
            }

            m_arHiddenNodes = new Perceptron[m_iNumHiddenNodes];

            for (i = 0; i < m_iNumHiddenNodes; i++)
            {
                m_arHiddenNodes[i] = new Perceptron(ActionvationFunction.SIGMOID_FUNCTION);

            }
            m_arOutputNodes = new Perceptron[m_iNumOutputNodes];

            for (i = 0; i < m_iNumOutputNodes; i++)
            {
                m_arOutputNodes[i] = new Perceptron(ActionvationFunction.SIGMOID_FUNCTION);

            }
            m_arInputHiddenConn = new double[m_iNumHiddenNodes, m_iNumInputNodes];  //wij is weight from node j to node i
           
            m_arHiddenOutputConn = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
           
            m_arHiddenBias = new double[m_iNumHiddenNodes];  // weight from bias node at input layer to hidden node
           
            m_arOutputBias = new double[m_iNumOutputNodes]; // weight from bias node at hidden layer to output node

            Backup_m_arInputHiddenConn = new double[m_iNumHiddenNodes, m_iNumInputNodes];
            Backup_m_arHiddenOutputConn = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
            Backup_m_arHiddenBias = new double[m_iNumHiddenNodes];
            Backup_m_arOutputBias = new double[m_iNumOutputNodes];

            Random rand = new Random();   // create a random double from 0.0 to 1.0
            for (i = 0; i < m_iNumHiddenNodes; i++)
            {   // initialize bias values of Hidden nodes
                m_arHiddenBias[i] = rand.NextDouble();
                Backup_m_arHiddenBias[i] = m_arHiddenBias[i];
            }
            for (i = 0; i < m_iNumOutputNodes; i++)
            {  // initialize bias values of Output nodes
                m_arOutputBias[i] = rand.NextDouble();
                Backup_m_arOutputBias[i] = m_arOutputBias[i];
            }
            for (i = 0; i < m_iNumHiddenNodes; i++)    // initialize weight of Input Hidden connection
            {
                for (j = 0; j < m_iNumInputNodes; j++)
                {
                    m_arInputHiddenConn[i, j] = rand.NextDouble();
                    Backup_m_arInputHiddenConn[i, j] = m_arInputHiddenConn[i, j];
                }
            }
            for (i = 0; i < m_iNumOutputNodes; i++)   // initialize weight of Hidden Output connection
            {
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    m_arHiddenOutputConn[i, j] = rand.NextDouble();
                    Backup_m_arHiddenOutputConn[i, j] = m_arHiddenOutputConn[i, j];
                }
            }

        }//constructor function

        public void SA_Train(List<double> sample){
            List<double> sample2 = new List<double>();
            //TODO show config window and get some config

            //convert input to [0.01-0.99]
            double max = sample.Max();
            double min = sample.Min();
            int count = sample.Count;
            for(int i=0;i<count;i++){
                double a = sample.ElementAt(i);
                double b = (a - min) / (max - min) * (0.99 - 0.01) + 0.01;
                sample2.Add(b);
            }

            //run training
            SA_Run2(sample2);

        }//SA_Train method

        public void SA_Train_2(List<double> sample)
        {
            List<double> sample2 = new List<double>();
            //TODO show config window and get some config

            //convert input to [0.01-0.99]
            double max = sample.Max() + 200;
            double min = sample.Min() - 200;
            int count = sample.Count;
            for (int i = 0; i < count; i++)
            {
                double a = sample.ElementAt(i);
                double b = (a - min) / (max - min) * (0.99 - 0.01) + 0.01;
                sample2.Add(b);
            }

            //run training
            SA_Run(sample2);

        }//SA_Train method

        public void SA_Run(List<double> sample){
            //Bp_run2(sample, 0.1, 0.5, 10000, 1.0E-6);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here

            int i,j;
            double T,TAlpha;
            int L;
            double MAE;
            //1.1 init solution random when init network

            //1.2 calculate begin solution
            MAE = 0.0;
            //Count through training set
            for (i = m_iNumInputNodes; i < sample.Count; i++)
            {                
                /* calculate output of input nodes*/
                for (j = m_iNumInputNodes; j > 0; j--)
                {
                    m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample.ElementAt(i - j));
                }
                /*calculate output of hidden nodes*/
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumInputNodes; k++)
                    {
                        net += m_arInputNodes[k].GetOutPut() * m_arInputHiddenConn[j, k];
                    }
                    net += m_arHiddenBias[j];
                    m_arHiddenNodes[j].SetInput(net);
                    m_arHiddenNodes[j].CalOutput();
                }
                /*calculate output of output nodes*/
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumHiddenNodes; k++)
                    {
                        net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k];
                    }
                    net += m_arOutputBias[j];
                    m_arOutputNodes[j].SetInput(net);
                   // m_arOutputNodes[j].CalOutput();
                }
                /*calculate abs error*/
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    MAE += Math.Abs(sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                }
            }

            MAE = MAE / (sample.Count); // calculate mean square errors
         
            //1.3 Init value
            T = 200;//temperature
            TAlpha = 0.99;//[0.8~0.99]
            L = 20;//loops
            float WAlpha = 0.01f;
            int phase = 500;
            int maxR = 20;

            float[,] deltaWeight = new float[m_iNumHiddenNodes, m_iNumInputNodes];
            float[] deltaBias = new float[m_iNumHiddenNodes];

            //to random new solution
            Random rand = new Random();

            //test variable
           
            List<Point> MAError = new List<Point>();
            DenseMatrix X = new DenseMatrix(sample.Count - m_iNumInputNodes,m_iNumHiddenNodes + 1);
            float []XRow = new float[m_iNumHiddenNodes+1];
            float[] Y = new float[sample.Count - m_iNumInputNodes];

            double Error = MAE;//first error
            double LastError = 10000000;
            int index = 0;

           
            int reHeat = 0;
            double bestTemp = 0;
            do
            {
                Console.WriteLine(reHeat);
                do//Main SA
                {
                    Console.WriteLine(phase);
                    var st = System.Diagnostics.Stopwatch.StartNew();
                    for (int l = 0; l < L; l++)//loop in L
                    {

                        //generate new  random solution
                        for (j = 0; j < m_iNumHiddenNodes; j++)
                        {

                            for (int k = 0; k < m_iNumInputNodes; k++)
                            {
                                if (rand.Next(2) == 1)
                                    deltaWeight[j, k] = WAlpha;
                                else
                                    deltaWeight[j, k] = -WAlpha;
                            }
                            if (rand.Next(2) == 1)
                                deltaBias[j] = WAlpha;
                            else
                                deltaBias[j] = -WAlpha;

                        }
                        //calculate Woh


                        for (i = m_iNumInputNodes; i < sample.Count; i++)
                        {
                            /* calculate output of input nodes*/
                            for (j = m_iNumInputNodes; j > 0; j--)
                            {
                                m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample.ElementAt(i - j));
                            }
                            /*calculate output of hidden nodes*/
                            for (j = 0; j < m_iNumHiddenNodes; j++)
                            {
                                double net = 0.0;
                                for (int k = 0; k < m_iNumInputNodes; k++)
                                {
                                    net += m_arInputNodes[k].GetOutPut() * m_arInputHiddenConn[j, k] * (1 + deltaWeight[j, k]);
                                }
                                net += m_arHiddenBias[j] * (1 + deltaBias[j]);
                                m_arHiddenNodes[j].SetInput(net);
                                //m_arHiddenNodes[j].CalOutput();
                                XRow[j + 1] = (float)m_arHiddenNodes[j].GetOutPut(); //- (float)(Math.Log(1.0 / m_arHiddenNodes[j].GetOutPut() - 1) / Math.Log(Math.E));
                            }
                            XRow[0] = 1;
                            X.SetRow(i - m_iNumInputNodes, XRow);
                            Y[i - m_iNumInputNodes] = (float)sample.ElementAt(i);//-(float)(Math.Log(1.0 / sample.ElementAt(i) - 1.0) / Math.Log(Math.E));
                        }
                        //enough to solve matrix
                        //                         if(index>(m_iNumHiddenNodes + 1)){
                        //                            
                        var w_o_h = X.QR().Solve(new DenseVector(Y));

                        for (j = 0; j < m_iNumOutputNodes; j++)
                        {
                            for (int k = 0; k < m_iNumHiddenNodes; k++)
                            {
                                m_arHiddenOutputConn[j, k] = w_o_h[k];
                            }
                            m_arOutputBias[j] = w_o_h[j];
                        }
                     
                   
                        for (i = m_iNumInputNodes; i < sample.Count; i++)
                        {
                            /* calculate output of input nodes*/
                            for (j = m_iNumInputNodes; j > 0; j--)
                            {
                                m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample.ElementAt(i - j));
                            }
                            /*calculate output of hidden nodes*/
                            for (j = 0; j < m_iNumHiddenNodes; j++)
                            {
                                double net = 0.0;
                                for (int k = 0; k < m_iNumInputNodes; k++)
                                {
                                    net += m_arInputNodes[k].GetOutPut() * m_arInputHiddenConn[j, k] * (1 + deltaWeight[j, k]);
                                }
                                net += m_arHiddenBias[j] * (1 + deltaBias[j]);
                                m_arHiddenNodes[j].SetInput(net);
                                m_arHiddenNodes[j].CalOutput();
                            }
                            /*calculate output of output nodes*/
                            for (j = 0; j < m_iNumOutputNodes; j++)
                            {
                                double net = 0.0;
                                for (int k = 0; k < m_iNumHiddenNodes; k++)
                                {
                                    net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k];
                                }
                                net += m_arOutputBias[j];
                                m_arOutputNodes[j].SetInput(net);
                               // m_arOutputNodes[j].CalOutput();
                            }
                            /*calculate abs error*/
                            for (j = 0; j < m_iNumOutputNodes; j++)
                            {
                                LastError += Math.Abs(sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                            }
                        }

                        LastError = LastError / (sample.Count); // calculate mean square errors
                        if (LastError < Error)
                        {
                            Error = LastError;
                            //assign new input-hidden
                            for (j = 0; j < m_iNumHiddenNodes; j++)
                            {
                                for (int k = 0; k < m_iNumInputNodes; k++)
                                {
                                    m_arInputHiddenConn[j, k] *= (1 + deltaWeight[j, k]);
                                }
                                m_arHiddenBias[j] *= (1 + deltaBias[j]);
                            }
                            bestTemp = T;
                            reHeat = 0;
                            phase = 100;
                            l = 0;
                        }

                        index++;

                        MAError.Add(new Point(index, Error));
                        // }//loop in data set

                        //                     MAError.Add(new Point((100 - epochs + 1) * (i + 1), MAE));
                        st.Stop();
                        //System.Windows.MessageBox.Show("Solve time" + st.ElapsedMilliseconds);

                    }//loop in L
                    //if(Error<0.001) break;
                    //Update T
                    T = T * TAlpha;
                    phase--;
                } while (phase > 0);//while loop
                reHeat++;
                T = 2 * bestTemp;
            } while (reHeat < maxR);

           
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            TrainingResult result = new TrainingResult(MAError);
            result.result_console.AppendText("Training time: " + elapsedMs + "\n");
            result.Show();
        }

        public void SA_Run2(List<double> sample)
        {
            Bp_run2(sample, 0.1, 0.5, 10000, 1.0E-6);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here

            int i, j;
            double T, TAlpha;
            int L;
            double MAE;
            //1.1 init solution random when init network

            //1.2 calculate begin solution
            MAE = 0.0;
            //Count through training set
            for (i = m_iNumInputNodes; i < sample.Count; i++)
            {
                /* calculate output of input nodes*/
                for (j = m_iNumInputNodes; j > 0; j--)
                {
                    m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample.ElementAt(i - j));
                }
                /*calculate output of hidden nodes*/
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumInputNodes; k++)
                    {
                        net += m_arInputNodes[k].GetOutPut() * m_arInputHiddenConn[j, k];
                    }
                    net += m_arHiddenBias[j];
                    m_arHiddenNodes[j].SetInput(net);
                    m_arHiddenNodes[j].CalOutput();
                }
                /*calculate output of output nodes*/
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumHiddenNodes; k++)
                    {
                        net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k];
                    }
                    net += m_arOutputBias[j];
                    m_arOutputNodes[j].SetInput(net);
                    m_arOutputNodes[j].CalOutput();
                }
                /*calculate abs error*/
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    MAE += Math.Abs(sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                }
            }

            MAE = MAE / (sample.Count); // calculate mean square errors

            //1.3 Init value
            T = 200;//temperature
            TAlpha = 0.99;//[0.8~0.99]
            L = 20;//loops
            float WAlpha = 0.01f;
            int phase = 500;
            int maxR = 20;

            float[,] deltaWeight = new float[m_iNumHiddenNodes, m_iNumInputNodes];
            float[] deltaBias = new float[m_iNumHiddenNodes];


            float[,] deltaWeightY = new float[m_iNumOutputNodes, m_iNumHiddenNodes];
            float[] deltaBiasY = new float[m_iNumOutputNodes];
            //to random new solution
            Random rand = new Random();

            //test variable

            List<Point> MAError = new List<Point>();
            DenseMatrix X = new DenseMatrix(sample.Count - m_iNumInputNodes, m_iNumHiddenNodes + 1);
            float[] XRow = new float[m_iNumHiddenNodes + 1];
            float[] Y = new float[sample.Count - m_iNumInputNodes];

            double Error = MAE;//first error
            double LastError = 10000000;
            int index = 0;


            int reHeat = 0;
            double bestTemp = 0;
            do
            {
                //Console.WriteLine(reHeat);
                do//Main SA
                {
                    //Console.WriteLine(phase);
                    var st = System.Diagnostics.Stopwatch.StartNew();
                    for (int l = 0; l < L; l++)//loop in L
                    {
                        //Console.WriteLine(l);
                        //generate new  random solution
                        //input to hiden
                        for (j = 0; j < m_iNumHiddenNodes; j++)
                        {

                            for (int k = 0; k < m_iNumInputNodes; k++)
                            {
                                if (rand.Next(2) == 1)
                                    deltaWeight[j, k] = WAlpha;
                                else
                                    deltaWeight[j, k] = -WAlpha;
                            }
                            if (rand.Next(2) == 1)
                                deltaBias[j] = WAlpha;
                            else
                                deltaBias[j] = -WAlpha;

                        }                    
                        //hiden to output
                        for (j = 0; j < m_iNumOutputNodes; j++)
                        {
                            for (int k = 0; k < m_iNumHiddenNodes; k++)
                            {
                                if (rand.Next(2) == 1)
                                    deltaWeightY[j, k] = WAlpha;
                                else
                                    deltaWeightY[j, k] = -WAlpha;
                            }
                            if (rand.Next(2) == 1)
                                deltaBiasY[j] = WAlpha;
                            else
                                deltaBiasY[j] = -WAlpha;
                        }
                        //                         }

                    
                        //                         }
                        for (i = m_iNumInputNodes; i < sample.Count; i++)
                        {
                            /* calculate output of input nodes*/
                            for (j = m_iNumInputNodes; j > 0; j--)
                            {
                                m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample.ElementAt(i - j));
                            }
                            /*calculate output of hidden nodes*/
                            for (j = 0; j < m_iNumHiddenNodes; j++)
                            {
                                double net = 0.0;
                                for (int k = 0; k < m_iNumInputNodes; k++)
                                {
                                    net += m_arInputNodes[k].GetOutPut() * m_arInputHiddenConn[j, k] * (1 + deltaWeight[j, k]);
                                }
                                net += m_arHiddenBias[j] * (1 + deltaBias[j]);
                                m_arHiddenNodes[j].SetInput(net);
                                m_arHiddenNodes[j].CalOutput();
                            }
                            /*calculate output of output nodes*/
                            for (j = 0; j < m_iNumOutputNodes; j++)
                            {
                                double net = 0.0;
                                for (int k = 0; k < m_iNumHiddenNodes; k++)
                                {
                                    net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k] * (1 + deltaWeightY[j, k]);
                                }
                                net += m_arOutputBias[j]*(1 + deltaBiasY[j]);
                                m_arOutputNodes[j].SetInput(net);
                                m_arOutputNodes[j].CalOutput();
                            }
                            /*calculate abs error*/
                            for (j = 0; j < m_iNumOutputNodes; j++)
                            {
                                LastError += Math.Abs(sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                            }
                        }

                        LastError = LastError / (sample.Count); // calculate mean square errors

                        if (LastError < Error)
                        {
                            Console.WriteLine(LastError);
                            Error = LastError;
                            //assign new input-hidden
                            for (j = 0; j < m_iNumHiddenNodes; j++)
                            {
                                for (int k = 0; k < m_iNumInputNodes; k++)
                                {
                                    m_arInputHiddenConn[j, k] *= (1 + deltaWeight[j, k]);
                                }
                                m_arHiddenBias[j] *= (1 + deltaBias[j]);
                            }

                            for (j = 0; j < m_iNumOutputNodes; j++)
                            {
                                for (int k = 0; k < m_iNumHiddenNodes; k++)
                                {
                                    m_arHiddenOutputConn[j, k] *= (1 + deltaWeightY[j, k]);
                                }
                                m_arOutputBias[j] *= (1 + deltaBiasY[j]);
                            }
                            bestTemp = T;
                            phase = 100;
                        }
                        else
                        {
                            double ran = rand.NextDouble();
                            double rate = Math.Exp((Error - LastError)/(T/50000));

                            if (ran < rate)
                            {
                                Console.WriteLine("up");
                                Console.WriteLine(LastError);
                                Error = LastError;
                                //assign new input-hidden
                                for (j = 0; j < m_iNumHiddenNodes; j++)
                                {
                                    for (int k = 0; k < m_iNumInputNodes; k++)
                                    {
                                        m_arInputHiddenConn[j, k] *= (1 + deltaWeight[j, k]);
                                    }
                                    m_arHiddenBias[j] *= (1 + deltaBias[j]);
                                }

                                for (j = 0; j < m_iNumOutputNodes; j++)
                                {
                                    for (int k = 0; k < m_iNumHiddenNodes; k++)
                                    {
                                        m_arHiddenOutputConn[j, k] *= (1 + deltaWeightY[j, k]);
                                    }
                                    m_arOutputBias[j] *= (1 + deltaBiasY[j]);
                                }
                            }
                        }

                        index++;

                        MAError.Add(new Point(index, Error));
                        // }//loop in data set

                        //                     MAError.Add(new Point((100 - epochs + 1) * (i + 1), MAE));
                        st.Stop();
                        //System.Windows.MessageBox.Show("Solve time" + st.ElapsedMilliseconds);

                    }//loop in L
                    //if(Error<0.001) break;
                    //Update T
                    T = T * TAlpha;
                    phase--;
                } while (phase > 0);//while loop
                reHeat++;
                T = 2 * bestTemp;
            } while (reHeat < maxR);


            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            TrainingResult result = new TrainingResult(MAError);
            result.result_console.AppendText("Training time: " + elapsedMs + "\n");
            result.Show();
        }
        public void Bp_Train(List<double> sample)
        {
            
            
            List<double> sample2 = new List<double>();
            BP_Congfig bp_config = new BP_Congfig();
            bool? result;
            bp_config.ShowDialog();
            result = bp_config.DialogResult;
            if (result.Equals(true))
            {
                /*convert input to [0.01-0.99]*/
                double max = sample.Max()+200;
                double min = sample.Min()-200;
                int count = sample.Count;
                for (int i = 0; i < count; i++)
                {
                    double a = sample.ElementAt(i);
                    double b = (a - min) / (max - min) * (0.99 - 0.01) + 0.01;
                    sample2.Add(b);
                }
                int epoch = Bp_run(sample2, bp_config.learn, bp_config.momen, bp_config.theEpoches, bp_config.residual);
                show();
                //   System.Windows.MessageBox.Show("Traning finish!, the Number of epoch is: " + epoch + " \nYou should test the network before forecasting");
            }

        }//bp_train

        public int Bp_run(List<double> sample, double learnRate, double momentum, double theEpoches = 10000, double residual = 1.0E-5)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
            
            int i, j;
            List<Point> MAError = new List<Point>();
            int epoch = 0;
            double MAE = Double.MaxValue;
            double LastError = Double.MaxValue;
            double[,] deltaInputHidden = new double[m_iNumHiddenNodes, m_iNumInputNodes];
            double[,] deltaHiddenOutput = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
            double[,] LagdeltaInputHidden = new double[m_iNumHiddenNodes, m_iNumInputNodes];
            double[,] LagdeltaHiddenOutput = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
            double[] deltaOutputBias = new double[m_iNumOutputNodes];
            double[] deltaHiddenBias = new double[m_iNumHiddenNodes];
            double[] LagdeltaOutputBias = new double[m_iNumOutputNodes];
            double[] LagdeltaHiddenBias = new double[m_iNumHiddenNodes];
            //double counter = 0;
            for (i = 0; i < m_iNumHiddenNodes; i++)    // initialize weight-step of Input Hidden connection
            {
                for (j = 0; j < m_iNumInputNodes; j++)
                {
                    deltaInputHidden[i, j] = 0.0;
                    LagdeltaInputHidden[i, j] = 0.0;
                }
                deltaHiddenBias[i] = 0.0;
                LagdeltaHiddenBias[i] = 0.0;
            }
            for (i = 0; i < m_iNumOutputNodes; i++)   // initialize weight-step of Hidden Output connection
            {
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    deltaHiddenOutput[i, j] = 0.0;
                    LagdeltaHiddenOutput[i, j] = 0.0;
                }
                deltaOutputBias[i] = 0.0;
                LagdeltaOutputBias[i] = 0.0;
            }
            while (epoch < theEpoches)
            {
                MAE = 0.0;
                /*training for each epoch*/
                for (i = m_iNumInputNodes; i < sample.Count; i++)
                {
                    //forward
                    /* calculate output of input nodes*/
                    for (j = m_iNumInputNodes; j > 0; j--)
                    {
                        m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample.ElementAt(i - j));
                    }
                    /*calculate output of hidden nodes*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        double net = 0.0;
                        for (int k = 0; k < m_iNumInputNodes; k++)
                        {
                            net += m_arInputNodes[k].GetOutPut() * m_arInputHiddenConn[j, k];
                        }
                        net += m_arHiddenBias[j];
                        m_arHiddenNodes[j].SetInput(net);
                        m_arHiddenNodes[j].CalOutput();
                    }
                    /*calculate output of output nodes*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        double net = 0.0;
                        for (int k = 0; k < m_iNumHiddenNodes; k++)
                        {
                            net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k];
                        }
                        net += m_arOutputBias[j];
                        m_arOutputNodes[j].SetInput(net);
                        m_arOutputNodes[j].CalOutput();
                    }
                    /*calculate abs error*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        MAE += Math.Abs(sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                    }
                    // backward
                    /*calculate weight-step for weights connecting from hidden nodes to output nodes*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        for (int k = 0; k < m_iNumHiddenNodes; k++)
                        {
                            double parDerv = -m_arOutputNodes[j].GetOutPut() * (1 - m_arOutputNodes[j].GetOutPut()) * m_arHiddenNodes[k].GetOutPut() * (sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                            deltaHiddenOutput[j, k] = -learnRate * parDerv + momentum * LagdeltaHiddenOutput[j, k];
                            LagdeltaHiddenOutput[j, k] = deltaHiddenOutput[j, k];
                        }
                        double parDervBias = -m_arOutputNodes[j].GetOutPut() * (1 - m_arOutputNodes[j].GetOutPut()) * (sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                        deltaOutputBias[j] = -learnRate * parDervBias + momentum * LagdeltaOutputBias[j];
                        LagdeltaOutputBias[j] = deltaOutputBias[j];
                    }
                    /*calculate weight-step for weights connecting from input nodes to hidden nodes*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        double temp = 0.0;
                        for (int r = 0; r < m_iNumOutputNodes; r++)
                        {
                            temp += -(sample.ElementAt(i) - m_arOutputNodes[r].GetOutPut()) * m_arOutputNodes[r].GetOutPut() * (1 - m_arOutputNodes[r].GetOutPut()) * m_arHiddenOutputConn[r, j];
                        }
                        for (int k = 0; k < m_iNumInputNodes; k++)
                        {
                            double parDerv = m_arHiddenNodes[j].GetOutPut() * (1 - m_arHiddenNodes[j].GetOutPut()) * m_arInputNodes[k].GetOutPut() * temp;
                            deltaInputHidden[j, k] = -learnRate * parDerv + momentum * LagdeltaInputHidden[j, k];
                            LagdeltaInputHidden[j, k] = deltaInputHidden[j, k];
                        }
                        double parDervBias = m_arHiddenNodes[j].GetOutPut() * (1 - m_arHiddenNodes[j].GetOutPut()) * temp;
                        deltaHiddenBias[j] = -learnRate * parDervBias + momentum * LagdeltaHiddenBias[j];
                        LagdeltaHiddenBias[j] = deltaHiddenBias[j];
                    }
                    /*updating weight from Input to Hidden*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        for (int k = 0; k < m_iNumInputNodes; k++)
                        {
                            m_arInputHiddenConn[j, k] += deltaInputHidden[j, k];
                        }
                    }
                    /*updating bias of Hidden nodes*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        m_arHiddenBias[j] += deltaHiddenBias[j];
                    }
                    /*updating weight from Hidden to Output*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        for (int k = 0; k < m_iNumHiddenNodes; k++)
                        {
                            m_arHiddenOutputConn[j, k] += deltaHiddenOutput[j, k];
                        }
                    }
                    /* updating bias of Output nodes*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        m_arOutputBias[j] += deltaOutputBias[j];
                    }

                } // end outer for
                MAE = MAE / (sample.Count); // calculate mean square error
                if (Math.Abs(MAE - LastError) < residual) // if the Error is not improved significantly, halt training process and rollback
                {
                  
                    break;

                }
                else
                { //else backup the current configuration and continue training
                    LastError = MAE;
                    MAError.Add(new Point(epoch,MAE));
                    epoch++;
                }
            }

            /*calculate elapse time*/
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;


            /* output training result */
            TrainingResult result = new TrainingResult(MAError);
            result.result_console.AppendText("Maximum Epochs: " + theEpoches + "\n");
            result.result_console.AppendText("Training Epochs: " + epoch + "\n");
            result.result_console.AppendText("Training MAE: " + MAE + "\n");
            result.result_console.AppendText("Terminated Condition: residual of Error is less than " + residual + "\n");
            result.result_console.AppendText("Learning Rate: " + learnRate + "\n");
            result.result_console.AppendText("Momentum Term: " + momentum + "\n");
            result.result_console.AppendText("Training time: " + elapsedMs + "\n");
            result.Show();
            return epoch;
        }//training

        public int Bp_run2(List<double> sample, double learnRate, double momentum, double theEpoches = 10000, double residual = 1.0E-5)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here

            int i, j;
            List<Point> MAError = new List<Point>();
            int epoch = 0;
            double MAE = Double.MaxValue;
            double LastError = Double.MaxValue;
            double[,] deltaInputHidden = new double[m_iNumHiddenNodes, m_iNumInputNodes];
            double[,] deltaHiddenOutput = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
            double[,] LagdeltaInputHidden = new double[m_iNumHiddenNodes, m_iNumInputNodes];
            double[,] LagdeltaHiddenOutput = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
            double[] deltaOutputBias = new double[m_iNumOutputNodes];
            double[] deltaHiddenBias = new double[m_iNumHiddenNodes];
            double[] LagdeltaOutputBias = new double[m_iNumOutputNodes];
            double[] LagdeltaHiddenBias = new double[m_iNumHiddenNodes];
            //double counter = 0;
            for (i = 0; i < m_iNumHiddenNodes; i++)    // initialize weight-step of Input Hidden connection
            {
                for (j = 0; j < m_iNumInputNodes; j++)
                {
                    deltaInputHidden[i, j] = 0.0;
                    LagdeltaInputHidden[i, j] = 0.0;
                }
                deltaHiddenBias[i] = 0.0;
                LagdeltaHiddenBias[i] = 0.0;
            }
            for (i = 0; i < m_iNumOutputNodes; i++)   // initialize weight-step of Hidden Output connection
            {
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    deltaHiddenOutput[i, j] = 0.0;
                    LagdeltaHiddenOutput[i, j] = 0.0;
                }
                deltaOutputBias[i] = 0.0;
                LagdeltaOutputBias[i] = 0.0;
            }
            while (epoch < theEpoches)
            {
                MAE = 0.0;
                /*training for each epoch*/
                for (i = m_iNumInputNodes; i < sample.Count; i++)
                {
                    //forward
                    /* calculate output of input nodes*/
                    for (j = m_iNumInputNodes; j > 0; j--)
                    {
                        m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample.ElementAt(i - j));
                    }
                    /*calculate output of hidden nodes*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        double net = 0.0;
                        for (int k = 0; k < m_iNumInputNodes; k++)
                        {
                            net += m_arInputNodes[k].GetOutPut() * m_arInputHiddenConn[j, k];
                        }
                        net += m_arHiddenBias[j];
                        m_arHiddenNodes[j].SetInput(net);
                        m_arHiddenNodes[j].CalOutput();
                    }
                    /*calculate output of output nodes*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        double net = 0.0;
                        for (int k = 0; k < m_iNumHiddenNodes; k++)
                        {
                            net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k];
                        }
                        net += m_arOutputBias[j];
                        m_arOutputNodes[j].SetInput(net);
                        m_arOutputNodes[j].CalOutput();
                    }
                    /*calculate abs error*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        MAE += Math.Abs(sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                    }
                    // backward
                    /*calculate weight-step for weights connecting from hidden nodes to output nodes*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        for (int k = 0; k < m_iNumHiddenNodes; k++)
                        {
                            double parDerv = -m_arOutputNodes[j].GetOutPut() * (1 - m_arOutputNodes[j].GetOutPut()) * m_arHiddenNodes[k].GetOutPut() * (sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                            deltaHiddenOutput[j, k] = -learnRate * parDerv + momentum * LagdeltaHiddenOutput[j, k];
                            LagdeltaHiddenOutput[j, k] = deltaHiddenOutput[j, k];
                        }
                        double parDervBias = -m_arOutputNodes[j].GetOutPut() * (1 - m_arOutputNodes[j].GetOutPut()) * (sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                        deltaOutputBias[j] = -learnRate * parDervBias + momentum * LagdeltaOutputBias[j];
                        LagdeltaOutputBias[j] = deltaOutputBias[j];
                    }
                    /*calculate weight-step for weights connecting from input nodes to hidden nodes*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        double temp = 0.0;
                        for (int r = 0; r < m_iNumOutputNodes; r++)
                        {
                            temp += -(sample.ElementAt(i) - m_arOutputNodes[r].GetOutPut()) * m_arOutputNodes[r].GetOutPut() * (1 - m_arOutputNodes[r].GetOutPut()) * m_arHiddenOutputConn[r, j];
                        }
                        for (int k = 0; k < m_iNumInputNodes; k++)
                        {
                            double parDerv = m_arHiddenNodes[j].GetOutPut() * (1 - m_arHiddenNodes[j].GetOutPut()) * m_arInputNodes[k].GetOutPut() * temp;
                            deltaInputHidden[j, k] = -learnRate * parDerv + momentum * LagdeltaInputHidden[j, k];
                            LagdeltaInputHidden[j, k] = deltaInputHidden[j, k];
                        }
                        double parDervBias = m_arHiddenNodes[j].GetOutPut() * (1 - m_arHiddenNodes[j].GetOutPut()) * temp;
                        deltaHiddenBias[j] = -learnRate * parDervBias + momentum * LagdeltaHiddenBias[j];
                        LagdeltaHiddenBias[j] = deltaHiddenBias[j];
                    }
                    /*updating weight from Input to Hidden*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        for (int k = 0; k < m_iNumInputNodes; k++)
                        {
                            m_arInputHiddenConn[j, k] += deltaInputHidden[j, k];
                        }
                    }
                    /*updating bias of Hidden nodes*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        m_arHiddenBias[j] += deltaHiddenBias[j];
                    }
                    /*updating weight from Hidden to Output*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        for (int k = 0; k < m_iNumHiddenNodes; k++)
                        {
                            m_arHiddenOutputConn[j, k] += deltaHiddenOutput[j, k];
                        }
                    }
                    /* updating bias of Output nodes*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        m_arOutputBias[j] += deltaOutputBias[j];
                    }

                } // end outer for
                MAE = MAE / (sample.Count); // calculate mean square error
                if (Math.Abs(MAE - LastError) < residual) // if the Error is not improved significantly, halt training process and rollback
                {

                    break;

                }
                else
                { //else backup the current configuration and continue training
                    LastError = MAE;
                    MAError.Add(new Point(epoch, MAE));
                    epoch++;
                }
            }

           
            return epoch;
        }//training


        public void Sa_Khanh_run(List<double> sample, double t1, double t2, int n_in, int n_out)
        {
            sample = to01List(sample);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<Point> MAError = new List<Point>();
            int index = 0;
            int i, j;
            double step = Math.Exp((Math.Log(t2 / t1)) / (n_out - 1));
            double temp = t1;
            int count = 0;
            double minMAE = calMAE(sample);
            Random rand = new Random();
            backup();
            while (temp > t2)
            {
                for (i = 0; i < n_in; i++)
                {
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        for (int k = 0; k < m_iNumInputNodes; k++)
                        {
                            double add = 0.5 - (rand.NextDouble());
                            add *= (temp / t1);
                            m_arInputHiddenConn[j, k] += add;
                        }
                    }
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        for (int k = 0; k < m_iNumHiddenNodes; k++)
                        {
                            double add = 0.5 - (rand.NextDouble());
                            add *= (temp / t1);
                            m_arHiddenOutputConn[j, k] += add;
                        }
                    }
                    double currentMAE = calMAE(sample);
                    if (currentMAE < minMAE)
                    {
                        minMAE = currentMAE;
                        MAError.Add(new Point(index, minMAE));
                        index++;
                        backup();
                    }
                    else
                    {
                        rollback();
                    }
                }
                count++;
                temp *= step;
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            TrainingResult result = new TrainingResult(MAError);
            result.result_console.AppendText("Training time: " + elapsedMs + "\n");
            result.Show();
        }

        public void Bp_Khanh_run(List<double> sample, double learnRate, double momentum, double theEpoches = 10000, double residual = 1.0E-5)
        {
            sample = to01List(sample);
            int i, j;
            List<double> MAError = new List<double>();
            int epoch = 0;
            double MAE = Double.MaxValue;
            double LastError = Double.MaxValue;
            double[,] deltaInputHidden = new double[m_iNumHiddenNodes, m_iNumInputNodes];
            double[,] deltaHiddenOutput = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
            double[,] LagdeltaInputHidden = new double[m_iNumHiddenNodes, m_iNumInputNodes];
            double[,] LagdeltaHiddenOutput = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
            double[] deltaOutputBias = new double[m_iNumOutputNodes];
            double[] deltaHiddenBias = new double[m_iNumHiddenNodes];
            double[] LagdeltaOutputBias = new double[m_iNumOutputNodes];
            double[] LagdeltaHiddenBias = new double[m_iNumHiddenNodes];
            double counter = 0;
            for (i = 0; i < m_iNumHiddenNodes; i++)    // initialize weight-step of Input Hidden connection
            {
                for (j = 0; j < m_iNumInputNodes; j++)
                {
                    deltaInputHidden[i, j] = 0.0;
                    LagdeltaInputHidden[i, j] = 0.0;
                }
                deltaHiddenBias[i] = 0.0;
                LagdeltaHiddenBias[i] = 0.0;
            }
            for (i = 0; i < m_iNumOutputNodes; i++)   // initialize weight-step of Hidden Output connection
            {
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    deltaHiddenOutput[i, j] = 0.0;
                    LagdeltaHiddenOutput[i, j] = 0.0;
                }
                deltaOutputBias[i] = 0.0;
                LagdeltaOutputBias[i] = 0.0;
            }
            while (epoch < theEpoches)
            {
                /*training for each epoch*/
                for (i = m_iNumInputNodes; i < sample.Count; i++)
                {
                    //forward
                    /* calculate output of input nodes*/
                    for (j = m_iNumInputNodes; j > 0; j--)
                    {
                        m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample.ElementAt(i - j));
                    }
                    /*calculate output of hidden nodes*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        double net = 0.0;
                        for (int k = 0; k < m_iNumInputNodes; k++)
                        {
                            net += m_arInputNodes[k].GetOutPut() * m_arInputHiddenConn[j, k];
                        }
                        net += m_arHiddenBias[j];
                        m_arHiddenNodes[j].SetOutput(net);
                        m_arHiddenNodes[j].CalOutput();
                    }
                    /*calculate output of output nodes*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        double net = 0.0;
                        for (int k = 0; k < m_iNumHiddenNodes; k++)
                        {
                            net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k];
                        }
                        net += m_arOutputBias[j];
                        m_arOutputNodes[j].SetOutput(net);
                        m_arOutputNodes[j].CalOutput();
                    }
                    // backward
                    /*calculate weight-step for weights connecting from hidden nodes to output nodes*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        for (int k = 0; k < m_iNumHiddenNodes; k++)
                        {
                            double parDerv = -m_arOutputNodes[j].GetOutPut() * (1 - m_arOutputNodes[j].GetOutPut()) * m_arHiddenNodes[k].GetOutPut() * (sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                            deltaHiddenOutput[j, k] = -learnRate * parDerv + momentum * LagdeltaHiddenOutput[j, k];
                            LagdeltaHiddenOutput[j, k] = deltaHiddenOutput[j, k];
                        }
                        double parDervBias = -m_arOutputNodes[j].GetOutPut() * (1 - m_arOutputNodes[j].GetOutPut()) * (sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                        deltaOutputBias[j] = -learnRate * parDervBias + momentum * LagdeltaOutputBias[j];
                        LagdeltaOutputBias[j] = deltaOutputBias[j];
                    }
                    /*calculate weight-step for weights connecting from input nodes to hidden nodes*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        double temp = 0.0;
                        for (int r = 0; r < m_iNumOutputNodes; r++)
                        {
                            temp += -(sample.ElementAt(i) - m_arOutputNodes[r].GetOutPut()) * m_arOutputNodes[r].GetOutPut() * (1 - m_arOutputNodes[r].GetOutPut()) * m_arHiddenOutputConn[r, j];
                        }
                        for (int k = 0; k < m_iNumInputNodes; k++)
                        {
                            double parDerv = m_arHiddenNodes[j].GetOutPut() * (1 - m_arHiddenNodes[j].GetOutPut()) * m_arInputNodes[k].GetOutPut() * temp;
                            deltaInputHidden[j, k] = -learnRate * parDerv + momentum * LagdeltaInputHidden[j, k];
                            LagdeltaInputHidden[j, k] = deltaInputHidden[j, k];
                        }
                        double parDervBias = m_arHiddenNodes[j].GetOutPut() * (1 - m_arHiddenNodes[j].GetOutPut()) * temp;
                        deltaHiddenBias[j] = -learnRate * parDervBias + momentum * LagdeltaHiddenBias[j];
                        LagdeltaHiddenBias[j] = deltaHiddenBias[j];
                    }
                    /*updating weight from Input to Hidden*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        for (int k = 0; k < m_iNumInputNodes; k++)
                        {
                            m_arInputHiddenConn[j, k] += deltaInputHidden[j, k];
                        }
                    }
                    /*updating bias of Hidden nodes*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        m_arHiddenBias[j] += deltaHiddenBias[j];
                    }
                    /*updating weight from Hidden to Output*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        for (int k = 0; k < m_iNumHiddenNodes; k++)
                        {
                            m_arHiddenOutputConn[j, k] += deltaHiddenOutput[j, k];
                        }
                    }
                    /* updating bias of Output nodes*/
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        m_arOutputBias[j] += deltaOutputBias[j];
                    }

                } // end outer for
                MAE = calMAE(sample); // caculate mean square error
                if (Math.Abs(MAE - LastError) < residual && epoch > 100) // if the Error is not improved significantly, halt training process and rollback
                {
                    rollback();
                    break;
                }
                else
                { //else backup the current configuration and continue training
                    LastError = MAE;
                    backup();
                    MAError.Add(MAE);
                    epoch++;
                }
            }
        }
        public double calMAE(List<double> sample)
        {
            int i, j;
            double MAE = 0;
            for (i = m_iNumInputNodes; i < sample.Count; i++)
            {
                for (j = m_iNumInputNodes; j > 0; j--)
                {
                    m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample.ElementAt(i - j));
                }
                /*calculate output of hidden nodes*/
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumInputNodes; k++)
                    {
                        net += m_arInputNodes[k].GetOutPut() * m_arInputHiddenConn[j, k];
                    }
                    net += m_arHiddenBias[j];
                    m_arHiddenNodes[j].SetInput(net);
                    m_arHiddenNodes[j].CalOutput();
                }
                /*calculate output of output nodes*/
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumHiddenNodes; k++)
                    {
                        net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k];
                    }
                    net += m_arOutputBias[j];
                    m_arOutputNodes[j].SetInput(net);
                    m_arOutputNodes[j].CalOutput();
                }
                /*calculate abs error*/
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    MAE += Math.Abs(sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                }
            }
            return MAE / (sample.Count - m_iNumInputNodes);
        }

        public void rollback()
        {
            int i, j;
            for (i = 0; i < m_iNumHiddenNodes; i++)
            {
                m_arHiddenBias[i] = Backup_m_arHiddenBias[i];
            }
            for (i = 0; i < m_iNumOutputNodes; i++)
            {
                m_arOutputBias[i] = Backup_m_arOutputBias[i];
            }
            for (i = 0; i < m_iNumHiddenNodes; i++)
            {
                for (j = 0; j < m_iNumInputNodes; j++)
                {
                    m_arInputHiddenConn[i, j] = Backup_m_arInputHiddenConn[i, j];
                }
            }
            for (i = 0; i < m_iNumOutputNodes; i++)
            {
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    m_arHiddenOutputConn[i, j] = Backup_m_arHiddenOutputConn[i, j];
                }
            }
        }
        /*
         * backup weights of neural 
         */
        public void backup()
        {
            int i, j;
            for (i = 0; i < m_iNumHiddenNodes; i++)
            {
                Backup_m_arHiddenBias[i] = m_arHiddenBias[i];
            }
            for (i = 0; i < m_iNumOutputNodes; i++)
            {
                Backup_m_arOutputBias[i] = m_arOutputBias[i];
            }
            for (i = 0; i < m_iNumHiddenNodes; i++)
            {
                for (j = 0; j < m_iNumInputNodes; j++)
                {
                    Backup_m_arInputHiddenConn[i, j] = m_arInputHiddenConn[i, j];
                }
            }
            for (i = 0; i < m_iNumOutputNodes; i++)
            {
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    Backup_m_arHiddenOutputConn[i, j] = m_arHiddenOutputConn[i, j];
                }
            }
        }

        public List<double> to01List(List<double> sample)
        {
            double max = sample.Max();
            double min = sample.Min();
            int j = 0;
            List<double> result = new List<double>();
            int count = sample.Count;
            for (j = 0; j < count; j++)
            {
                double a = sample[j];
                double b = (a - min) / (max - min) * (0.99 - 0.02) + 0.02;
                result.Add(b);
            }
            return result;
        }
        
        //Show result of training
        public void show()
        {
            Console.WriteLine("Bias of Hidden layer: ");
            for (int i = 0; i < m_iNumHiddenNodes; i++)
            {
                Console.WriteLine("Hidden node #" + i + " : " + m_arHiddenBias[i]);
            }
            Console.WriteLine("\nWeight of Input - Hidden layer: ");
            for (int j = 0; j < m_iNumInputNodes; j++)
            {
                for (int i = 0; i < m_iNumHiddenNodes; i++)
                {
                    Console.WriteLine("[" + j + "," + i + "] : " + m_arInputHiddenConn[i, j]);
                }
            }
            Console.WriteLine("Bias of Output layer: ");
            for (int i = 0; i < m_iNumOutputNodes; i++)
            {
                Console.WriteLine("Output node #" + i + " : " + m_arOutputBias[i]);
            }
            Console.WriteLine("\nWeight of output - Hidden layer: ");
            for (int j = 0; j < m_iNumOutputNodes; j++)
            {
                for (int i = 0; i < m_iNumHiddenNodes; i++)
                {
                    Console.WriteLine("[" + j + "," + i + "] : " + m_arHiddenOutputConn[j, i]);
                }
            }

        }//show

        public void test(List<double> sample)
        {
            double[] result = new double[sample.Count];
            int i = 0;
            int j = 0;
            double MAE = 0;
            double SSE = 0;
            double MSE = 0;
            double MAPE = 0;

            
            List<double> sample2 = new List<double>();
            /*convert input to [0.01-0.99]*/
            double max = sample.Max();
            double min = sample.Min();
            int count = sample.Count;
            for (i = 0; i < count; i++)
            {
                double a = sample.ElementAt(i);
                double b = (a - min) / (max - min) * (0.99 - 0.01) + 0.01;
                sample2.Add(b);
            }
            for (i = 0; i < m_iNumInputNodes; i++)
            {
                result[i] = sample2[i];
            }
            for (i = m_iNumInputNodes; i < sample2.Count; i++)
            {
                //forward
                /* calculate output of input nodes*/
                for (j = m_iNumInputNodes; j > 0; j--)
                {
                    m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample2.ElementAt(i - j));
                }
                /*calculate output of hidden nodes*/
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumInputNodes; k++)
                    {
                        net += m_arInputNodes[k].GetOutPut() * m_arInputHiddenConn[j, k];
                    }
                    net += m_arHiddenBias[j];
                    m_arHiddenNodes[j].SetInput(net);
                    m_arHiddenNodes[j].CalOutput();
                }
                /*calculate output of output nodes*/
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumHiddenNodes; k++)
                    {
                        net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k];
                    }
                    net += m_arOutputBias[j];
                    m_arOutputNodes[j].SetInput(net);
                    m_arOutputNodes[j].CalOutput();
                    /*convert output int range [0.01-0.99] to real value*/
                    double a = m_arOutputNodes[j].GetOutPut();
                    result[i] = a;
                }
            }
            for (i = 0; i < sample.Count; i++)
            {
                MAE += Math.Abs(result[i] - sample2[i]);
                SSE += Math.Pow(result[i] - sample2[i], 2);
                MAPE += Math.Abs(result[i] - sample2[i]) / sample2[i];
            }
            
            MAE = MAE / sample2.Count;
            MSE = SSE / sample2.Count;
            MAPE = MAPE / sample2.Count;
            //convert result and sample to list and pass through TestingResult form
            List<Point> s = new List<Point>();
            List<Point> c = new List<Point>();
            for (int t = 0; t < sample2.Count; t++)
            {
                Console.WriteLine(sample2.ElementAt(t));
                Console.WriteLine(result[t]);
                s.Add(new Point(t + 1, sample2.ElementAt(t)));
                c.Add(new Point(t + 1, result[t]));
            }

            /* output testing result */
            TestingResult form = new TestingResult(s,c);
            form.result_console.AppendText("Mean Absolute Error MAE =  " + MAE + "\n");
            form.result_console.AppendText("Sum Square Error SSE =  " + SSE + "\n");
            form.result_console.AppendText("Mean Square Error MSE =  " + MSE + "\n");
            form.result_console.AppendText("Mean Absolute Percentage Eror MAPE =  " + MAPE + "\n");
                      
            form.Show();
          
        }//test


        public void test2(List<double> sample)
        {
            double[] result = new double[sample.Count];
            int i = 0;
            int j = 0;
            double MAE = 0;
            double SSE = 0;
            double MSE = 0;
            double MAPE = 0;

            
            List<double> sample2 = new List<double>();
            /*convert input to [0.01-0.99]*/
            double max = sample.Max();
            double min = sample.Min();
            int count = sample.Count;
            for (i = 0; i < count; i++)
            {
                double a = sample.ElementAt(i);
                double b = (a - min) / (max - min) * (0.99 - 0.01) + 0.01;
                sample2.Add(b);
            }
            for (i = 0; i < m_iNumInputNodes; i++)
            {
                result[i] = sample2[i];
            }
            for (i = m_iNumInputNodes; i < sample2.Count; i++)
            {
                //forward
                /* calculate output of input nodes*/
                for (j = m_iNumInputNodes; j > 0; j--)
                {
                    m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample2.ElementAt(i - j));
                }
                /*calculate output of hidden nodes*/
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumInputNodes; k++)
                    {
                        net += m_arInputNodes[k].GetOutPut() * m_arInputHiddenConn[j, k];
                    }
                    net += m_arHiddenBias[j];
                    m_arHiddenNodes[j].SetInput(net);
                    m_arHiddenNodes[j].CalOutput();
                }
                /*calculate output of output nodes*/
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumHiddenNodes; k++)
                    {
                        net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k];
                    }
                    net += m_arOutputBias[j];
                    m_arOutputNodes[j].SetInput(net);
                   // m_arOutputNodes[j].CalOutput();
                    /*convert output int range [0.01-0.99] to real value*/
                    double a = m_arOutputNodes[j].GetOutPut();
                    result[i] = a;
                    //result[i] = (a - 0.01) / (0.99 - 0.01) * (max - min) + min;
                }
            }
            for (i = 0; i < sample2.Count; i++)
            {
                MAE += Math.Abs(result[i] - sample2[i]);
                SSE += Math.Pow(result[i] - sample2[i], 2);
                MAPE += Math.Abs(result[i] - sample2[i]) / sample2[i];
            }
            MAE = MAE / sample2.Count;
            MSE = SSE / sample2.Count;
            MAPE = MAPE / sample2.Count;
            //convert result and sample to list and pass through TestingResult form
            List<Point> s = new List<Point>();
            List<Point> c = new List<Point>();
            for (int t = 0; t < sample2.Count; t++)
            {
                Console.WriteLine(sample2.ElementAt(t));
                Console.WriteLine(result[t]);
                s.Add(new Point(t + 1, sample2.ElementAt(t)));
                c.Add(new Point(t + 1, result[t]));
            }
            
            /* output testing result */
            TestingResult form = new TestingResult(s, c);
            form.result_console.AppendText("Mean Absolute Error MAE =  " + MAE + "\n");
            form.result_console.AppendText("Sum Square Error SSE =  " + SSE + "\n");
            form.result_console.AppendText("Mean Square Error MSE =  " + MSE + "\n");
            form.result_console.AppendText("Mean Absolute Percentage Eror MAPE =  " + MAPE + "\n");

            form.Show();

        }//test 2 linear

        public void Forecast(List<double> sample, int nahead = 1)
        {
            int i = 0;
            int j = 0;
            double[] result = new double[nahead];
            double max = 0;
            double min = 0;
            for (i = 0; i < nahead; i++)
            {
                int count = sample.Count;
                min = sample.Min()+200;
                max = sample.Max()-200;
                /* calculate output of input nodes*/
                for (j = m_iNumInputNodes; j > 0; j--)
                {
                    double a = sample[count - j];
                    double b = (a - min) / (max - min) * (0.99 - 0.01) + 0.01;
                    m_arInputNodes[m_iNumInputNodes - j].SetOutput(b);
                }
                /*calculate output of hidden nodes*/
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumInputNodes; k++)
                    {
                        net += m_arInputNodes[k].GetOutPut() * m_arInputHiddenConn[j, k];
                    }
                    net += m_arHiddenBias[j];
                    m_arHiddenNodes[j].SetInput(net);
                    m_arHiddenNodes[j].CalOutput();
                }
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumHiddenNodes; k++)
                    {
                        net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k];
                    }
                    net += m_arOutputBias[j];
                    m_arOutputNodes[j].SetInput(net);
                    m_arOutputNodes[j].CalOutput();
                    /*convert output int range [0.01-0.99] to real value*/
                    double a = m_arOutputNodes[j].GetOutPut();
                    result[i] = (a - 0.01) / (0.99 - 0.01) * (max - min) + min;
                    sample.Add(result[i]);
                }

            }

            List<Point> s = new List<Point>();
            List<Point> c = new List<Point>();
            int t = 0;
            for (t = 0; t < sample.Count; t++)
            {
                s.Add(new Point(t + 1, sample.ElementAt(t)));
                
            }
            for (i = 0; i < result.Length; i++)
            {
                c.Add(new Point(i + t, result[i]));
            }

            ForecastResult form = new ForecastResult(s, c);
         
            form.result_console.AppendText("nAhead           value\n");
            for (i = 0; i < result.Length; i++)
            {
                int ahead = i + 1;
                form.result_console.AppendText(ahead + "         " + result[i] + "\n");
            }
            form.Show();
            
        }//forecast

    }//class
}//namespace
