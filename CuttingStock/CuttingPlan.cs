/* Copyright: Muamer Hrncic */
/* Original author(s): Muamer Hrncic */
/* License: MIT */
/* Purpose: Heuristic procedures and cutting pattern generation for the one dimensional cutting stock problem.*/
/* This code comes with no warranty. */

using System;
using System.Linq;

namespace CuttingStock
{
    public class CuttingPlan
    {          
        private int[] orderedLengths;               // desired lengths to cut
        private int[] orderedAmount;                // desired amounts of each length
        private int waste;                          // parameter to indicate if the cutting process produces a loss with length "waste"
        private int rawMaterial;                    // length of the raw material bar
        private int[] cutSchemeLengths;             // lengths + waste
        private int[] cutSchemeMapping;             // mapping to indicate which length is cut from which raw material bar 
                                                    // "length[i]" will be cut from bar "cutSchemeMapping[i]"
        private int[] usedBarLengths;               // total used length of the raw material bars
        private int[] remainingBarLengths;          // remaining lengths of the raw material bar, which are not used
        private int usedBars;                       // number of used bars

        public int[] OrderedLengths { get => orderedLengths; set => orderedLengths = value; }
        public int[] OrderedAmount { get => orderedAmount; set => orderedAmount = value; }
        public int Waste { get => waste; set => waste = value; }
        public int RawMaterial { get => rawMaterial; set => rawMaterial = value; }
        public int[] CutSchemeLengths { get => cutSchemeLengths; set => cutSchemeLengths = value; }
        public int[] CutSchemeMapping { get => cutSchemeMapping; set => cutSchemeMapping = value; }
        public int[] UsedBarLengths { get => usedBarLengths; set => usedBarLengths = value; }
        public int[] RemainingBarLengths { get => remainingBarLengths; set => remainingBarLengths = value; }
        public int UsedBars { get => usedBars; set => usedBars = value; }

        public CuttingPlan()
        {
            this.orderedLengths = null;
            this.orderedAmount = null;
            this.rawMaterial = 0;
            this.cutSchemeLengths = null;
            this.cutSchemeMapping = null;
            this.usedBarLengths = null;
            this.waste = 0;
            this.usedBars = 0;
        }

        public CuttingPlan(int[] lengths, int[] amounts, int waste, int rawMaterial)
        {
            this.orderedLengths = lengths;
            this.orderedAmount = amounts;
            CuttingPlan.SortDecreasing(OrderedLengths, OrderedAmount);
            this.waste = waste;
            this.cutSchemeLengths = new int[amounts.Sum()];

            int i = 0;
            for (int j = 0; j < OrderedAmount.Length; ++j)
            {
                for (int k = 0; k < this.orderedAmount[j]; ++k)
                {
                    this.cutSchemeLengths[i] = lengths[j] + waste;
                    ++i;
                }
            }
            this.rawMaterial = rawMaterial;
        }

        public static void SortDecreasing(int[] lengths, int[] amounts)
        {
            // Sort the lengths and the corresponding amounts in noincreasing order.
            int m = lengths.Length;
            Bar[] toSort = new Bar[m];

            for (int index = 0; index < m; ++index)
            {
                toSort[index].Length = lengths[index];
                toSort[index].Amount = amounts[index];
            }

            Array.Sort(toSort, new LengthComparer());

            for (int i = 0; i < m; ++i)
            {
                lengths[i] = toSort[i].Length;
                amounts[i] = toSort[i].Amount;
            }
        }

    }
}
