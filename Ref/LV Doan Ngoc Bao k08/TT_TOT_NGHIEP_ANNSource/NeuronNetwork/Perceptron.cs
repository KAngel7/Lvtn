using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronNetwork
{
    public enum ActionvationFunction
    {
        SIGMOID_FUNCTION = 0x01,
    }

    public enum PerceptionType
    {
        PERCEPTION_INPUT,
        PERCEPTION_HIDDEN,
        PERCEPTION_OUTPUT,
    }

    public class Perceptron
    {
        private float m_fInput;
        protected double output = 0.0; 
        public ActionvationFunction m_activeFuncType;

        /**
         * Constructor
         * init for type, bias, activation function
         */
        public Perceptron(ActionvationFunction activeType)
        {
            m_activeFuncType = activeType;
        }

        /**
         * Function: SetInput
         */
        public void SetInput(float input)
        {
            m_fInput = input;
        }

        /**
         * Function: GetInput
         */
        public float GetInput()
        {
            return m_fInput;
        }

        /**
         * Function: GetOutput
         * Return: the output of the perceptron
         */
        public double GetOutPut()
        {
        //    float result = 0;
         //   if (m_activeFuncType == ActionvationFunction.SIGMOID_FUNCTION)
          //  { 
                
          //  }
            return output;
        }

        /*
         * Function: Calculate output of the Perceptron
         * Parameter: net input
         * Author: DataMining-Research08
         */
        public double CalOutput(double netinput)
        {
            if (m_activeFuncType == ActionvationFunction.SIGMOID_FUNCTION)
            {
                output = 1 / (1 + Math.Exp(-netinput));
            }
            return output;
        }
        public double SetOutput(double value)
        {
            this.output = value;
            return output;
        }
    }
}
