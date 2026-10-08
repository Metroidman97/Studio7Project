using System;
using Unity.Properties;

public static class ArrayExtensions
{
    private static readonly Random _rng = new Random();

    public static void Shuffle<T>(this T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = _rng.Next(n--);

            (array[n], array[k]) = (array[k], array[n]);
        }
    }
}
