/* Copyright: Muamer Hrncic */
/* Original author(s): Muamer Hrncic */
/* License: MIT */
/* Purpose: Heuristic procedures and cutting pattern generation for the one dimensional cutting stock problem.*/
/* This code comes with no warranty. */

using System.Linq;

namespace CuttingStock
{
    public static class Heuristics
    {
        public static CuttingPlan FirstFitDecreasing(int[] lengths, int[] amounts, int waste, int maxLength)
        {
            // Execute the First Fit Heurisitc, which assings the actual considered
            // length to the first bar (used or unused) where it fits.

            CuttingPlan cp = new CuttingPlan(lengths, amounts, waste, maxLength);
            int i = 0;
            int k;
            int m = cp.CutSchemeLengths.Length;
            int[] usedLength = new int[m];
            int[] cutMapp = new int[m];

            cutMapp[0] = 1;
            for (; i < m; i++)
            {
                k = FFmin(usedLength, cp.CutSchemeLengths[i], maxLength);
                cutMapp[i] = k;
                usedLength[k] += cp.CutSchemeLengths[i];
            }

            for (i = 0; i < cutMapp.Length; i++)
            {
                cutMapp[i]++;
            }

            cp.UsedBars = cutMapp.Max();
            cp.CutSchemeMapping = cutMapp;

            int[] h1 = new int[cp.UsedBars];
            int[] h2 = new int[cp.UsedBars];

            for (i = 0; i < cp.UsedBars; i++)
            {
                h1[i] = usedLength[i];
                h2[i] = maxLength - usedLength[i];
            }

            cp.UsedBarLengths = h1;
            cp.RemainingBarLengths = h2;

            return cp;
        }

        public static CuttingPlan BestFitDecreasing(int[] lengths, int[] amounts, int waste, int maxLength)
        {
            // Execute the Best Fit Heurisitc, which assings the actual considered
            // length to the bar (used or unused) where the remaining length is minimal.

            CuttingPlan cp = new CuttingPlan(lengths, amounts, waste, maxLength);
            int i = 0;
            int k;
            int m = cp.CutSchemeLengths.Length;
            int[] usedLength = new int[m];
            int[] cutMapp = new int[m];
            cutMapp[0] = 1;

            for (; i < m; i++)
            {
                k = BFmax(usedLength, cp.CutSchemeLengths[i], maxLength);
                cutMapp[i] = k;
                usedLength[k] += cp.CutSchemeLengths[i];
            }

            for (i = 0; i < cutMapp.Length; i++)
            {
                cutMapp[i]++;
            }

            cp.UsedBars = cutMapp.Max();
            cp.CutSchemeMapping = cutMapp;

            int[] h1 = new int[cp.UsedBars];
            int[] h2 = new int[cp.UsedBars];

            for (i = 0; i < cp.UsedBars; i++)
            {
                h1[i] = usedLength[i];
                h2[i] = maxLength - usedLength[i];
            }

            cp.UsedBarLengths = h1;
            cp.RemainingBarLengths = h2;

            return cp;
        }

        public static int FFmin(int[] rlengths, int actLength, int maxLength)
        {
            // Find the index of the first bar of all where "actLength" fits.

            int m = rlengths.Length;
            for (int i = 0; i < m; i++)
            {
                if (rlengths[i] + actLength <= maxLength)
                    return i;
            }
            return 0;
        }

        public static int BFmax(int[] rlengths, int actLength, int maxLength)
        {
            // Find the index of the bar where "actLength" fits and the remaining length is minimal.

            int m = rlengths.Length;
            int[] v = new int[m];
            int argmax = 0;
            int max = 0;
            rlengths.CopyTo(v, 0);

            for (int i = 0; i < m; i++)
            {
                if (rlengths[i] + actLength <= maxLength)
                {
                    v[i] += actLength;

                    if (max < v[i])
                    {
                        max = v[i];
                        argmax = i;
                    }
                }
            }
            return argmax;
        }

        private static int UpperBound(int[] lengths, int[] amounts, int maxLength)
        {
            int bars = 0;
            int m = lengths.Length;
            for (int i = 0; i < m; i++)
            {
                bars += amounts[i] * lengths[i] / maxLength + 1;
            }
            return bars;
        }
    }
}
