using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Windows;
using MathNet.Numerics.LinearAlgebra.Single;


namespace NeuronNetwork
{
    public class Network
    {
        /**
         * Network parameter
         */
        /**
         * the number of input nodes of the network
         */
        public int m_iNumInputNodes;
        /*
         * the number of hiddden nodes of the network 
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

        /**
         * Default Contructor
         */
        private Network()
        {
            m_iNumInputNodes = 0;
            m_iNumHiddenNodes = 0;
            m_iNumOutputNodes = 0;
        }

        /**
         * Init basic network
         */
        private void BasicInitForNetwork(int nInputNodes, int nHiddenNodes, int nOutputNodes)
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
        }

        /**
         * Constructor
         * Create a three-layer network with nInputNodes input nodes, nHiddenNodes hidden nodes
         * nOutputNodes outputnodes
         * Author: DataMining-Research08
         */
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
            Backup_m_arInputHiddenConn = new double[m_iNumHiddenNodes, m_iNumInputNodes];
            m_arHiddenOutputConn = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
            Backup_m_arHiddenOutputConn = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
            m_arHiddenBias = new double[m_iNumHiddenNodes];  // weight from bias node at input layer to hidden node
            Backup_m_arHiddenBias = new double[m_iNumHiddenNodes];
            m_arOutputBias = new double[m_iNumOutputNodes]; // weight from bias node at hidden layer to output node
            Backup_m_arOutputBias = new double[m_iNumOutputNodes];

