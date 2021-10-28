using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MathUtility : MonoBehaviour
{
    /// <summary>
    /// Source: https://forums.codeguru.com/showthread.php?396630-Getting-the-individual-digits-of-a-number
    /// </summary>
    /// <param name="_num"></param>
    /// <returns></returns>
    public static int[] GetDigits(int _num, int _count)
    {
        Queue<int> _digits = new Queue<int>();

        int _res = _num;

        int _index = 0;
        while (_res != 0 || _index != _count)
        {
            _digits.Enqueue(_res % 10);
            _res /= 10;
            _index++;
        }
        
        return _digits.Reverse().ToArray();;
    }

    public static char IntToChar(int _num)
    {
        if (CheckLength(_num) == -1)
            throw new ArgumentException("Integer needs to be a single digit");

        return (char)(_num % 10 + '0');
    }

    public static char[] IntArrayToCharArray(int[] _digits)
    {
        char[] _chars = new char[_digits.Length + 1];
        int _index = 0;
        for (int i = 0; i < _digits.Length; i++)
        {
            _chars[i] = IntToChar(_digits[i]);
            _index++;
        }

        return _chars;
    }

    public static int CheckLength(int _num)
    {
        int _length = 0;
        int _res = _num;

        while (_res != 0)
        {
            _length++;
            _res /= 10;
        }

        if (_length > 1)
            return -1;

        return 0;
    }

    public static string IntArrayToString(int[] _nums)
    {
        string _numString = String.Empty;
        for (int i = 0; i < _nums.Length; i++)
        {
            _numString += _nums[i];
        }

        return _numString;
    }
}
