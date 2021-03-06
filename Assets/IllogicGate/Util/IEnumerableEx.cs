﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IllogicGate
{
    // --- Class Declaration ------------------------------------------------------------------------
    public static class IListEx
    {
        // --- Events -----------------------------------------------------------------------------------
        // --- Constants --------------------------------------------------------------------------------
        // --- Static Properties ------------------------------------------------------------------------
        // --- Static Methods ---------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------
        public static void Swap(this IList list, int a, int b)
        {
            object tmp = list[a];
            list[a] = list[b];
            list[b] = tmp;
        }

        // -----------------------------------------------------------------------------------
        public static void Shuffle(this IList list, int iterations = 100)
        {
            System.Random rnd = new System.Random();
            for (int i = 0; i < iterations; i++)
            {
                int rnd_a = rnd.Next() % list.Count;
                int rnd_b = rnd.Next() % list.Count;
                Swap(list, rnd_a, rnd_b);
            }
        }

        // --- Inspector --------------------------------------------------------------------------------
        // --- Properties -------------------------------------------------------------------------------
        // --- MonoBehaviour ----------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        // --- Methods ----------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
    }
}