            Random rand = new Random();   // create a random double from 0.0 to 1.0
            for (i = 0; i < m_iNumHiddenNodes; i++){   // initialize bias values of Hidden nodes
                m_arHiddenBias[i] = rand.NextDouble();
                Backup_m_arHiddenBias[i] = m_arHiddenBias[i];
            }
            for (i = 0; i < m_iNumOutputNodes; i++) {  // initialize bias values of Output nodes
                m_arOutputBias[i] = rand.NextDouble();
                Backup_m_arOutputBias[i] = m_arOutputBias[i];
            }
            for (i = 0; i < m_iNumHiddenNodes; i++)    // initialize weight of Input Hidden connection
            {
                for (j = 0; j < m_iNumInputNodes; j++){
                    m_arInputHiddenConn[i, j] = rand.NextDouble();
                    Backup_m_arInputHiddenConn[i, j] = m_arInputHiddenConn[i, j];
                }
            }
            for (i = 0; i < m_iNumOutputNodes; i++)   // initialize weight of Hidden Output connection
            {
                for (j = 0; j < m_iNumHiddenNodes; j++){
                    m_arHiddenOutputConn[i, j] = rand.NextDouble();
                    Backup_m_arHiddenOutputConn[i, j] = m_arHiddenOutputConn[i, j];
                }
            }

        }

        /**
         * Network import/export method
         * Parameter: pathFile
         * Return: a network object or null if file format is wrong.
         */

        static public Network Import(string pathFile)
        {
            XmlDocument input = new XmlDocument();
            Network loadedNetwork = null;
            try
            {
                input.Load(pathFile);
                XmlNode root = input.FirstChild;
                //create a empty network
                loadedNetwork = new Network();
                //Get number of input, hidden, output nodes
                int numInputNodes = Int32.Parse(root.SelectSingleNode("descendant::numInputNodes").InnerText);
                int numHiddenNodes = Int32.Parse(root.SelectSingleNode("descendant::numHiddenNodes").InnerText);
                int numOutputNodes = Int32.Parse(root.SelectSingleNode("descendant::numOutputNodes").InnerText);
                //Basic init for loadedNetwork
                loadedNetwork.BasicInitForNetwork(numInputNodes, numHiddenNodes, numOutputNodes);
                //Get Input Nodes
                for (int i = 0; i < loadedNetwork.m_iNumInputNodes; i++)
                {
                    //get a input node
                    XmlNode tempNode = root.SelectSingleNode("descendant::Input" + Convert.ToString(i + 1));
                    //get activation function type
                    string activationFunc = tempNode.SelectSingleNode("descendant::activateFunc").InnerText;
                    if (activationFunc.Equals("SIGMOID_FUNCTION"))
                    {
                        loadedNetwork.m_arInputNodes[i].m_activeFuncType = ActionvationFunction.SIGMOID_FUNCTION;
                    }
                    //get weight
                    for (int j = 0; j < loadedNetwork.m_iNumHiddenNodes; j++)
                    {
                        loadedNetwork.m_arInputHiddenConn[j, i] = Convert.ToDouble(tempNode.SelectSingleNode("descendant::InHid" + Convert.ToString(i + 1) + Convert.ToString(j + 1)).InnerText);
                    }
                }
                //Get Hidden Nodes
                for (int i = 0; i < loadedNetwork.m_iNumHiddenNodes; i++)
                {
                    //get a hidden node
                    XmlNode tempNode = root.SelectSingleNode("descendant::Hidden" + Convert.ToString(i + 1));
                    //get activation function type
                    string activationFunc = tempNode.SelectSingleNode("descendant::activateFunc").InnerText;
                    if (activationFunc.Equals("SIGMOID_FUNCTION"))
                    {
                        loadedNetwork.m_arHiddenNodes[i].m_activeFuncType = ActionvationFunction.SIGMOID_FUNCTION;
                    }
                    //get bias
                    loadedNetwork.m_arHiddenBias[i] = Convert.ToDouble(tempNode.SelectSingleNode("descendant::bias").InnerText);
                    //get weight
                    for (int j = 0; j < loadedNetwork.m_iNumOutputNodes; j++)
                    {
                        loadedNetwork.m_arHiddenOutputConn[j, i] = Convert.ToDouble(tempNode.SelectSingleNode("descendant::HidOut" + Convert.ToString(i + 1) + Convert.ToString(j + 1)).InnerText);
                    }
                }
                //Get Output Nodes
                for (int i = 0; i < loadedNetwork.m_iNumOutputNodes; i++)
                {
                    //get a output node
                    XmlNode tempNode = root.SelectSingleNode("descendant::Output" + Convert.ToString(i + 1));
                    //get activation function type
                    string activationFunc = tempNode.SelectSingleNode("descendant::activateFunc").InnerText;
                    if (activationFunc.Equals("SIGMOID_FUNCTION"))
                    {
                        loadedNetwork.m_arOutputNodes[i].m_activeFuncType = ActionvationFunction.SIGMOID_FUNCTION;
                    }
                    //get bias
                    loadedNetwork.m_arOutputBias[i] = Convert.ToDouble(tempNode.SelectSingleNode("descendant::bias").InnerText);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return loadedNetwork;
        }

        static public bool Export(Network network, string pathFile)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Network");
            doc.AppendChild(root);
            //save number of Input, Hidden, Output Nodes
            XmlElement numInput = doc.CreateElement("numInputNodes");
            numInput.InnerText = Convert.ToString(network.m_iNumInputNodes);
            XmlElement numHidden = doc.CreateElement("numHiddenNodes");
            numHidden.InnerText = Convert.ToString(network.m_iNumHiddenNodes);
            XmlElement numOutput = doc.CreateElement("numOutputNodes");
            numOutput.InnerText = Convert.ToString(network.m_iNumOutputNodes);
            root.AppendChild(numInput);
            root.AppendChild(numHidden);
            root.AppendChild(numOutput);
            //save input nodes
            XmlElement InputNodes = doc.CreateElement("InputNodes");
            for (int i = 0; i < network.m_iNumInputNodes; i++)
            {
                XmlElement aInputNode = doc.CreateElement("Input" + Convert.ToString(i + 1));
                //save activation func
                if (network.m_arInputNodes[i].m_activeFuncType == ActionvationFunction.SIGMOID_FUNCTION)
                {
                    XmlElement actFunc = doc.CreateElement("activateFunc");
                    actFunc.InnerText = "SIGMOID_FUNCTION";
                    aInputNode.AppendChild(actFunc);
                }

                //save weight for in-hid connection
                for (int j = 0; j < network.m_iNumHiddenNodes; j++)
                {
                    XmlElement aWeight = doc.CreateElement("InHid" + Convert.ToString(i + 1) + Convert.ToString(j + 1));
                    aWeight.InnerText = Convert.ToString(network.m_arInputHiddenConn[j, i]);
                    aInputNode.AppendChild(aWeight);
                }
                InputNodes.AppendChild(aInputNode);
            }
            root.AppendChild(InputNodes);

            //save hidden nodes
            XmlElement HiddenNodes = doc.CreateElement("HiddenNodes");
            for (int i = 0; i < network.m_iNumHiddenNodes; i++)
            {
                XmlElement aHiddenNode = doc.CreateElement("Hidden" + Convert.ToString(i + 1));
                //save activation func
                if (network.m_arHiddenNodes[i].m_activeFuncType == ActionvationFunction.SIGMOID_FUNCTION)
                {
                    XmlElement actFunc = doc.CreateElement("activateFunc");
                    actFunc.InnerText = "SIGMOID_FUNCTION";
                    aHiddenNode.AppendChild(actFunc);
                }

                //save bias
                XmlElement bias = doc.CreateElement("bias");
                bias.InnerText = Convert.ToString(network.m_arHiddenBias[i]);
                aHiddenNode.AppendChild(bias);

                //save weight for hid-out connection
                for (int j = 0; j < network.m_iNumOutputNodes; j++)
                {
                    XmlElement aWeight = doc.CreateElement("HidOut" + Convert.ToString(i + 1) + Convert.ToString(j + 1));
                    aWeight.InnerText = Convert.ToString(network.m_arHiddenOutputConn[j, i]);
                    aHiddenNode.AppendChild(aWeight);
                }
                HiddenNodes.AppendChild(aHiddenNode);
            }
            root.AppendChild(HiddenNodes);

            //save output nodes
            XmlElement OutputNodes = doc.CreateElement("OutputNodes");
            for (int i = 0; i < network.m_iNumOutputNodes; i++)
            {
                XmlElement aOutputNode = doc.CreateElement("Output" + Convert.ToString(i + 1));
                //save activation func
                if (network.m_arOutputNodes[i].m_activeFuncType == ActionvationFunction.SIGMOID_FUNCTION)
                {
                    XmlElement actFunc = doc.CreateElement("activateFunc");
                    actFunc.InnerText = "SIGMOID_FUNCTION";
                    aOutputNode.AppendChild(actFunc);
                }

                //save bias
                XmlElement bias = doc.CreateElement("bias");
                bias.InnerText = Convert.ToString(network.m_arOutputBias[i]);
                aOutputNode.AppendChild(bias);

                OutputNodes.AppendChild(aOutputNode);
            }
            root.AppendChild(OutputNodes);
            doc.Save(pathFile);
            return true;
        }

        /*
         * training the network by Backpropogation algorithm
         * sample is data used to train 
         * Author: DataMining-Research08
         */

        /*
         * Run the BackPropogatinon algorithm
         * Author: DataMining-Research08
         */
        public StringBuilder Bp_run(List<double> sample, double learnRate, double momentum, double theEpoches = 10000, double residual = 1.0E-5)
        {
            int i, j;
            StringBuilder a = new StringBuilder();
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
                        m_arHiddenNodes[j].CalOutput(net);
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
                        m_arOutputNodes[j].CalOutput(net);
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
                    for (j = 0; j < m_iNumHiddenNodes; j++) {
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
                    for (j = 0; j < m_iNumOutputNodes; j++ )
                    {
                        m_arOutputBias[j] += deltaOutputBias[j];
                    }

                } // end outer for
                MAE = calMAE(sample); // caculate mean square error
                if (Math.Abs(MAE - LastError) < residual && epoch>100) // if the Error is not improved significantly, halt training process and rollback
                {
                    rollback();
                    break;
                }
                else{ //else backup the current configuration and continue training
                    LastError = MAE;
                    backup();
                    MAError.Add(MAE);
                    epoch++;
                }
            }
            /* output training result */
            a.Append(epoch.ToString());
            return a;
        }
        public StringBuilder Sa_run(List<double> sample, double t1, double t2, int n_in, int n_out)
        {
            int i, j;
            StringBuilder a = new StringBuilder();
            double step = Math.Exp((Math.Log(t2/t1))/(n_out-1));
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
            a.Append("SA MAE: "+calMAE(sample));
            /* output training result */
            return a;
        }
        public List<double> to01List(List<double> sample, double max, double min)
        {
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
        public double calMAE(List<double> sample)
        {
            int i,j;
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
                    m_arHiddenNodes[j].CalOutput(net);
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
                    m_arOutputNodes[j].CalOutput(net);
                }
                /*calculate abs error*/
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    MAE += Math.Abs(sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                }
            }
            return MAE / (sample.Count - m_iNumInputNodes);
        }
        public List<double> listResult(List<double> sample, double max, double min)
        {
            int i, j;
            List<double> result = new List<double>();
            List<double> sample01 = to01List(sample, max, min);
            for (i = m_iNumInputNodes; i < sample01.Count; i++)
            {
                for (j = m_iNumInputNodes; j > 0; j--)
                {
                    m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample01.ElementAt(i - j));
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
                    m_arHiddenNodes[j].CalOutput(net);
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
                    m_arOutputNodes[j].CalOutput(net);
                }
                /*calculate abs error*/
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    double output = toValue(m_arOutputNodes[j].GetOutPut(), max, min);
                    result.Add(output);
                }
            }
            return result;
        }
        public StringBuilder calErr(List<double> sample,double max ,double min)
        {
            int i, j;
            StringBuilder result = new StringBuilder();
            double MAE = 0;
            double MAPE = 0;
            double SSE = 0;
            int count = 0;
            List<double> sample01 = to01List(sample, max , min);
            for (i = m_iNumInputNodes; i < sample01.Count; i++)
            {
                for (j = m_iNumInputNodes; j > 0; j--)
                {
                    m_arInputNodes[m_iNumInputNodes - j].SetOutput(sample01.ElementAt(i - j));
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
                    m_arHiddenNodes[j].CalOutput(net);
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
                    m_arOutputNodes[j].CalOutput(net);
                }
                /*calculate abs error*/
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    double output = m_arOutputNodes[j].GetOutPut();

                    MAE += Math.Abs(sample01.ElementAt(i) - output);
                    double yy = (Math.Abs(sample01.ElementAt(i) - output) / Math.Abs(sample01.ElementAt(i)));
                    MAPE += yy;
                    
                    //Console.WriteLine("ele:" + Math.Abs(sample01.ElementAt(i) - 0.5)); Console.WriteLine("delta:" + Math.Abs(sample01.ElementAt(i) - output)); Console.WriteLine("mape:" + yy2);

                    SSE += Math.Pow(sample01.ElementAt(i) - output, 2);
                }
            }
            result.Append((MAE / (sample01.Count - m_iNumInputNodes))+";");
            result.Append((MAPE * 100 / (sample01.Count - m_iNumInputNodes)) + "%;");
            result.Append((SSE) + ";");
            result.Append((SSE / (sample01.Count - m_iNumInputNodes)) + ";");
            return result;
        }
        
        public double toValue(double value01, double max, double min){
            
            double value = ((value01-0.02)*(max-min)/(0.99-0.02))+min;
            return value;
        }
        public StringBuilder SA_new_Run(List<double> sample, bool isReheat)
        {
            
            List<double>  sampleNormal = sample;
            sample = to01List(sample, sample.Max(), sample.Min());
            Bp_run(sample, 0.3, 0.1, 10000, 1.0E-6);
            StringBuilder result = new StringBuilder();
            int i, j;
            double T, TAlpha;
            int L, freeze;
            freeze = 0;
            int reheat = 5;
            double bestT;
            double MAE = calMAE(sample);
            Console.WriteLine(MAE);
            float WAlpha = 0.01f;
            int phase = 200;
            int maxPhase = phase;
            TAlpha = 0.99;//[0.8~0.99]
            float[,] deltaWeight = new float[m_iNumHiddenNodes, m_iNumInputNodes];
            float[] deltaBias = new float[m_iNumHiddenNodes];
            float[,] deltaWeightY = new float[m_iNumOutputNodes, m_iNumHiddenNodes];
            float[] deltaBiasY = new float[m_iNumOutputNodes];
            //to random new solution
            Random rand = new Random();

            //test variable
            DenseMatrix X = new DenseMatrix(sample.Count - m_iNumInputNodes, m_iNumHiddenNodes + 1);
            float[] XRow = new float[m_iNumHiddenNodes + 1];
            float[] Y = new float[sample.Count - m_iNumInputNodes];

            double Error = 999;//first error
            double LastError = 10000000;
            int index = 0;
            T = 1.5/1000;//temperature
            bestT = T;
            L = 10;//loops
            double maxT = T;
            double rate = 0;
                do//Main SA
                {
                    int success = 0;
                    int up = 0;
                    float rateWAlpha = WAlpha;
                    for (int l = 0; l < L; l++)//loop in L
                    {
                        //generate new  random solution
                        //input to hiden
                        for (j = 0; j < m_iNumHiddenNodes; j++)
                        {

                            for (int k = 0; k < m_iNumInputNodes; k++)
                            {
                                if (rand.Next(2) == 1)
                                    deltaWeight[j, k] = rateWAlpha;
                                else
                                    deltaWeight[j, k] = -rateWAlpha;
                            }
                            if (rand.Next(2) == 1)
                                deltaBias[j] = rateWAlpha;
                            else
                                deltaBias[j] = -rateWAlpha;

                        }
                        //hiden to output
                        for (j = 0; j < m_iNumOutputNodes; j++)
                        {
                            for (int k = 0; k < m_iNumHiddenNodes; k++)
                            {
                                if (rand.Next(2) == 1)
                                    deltaWeightY[j, k] = rateWAlpha;
                                else
                                    deltaWeightY[j, k] = -rateWAlpha;
                            }
                            if (rand.Next(2) == 1)
                                deltaBiasY[j] = rateWAlpha;
                            else
                                deltaBiasY[j] = -rateWAlpha;
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
                                m_arHiddenNodes[j].CalOutput(net);
                            }
                            /*calculate output of output nodes*/
                            for (j = 0; j < m_iNumOutputNodes; j++)
                            {
                                double net = 0.0;
                                for (int k = 0; k < m_iNumHiddenNodes; k++)
                                {
                                    net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k] * (1 + deltaWeightY[j, k]);
                                }
                                net += m_arOutputBias[j] * (1 + deltaBiasY[j]);
                                m_arOutputNodes[j].CalOutput(net);
                            }
                            /*calculate abs error*/
                            for (j = 0; j < m_iNumOutputNodes; j++)
                            {
                                LastError += (sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut())*(sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                            }
                        }

                        LastError = Math.Sqrt(LastError / (sample.Count - m_iNumOutputNodes)); // calculate mean square errors
                        //Console.WriteLine((Error - LastError) * 1000);
                        if (LastError < Error)
                        {
                            //Console.WriteLine("(S)phase : " + phase + "  --  Rate : " + rate * 100 + "  --  Err : " + (Error - LastError) + "  --  T : " + T + "  --  MAE : " + (Error));
                            //Console.WriteLine(LastError);
                            Error = LastError;
                            success++;
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
                        else
                        {
                            if((Error - LastError) * 1000>-1){
                                l--;
                            }
                            else
                            {
                                double ran = rand.NextDouble();
                                rate = Math.Exp((Error - LastError) / T);
                                //Console.WriteLine("phase : " + phase + "  --  Rate : " + rate * 100 + "  --  Err : " + (Error - LastError) + "  --  T : " + T + "  --  MAE : " + (Error));
                                if (ran < rate)
                                {
                                    success++;
                                    up++;
                                    //Console.WriteLine("(F)phase : " + phase + "  --  Rate : " + rate * 100 + "  --  Err : " + (Error - LastError) + "  --  T : " + T + "  --  MAE : " + (Error));
                                    Error = LastError;
                                    
                                    //Console.WriteLine("up");
                                    //Console.WriteLine(LastError);
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
                                else
                                {
                                    //Console.WriteLine("(Q)phase : " + phase + "  --  Rate : " + rate * 100 + "  --  Err : " + (Error - LastError) + "  --  T : " + T + "  --  MAE : " + (Error));
                                    freeze += 1;
                                    if (freeze > maxPhase * L / 5 && reheat > 0 && isReheat)
                                    {
                                        //Console.WriteLine("reheatttttttttttttyyyyyyyyyyyyyyyyyyyyyytttttttttttttttttttttttttttttttttttt");
                                        T = 2 * bestT;
                                        freeze = 0;
                                        reheat--;
                                        phase += 10 + maxPhase / 5;
                                    }
                                }
                            }
                            
                        }
                        //Console.WriteLine("phase : " + phase + " - lan : " + l + " - T : " + T);
                        index++;
                    }//loop in L
                    //if(Error<0.001) break;
                    //Update T
                    //Console.WriteLine("phase : " + phase + "  --  Rate : " + success * 100 / (L) + " -- up: " + up + " -- MAE: " + Error + " -- at  T: " + T +" -- reheatleft: "+reheat);
                    T = T * TAlpha;
                    if (success * 100 / (L) > 60 && up > 3) bestT = T;
                    phase--;
                } while (phase > 0);//while loop

            return result;
        }
        public StringBuilder SA_new_Run2(List<double> sample, bool isReheat)
        {

            List<double> sampleNormal = sample;
            sample = to01List(sample, sample.Max(), sample.Min());
            Bp_run(sample, 0.3, 0.05);
            StringBuilder result = new StringBuilder();
            int i, j;
            double T, TAlpha;
            int L, freeze;
            freeze = 0;
            int reheat = 5;
            double bestT;
            double MAE = calMAE(sample);
            float WAlpha = 0.01f;
            int phase = 400;
            int maxPhase = phase;
            TAlpha = 0.97;//[0.8~0.99]
            float[,] deltaWeight = new float[m_iNumHiddenNodes, m_iNumInputNodes];
            float[] deltaBias = new float[m_iNumHiddenNodes];
            float[,] deltaWeightY = new float[m_iNumOutputNodes, m_iNumHiddenNodes];
            float[] deltaBiasY = new float[m_iNumOutputNodes];
            //to random new solution
            Random rand = new Random();

            //test variable
            DenseMatrix X = new DenseMatrix(sample.Count - m_iNumInputNodes, m_iNumHiddenNodes + 1);
            float[] XRow = new float[m_iNumHiddenNodes + 1];
            float[] Y = new float[sample.Count - m_iNumInputNodes];

            double Error = MAE;//first error
            double LastError = 10000000;
            int index = 0;
            T = 500;//temperature
            bestT = T;
            L = 15;//loops
            double maxT = T;
            double rate = 0;
            do//Main SA
            {
                int success = 0;
                int up = 0;
                float rateWAlpha = WAlpha;
                for (int l = 0; l < L; l++)//loop in L
                {
                    //generate new  random solution
                    //input to hiden
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {

                        for (int k = 0; k < m_iNumInputNodes; k++)
                        {
                            if (rand.Next(2) == 1)
                                deltaWeight[j, k] = rateWAlpha;
                            else
                                deltaWeight[j, k] = -rateWAlpha;
                        }
                        if (rand.Next(2) == 1)
                            deltaBias[j] = rateWAlpha;
                        else
                            deltaBias[j] = -rateWAlpha;

                    }
                    //hiden to output
                    for (j = 0; j < m_iNumOutputNodes; j++)
                    {
                        for (int k = 0; k < m_iNumHiddenNodes; k++)
                        {
                            if (rand.Next(2) == 1)
                                deltaWeightY[j, k] = rateWAlpha;
                            else
                                deltaWeightY[j, k] = -rateWAlpha;
                        }
                        if (rand.Next(2) == 1)
                            deltaBiasY[j] = rateWAlpha;
                        else
                            deltaBiasY[j] = -rateWAlpha;
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
                            m_arHiddenNodes[j].CalOutput(net);
                        }
                        /*calculate output of output nodes*/
                        for (j = 0; j < m_iNumOutputNodes; j++)
                        {
                            double net = 0.0;
                            for (int k = 0; k < m_iNumHiddenNodes; k++)
                            {
                                net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k] * (1 + deltaWeightY[j, k]);
                            }
                            net += m_arOutputBias[j] * (1 + deltaBiasY[j]);
                            m_arOutputNodes[j].CalOutput(net);
                        }
                        /*calculate abs error*/
                        for (j = 0; j < m_iNumOutputNodes; j++)
                        {
                            LastError += Math.Abs(sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
                        }
                    }

                    LastError = LastError / (sample.Count - m_iNumOutputNodes); // calculate mean square errors

                    if (LastError < Error)
                    {
                        //Console.WriteLine(LastError);
                        Error = LastError;
                        success++;
                        rate = 0;
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
                    else
                    {
                        double ran = rand.NextDouble();
                        rate += 0.3 * T / maxT;

                        //Console.WriteLine("ran  "+ran+"   rate  "+rate);
                        if (ran < rate)
                        {
                            success++;
                            up++;
                            Error = LastError;
                            //Console.WriteLine("up");
                            //Console.WriteLine(LastError);
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
                        else
                        {
                            freeze += 1;
                            if (freeze > maxPhase * L / 5 && reheat > 0 && isReheat)
                            {
                                //Console.WriteLine("reheatttttttttttttyyyyyyyyyyyyyyyyyyyyyytttttttttttttttttttttttttttttttttttt");
                                T = 2 * bestT;
                                freeze = 0;
                                reheat--;
                                phase += 10 + maxPhase / 5;
                            }
                        }
                    }
                    //Console.WriteLine("phase : " + phase + " - lan : " + l + " - T : " + T);
                    index++;
                }//loop in L
                //if(Error<0.001) break;
                //Update T
                //Console.WriteLine("phase : " + phase + "  --  Rate : " + success * 100 / (L) + " -- up: " + up + " -- MAE: " + Error + " -- at  T: " + T +" -- reheatleft: "+reheat);
                T = T * TAlpha;
                if (success * 100 / (L) > 60 && up > 3) bestT = T;
                phase--;
            } while (phase > 0);//while loop

            return result;
        }
        /*
          * training the network by Resilient Backpropogation algorithm
          * sample is data used to train 
          * Author: DataMining-Research08
          */


        /*
        * training the network by RPROP algorithm
        * sample is data used to train 
        * Author: DataMining-Research08
        */
        public void Rprop_Train(List<double> sample, double min = 0.00001, double max = 50.0, double theEpoches = 10000,double residual = 1.0E-5)
        {
            int i, j;
            int epoch = 0;
            double MAE = Double.MaxValue;
            double defaultWeightChange = 0.0;
         //   double defaultDeltaValue = 0.1;
            double defaultDeltaValue = min; // min is the default delta value, naming is not good because of old versions
            double defaultGradientValue = 0.0;
            double maxDelta = max;
           // double minDelta = min;
           double minDelta = 1.0E-6;
            double maxStep = 1.2;
            double minStep = 0.5;
            double LastError = Double.MaxValue;
            double counter = 0;
            List<double> MAError = new List<double>();

            double[,] weightChangeInputHidden = new double[m_iNumHiddenNodes, m_iNumInputNodes];
            double[,] deltaInputHidden = new double[m_iNumHiddenNodes, m_iNumInputNodes];
            double[,] gradientInputHidden = new double[m_iNumHiddenNodes, m_iNumInputNodes];
            double[,] newGradientInputHidden = new double[m_iNumHiddenNodes, m_iNumInputNodes];

            double[,] weightChangeHiddenOutput = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
            double[,] deltaHiddenOutput = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
            double[,] gradientHiddenOutput = new double[m_iNumOutputNodes, m_iNumHiddenNodes];
            double[,] newGradientHiddenOutput = new double[m_iNumOutputNodes, m_iNumHiddenNodes];

            double[] weightChangeHiddenBias = new double[m_iNumHiddenNodes];
            double[] deltaHiddenBias = new double[m_iNumHiddenNodes];
            double[] gradientHiddenBias = new double[m_iNumHiddenNodes];
            double[] newGradientHiddenBias = new double[m_iNumHiddenNodes];

            double[] weightChangeOutputBias = new double[m_iNumOutputNodes];
            double[] deltaOutputBias = new double[m_iNumOutputNodes];
            double[] gradientOutputBias = new double[m_iNumOutputNodes];
            double[] newGradientOutputBias = new double[m_iNumOutputNodes];

            // initialize Input Hidden connection
            for (i = 0; i < m_iNumHiddenNodes; i++)
            {
                for (j = 0; j < m_iNumInputNodes; j++)
                {
                    weightChangeInputHidden[i, j] = defaultWeightChange;
                    deltaInputHidden[i, j] = defaultDeltaValue;
                    gradientInputHidden[i, j] = defaultGradientValue;
                    newGradientInputHidden[i, j] = defaultGradientValue;
                }
                weightChangeHiddenBias[i] = defaultWeightChange;
                deltaHiddenBias[i] = defaultDeltaValue;
                gradientHiddenBias[i] = defaultGradientValue;
                newGradientHiddenBias[i] = defaultGradientValue;
            }

            // initialize Hidden Output connection
            for (i = 0; i < m_iNumOutputNodes; i++)
            {
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    weightChangeHiddenOutput[i, j] = defaultWeightChange;
                    deltaHiddenOutput[i, j] = defaultDeltaValue;
                    gradientHiddenOutput[i, j] = defaultGradientValue;
                    newGradientHiddenOutput[i, j] = defaultGradientValue;
                }
                weightChangeOutputBias[i] = defaultWeightChange;
                deltaOutputBias[i] = defaultDeltaValue;
                gradientOutputBias[i] = defaultGradientValue;
                newGradientOutputBias[i] = defaultGradientValue;
            }

            while (epoch < theEpoches)
            {
                MAE = 0.0;
                for (i = 0; i < m_iNumHiddenNodes; i++) // reinnitialize value of gradien for new epoch
                {
                    for (j = 0; j < m_iNumInputNodes; j++)
                    {
                        newGradientInputHidden[i, j] = 0.0;
                    }
                    newGradientHiddenBias[i] = 0.0;
                }
                for (i = 0; i < m_iNumOutputNodes; i++)
                {
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        newGradientHiddenOutput[i, j] = 0.0;
                    }
                    newGradientOutputBias[i] = 0.0;
                }
                //training for each epoch
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
                        m_arHiddenNodes[j].CalOutput(net);
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
                        m_arOutputNodes[j].CalOutput(net);
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
                            newGradientHiddenOutput[j,k] += -m_arOutputNodes[j].GetOutPut() * (1 - m_arOutputNodes[j].GetOutPut()) * m_arHiddenNodes[k].GetOutPut() * (sample[i] - m_arOutputNodes[j].GetOutPut());
                        }
                        newGradientOutputBias[j] += -m_arOutputNodes[j].GetOutPut() * (1 - m_arOutputNodes[j].GetOutPut()) * (sample.ElementAt(i) - m_arOutputNodes[j].GetOutPut());
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
                            newGradientInputHidden[j,k] += m_arHiddenNodes[j].GetOutPut() * (1 - m_arHiddenNodes[j].GetOutPut()) * m_arInputNodes[k].GetOutPut() * temp;
                        }
                        newGradientHiddenBias[j] += m_arHiddenNodes[j].GetOutPut() * (1 - m_arHiddenNodes[j].GetOutPut()) * temp;                       
                    }

                } // end outer for

                int sign;
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    for (int k = 0; k < m_iNumHiddenNodes; k++)
                    {
                        sign = Math.Sign(newGradientHiddenOutput[j,k] * gradientHiddenOutput[j, k]);
                        if (sign > 0)
                        {
                            deltaHiddenOutput[j, k] = Math.Min(deltaHiddenOutput[j, k] * maxStep, maxDelta);
                            weightChangeHiddenOutput[j, k] = -Math.Sign(newGradientHiddenOutput[j, k]) * deltaHiddenOutput[j, k];
                            m_arHiddenOutputConn[j, k] += weightChangeHiddenOutput[j, k];
                            gradientHiddenOutput[j, k] = newGradientHiddenOutput[j, k];                           
                        }
                        else if (sign < 0)
                        {
                            deltaHiddenOutput[j, k] = Math.Max(deltaHiddenOutput[j, k] * minStep, minDelta);
                            m_arHiddenOutputConn[j, k] -= weightChangeHiddenOutput[j, k]; //restore old value
                            newGradientHiddenOutput[j, k] = 0.0;
                            gradientHiddenOutput[j, k] = newGradientHiddenOutput[j, k];
                        }
                        else
                        {
                            weightChangeHiddenOutput[j, k] = -Math.Sign(newGradientHiddenOutput[j, k]) * deltaHiddenOutput[j, k];
                            m_arHiddenOutputConn[j, k] += weightChangeHiddenOutput[j, k];
                            gradientHiddenOutput[j, k] = newGradientHiddenOutput[j, k];
                        }
                        newGradientHiddenOutput[j, k] = 0.0;
                    }

                    sign = Math.Sign(newGradientOutputBias[j] * gradientOutputBias[j]);
                    if (sign > 0)
                    {
                        deltaOutputBias[j] = Math.Min(deltaOutputBias[j] * maxStep, maxDelta);
                        weightChangeOutputBias[j] = -Math.Sign(newGradientOutputBias[j]) * deltaOutputBias[j];
                        m_arOutputBias[j] += weightChangeOutputBias[j];
                        gradientOutputBias[j] = newGradientOutputBias[j];
                    }
                    else if (sign < 0)
                    {
                        deltaOutputBias[j] = Math.Max(deltaOutputBias[j] * minStep, minDelta);
                        m_arOutputBias[j] -= weightChangeOutputBias[j];
                        newGradientOutputBias[j] = 0.0;
                        gradientOutputBias[j] = newGradientOutputBias[j];
                    }
                    else
                    {
                        weightChangeOutputBias[j] = -Math.Sign(newGradientOutputBias[j]) * deltaOutputBias[j];
                        m_arOutputBias[j] += weightChangeOutputBias[j];
                        gradientOutputBias[j] = newGradientOutputBias[j];
                    }
                    newGradientOutputBias[j] = 0.0;
                }
                /*calculate weight-step for weights connecting from input nodes to hidden nodes*/
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    for (int k = 0; k < m_iNumInputNodes; k++)
                    {
                        sign = Math.Sign(newGradientInputHidden[j,k] * gradientInputHidden[j, k]);
                        if (sign > 0)
                        {
                            deltaInputHidden[j, k] = Math.Min(deltaInputHidden[j, k] * maxStep, maxDelta);
                            weightChangeInputHidden[j, k] = -Math.Sign(newGradientInputHidden[j, k]) * deltaInputHidden[j, k];
                            m_arInputHiddenConn[j, k] += weightChangeInputHidden[j, k];
                            gradientInputHidden[j, k] = newGradientInputHidden[j, k];
                        }
                        else if (sign < 0)
                        {
                            deltaInputHidden[j, k] = Math.Max(deltaInputHidden[j, k] * minStep, minDelta);
                            m_arInputHiddenConn[j, k] -= weightChangeInputHidden[j, k];
                            newGradientInputHidden[j, k] = 0.0;
                            gradientInputHidden[j, k] = 0.0;
                        }
                        else
                        {
                            weightChangeInputHidden[j, k] = -Math.Sign(newGradientInputHidden[j, k]) * deltaInputHidden[j, k];
                            m_arInputHiddenConn[j, k] += weightChangeInputHidden[j, k];
                            gradientInputHidden[j, k] = newGradientInputHidden[j, k];
                        }
                        newGradientInputHidden[j, k] = 0.0;
                    }

                    sign = Math.Sign(newGradientHiddenBias[j] * gradientHiddenBias[j]);
                    if (sign > 0)
                    {
                        deltaHiddenBias[j] = Math.Min(deltaHiddenBias[j] * maxStep, maxDelta);
                        weightChangeHiddenBias[j] = -Math.Sign(newGradientHiddenBias[j]) * deltaHiddenBias[j];
                        m_arHiddenBias[j] += weightChangeHiddenBias[j];
                        gradientHiddenBias[j] = newGradientHiddenBias[j];
                    }
                    else if (sign < 0)
                    {
                        deltaHiddenBias[j] = Math.Max(deltaHiddenBias[j] * minStep, minDelta);
                        m_arHiddenBias[j] -= weightChangeHiddenBias[j];
                        newGradientHiddenBias[j] = 0.0;
                        gradientHiddenBias[j] = 0;
                    }
                    else
                    {
                        weightChangeHiddenBias[j] = -Math.Sign(newGradientHiddenBias[j]) * deltaHiddenBias[j];
                        m_arHiddenBias[j] += weightChangeHiddenBias[j];
                        gradientHiddenBias[j] = newGradientHiddenBias[j];
                    }
                    newGradientHiddenBias[j] = 0.0;
                }
                MAE = MAE / (sample.Count); // caculate mean square error
                if (Math.Abs(MAE - LastError) < residual) // if the Error is not improved significantly, halt training process and rollback
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
            /* output training result */

        }

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

        }
        /*
         * Procedure: Test the network
         * Parameter: a list of double value
         * post: show a form to inform user about MAE, SSE,MSE
         * Author : DataMining-Research08
         */
        public void test(List<double> sample)
        {
            double[] result = new double[sample.Count];
            int i = 0;
            int j = 0;
            double MAE = 0;
            double SSE = 0;
            double MSE = 0;
            for (i = 0; i < m_iNumInputNodes; i++)
            {
                result[i] = sample[i];
            }
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
                    m_arHiddenNodes[j].CalOutput(net);
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
                    m_arOutputNodes[j].CalOutput(net);
                    /*convert output int range [0.01-0.99] to real value*/
                    double a = m_arOutputNodes[j].GetOutPut();
                    result[i] = (a - 0.01) / (0.99 - 0.01) * (max - min) + min;
                }
            }
            for (i = 0; i < sample.Count; i++)
            {
                MAE += Math.Abs(result[i] - sample[i]);
                SSE += Math.Pow(result[i] - sample[i], 2);
            }
            MAE = MAE / sample.Count;
            MSE = SSE / sample.Count;
            /* output testing result */

        }
        /*
         * Procedure: Forecast
         * Param: a list of sample data, the number of ahead to forecast
         * Author: DataMining-Reseach08
         */
        public void forecast(List<double> sample, int nahead = 1)
        {
            int i = 0;
            int j = 0;
            double[] result = new double[nahead];
            double max = 0;
            double min = 0;
            for (i = 0; i < nahead; i++)
            {
                int count = sample.Count;
                min = sample.Min();
                max = sample.Max();
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
                    m_arHiddenNodes[j].CalOutput(net);
                }
                for (j = 0; j < m_iNumOutputNodes; j++)
                {
                    double net = 0.0;
                    for (int k = 0; k < m_iNumHiddenNodes; k++)
                    {
                        net += m_arHiddenNodes[k].GetOutPut() * m_arHiddenOutputConn[j, k];
                    }
                    net += m_arOutputBias[j];
                    m_arOutputNodes[j].CalOutput(net);
                    /*convert output int range [0.01-0.99] to real value*/
                    double a = m_arOutputNodes[j].GetOutPut();
                    result[i] = (a - 0.01) / (0.99 - 0.01) * (max - min) + min;
                    sample.Add(result[i]);
                }

            }
            //drawResult(sample, result);
        }
        /*
         * Procedure: Draw the forecast Result
         * Param: the data sample, the forcast result
         * Author: DataMining-Research08
         */

        /*
         * rollback weights of neural to last value
         */
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
    }
}
