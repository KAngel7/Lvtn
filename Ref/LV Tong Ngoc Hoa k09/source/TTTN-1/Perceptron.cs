using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTTN_1
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
        private double m_fInput;
        protected double m_output = 0.0;
        public ActionvationFunction m_activeFuncType;


        //Constructor
        public Perceptron(ActionvationFunction activeType)
        {
            m_activeFuncType = activeType;
        }

        //Set input
        public void SetInput(double input)
        {
            m_fInput = input;

            //not use acivation funtion
            m_output = input;
        }

        //get input
        public double GetInput()
        {
            return m_fInput;
        }

        //get output
        public double GetOutPut()
        {
            return m_output;
        }

        //calculate the output of this perceptron
        public double CalOutput()
        {
            if (m_activeFuncType == ActionvationFunction.SIGMOID_FUNCTION)
            {
                m_output = 1 / (1 + Math.Exp(-m_fInput));
            }
            return m_output;
        }


        public double SetOutput(double value)
        {
            this.m_output = value;
            return m_output;
        }
    }

}
