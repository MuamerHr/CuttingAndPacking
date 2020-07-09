/* Copyright: Muamer Hrncic */
/* Original author(s): Muamer Hrncic */
/* License: MIT */
/* Purpose: Heuristic procedures and cutting pattern generation for the one dimensional cutting stock problem.*/
/* This code comes with no warranty. */

using System.Collections.Generic;

namespace CuttingStock
{
    public struct LengthComparer : IComparer<Bar>
    {
        public int Compare(Bar a, Bar b)
        {
            int r = b.Length - a.Length;
            return r > 0 ? r : b.Amount - a.Amount;
        }
    }
}
