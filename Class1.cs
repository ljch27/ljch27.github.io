using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AN_2DO_PARCIAL
{
    public static class GaussSeidelSolver
    {
        public static double[] Solve(double[,] coefficients, double[] constants, double[] initialGuess, double tolerance = 1e-9, int maxIterations = 1000)
        {
            int n = constants.Length;
            double[] x = new double[n];
            double[] previous = new double[n];

            Array.Copy(initialGuess, x, n);

            int iterations = 0;
            double error = double.MaxValue;

            while (iterations < maxIterations && error > tolerance)
            {
                Array.Copy(x, previous, n);

                for (int i = 0; i < n; i++)
                {
                    double sum = constants[i];
                    for (int j = 0; j < n; j++)
                    {
                        if (j != i)
                        {
                            sum -= coefficients[i, j] * x[j];
                        }
                    }
                    x[i] = sum / coefficients[i, i];
                }

                error = CalculateError(x, previous);
                iterations++;
            }

            if (iterations == maxIterations)
            {
                throw new InvalidOperationException("El método de Gauss-Seidel no convergió dentro del número máximo de iteraciones especificado.");
            }

            return x;
        }

        private static double CalculateError(double[] current, double[] previous)
        {
            double maxError = 0;
            for (int i = 0; i < current.Length; i++)
            {
                double error = Math.Abs(current[i] - previous[i]);
                if (error > maxError)
                {
                    maxError = error;
                }
            }
            return maxError;
        }
    }
}