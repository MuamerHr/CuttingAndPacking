/* Copyright: Muamer Hrncic */
/* Original author(s): Muamer Hrncic */
/* License: MIT */
/* Purpose: Heuristic procedures and cutting pattern generation for the one dimensional cutting stock problem.*/
/* This code comes with no warranty. */

using System.Collections.Generic;
using System.IO;

namespace CuttingStock
{
    public static class CuttingPattern
    {
        public static int[,] CuttingPatternEnumeration(int[] lengths, int maxLength)
        {
            // Create the matrix of cutting patterns with respect to the desired lengths
            // and the maximum length of the raw material.

            int i = 0;
            int k;
            int n = lengths.Length;
            int lr = maxLength;
            int[] q = new int[n];
            List<int[]> patList = new List<int[]>();

            for (int j = 0; j < n; j++)
            {
                q[j] = lr / lengths[j];
            }

            while (q[n - 1] > 0)
            {
                int[] a = new int[n];
                a[i] = q[i];
                lr -= a[i] * lengths[i];
                k = i + 1;
                for (int j = k; j < n; j++)
                {
                    a[j] = lr / lengths[j];
                    lr -= a[j] * lengths[j];
                }
                int[] b = new int[n];
                a.CopyTo(b, 0);
                patList.Add(b);
                while (k < n)
                {
                    if (a[k] > 0)
                    {
                        a[k]--;
                        lr = maxLength;
                        for (int index = 0; index <= k; index++)
                        {
                            lr -= a[index] * lengths[index];
                        }
                        int j = k + 1;
                        while (j < n)
                        {
                            a[j] = lr / lengths[j];
                            lr -= a[j] * lengths[j];
                            j++;
                        }
                        int[] c = new int[n];
                        a.CopyTo(c, 0);
                        patList.Add(c);

                    }
                    int h = 0;

                    for (int i1 = i + 1; i1 < n; i1++)
                    {
                        h += a[i1];
                    }
                    if (h == 0)
                    {
                        k++;
                    }
                    else
                    {
                        if (k == (n - 1) && h != 0)
                        {
                            k = i + 1;
                        }
                        else
                        {
                            k++;
                        }
                    }
                }

                lr = maxLength;
                q[i]--;
                if (q[i] == 0)
                {
                    i++;
                }
            }

            int[,] pattern = new int[n, patList.Count];

            for (int i1 = 0; i1 < n; i1++)
            {
                for (int j1 = 0; j1 < patList.Count; j1++)
                {
                    pattern[i1, j1] = patList[j1][i1];
                }
            }
            return pattern;
        }
        public static void WriteAMPLdat(int[] lengths, int[] amounts, int maxLength, string path)
        {
            // Write the matrix of cutting patterns and the amounts into an AMPL readable DATA file.
            // This file together with a MODEL file and a COMMAND file can be used as input for a solver
            // which implements the AMPL interface e.g. CPLEX, SCIP, BARON, GUROBI, and others.
        
            int[,] cp = CuttingPatternEnumeration(lengths, maxLength);
            int m = cp.GetLength(0);
            int n = cp.GetLength(1);

            StreamWriter sw = new StreamWriter(path);
            sw.Write("set lengths :=");
            for (int i = 0; i < m; i++)
            {
                sw.Write("\t" + "l" + (i + 1).ToString());
            }
            sw.Write(";");

            sw.WriteLine("");
            sw.Write("set col :=");
            for (int i = 0; i < n; i++)
            {
                sw.Write("\t" + "j" + (i + 1).ToString());
            }
            sw.Write(";");

            sw.WriteLine("");
            sw.Write("# lengths:");
            for (int i = 0; i < lengths.Length; i++)
            {
                sw.Write("\t" + lengths[i].ToString());
            }
            sw.WriteLine("");
            sw.WriteLine("");
            sw.Write("param cutScheme :");
            for (int i = 0; i < m; i++)
            {
                sw.Write("\t" + "l" + (i + 1).ToString());

            }
            sw.Write(":=");
            sw.WriteLine("");
            for (int i = 0; i < n; i++)
            {
                sw.Write("\t" + "j" + (i + 1).ToString());
                for (int j = 0; j < m; j++)
                {
                    sw.Write("\t" + cp[j, i].ToString());
                }
                sw.WriteLine("");
            }
            sw.Write(";");

            sw.WriteLine("");
            sw.Write("param amounts :=");
            sw.WriteLine("");

            for (int i = 0; i < amounts.Length; i++)
            {
                sw.WriteLine("l" + (i + 1).ToString() + "\t" + amounts[i].ToString());
            }
            sw.Write(";");
            sw.Close();
        }
    }
}
