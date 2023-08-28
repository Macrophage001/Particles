using System.Collections.Generic;

namespace Particles.Packages.Core.Runtime.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Returns the N'th permutation of a string using a Lexicographical Permutation algorithm.
        /// </summary>
        /// <param name="_inputString">The string to find the permutation from</param>
        /// <param name="_n">The permutation to find</param>
        /// <returns>a string permutation</returns>
        public static string NextPermutation(this string _inputString, int _n)
        {
            Stack<int> _stack = new Stack<int>();
            string _result = "";

            _n = _n - 1;

            for (int _i = 1; _i < _inputString.Length + 1; _i++)
            {
                _stack.Push(_n % _i);
                _n = _n / _i;
            }

            for (int _i = 0; _i < _inputString.Length; _i++)
            {
                int _a = _stack.Peek();
                _result += _inputString[_a];

                int _j;

                for (_j = _a; _j < _inputString.Length - 1; _j++)
                {
                    _inputString = _inputString.Substring(0, _j)
                                   + _inputString[_j + 1]
                                   + _inputString.Substring(_j + 1);
                }

                _inputString = _inputString.Substring(0, _j + 1);
                _stack.Pop();
            }
            return _result;
        }

        /// <summary>
        /// Returns all the possible permutations of the given string.
        /// </summary>
        /// <param name="_inputString"></param>
        /// <param name="_l">the start index to check</param>
        /// <param name="_r">the length of the string to check</param>
        /// <returns>A list containing all permutations</returns>
        public static List<string> Permutations(this string _inputString, int _l, int _r)
        {
            List<string> _permutations = new List<string>();
            string Swap(string _a, int _i, int _j)
            {
                char _temp;
                char[] _charArray = _a.ToCharArray();
                _temp = _charArray[_i];
                _charArray[_i] = _charArray[_j];
                _charArray[_j] = _temp;
                return new string(_charArray);
            }

            if (_l == _r)
            {
                _permutations.Add(_inputString);
            }
            else
            {
                for (int _i = _l; _i <= _r; _i++)
                {
                    _inputString = Swap(_inputString, _l, _i);
                    _permutations.AddRange(Permutations(_inputString, _l + 1, _r));
                    _inputString = Swap(_inputString, _l, _i);
                }
            }
            return _permutations;
        }
    }
}
