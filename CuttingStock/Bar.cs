/* Copyright: Muamer Hrncic */
/* Original author(s): Muamer Hrncic */
/* License: MIT */
/* Purpose: Heuristic procedures and cutting pattern generation for the one dimensional cutting stock problem.*/
/* This code comes with no warranty. */

namespace CuttingStock
{
    public struct Bar
    {
        private int length;
        private int amount;

        public int Length { get => length; set => length = value; }
        public int Amount { get => amount; set => amount = value; }
    }

}