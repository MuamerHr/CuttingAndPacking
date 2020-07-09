/* Copyright: Muamer Hrncic */
/* Original author(s): Muamer Hrncic */
/* License: MIT */
/* Purpose: Heuristic procedures and cutting pattern generation for the one dimensional cutting stock problem.*/
/* This code comes with no warranty. */

using System;
using CuttingStock;

namespace CuttingStockTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // The main program contains examples of how to use the heuristics to
            // find a feasible solution to the one dimensional cutting stock problem.
            // After that a AMPL-DATA file "cuttingPattern.dat" is created which contains the matrix of 
            // cutting pattern to a given set of lengths. This data file together 
            // with the AMPL-MODEL file "cuttingStockMod.mod" and the AMPL-COMMANDS file
            // "commands.run" can be used as an input to a solver which implements an 
            // AMPL interface. A list of solvers which implement the AMPL interface
            // and which can be used for testing this model and others can be found on
            // https://neos-server.org/neos/solvers/index.html .

            int[] lengths = new int[10] { 6000, 5500, 5200, 4800, 4400, 3500, 2500, 2400, 1700, 1150 };
            int[] amounts = new int[10] { 200, 150, 220, 100, 100, 264, 80, 156, 50, 150 };
            int maxLength = 12000;
            int waste = 0;

            #region Different Input Data

            //int[] lengths = new int[5] { 9, 8, 7, 6, 5};
            //int[] amounts = new int[5] { 1, 1, 2, 2, 2 };
            //int max_length = 14;

            //int[] lengths = new int[10] { 6000,5500,5200,4800,4400,3500,2500,2400,1700,1150 };
            //int[] amounts = new int[10] { 200,150,220,100,100,264,80,156,50,150 };
            //int maxLength = 12000;

            //int[] lengths = new int[12] { 4200, 3900, 3650, 3200, 2980, 2700, 2200, 2050, 1890, 1560, 1200, 1100 };
            //int[] amounts = new int[12] { 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120 };
            //int maxLength = 13800;

            //int[] lengths = new int[20] { 9000,7800,7600,4200, 3900, 3650, 3200, 2980,2800, 2700, 2200, 2050, 1890,1750, 1560,1480,1450,1300, 1200, 1100 };
            //int[] amounts = new int[20] { 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300 };
            //int maxLength = 15200;

            //int[] lengths = new int[20] { 9000, 7800, 7600, 4200, 3900, 3650, 3200, 2980, 2800, 2700, 2200, 2050, 1890, 1750, 1560, 1480, 1450, 1300, 1200, 1100 };
            //int[] amounts = new int[20] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, };
            //int maxLength = 17000;

            //int[] lengths = new int[20] { 9000, 7800, 7600, 4200, 3900, 3650, 3200, 2980, 2800, 2700, 2200, 2050, 1890, 1750, 1560, 1480, 1450, 1300, 1200, 1100 };
            //int[] amounts = new int[20] { 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500 };
            //int maxLength = 17000;

            #endregion

            //Execute First Fit Heurisitic
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            CuttingPlan cpFF = Heuristics.FirstFitDecreasing(lengths, amounts, waste, maxLength);
            stopwatch.Stop();
            var elapsedMsFF = stopwatch.ElapsedMilliseconds;

            //Execute Best Fit Heurisitic
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
            CuttingPlan cpBF = Heuristics.BestFitDecreasing(lengths, amounts, waste, maxLength);
            stopwatch.Stop();
            long elapsedMsBF = stopwatch.ElapsedMilliseconds;

            Console.WriteLine("###############First-Fit-Heuristic#################");
            Console.WriteLine("Used Bars: {0}", (object)cpFF.UsedBars);
            Console.WriteLine("Elapsed time: {0} ms", elapsedMsFF);
            Console.WriteLine("################################");

            Console.WriteLine("###############Best-Fit-Heuristic#################");
            Console.WriteLine("Used Bars: {0}", (object)cpBF.UsedBars);
            Console.WriteLine("Elapsed time: {0} ms", (object)elapsedMsBF);
            Console.WriteLine("################################");

            // Generate matrix of cutting pattern and store to desired destination "path"

            string path = @"C:\Temp\cuttingPattern.dat";
            try
            {
                CuttingPattern.WriteAMPLdat(lengths, amounts, maxLength, path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred.");
            }
            Console.ReadKey();
        }
    }
}
