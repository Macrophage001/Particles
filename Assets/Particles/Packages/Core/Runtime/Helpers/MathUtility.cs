using System;
using System.Collections.Generic;

namespace Particles.Packages.Core.Runtime.Helpers
{
    public static partial class MathUtility
    {
        public static IEnumerable<int> GetDigits(this int num, int baseNumber = 10)
        {
            if (num <= 0) throw new ArgumentOutOfRangeException("The input number must be a positive number.");
            if (baseNumber <= 1) throw new ArgumentOutOfRangeException("The base number must be greater than 1.");

            while (num > 0)
            {
                yield return num % baseNumber;
                num /= baseNumber;
            }
        }

        public static int Length(this double num, int baseNumber = 10) => (int)Math.Floor(Math.Log10(num) / baseNumber);

        /// <summary>
        /// Moves decimal point to the left by <see cref="_numOfPlaces"/>>
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_numOfPlaces"></param>
        /// <returns></returns>
        // public static double Shrink(double _value, int _numOfPlaces) => _value / Math.Pow(10, _numOfPlaces);
        public static double Shrink(double _value, int _numOfPlaces) => _value / Math.Pow(10, _numOfPlaces);

        public static float Remap(float value, float inMin, float inMax, float outMin, float outMax)
        {
            var t = (value - inMin) / (inMax - inMin); // Inverse Lerp Equation normalizes the value between 0 and 1.
            var v = outMin + (outMax - outMin) * t; // Lerp Equation scales the value between outMin and outMax.
            var clampedV = v < outMin ? outMin : v > outMax ? outMax : v; // Finally clamp the value to outMin and outMax.
            return clampedV;
        }
    }
}
