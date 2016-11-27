using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int n;
            String s= System.Console.ReadLine();          
            n = Convert.ToInt32(s);
            System.Console.WriteLine(n*2);
            int [,]matrix= new int[n,n+1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n+1; j++)
                {
                    String temp = System.Console.ReadLine();
                    matrix[i,j] = Convert.ToInt32(temp);
                }
            }

            printMatrix(matrix);
            int[,] m = transMatrix(matrix);
            printMatrix(m);
            System.Console.WriteLine("Number of column: " + matrix.GetLength(1));
            System.Console.WriteLine("Number of row: " + matrix.GetLength(0));
        }

        static int[,] transMatrix(int[,] m)
        {
            int[,] matrix = new int[m.GetLength(1), m.GetLength(0)];
            for (int i = 0; i < m.GetLength(0) ; i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    matrix[j, i] = m[i, j];
                }               
            }
            return matrix;
        }

        static int[,] mulMatrix(int[,] m, int[,] n) {
            //todo here
            return m;
        }

        static int[,] invMatrix(int[,] m)
        {
            //todo here
            return m;
        }

        static void printMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    System.Console.Write(matrix[i, j] + "  ");
                }
                System.Console.WriteLine();
            }
        }
    }
}
